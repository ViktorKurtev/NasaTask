using AutoMapper;
using Nasa.Data.Models.Asteroid;
using Nasa.Data.Models.Excel.Sheets;
using Nasa.Data.Models.Excel.Tables;
using System.Collections.Generic;

namespace Nasa.Services.AutomapperConfigs.ExcelConfigs
{
    public class CloseApproachInfoSheetConfig : Profile
    {
        public CloseApproachInfoSheetConfig()
        {
            CreateMap<AsteroidData, CloseApproachInfoSubTable>();

            CreateMap<IEnumerable<CloseApproachInfoSubTable>, CloseApproachInfoSpreadsheet>()
                .ForMember(a => a.SerializationData, b => b.MapFrom(c => c));
        }
    }
}
