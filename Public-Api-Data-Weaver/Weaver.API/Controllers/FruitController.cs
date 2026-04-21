using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Weaver.Models.Entities;
using Weaver.Infrastructure.Data;
using Weaver.Services.Interfaces.Services;
using Weaver.API.DTOs;
using AutoMapper;
using Microsoft.Extensions.FileSystemGlobbing.Internal;

namespace Weaver.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FruitsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IFruitSyncService _fruitSyncService;
        private readonly IMapper _mapper;

        public FruitsController(AppDbContext context, IFruitSyncService fruitSyncService, IMapper mapper)
        {
            _context = context;
            _fruitSyncService = fruitSyncService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FruitDto>>> GetFruits(CancellationToken ct)
        {
            var fruits = await _context.Fruits.Include(f => f.Nutritions).ToListAsync(ct);

            var fruitsDto = fruits?.Select(f => _mapper.Map<FruitDto>(f));

            return Ok(fruitsDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FruitDto>> GetFruit(Guid id, CancellationToken ct)
        {
            var fruit = await _context.Fruits
                .Include(f => f.Nutritions)
                .Where(f => f.Id == id)
                .FirstAsync(ct);

            if (fruit == null)
            {
                return NotFound(new { Message = $"Fruit with ID {id} not found." });
            }

            var fruitDto = _mapper.Map<FruitDto>(fruit);

            return Ok(fruitDto);
        }

        [HttpGet("fitness/{category}")]
        public async Task<ActionResult<IEnumerable<FruitDto>>> GetFruitsByCategory(string category, CancellationToken ct)
        {
            var pattern = $"%,{category},%";

            var matchingIds = await _context.Fruits
                .FromSqlInterpolated($"SELECT Id FROM Fruits WHERE ',' + FitnessCategories + ',' LIKE {pattern}")
                .Select(f => f.Id)
                .ToListAsync(ct);

            var fruits = await _context.Fruits
                .Include(f => f.Nutritions)
                .Where(f => matchingIds.Contains(f.Id))
                .ToListAsync(ct);

            if (fruits.Count == 0)
            {
                return NotFound(new { Message = $"Fruits in category {category} not found." });
            }

            var fruitsDto = fruits?.Select(f => _mapper.Map<FruitDto>(f));

            return Ok(fruitsDto);

        }

        [HttpGet("vitamins/{vitamin}")]
        public async Task<ActionResult<IEnumerable<FruitDto>>> GetFruitsByCategory(char vitamin, CancellationToken ct)
        {
            var pattern = $"%{vitamin}%";

            var matchingIds = await _context.Fruits
                .FromSqlInterpolated($"SELECT Id FROM Fruits WHERE HighVitamins LIKE {pattern}")
                .Select(f => f.Id)
                .ToListAsync(ct);

            var fruits = await _context.Fruits
                .Include(f => f.Nutritions)
                .Where(f => matchingIds.Contains(f.Id))
                .ToListAsync(ct);

            if (fruits.Count == 0)
            {
                return NotFound(new { Message = $"Fruits with high vitamin {vitamin} not found." });
            }

            var fruitsDto = fruits?.Select(f => _mapper.Map<FruitDto>(f));

            return Ok(fruitsDto);

        }

        [HttpPost("sync")]
        public async Task<IActionResult> Sync(CancellationToken ct)
        {
            var count = await _fruitSyncService.SyncFruitsAsync(ct);
            if(count == 0)
            {
                return Ok(new { Message = $"Nothing to sync." });
            }
            else
            {
                return Ok(new { Message = $"Successfully synced {count} new fruits." });
            }
        }
    }
}