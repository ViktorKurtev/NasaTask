using AutoMapper;
using Nasa.Data.Contracts.Spreadsheets;
using Nasa.Data.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nasa.Services.AutomapperConfigs.ViewModelConfigs
{
    public class TableViewModelConfig : Profile
    {
        public TableViewModelConfig()
        {
            CreateMap<IExcelConvertible, TableViewModel>()
                .ForMember(a => a.TableName, b => b.MapFrom(c => c.SpreadsheetName))
                .ForMember(a => a.DataTable, b => b.MapFrom(c => c.ConvertToDataTables().First()));
        }
    }
}
