using AutoMapper;
using Nasa.Data.Models.Asteroid;
using Nasa.Data.Models.Excel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nasa.Services.AutomapperConfigs.ExcelConfigs
{
    public class BasicInfoSheetConfig : Profile
    {
        public BasicInfoSheetConfig()
        {
            CreateMap<AsteroidData, ExcelBasicInfoSheet>();
        }
    }
}
