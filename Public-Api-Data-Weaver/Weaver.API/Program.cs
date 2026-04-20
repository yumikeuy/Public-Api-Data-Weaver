using System;
using Weaver.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Weaver.Services.Interfaces.Services;
using Weaver.Services.Services;
using AutoMapper;
using Weaver.Services.Services.Extensions;

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

            builder.Services.AddBusinessServices();

            builder.Services.AddHttpClient<IFruitSyncService, FruitSyncService>(client =>
            {
                client.BaseAddress = new Uri(builder.Configuration.GetSection("ExternalAPIs")["Fruityvice"]!);
            });


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

            
            if (app.Environment.IsDevelopment()) 
            {
                app.UseHttpsRedirection();
            }
                
            app.UseAuthorization();


            app.MapControllers();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<Program>>();
                var context = services.GetRequiredService<AppDbContext>();

                int retries = 10;
                while (retries > 0)
                {
                    try
                    {
                        logger.LogInformation("Attempting to migrate database... (Retries left: {Retries})", retries);
                        context.Database.Migrate();
                        logger.LogInformation("Database migration successful.");
                        break;
                    }
                    catch (Exception ex)
                    {
                        retries--;
                        if (retries == 0)
                        {
                            logger.LogCritical(ex, "Could not migrate database after multiple attempts.");
                            throw;
                        }

                        logger.LogWarning("Database not ready yet. Retrying in 2 seconds...");
                        Thread.Sleep(2000);
                    }
                }
            }

            app.Run();
        }
    }
}
