using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Weaver.API.DTOs;
using Weaver.Models.Entities;
using Weaver.Services.DTOs;

namespace Weaver.Services.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ExternalFruitDto, Fruit>()
                .ForMember(f => f.Id, opt => opt.Ignore());

            CreateMap<ExternalNutritionsDto, Nutritions>()
                .ForMember(n => n.Id, opt => opt.Ignore());

            CreateMap<FruitDto, Fruit>();
            CreateMap<NutritionsDto, Nutritions>();

            CreateMap<Fruit, FruitDto>();
            CreateMap<Nutritions, NutritionsDto>();

        }
    }
}
