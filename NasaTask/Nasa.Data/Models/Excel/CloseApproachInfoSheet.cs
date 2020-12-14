using Nasa.Data.Models.CloseApproach;
using Nasa.Data.Models.Contracts;
using System.Collections.Generic;

namespace Nasa.Data.Models.Excel
{
    public class CloseApproachInfoSheet : ICloseApproachDataContainer
    {
        public string Name { get; set; }
        public IEnumerable<CloseApproachData> CloseApproachData { get; set; }
    }
}
