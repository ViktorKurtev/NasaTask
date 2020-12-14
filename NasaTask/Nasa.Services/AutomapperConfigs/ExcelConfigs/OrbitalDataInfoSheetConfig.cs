using AutoMapper;
using Nasa.Data.Models.Asteroid;
using Nasa.Data.Models.Excel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nasa.Services.AutomapperConfigs.ExcelConfigs
{
    public class OrbitalDataInfoSheetConfig : Profile
    {
        public OrbitalDataInfoSheetConfig()
        {
            CreateMap<AsteroidData, OrbitalDataInfoSheet>();
        }
    }
}
