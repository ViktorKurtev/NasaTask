using AutoMapper;
using Nasa.Data.Models.Asteroid;
using Nasa.Data.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nasa.Services.AutomapperConfigs.ViewModelConfigs
{
    public class AsteroidViewModelConfig : Profile
    {
        public AsteroidViewModelConfig()
        {
            CreateMap<AsteroidData, AsteroidViewModel>()
                .ForMember(a => a.CloseApproachCount, b => b.MapFrom(c => c.CloseApproachData.Count()));
        }
    }
}
