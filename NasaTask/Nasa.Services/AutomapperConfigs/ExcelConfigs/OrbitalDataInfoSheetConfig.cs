using AutoMapper;
using Nasa.Data.Models.Asteroid;
using Nasa.Data.Models.Excel.Sheets;
using Nasa.Data.Models.Excel.Tables;
using System.Collections.Generic;

namespace Nasa.Services.AutomapperConfigs.ExcelConfigs
{
    public class OrbitalDataInfoSheetConfig : Profile
    {
        public OrbitalDataInfoSheetConfig()
        {
            CreateMap<AsteroidData, OrbitalDataInfoRow>();

            CreateMap<IEnumerable<OrbitalDataInfoRow>, OrbitalDataSpreadsheet>()
                .ForMember(a => a.SerializationData, b => b.MapFrom(c => c));
        }
    }
}
