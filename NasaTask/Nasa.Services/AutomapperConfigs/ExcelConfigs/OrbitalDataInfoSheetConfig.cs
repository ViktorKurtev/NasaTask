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
    public class OrbitalDataInfoSheetConfig : Profile
    {
        public OrbitalDataInfoSheetConfig()
        {
            CreateMap<AsteroidData, OrbitalDataInfoRow>();

            CreateMap<IEnumerable<OrbitalDataInfoRow>, OrbitalDataInfoSpreadsheet>()
                .ForMember(a => a.SerializationData, b => b.MapFrom(c => c));
        }
    }
}
