using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Weaver.Services.Interfaces.Services;
using Weaver.Services.Services.FruitFitnessCategory;
using Weaver.Services.Services.VitaminsCheckers;

namespace Weaver.Services.Services.Extensions
{
    public static class AddBusinesLogicDIExtension
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<IVitaminsCheckerComposer, DefaultVitaminsCheckerComposer>();
            services.AddScoped<IFruitFitnessCategoryCheckerComposer, FitnessCategoryComposer>();
            services.AddScoped<IFruitTransformator, FruitTransformator>();

            return services;
        }
    }
}
