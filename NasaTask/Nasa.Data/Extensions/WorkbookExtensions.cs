using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;

namespace Nasa.Data.Extensions
{
    public static class WorkbookExtensions
    {
        /// <summary>
        /// Add a predefined sample header to an existing ExcelWorkbook object with a specific name.
        /// </summary>
        /// <param name="excelWorkbook">Workbook to add the header style to.</param>
        /// <param name="styleName">Header style name.</param>
        public static void AddNameHeaderStyle(this ExcelWorkbook excelWorkbook, string styleName)
        {
            var namedStyle = excelWorkbook.Styles.CreateNamedStyle(styleName);

            namedStyle.Style.Font.Size = 14;
            namedStyle.Style.Font.Bold = true;
            namedStyle.Style.Font.UnderLine = true;

            namedStyle.Style.Border.Top.Style = ExcelBorderStyle.Thin;
            namedStyle.Style.Border.Right.Style = ExcelBorderStyle.Thin;

            namedStyle.Style.Fill.SetBackground(Color.AntiqueWhite);
        }
    }
}
