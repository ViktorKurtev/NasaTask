using System.Data;

namespace Nasa.Data.Models.ViewModels
{
    /// <summary>
    /// Table View Model containing the name of the table and the table itself. Used to display the detailed information of an
    /// asteroid.
    /// </summary>
    public class TableViewModel
    {
        public string TableName { get; set; }
        public DataTable DataTable { get; set; }
    }
}
