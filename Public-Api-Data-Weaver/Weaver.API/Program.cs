using System;
using Weaver.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Weaver.Services.Interfaces.Services;
using Weaver.Services.Services;
using AutoMapper;

namespace Weaver.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddHttpClient<IFruitSyncService, FruitSyncService>(client =>
            {
                client.BaseAddress = new Uri("https://www.fruityvice.com/api/");
            });

            builder.Services.AddScoped<IFruitTransformator, FruitTransformator>();

            builder.Services.AddAutoMapper(cfg => {
                cfg.AddProfile<MappingProfile>();
            });

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            
        

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<AppDbContext>();
                    Thread.Sleep(10000);
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occured when tried to migrate.");
                }
            }

            app.Run();
        }
    }
}
