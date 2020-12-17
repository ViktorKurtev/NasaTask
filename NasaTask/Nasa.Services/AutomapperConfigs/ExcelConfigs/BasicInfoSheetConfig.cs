using AutoMapper;
using Nasa.Data.Models.Asteroid;
using Nasa.Data.Models.Excel.Sheets;
using Nasa.Data.Models.Excel.Tables;
using System.Collections.Generic;

namespace Nasa.Services.AutomapperConfigs.ExcelConfigs
{
    public class BasicInfoSheetConfig : Profile
    {
        public BasicInfoSheetConfig()
        {
            CreateMap<AsteroidData, BasicInfoRow>();

            CreateMap<IEnumerable<BasicInfoRow>, BasicDataSpreadsheet>()
                .ForMember(a => a.SerializationData, b => b.MapFrom(c => c));
        }
    }
}
