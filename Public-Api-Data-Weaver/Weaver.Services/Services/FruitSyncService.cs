using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Weaver.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Weaver.Services.DTOs;
using Weaver.Services.Interfaces.Services;
using AutoMapper;
using Weaver.Models.Entities;

namespace Weaver.Services.Services
{
    public class FruitSyncService : IFruitSyncService
    {
        private readonly HttpClient _httpClient;
        private readonly AppDbContext _context;
        private readonly IFruitTransformator _transformator;
        private readonly IMapper _mapper;

        public FruitSyncService(HttpClient httpClient, AppDbContext dbContext, IFruitTransformator transformator, IMapper mapper)
        {
            _httpClient = httpClient;
            _context = dbContext;
            _transformator = transformator;
            _mapper = mapper;
        }

        public async Task<int> SyncFruitsAsync()
        {
            var url = "fruit/all";
            var fruitsDto = await _httpClient.GetFromJsonAsync<List<ExternalFruitDto>>(url);

            if (fruitsDto == null) return 0;

            var fruits = fruitsDto.Select(dto => _mapper.Map<Fruit>(dto)).ToList();

            fruits.ForEach(f => _transformator.Transformate(f));

            var counter = 0;

            foreach (var fruit in fruits)
            {
                var exists = _context.Fruits.Any(f => f.Name == fruit.Name);
                if (!exists)
                {
                    counter++;
                    _context.Fruits.Add(fruit);
                }
            }

            await _context.SaveChangesAsync();
            return counter;
        }
    }
}
