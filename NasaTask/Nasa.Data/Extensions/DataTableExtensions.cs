using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Nasa.Data.Extensions
{
    public static class DataTableExtensions
    {
        /// <summary>
        /// Transposes a Data Table, changing the position of the rows and columns. For this to work the first column
        /// needs to have unique values (Id, Name perhaps), otherwise an exception is thrown because the column name needs to be unique.
        /// </summary>
        /// <param name="dataTableToTranspose"></param>
        /// <returns></returns>
        public static DataTable Transpose(this DataTable dataTableToTranspose)
        {
            DataTable newDataTable = new DataTable();

            for (int row = 0; row <= dataTableToTranspose.Rows.Count; row++)
            {
                newDataTable.Columns.Add(row.ToString());
            }

            newDataTable.Columns[0].ColumnName = "Columns";

            for (int row = 0; row < dataTableToTranspose.Rows.Count; row++)
            {
                newDataTable.Columns[row + 1].ColumnName = dataTableToTranspose.Rows[row].ItemArray[0].ToString();
            }

            for (int col = 1; col < dataTableToTranspose.Columns.Count; col++)
            {
                DataRow newRow = newDataTable.NewRow();

                newRow[0] = dataTableToTranspose.Columns[col].ToString();

                for (int row = 1; row <= dataTableToTranspose.Rows.Count; row++)
                {
                    newRow[row] = dataTableToTranspose.Rows[row - 1][col];
                }

                newDataTable.Rows.Add(newRow);
            }

            return newDataTable;
        }
    }
}
