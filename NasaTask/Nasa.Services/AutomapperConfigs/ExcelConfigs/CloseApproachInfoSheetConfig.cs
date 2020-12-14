using AutoMapper;
using Nasa.Data.Models.Asteroid;
using Nasa.Data.Models.CloseApproach;
using Nasa.Data.Models.Excel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nasa.Services.AutomapperConfigs.ExcelConfigs
{
    public class CloseApproachInfoSheetConfig : Profile
    {
        public CloseApproachInfoSheetConfig()
        {
            CreateMap<AsteroidData, CloseApproachInfoSheet>();
        }
    }
}
