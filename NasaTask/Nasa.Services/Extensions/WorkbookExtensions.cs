﻿using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Nasa.Services.Extensions
{
    public static class WorkbookExtensions
    {
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
