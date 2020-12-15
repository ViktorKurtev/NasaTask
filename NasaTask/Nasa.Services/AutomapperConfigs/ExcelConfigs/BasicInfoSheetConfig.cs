using AutoMapper;
using Nasa.Data.Models.Asteroid;
using Nasa.Data.Models.Excel;
using Nasa.Data.Models.Excel.Sheets;
using Nasa.Data.Models.Excel.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nasa.Services.AutomapperConfigs.ExcelConfigs
{
    public class BasicInfoSheetConfig : Profile
    {
        public BasicInfoSheetConfig()
        {
            CreateMap<AsteroidData, BasicInfoRow>();

            CreateMap<IEnumerable<BasicInfoRow>, BasicInfoSpreadsheet>()
                .ForMember(a => a.SerializationData, b => b.MapFrom(c => c));
        }
    }
}
