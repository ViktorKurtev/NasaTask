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

            CreateMap<AsteroidCollection, AsteroidPageViewModel>()
                .ForMember(a => a.Asteroids, b => b.MapFrom(c => c.Asteroids))
                .ForMember(a => a.PageCount, b => b.MapFrom(c => c.PageData.PageCount))
                .ForMember(a => a.PageNumber, b => b.MapFrom(c => c.PageData.PageNumber));
        }
    }
}
