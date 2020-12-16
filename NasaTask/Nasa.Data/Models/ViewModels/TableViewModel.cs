using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Nasa.Data.Models.ViewModels
{
    public class TableViewModel
    {
        public string TableName { get; set; }
        public DataTable DataTable { get; set; }
    }
}
