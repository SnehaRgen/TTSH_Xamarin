using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

using System.ServiceModel.Web;
using System.IO;
using System.Data;


using System.Windows;
using System.Xml;
using System.Text.RegularExpressions;
using System.Text;
using TTSH.Entity;

namespace TTSHWCFLayer.Excel
{
    public class ExportExcel
    {

        internal static System.IO.Stream DownloadSampleFilePrev(string filePath)
        {
            //System.IO.Stream stream = System.IO.File.OpenRead(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\UATSampleExportFile\\TestUATExport.xlsx");

            filePath = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath + "SampleTemplate\\TestUATExport.xlsx";
            System.IO.Stream stream = System.IO.File.OpenRead(filePath);

            OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;

            response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            response.Headers.Add("Cache-Control", "private");
            response.Headers.Add("X-Download-Options", "noopen");

            response.Headers.Add("Content-Length", stream.Length.ToString());

            response.Headers.Add("Content-Disposition", "attachment; filename=UATExport Test File.xlsx");

            return stream;
        }

        internal System.IO.Stream DownloadSampleFile(string filePath)
        {
            System.IO.Stream stream = System.IO.File.OpenRead(filePath);

            try
            {
                //filePath = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath + "SampleTemplate\\AnalysisTemplate.xlsx";

                //get the orginal name of Excel Template from which it is copied by removing guid appended in copied template file
                int pos = Path.GetFileNameWithoutExtension(filePath).LastIndexOf(@"_");
                string FileName = Path.GetFileNameWithoutExtension(filePath).Substring(0, pos) + Path.GetExtension(filePath);

                OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;

                response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                response.Headers.Add("Cache-Control", "private");
                response.Headers.Add("X-Download-Options", "noopen");

                response.Headers.Add("Content-Length", stream.Length.ToString());

                //response.Headers.Add("Content-Disposition", "attachment; filename=AnalysisTemplate.xlsx");
                response.Headers.Add("Content-Disposition", string.Format(@"attachment;filename={0}", FileName));
            }
            catch (Exception ex)
            {

            }
            finally
            {
                //delete the file after download
                //if(File.Exists(filePath))
                //File.Delete(filePath);
            }

            return stream;
        }

        internal string CreateTemplateCopy(string sourceTemplatePath)
        {
            string exportFilePath = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(sourceTemplatePath))
                {
                    string excelCopyFileName = Path.GetFileNameWithoutExtension(sourceTemplatePath) + "_" + Guid.NewGuid() + Path.GetExtension(sourceTemplatePath);
                    exportFilePath = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath + "SampleTemplate\\" + excelCopyFileName;

                    if (!System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(exportFilePath)))
                        System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(exportFilePath));

                    if (System.IO.File.Exists(exportFilePath))
                        System.IO.File.Delete(exportFilePath);

                    File.Copy(sourceTemplatePath, exportFilePath);

                }

            }
            catch (Exception ex)
            {
            }
            return exportFilePath;
        }

        /// <summary>
        /// Convert Formated html to Plain Text
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        internal string ConvertHtmlToPlainText(string source)
        {
            try
            {
                string result;
                // Remove HTML Development formatting
                // Replace line breaks with space
                // because browsers inserts space
                result = source.Replace("\r", " ");
                // Replace line breaks with space
                // because browsers inserts space
                result = result.Replace("\n", " ");
                // Remove step-formatting
                result = result.Replace("\t", string.Empty);
                // Remove repeating spaces because browsers ignore them
                result = System.Text.RegularExpressions.Regex.Replace(result,
                                                                      @"( )+", " ");

                // Remove the header (prepare first by clearing attributes)
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*head([^>])*>", "<head>",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"(<( )*(/)( )*head( )*>)", "</head>",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(<head>).*(</head>)", string.Empty,
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // remove all scripts (prepare first by clearing attributes)
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*script([^>])*>", "<script>",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"(<( )*(/)( )*script( )*>)", "</script>",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                //result = System.Text.RegularExpressions.Regex.Replace(result,
                //         @"(<script>)([^(<script>\.</script>)])*(</script>)",
                //         string.Empty,
                //         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"(<script>).*(</script>)", string.Empty,
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // remove all styles (prepare first by clearing attributes)
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*style([^>])*>", "<style>",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"(<( )*(/)( )*style( )*>)", "</style>",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(<style>).*(</style>)", string.Empty,
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // insert tabs in spaces of <td> tags
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*td([^>])*>", "\t",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // insert line breaks in places of <BR> and <LI> tags
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*br( )*>", "\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*li( )*>", "\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // insert line paragraphs (double line breaks) in place
                // if <P>, <DIV> and <TR> tags
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*div([^>])*>", "\r\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*tr([^>])*>", "\r\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                //commented :SD
                //result = System.Text.RegularExpressions.Regex.Replace(result,
                //         @"<( )*p([^>])*>","\r\r",
                //         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                //replace p tag with space:SD
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*p([^>])*>", "",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // Remove remaining tags like <a>, links, images,
                // comments etc - anything that's enclosed inside < >
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<[^>]*>", string.Empty,
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // replace special characters:
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @" ", " ",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"•", " * ",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"‹", "<",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"›", ">",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"™", "(tm)",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"⁄", "/",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<", "<",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @">", ">",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"©", "(c)",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"®", "(r)",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                // Remove all others. More can be added, see
                // http://hotwired.lycos.com/webmonkey/reference/special_characters/
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&(.{2,6});", string.Empty,
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // for testing
                //System.Text.RegularExpressions.Regex.Replace(result,
                //       this.txtRegex.Text,string.Empty,
                //       System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // make line breaking consistent
                result = result.Replace("\n", "\r");

                // Remove extra line breaks and tabs:
                // replace over 2 breaks with 2 and over 4 tabs with 4.
                // Prepare first to remove any whitespaces in between
                // the escaped characters and remove redundant tabs in between line breaks
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(\r)( )+(\r)", "\r\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(\t)( )+(\t)", "\t\t",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(\t)( )+(\r)", "\t\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(\r)( )+(\t)", "\r\t",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                // Remove redundant tabs
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(\r)(\t)+(\r)", "\r\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                // Remove multiple tabs following a line break with just one tab
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(\r)(\t)+", "\r\t",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                // Initial replacement target string for line breaks
                string breaks = "\r\r\r";
                // Initial replacement target string for tabs
                string tabs = "\t\t\t\t\t";
                for (int index = 0; index < result.Length; index++)
                {
                    result = result.Replace(breaks, "\r\r");
                    result = result.Replace(tabs, "\t\t\t\t");
                    breaks = breaks + "\r";
                    tabs = tabs + "\t";
                }

                // That's it.
                return result;
            }
            catch
            {
                return source;
            }
        }

        internal WorksheetPart GetWorksheetPartByName(SpreadsheetDocument document, string sheetName)
        {
            IEnumerable<Sheet> sheets = document.WorkbookPart.Workbook.GetFirstChild<Sheets>().Elements<Sheet>().Where(s => s.Name == sheetName);

            if (sheets.Count() == 0)
            {
                // The specified worksheet does not exist.
                return null;
            }

            string relationshipId = sheets.First().Id.Value;
            WorksheetPart worksheetPart = (WorksheetPart)document.WorkbookPart.GetPartById(relationshipId);
            return worksheetPart;
        }

        /// <summary>
        /// Given a row, a column name, and a row index, gets the cell at the specified column 
        /// </summary>
        /// <param name="row"></param>
        /// <param name="columnName"></param>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        internal Cell GetCellOfRow(Row row, string columnName, uint rowIndex)
        {
            if (row == null)
                return null;
            //return row.Elements<Cell>().Where(c => string.Compare(c.CellReference.Value, columnName + rowIndex, true) == 0).First();
            return row.Elements<Cell>().Where(c => string.Compare(c.CellReference.Value, columnName + rowIndex, true) == 0).FirstOrDefault();
        }

        /// <summary>
        /// Given a worksheet and a row index, return the row
        /// </summary>
        /// <param name="worksheet"></param>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        internal Row GetRow(Worksheet worksheet, uint rowIndex)
        {
            //return worksheet.GetFirstChild<SheetData>().Elements<Row>().Where(r => r.RowIndex == rowIndex).First();
            return worksheet.GetFirstChild<SheetData>().Elements<Row>().Where(r => r.RowIndex == rowIndex).FirstOrDefault();
        }

        /// <summary>       
        /// This method using OpenXml opens the excel file and write values to associated cells of excel sheet
        /// </summary>
        /// <param name="docPath"></param>
        /// <param name="text"></param>
        /// <param name="rowIndex"></param>
        /// <param name="columnName"></param>
        internal void WriteToExcel(string docPath, string sheetName, List<Export> listExport)
        {
            try
            {
                FileInfo fi = new FileInfo(docPath);
                if (fi.Exists)
                {
                    // Open the document for editing.
                    using (SpreadsheetDocument spreadSheet = SpreadsheetDocument.Open(docPath, true))
                    {
                        WorksheetPart worksheetPart = GetWorksheetPartByName(spreadSheet, sheetName);
                        if (worksheetPart != null)
                        {

                            CellFormat customPercentCellFormat = new CellFormat() { NumberFormatId = (UInt32Value)9U, FontId = (UInt32Value)0U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyNumberFormat = true };
                            CellFormat customBoldCellFormat = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)2U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFont = true };
                            CellFormat customColorCellFormat = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)0U, FillId = (UInt32Value)6U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFill = true };
                            CellFormat customPercentBoldCellFormat = new CellFormat() { NumberFormatId = (UInt32Value)9U, FontId = (UInt32Value)2U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyNumberFormat = true };
                            CellFormats cellFormats = spreadSheet.WorkbookPart.WorkbookStylesPart.Stylesheet.Elements<CellFormats>().First();
                            cellFormats.Append(customPercentCellFormat);
                            uint styleIndexPercent = (uint)cellFormats.Count++;
                            cellFormats.Append(customBoldCellFormat);
                            uint styleIndexBold = (uint)cellFormats.Count++;
                            cellFormats.Append(customColorCellFormat);
                            uint styleIndexColor = (uint)cellFormats.Count++;
                            cellFormats.Append(customPercentBoldCellFormat);
                            uint styleIndexPercentBold = (uint)cellFormats.Count++;

                            foreach (Export _epRow in listExport)
                            {
                                //get the row
                                uint rowStartIndex = (uint)_epRow.rowIndex;
                                Row row = GetRow(worksheetPart.Worksheet, rowStartIndex);
                                //row.StyleIndex = styleIndexBold;
                                List<ExportCellData> listRowData = new List<ExportCellData>();
                                listRowData = _epRow.listExportRowData;

                                foreach (ExportCellData _epRowData in listRowData)
                                {
                                    //get the cell
                                    Cell cell = GetCellOfRow(row, Convert.ToString(_epRowData.cellName), rowStartIndex);
                                    if (cell != null)
                                    {
                                        cell.CellValue = new CellValue(_epRowData.dataValue);

                                        if (Convert.ToString(_epRowData.dataType).ToUpper() == "STRING")
                                        {
                                            cell.DataType = new EnumValue<CellValues>(CellValues.String);
                                        }
                                        else if (Convert.ToString(_epRowData.dataType).ToUpper() == "INTEGER")
                                        {
                                            cell.DataType = new EnumValue<CellValues>(CellValues.Number);
                                        }
                                        else if (Convert.ToString(_epRowData.dataType).ToUpper() == "DATE")
                                        {
                                            cell.DataType = new EnumValue<CellValues>(CellValues.Date);
                                        }
                                        else if (Convert.ToString(_epRowData.dataType).ToUpper() == "PERCENTAGE")
                                        {
                                            cell.DataType = new EnumValue<CellValues>(CellValues.Number);
                                            cell.StyleIndex = styleIndexPercent;
                                        }
                                        else if (Convert.ToString(_epRowData.dataType).ToUpper() == "STRINGBOLD")
                                        {
                                            cell.DataType = new EnumValue<CellValues>(CellValues.String);
                                            cell.StyleIndex = styleIndexBold;
                                        }
                                        else if (Convert.ToString(_epRowData.dataType).ToUpper() == "INTEGERBOLD")
                                        {
                                            cell.DataType = new EnumValue<CellValues>(CellValues.Number);
                                            cell.StyleIndex = styleIndexBold;
                                        }
                                        else if (Convert.ToString(_epRowData.dataType).ToUpper() == "PERCENTBOLD")
                                        {
                                            cell.DataType = new EnumValue<CellValues>(CellValues.Number);
                                            //cell.StyleIndex = styleIndexPercent;
                                            cell.StyleIndex = styleIndexPercentBold;
                                        }
                                    }
                                }
                            }
                            // Save the worksheet.
                            worksheetPart.Worksheet.Save();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private static ForegroundColor TranslateForeground(System.Drawing.Color fillColor)
        {
            return new ForegroundColor()
            {
                Rgb = new HexBinaryValue()
                {
                    Value =
                         System.Drawing.ColorTranslator.ToHtml(
                            System.Drawing.Color.FromArgb(
                                fillColor.A,
                                fillColor.R,
                                fillColor.G,
                                fillColor.B)).Replace("#", "")
                }
            };
        }

        private static Stylesheet CreateStyleSheet()
        {
            Stylesheet _stylesheet = new Stylesheet();
            /*var fonts = new Fonts();
                                var font = new DocumentFormat.OpenXml.Spreadsheet.Font();
                                var fontName = new FontName { Val = StringValue.FromString("Calibri") };
                                var fontsize = new FontSize { Val = DoubleValue.FromDouble(11) };
                                font.Bold = new Bold();
                                fonts.Append(font);
                                fonts.Count = UInt32Value.FromUInt32((uint)fonts.ChildElements.Count);
                                fonts.KnownFonts = true;

            Fills _fills = new Fills();
            Fill _fill = new Fill();
            PatternFill _patternfill = new PatternFill();
            _patternfill.PatternType = PatternValues.Solid;
            _patternfill.ForegroundColor = new ForegroundColor();
            _patternfill.ForegroundColor = TranslateForeground(System.Drawing.Color.Green);
            _patternfill.BackgroundColor = new BackgroundColor { Rgb = _patternfill.ForegroundColor.Rgb };

            _fill.PatternFill = _patternfill;
            _fills.Append(_fill);
            _fills.Count = UInt32Value.FromUInt32((uint)_fills.ChildElements.Count);

            _stylesheet.Append(fonts);
            _stylesheet.Append(_fills);

            return _stylesheet;*/

            var fonts = new Fonts();
            var font = new DocumentFormat.OpenXml.Spreadsheet.Font();
            var fontName = new FontName { Val = StringValue.FromString("Arial") };
            var fontSize = new FontSize { Val = DoubleValue.FromDouble(11) };
            font.FontName = fontName;
            font.FontSize = fontSize;
            fonts.Append(font);
            //Font Index 1
            font = new DocumentFormat.OpenXml.Spreadsheet.Font();
            fontName = new FontName { Val = StringValue.FromString("Arial") };
            fontSize = new FontSize { Val = DoubleValue.FromDouble(12) };
            font.FontName = fontName;
            font.FontSize = fontSize;
            font.Bold = new Bold();
            fonts.Append(font);
            fonts.Count = UInt32Value.FromUInt32((uint)fonts.ChildElements.Count);
            var fills = new Fills();
            var fill = new Fill();
            var patternFill = new PatternFill { PatternType = PatternValues.None };
            fill.PatternFill = patternFill;
            fills.Append(fill);
            fill = new Fill();
            patternFill = new PatternFill { PatternType = PatternValues.Gray125 };
            fill.PatternFill = patternFill;
            fills.Append(fill);
            //Fill index  2
            fill = new Fill();
            patternFill = new PatternFill
            {
                PatternType = PatternValues.Solid,
                ForegroundColor = new ForegroundColor()
            };
            patternFill.ForegroundColor =
               TranslateForeground(System.Drawing.Color.LightBlue);
            patternFill.BackgroundColor =
                new BackgroundColor { Rgb = patternFill.ForegroundColor.Rgb };
            fill.PatternFill = patternFill;
            fills.Append(fill);
            //Fill index  3
            fill = new Fill();
            patternFill = new PatternFill
            {
                PatternType = PatternValues.Solid,
                ForegroundColor = new ForegroundColor()
            };
            patternFill.ForegroundColor =
               TranslateForeground(System.Drawing.Color.DodgerBlue);
            patternFill.BackgroundColor =
               new BackgroundColor { Rgb = patternFill.ForegroundColor.Rgb };
            fill.PatternFill = patternFill;
            fills.Append(fill);
            fills.Count = UInt32Value.FromUInt32((uint)fills.ChildElements.Count);
            var borders = new Borders();
            var border = new Border
            {
                LeftBorder = new LeftBorder(),
                RightBorder = new RightBorder(),
                TopBorder = new TopBorder(),
                BottomBorder = new BottomBorder(),
                DiagonalBorder = new DiagonalBorder()
            };
            borders.Append(border);
            //All Boarder Index 1
            border = new Border
            {
                LeftBorder = new LeftBorder { Style = BorderStyleValues.Thin },
                RightBorder = new RightBorder { Style = BorderStyleValues.Thin },
                TopBorder = new TopBorder { Style = BorderStyleValues.Thin },
                BottomBorder = new BottomBorder { Style = BorderStyleValues.Thin },
                DiagonalBorder = new DiagonalBorder()
            };
            borders.Append(border);
            //Top and Bottom Boarder Index 2
            border = new Border
            {
                LeftBorder = new LeftBorder(),
                RightBorder = new RightBorder(),
                TopBorder = new TopBorder { Style = BorderStyleValues.Thin },
                BottomBorder = new BottomBorder { Style = BorderStyleValues.Thin },
                DiagonalBorder = new DiagonalBorder()
            };
            borders.Append(border);
            borders.Count = UInt32Value.FromUInt32((uint)borders.ChildElements.Count);
            var cellStyleFormats = new CellStyleFormats();
            var cellFormat = new CellFormat
            {
                NumberFormatId = 0,
                FontId = 0,
                FillId = 0,
                BorderId = 0
            };
            cellStyleFormats.Append(cellFormat);
            cellStyleFormats.Count =
               UInt32Value.FromUInt32((uint)cellStyleFormats.ChildElements.Count);
            uint iExcelIndex = 164;
            var numberingFormats = new NumberingFormats();
            var cellFormats = new CellFormats();
            cellFormat = new CellFormat
            {
                NumberFormatId = 0,
                FontId = 0,
                FillId = 0,
                BorderId = 0,
                FormatId = 0
            };
            cellFormats.Append(cellFormat);
            var nformatDateTime = new NumberingFormat
            {
                NumberFormatId = UInt32Value.FromUInt32(iExcelIndex++),
                FormatCode = StringValue.FromString("dd/mm/yyyy hh:mm:ss")
            };
            numberingFormats.Append(nformatDateTime);
            var nformat4Decimal = new NumberingFormat
            {
                NumberFormatId = UInt32Value.FromUInt32(iExcelIndex++),
                FormatCode = StringValue.FromString("#,##0.0000")
            };
            numberingFormats.Append(nformat4Decimal);
            var nformat2Decimal = new NumberingFormat
            {
                NumberFormatId = UInt32Value.FromUInt32(iExcelIndex++),
                FormatCode = StringValue.FromString("#,##0.00")
            };
            numberingFormats.Append(nformat2Decimal);
            var nformatForcedText = new NumberingFormat
            {
                NumberFormatId = UInt32Value.FromUInt32(iExcelIndex),
                FormatCode = StringValue.FromString("@")
            };
            numberingFormats.Append(nformatForcedText);
            // index 1
            // Cell Standard Date format 
            cellFormat = new CellFormat
            {
                NumberFormatId = 14,
                FontId = 0,
                FillId = 0,
                BorderId = 0,
                FormatId = 0,
                ApplyNumberFormat = BooleanValue.FromBoolean(true)
            };
            cellFormats.Append(cellFormat);
            // Index 2
            // Cell Standard Number format with 2 decimal placing
            cellFormat = new CellFormat
            {
                NumberFormatId = 4,
                FontId = 0,
                FillId = 0,
                BorderId = 0,
                FormatId = 0,
                ApplyNumberFormat = BooleanValue.FromBoolean(true)
            };
            cellFormats.Append(cellFormat);
            // Index 3
            // Cell Date time custom format
            cellFormat = new CellFormat
            {
                NumberFormatId = nformatDateTime.NumberFormatId,
                FontId = 0,
                FillId = 0,
                BorderId = 0,
                FormatId = 0,
                ApplyNumberFormat = BooleanValue.FromBoolean(true)
            };
            cellFormats.Append(cellFormat);
            // Index 4
            // Cell 4 decimal custom format
            cellFormat = new CellFormat
            {
                NumberFormatId = nformat4Decimal.NumberFormatId,
                FontId = 0,
                FillId = 0,
                BorderId = 0,
                FormatId = 0,
                ApplyNumberFormat = BooleanValue.FromBoolean(true)
            };
            cellFormats.Append(cellFormat);
            // Index 5
            // Cell 2 decimal custom format
            cellFormat = new CellFormat
            {
                NumberFormatId = nformat2Decimal.NumberFormatId,
                FontId = 0,
                FillId = 0,
                BorderId = 0,
                FormatId = 0,
                ApplyNumberFormat = BooleanValue.FromBoolean(true)
            };
            cellFormats.Append(cellFormat);
            // Index 6
            // Cell forced number text custom format
            cellFormat = new CellFormat
            {
                NumberFormatId = nformatForcedText.NumberFormatId,
                FontId = 0,
                FillId = 0,
                BorderId = 0,
                FormatId = 0,
                ApplyNumberFormat = BooleanValue.FromBoolean(true)
            };
            cellFormats.Append(cellFormat);
            // Index 7
            // Cell text with font 12 
            cellFormat = new CellFormat
            {
                NumberFormatId = nformatForcedText.NumberFormatId,
                FontId = 1,
                FillId = 0,
                BorderId = 0,
                FormatId = 0,
                ApplyNumberFormat = BooleanValue.FromBoolean(true)
            };
            cellFormats.Append(cellFormat);
            // Index 8
            // Cell text
            cellFormat = new CellFormat
            {
                NumberFormatId = nformatForcedText.NumberFormatId,
                FontId = 0,
                FillId = 0,
                BorderId = 1,
                FormatId = 0,
                ApplyNumberFormat = BooleanValue.FromBoolean(true)
            };
            cellFormats.Append(cellFormat);
            // Index 9
            // Coloured 2 decimal cell text
            cellFormat = new CellFormat
            {
                NumberFormatId = nformat2Decimal.NumberFormatId,
                FontId = 0,
                FillId = 2,
                BorderId = 2,
                FormatId = 0,
                ApplyNumberFormat = BooleanValue.FromBoolean(true)
            };
            cellFormats.Append(cellFormat);
            // Index 10
            // Coloured cell text
            cellFormat = new CellFormat
            {
                NumberFormatId = nformatForcedText.NumberFormatId,
                FontId = 0,
                FillId = 2,
                BorderId = 2,
                FormatId = 0,
                ApplyNumberFormat = BooleanValue.FromBoolean(true)
            };
            cellFormats.Append(cellFormat);
            // Index 11
            // Coloured cell text
            cellFormat = new CellFormat
            {
                NumberFormatId = nformatForcedText.NumberFormatId,
                FontId = 1,
                FillId = 3,
                BorderId = 2,
                FormatId = 0,
                ApplyNumberFormat = BooleanValue.FromBoolean(true)
            };
            cellFormats.Append(cellFormat);
            numberingFormats.Count =
              UInt32Value.FromUInt32((uint)numberingFormats.ChildElements.Count);
            cellFormats.Count = UInt32Value.FromUInt32((uint)cellFormats.ChildElements.Count);
            _stylesheet.Append(numberingFormats);
            _stylesheet.Append(fonts);
            _stylesheet.Append(fills);
            _stylesheet.Append(borders);
            _stylesheet.Append(cellStyleFormats);
            _stylesheet.Append(cellFormats);
            var css = new CellStyles();
            var cs = new CellStyle
            {
                Name = StringValue.FromString("Normal"),
                FormatId = 0,
                BuiltinId = 0
            };
            css.Append(cs);
            css.Count = UInt32Value.FromUInt32((uint)css.ChildElements.Count);
            _stylesheet.Append(css);
            var dfs = new DifferentialFormats { Count = 0 };
            _stylesheet.Append(dfs);
            var tss = new TableStyles
            {
                Count = 0,
                DefaultTableStyle = StringValue.FromString("TableStyleMedium9"),
                DefaultPivotStyle = StringValue.FromString("PivotStyleLight16")
            };
            _stylesheet.Append(tss);

            return _stylesheet;

        }
        #region ExportCode


        public string createExcel2()
        {
            string SPREADSHEET_NAME = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath + "SampleTemplate\\Html" + "_" + Guid.NewGuid() + ".xlsx";
            using (SpreadsheetDocument spreadSheet =
                    SpreadsheetDocument.Create(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, SPREADSHEET_NAME),
                    SpreadsheetDocumentType.Workbook))
            {
                // create the workbook
                spreadSheet.AddWorkbookPart();
                spreadSheet.WorkbookPart.Workbook = new Workbook();     // create the worksheet
                spreadSheet.WorkbookPart.AddNewPart<WorksheetPart>();
                spreadSheet.WorkbookPart.WorksheetParts.First().Worksheet = new Worksheet();

                // create sheet data
                spreadSheet.WorkbookPart.WorksheetParts.First().Worksheet.AppendChild(new SheetData());

                // create row
                spreadSheet.WorkbookPart.WorksheetParts.First().Worksheet.First().AppendChild(new Row());

                // create cell with data
                spreadSheet.WorkbookPart.WorksheetParts.First().Worksheet.First().First().AppendChild(
                      new Cell()
                      {
                          CellValue = new CellValue("101")
                      });

                // save worksheet
                spreadSheet.WorkbookPart.WorksheetParts.First().Worksheet.Save();

                // create the worksheet to workbook relation
                spreadSheet.WorkbookPart.Workbook.AppendChild(new Sheets());
                spreadSheet.WorkbookPart.Workbook.GetFirstChild<Sheets>().AppendChild(new Sheet()
                {
                    Id = spreadSheet.WorkbookPart.GetIdOfPart(spreadSheet.WorkbookPart.WorksheetParts.First()),
                    SheetId = 1,
                    Name = "test"
                });

                spreadSheet.WorkbookPart.Workbook.Save();
            }
            return "";
        }

        public static void setValueToRun(string filepath, string txt)
        {
            try
            {
                /*
                WordprocessingDocument wordprocessingDocument =  //WordprocessingDocument.Open(@"D:\UAT\file1.docx", true);
                WordprocessingDocument.Create(@"D:\UAT\file1.docx", WordprocessingDocumentType.Document, true);

                // Assign a reference to the existing document body.
                DocumentFormat.OpenXml.Wordprocessing.Body body = wordprocessingDocument.MainDocumentPart.Document.Body;

                // Add new text.
                DocumentFormat.OpenXml.Wordprocessing.Paragraph para = body.AppendChild(new DocumentFormat.OpenXml.Wordprocessing.Paragraph());
                Run run = para.AppendChild(new Run());
                run.AppendChild(new Text(txt));

                // Close the handle explicitly.
                wordprocessingDocument.Close();
                return;
                */
                // Open a WordprocessingDocument for editing using the filepath.
                /*  using (SpreadsheetDocument SpreadsheetDocument =
                       SpreadsheetDocument.Open(filepath, true))
                  {
                      string text = "<p><Some <b> HTML</b><i>text</i></p>";
                
                      Run run = new Run();
                
              
                      RunProperties runProperties = run.AppendChild(new RunProperties(new Bold()));
                      run.AppendChild(new Text(text));
              
                
                      WorkbookPart newWorkbookPart = SpreadsheetDocument.WorkbookPart;
                      SharedStringTablePart sharedStringTable = newWorkbookPart.SharedStringTablePart;
                      sharedStringTable.AddPart<Run>(run);

                  }*/

                string outerText = "<span style=\"color:#FF0000;\"><span style=\"font-family:'Calibri Light';\"><span style=\"font-size:12px;\">Expected</span></span></span><span style=\"font-family:'Calibri Light';\"><span style=\"font-size:12px;\"></span></span>";
                outerText = "Plain Text";
                SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create(filepath, SpreadsheetDocumentType.Workbook);
                // Add a WorkbookPart to the document.
                WorkbookPart workbookpart = spreadsheetDocument.AddWorkbookPart();
                workbookpart.Workbook = new Workbook();

                // Add a WorksheetPart to the WorkbookPart.
                WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
                worksheetPart.Worksheet = new Worksheet(new SheetData());

                // Add Sheets to the Workbook.
                Sheets sheets = spreadsheetDocument.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());

                // Append a new worksheet and associate it with the workbook.
                Sheet sheet = new Sheet() { Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = "mySheet" };
                sheets.Append(sheet);

                SharedStringTablePart sharedStringTable = workbookpart.SharedStringTablePart; //worksheetPart.AddNewPart<SharedStringTablePart>();


                Run r = new Run();
                r.AppendChild(new Text(outerText));
                RunProperties rp = r.AppendChild(new RunProperties(new Bold()));


                spreadsheetDocument.WorkbookPart.Workbook.AppendChild<Run>(r);



                // Close the document.
                spreadsheetDocument.Close();
            }
            catch (Exception ex)
            {

            }
        }

        public static void callExcel()
        {
            try
            {
                string TEMPLATE_FILE_NAME = @"D:\UAT\ExcelFile.xlsx";
                SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create(TEMPLATE_FILE_NAME, SpreadsheetDocumentType.Workbook);
                WorkbookPart mExcelWorkbookPart = null;
                SharedStringTablePart mSharedStringTablePart = null;

                using (spreadsheetDocument)
                {
                    // Set private variable template component references (for reuse between methods)
                    mExcelWorkbookPart = spreadsheetDocument.WorkbookPart;
                    if (mExcelWorkbookPart == null)
                    { // Add a WorkbookPart to the document.
                        mExcelWorkbookPart = spreadsheetDocument.AddWorkbookPart();
                    }
                    mExcelWorkbookPart.Workbook = new Workbook();
                    mSharedStringTablePart = mExcelWorkbookPart.SharedStringTablePart;

                    //mExcelWorkbookPart.SharedStringTablePart = mSharedStringTablePart;
                    /**/
                    if (mSharedStringTablePart == null)
                    {
                        mSharedStringTablePart = mExcelWorkbookPart.AddNewPart<SharedStringTablePart>();
                        //mExcelWorkbookPart.AddPart<SharedStringTablePart>(mSharedStringTablePart);
                    }

                    if (mSharedStringTablePart.SharedStringTable == null)
                    {
                        mSharedStringTablePart.SharedStringTable = new SharedStringTable();
                    }
                    /*Iteration 1*/
                    SharedStringItem sharedStringItem1 = new SharedStringItem();

                    Run run1 = new Run();
                    Text text1 = new Text() { Space = SpaceProcessingModeValues.Preserve };
                    text1.Text = "Normal text… ";

                    run1.Append(text1);

                    Run run2 = new Run();

                    RunProperties runProperties1 = new RunProperties();
                    Bold bold1 = new Bold();
                    FontSize fontSize1 = new FontSize() { Val = 11D };
                    //Color color1 = new Color() { Theme = (UInt32Value)5U };
                    Color color1 = new Color() { Rgb = "FF0000" };
                    RunFont runFont1 = new RunFont() { Val = "Calibri" };
                    FontFamily fontFamily1 = new FontFamily() { Val = 2 };
                    FontScheme fontScheme1 = new FontScheme() { Val = FontSchemeValues.Minor };

                    runProperties1.Append(bold1);
                    runProperties1.Append(fontSize1);
                    runProperties1.Append(color1);
                    runProperties1.Append(runFont1);
                    runProperties1.Append(fontFamily1);
                    runProperties1.Append(fontScheme1);
                    Text text2 = new Text();
                    text2.Text = "bold text…";

                    run2.Append(runProperties1);
                    run2.Append(text2);

                    sharedStringItem1.Append(run1);
                    sharedStringItem1.Append(run2);
                    /**/
                    // mSharedStringTablePart.SharedStringTable.AppendChild(GenerateSharedStringItem());

                    mSharedStringTablePart.SharedStringTable.Append(sharedStringItem1);
                    /*Iteration 1 ends*/

                    /*second iteration*/
                    SharedStringItem sharedStringItem2 = new SharedStringItem();

                    Run run3 = new Run();
                    Text text3 = new Text() { Space = SpaceProcessingModeValues.Preserve };
                    text3.Text = "Prasanna";

                    RunProperties runProperties2 = new RunProperties();

                    runProperties2.Append(new Bold());
                    runProperties2.Append(new FontSize() { Val = 20D });
                    //runProperties2.Append(new Color() { Theme = (UInt32Value)11U  });
                    runProperties2.Append(new Color() { Rgb = "00FF00" });
                    runProperties2.Append(new RunFont() { Val = "Arial" });
                    runProperties2.Append(new FontFamily() { Val = 3 });
                    runProperties2.Append(new BackgroundColor() { Theme = (UInt32Value)5U });
                    //runProperties2.Append(new DocumentFormat.OpenXml.Spreadsheet.Fill() { GradientFill = (UInt32Value)5U });
                    run3.Append(runProperties2);
                    run3.Append(text3);

                    sharedStringItem2.Append(run3);


                    mSharedStringTablePart.SharedStringTable.Append(sharedStringItem2);
                    /*second iteration ends*/

                    WorksheetPart worksheetPart = InsertWorksheet(mExcelWorkbookPart);
                    // Insert cell A1 into the new worksheet.

                    Cell cell = InsertCellInWorksheet("A", 1, worksheetPart);
                    // Set the value of cell A1.
                    cell.CellValue = new CellValue("0");
                    cell.DataType = new EnumValue<CellValues>(CellValues.SharedString);

                    Cell cell2 = InsertCellInWorksheet("A", 2, worksheetPart);
                    // Set the value of cell A1.
                    cell2.CellValue = new CellValue("1");
                    cell2.DataType = new EnumValue<CellValues>(CellValues.SharedString);

                    spreadsheetDocument.WorkbookPart.Workbook.Save();
                }

            }
            catch (Exception ex)
            {

            }
        }

        private static Cell InsertCellInWorksheet(string columnName, uint rowIndex, WorksheetPart worksheetPart)
        {
            Cell newCell = null;
            try
            {
                Worksheet worksheet = worksheetPart.Worksheet;
                SheetData sheetData = worksheet.GetFirstChild<SheetData>();
                string cellReference = columnName + rowIndex;

                // If the worksheet does not contain a row with the specified row index, insert one.
                Row row;
                if (sheetData.Elements<Row>().Where(r => r.RowIndex == rowIndex).Count() != 0)
                {
                    row = sheetData.Elements<Row>().Where(r => r.RowIndex == rowIndex).First();
                }
                else
                {
                    row = new Row() { RowIndex = rowIndex };
                    sheetData.Append(row);
                }

                // If there is not a cell with the specified column name, insert one.  
                if (row.Elements<Cell>().Where(c => c.CellReference.Value == columnName + rowIndex).Count() > 0)
                {
                    return row.Elements<Cell>().Where(c => c.CellReference.Value == cellReference).First();
                }
                else
                {
                    // Cells must be in sequential order according to CellReference. Determine where to insert the new cell.
                    Cell refCell = null;
                    foreach (Cell cell in row.Elements<Cell>())
                    {
                        if (string.Compare(cell.CellReference.Value, cellReference, true) > 0)
                        {
                            refCell = cell;
                            break;
                        }
                    }

                    newCell = new Cell() { CellReference = cellReference };
                    row.InsertBefore(newCell, refCell);

                    worksheet.Save();

                }
            }
            catch (Exception ex)
            {

            }
            return newCell;
        }

        public static WorksheetPart InsertWorksheet(WorkbookPart workbookPart)
        {
            WorksheetPart newWorksheetPart = null;
            try
            {
                // Add a new worksheet part to the workbook.
                newWorksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                newWorksheetPart.Worksheet = new Worksheet(new SheetData());
                newWorksheetPart.Worksheet.Save();

                Sheets sheets = workbookPart.Workbook.GetFirstChild<Sheets>();
                string relationshipId = workbookPart.GetIdOfPart(newWorksheetPart);

                if (sheets == null)
                {
                    workbookPart.Workbook.AppendChild(new Sheets());

                    sheets = workbookPart.Workbook.GetFirstChild<Sheets>();
                }
                // Get a unique ID for the new sheet.
                uint sheetId = 1;
                if (sheets.Elements<Sheet>().Count() > 0)
                {
                    sheetId = sheets.Elements<Sheet>().Select(s => s.SheetId.Value).Max() + 1;
                }

                string sheetName = "Sheet" + sheetId;

                // Append the new worksheet and associate it with the workbook.
                Sheet sheet = new Sheet() { Id = relationshipId, SheetId = sheetId, Name = sheetName };
                sheets.Append(sheet);
                workbookPart.Workbook.Save();


            }
            catch (Exception ex)
            {

            }
            return newWorksheetPart;
        }

        public static SharedStringTable GenerateSST()
        {
            try
            {

            }
            catch (Exception ex)
            {

            }
            return null;
        }

        /*temp*/
        public static void callExcel2()
        {
            try
            {
                string TEMPLATE_FILE_NAME = @"D:\UAT\ExcelFile.xlsx";
                SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create(TEMPLATE_FILE_NAME, SpreadsheetDocumentType.Workbook);
                WorkbookPart mExcelWorkbookPart = null;
                SharedStringTablePart mSharedStringTablePart = null;

                using (spreadsheetDocument)
                {
                    // Set private variable template component references (for reuse between methods)
                    mExcelWorkbookPart = spreadsheetDocument.WorkbookPart;
                    if (mExcelWorkbookPart == null)
                    { // Add a WorkbookPart to the document.
                        mExcelWorkbookPart = spreadsheetDocument.AddWorkbookPart();
                    }
                    mExcelWorkbookPart.Workbook = new Workbook();
                    mSharedStringTablePart = mExcelWorkbookPart.SharedStringTablePart;

                    //mExcelWorkbookPart.SharedStringTablePart = mSharedStringTablePart;

                    if (mSharedStringTablePart == null)
                    {
                        mSharedStringTablePart = mExcelWorkbookPart.AddNewPart<SharedStringTablePart>();
                        //mExcelWorkbookPart.AddPart<SharedStringTablePart>(mSharedStringTablePart);
                    }

                    if (mSharedStringTablePart.SharedStringTable == null)
                    {
                        mSharedStringTablePart.SharedStringTable = new SharedStringTable();
                    }
                    /*
                         //sHtmlValue = "<B><span style=\"color:#FF0000;\"> simple </span></B> <span style=\"font-size:72px;\"> </span> <span style=\"font-family:'Calibri Light';\"><span style=\"font-size:72px;\">one</span></span>";
                         //sHtmlValue = "<B><span style=\"color:#C00000;\"><I><span style=\"font-family:'Arial Unicode MS';\"><span style=\"font-size:12px;\">1</span></span></I></span></B><B><span style=\"color:#FF0000;\"><I><span style=\"font-family:'Arial Unicode MS';\"><span style=\"font-size:12px;\">2</span></span></I></span></B><B><span style=\"color:#00B0F0;\"><I><span style=\"font-family:'Arial Unicode MS';\"><span style=\"font-size:12px;\"> </span></span></I></span></B><B><span style=\"color:#FFC000;\"><I><span style=\"font-family:'Arial Unicode MS';\"><span style=\"font-size:12px;\">3</span></span></I></span></B><B><span style=\"color:#00B0F0;\"><I><span style=\"font-family:'Arial Unicode MS';\"><span style=\"font-size:12px;\"> </span></span></I></span></B><B><span style=\"color:#FFFF00;\"><I><span style=\"font-family:'Arial Unicode MS';\"><span style=\"font-size:12px;\">4</span></span></I></span></B><B><span style=\"color:#00B0F0;\"><I><span style=\"font-family:'Arial Unicode MS';\"><span style=\"font-size:12px;\"> </span></span></I></span></B><B><span style=\"color:#92D050;\"><I><span style=\"font-family:'Arial Unicode MS';\"><span style=\"font-size:12px;\">5</span></span></I></span></B><B><span style=\"color:#00B0F0;\"><I><span style=\"font-family:'Arial Unicode MS';\"><span style=\"font-size:12px;\"> </span></span></I></span></B><B><span style=\"color:#00B050;\"><I><span style=\"font-family:'Arial Unicode MS';\"><span style=\"font-size:12px;\">6</span></span></I></span></B><B><span style=\"color:#00B0F0;\"><I><span style=\"font-family:'Arial Unicode MS';\"><span style=\"font-size:12px;\"> 7 </span></span></I></span></B><B><span style=\"color:#0070C0;\"><I><span style=\"font-family:'Arial Unicode MS';\"><span style=\"font-size:12px;\">8</span></span></I></span></B><B><span style=\"color:#00B0F0;\"><I><span style=\"font-family:'Arial Unicode MS';\"><span style=\"font-size:12px;\"> </span></span></I></span></B><B><span style=\"color:#002060;\"><I><span style=\"font-family:'Arial Unicode MS';\"><span style=\"font-size:12px;\">9</span></span></I></span></B><B><span style=\"color:#00B0F0;\"><I><span style=\"font-family:'Arial Unicode MS';\"><span style=\"font-size:12px;\"> </span></span></I></span></B><B><span style=\"color:#7030A0;\"><I><span style=\"font-family:'Arial Unicode MS';\"><span style=\"font-size:12px;\">10</span></span></I></span></B>";
                         //sHtmlValue = "<B><span style=\"color:#FF0000;\"><span style=\"font-family:'Calibri Light';\"><span style=\"font-size:16px;\">This is </span></span></span></B><B><span style=\"color:#00B050;\"><I><span style=\"font-family:'Calibri Light';\"><span style=\"font-size:12px;\">Test Step1</span></span></I></span></B><span style=\"font-family:'Calibri Light';\"><span style=\"font-size:12px;\"> of Test Case1 for Test Pass1</span></span>";
                     */
                    /*
                                        string sHtmlValue = "";
                                        sHtmlValue = "<B> <span style=\"color:#FF0000;\"> <span style=\"font-family:'Calibri Light';\"> <span style=\"font-size:12px;\"> TS</span> </span> </span> </B> <span style=\"color:#FF0000;\"> <span style=\"font-family:'Calibri Light';\"> <span style=\"font-size:12px;\">  </span> </span> </span> <span style=\"color:#FF0000;\"> <I><span style=\"font-family:'Calibri Light';\"> <span style=\"font-size:12px;\"> De</span> </span> </I></span> <span style=\"color:#FF0000;\"> <span style=\"font-family:'Calibri Light';\"> <span style=\"font-size:12px;\"> <U> m</U> </span> </span> </span> <span style=\"font-family:'Calibri Light';\"> <span style=\"font-size:12px;\"> <U> o</U> </span> </span> </span>";
                                        //sHtmlValue = "<B><span style=\"color:#FF0000;\"> simple </span></B> <span style=\"font-size:72px;\"> </span> <span style=\"font-family:'Calibri Light';\"><span style=\"font-size:72px;\">one</span></span>";
                                        SharedStringItem sharedStringItem2 = HtmlToRunProperties(sHtmlValue);
                                        mSharedStringTablePart.SharedStringTable.Append(sharedStringItem2);


                                        string sHtmlValue2 = "<B><span style=\"color:#FF0000;\"><span style=\"font-family:'Calibri Light';\"><span style=\"font-size:16px;\">This is </span></span></span></B><B><span style=\"color:#00B050;\"><I><span style=\"font-family:'Calibri Light';\"><span style=\"font-size:12px;\">Test Step1</span></span></I></span></B><span style=\"font-family:'Calibri Light';\"><span style=\"font-size:12px;\"> of Test Case1 for Test Pass1</span></span>";
                                        SharedStringItem sharedStringItem3 = HtmlToRunProperties(sHtmlValue2);
                                        mSharedStringTablePart.SharedStringTable.Append(sharedStringItem3);
                    

                                        string sHtmlValue3 = "<B><span style=\"color:#C00000;\"><I><span style=\"font-family:'Arial Unicode MS';\"><span style=\"font-size:12px;\">1</span></span></I></span></B><B><span style=\"color:#FF0000;\"><I><span style=\"font-family:'Arial Unicode MS';\"><span style=\"font-size:12px;\">2</span></span></I></span></B><B><span style=\"color:#00B0F0;\"><I><span style=\"font-family:'Arial Unicode MS';\"><span style=\"font-size:12px;\"> </span></span></I></span></B><B><span style=\"color:#FFC000;\"><I><span style=\"font-family:'Arial Unicode MS';\"><span style=\"font-size:12px;\">3</span></span></I></span></B><B><span style=\"color:#00B0F0;\"><I><span style=\"font-family:'Arial Unicode MS';\"><span style=\"font-size:12px;\"> </span></span></I></span></B><B><span style=\"color:#FFFF00;\"><I><span style=\"font-family:'Arial Unicode MS';\"><span style=\"font-size:12px;\">4</span></span></I></span></B><B><span style=\"color:#00B0F0;\"><I><span style=\"font-family:'Arial Unicode MS';\"><span style=\"font-size:12px;\"> </span></span></I></span></B><B><span style=\"color:#92D050;\"><I><span style=\"font-family:'Arial Unicode MS';\"><span style=\"font-size:12px;\">5</span></span></I></span></B><B><span style=\"color:#00B0F0;\"><I><span style=\"font-family:'Arial Unicode MS';\"><span style=\"font-size:12px;\"> </span></span></I></span></B><B><span style=\"color:#00B050;\"><I><span style=\"font-family:'Arial Unicode MS';\"><span style=\"font-size:12px;\">6</span></span></I></span></B><B><span style=\"color:#00B0F0;\"><I><span style=\"font-family:'Arial Unicode MS';\"><span style=\"font-size:12px;\"> 7 </span></span></I></span></B><B><span style=\"color:#0070C0;\"><I><span style=\"font-family:'Arial Unicode MS';\"><span style=\"font-size:12px;\">8</span></span></I></span></B><B><span style=\"color:#00B0F0;\"><I><span style=\"font-family:'Arial Unicode MS';\"><span style=\"font-size:12px;\"> </span></span></I></span></B><B><span style=\"color:#002060;\"><I><span style=\"font-family:'Arial Unicode MS';\"><span style=\"font-size:12px;\">9</span></span></I></span></B><B><span style=\"color:#00B0F0;\"><I><span style=\"font-family:'Arial Unicode MS';\"><span style=\"font-size:12px;\"> </span></span></I></span></B><B><span style=\"color:#7030A0;\"><I><span style=\"font-family:'Arial Unicode MS';\"><span style=\"font-size:12px;\">10</span></span></I></span></B>";
                                        SharedStringItem sharedStringItem4 = HtmlToRunProperties(sHtmlValue3);
                                        mSharedStringTablePart.SharedStringTable.Append(sharedStringItem4);*/


                    WorksheetPart worksheetPart = InsertWorksheet(mExcelWorkbookPart);
                    // Insert cell A1 into the new worksheet.

                    Cell cell = InsertCellInWorksheet("A", 1, worksheetPart);
                    cell.CellValue = new CellValue("0");
                    cell.DataType = new EnumValue<CellValues>(CellValues.SharedString);

                    Cell cell2 = InsertCellInWorksheet("B", 2, worksheetPart);
                    cell2.CellValue = new CellValue("1");
                    cell2.DataType = new EnumValue<CellValues>(CellValues.SharedString);

                    Cell cell3 = InsertCellInWorksheet("C", 3, worksheetPart);
                    cell3.CellValue = new CellValue("2");
                    cell3.DataType = new EnumValue<CellValues>(CellValues.SharedString);

                    spreadsheetDocument.WorkbookPart.Workbook.Save();
                }

            }
            catch (Exception ex)
            {

            }
        }
        /*temp end*/
        //public static SharedStringItem HtmlToRunProperties(string sHtmlValue)
        public static SharedStringItem HtmlToRunProperties(string sHtmlValue, int rowIndex, ref Row rowNew, ref List<ImageInfo> listImageInfo, ref Cell cell, ref Columns columns1, ref CellData celldata)
        {
            SharedStringItem sharedItem = null;
            try
            {
                //sHtmlValue = "<span style=\"color:#FF0000;\">	<span style=\"font-family:'Calibri Light';\">		<span style=\"font-size:72px;\">			one		</span>	</span></span><span style=\"font-family:'Calibri Light';\">	<span style=\"font-size:12px;\">	</span></span><span style=\"color:#00B050;\">	<I>		<span style=\"font-family:'Calibri Light';\">			<span style=\"font-size:12px;\">				t			</span>		</span>	</I></span><span style=\"color:#00B050;\">	<I>		<span style=\"font-family:'Calibri Light';\">			<span style=\"font-size:36px;\">				wo			</span>		</span>	</I></span><span style=\"font-family:'Calibri Light';\">	<span style=\"font-size:12px;\">	</span></span><span style=\"color:#00B0F0;\">	<span style=\"font-family:'Calibri Light';\">		<span style=\"font-size:72px;\">			t		</span>	</span></span><B>	<span style=\"color:#00B0F0;\">		<span style=\"font-family:'Calibri Light';\">			<span style=\"font-size:72px;\">				hre			</span>		</span>	</span></B><B>	<span style=\"font-family:'Calibri Light';\">		<span style=\"font-size:72px;\">			e		</span>	</span></B>";

                //sHtmlValue = "<B><span style=\"color:#FF0000;\"> simple </span></B> <span style=\"font-size:72px;\"> </span> <span style=\"font-family:'Calibri Light';\"><span style=\"font-size:72px;\">one</span></span>";

                //sHtmlValue = "<B><span style=\"color:#C00000;\"><I><span style=\"font-family:'Arial Unicode MS';\"><span style=\"font-size:12px;\">1</span></span></I></span></B><B><span style=\"color:#FF0000;\"><I><span style=\"font-family:'Arial Unicode MS';\"><span style=\"font-size:12px;\">2</span></span></I></span></B><B><span style=\"color:#00B0F0;\"><I><span style=\"font-family:'Arial Unicode MS';\"><span style=\"font-size:12px;\"> </span></span></I></span></B><B><span style=\"color:#FFC000;\"><I><span style=\"font-family:'Arial Unicode MS';\"><span style=\"font-size:12px;\">3</span></span></I></span></B><B><span style=\"color:#00B0F0;\"><I><span style=\"font-family:'Arial Unicode MS';\"><span style=\"font-size:12px;\"> </span></span></I></span></B><B><span style=\"color:#FFFF00;\"><I><span style=\"font-family:'Arial Unicode MS';\"><span style=\"font-size:12px;\">4</span></span></I></span></B><B><span style=\"color:#00B0F0;\"><I><span style=\"font-family:'Arial Unicode MS';\"><span style=\"font-size:12px;\"> </span></span></I></span></B><B><span style=\"color:#92D050;\"><I><span style=\"font-family:'Arial Unicode MS';\"><span style=\"font-size:12px;\">5</span></span></I></span></B><B><span style=\"color:#00B0F0;\"><I><span style=\"font-family:'Arial Unicode MS';\"><span style=\"font-size:12px;\"> </span></span></I></span></B><B><span style=\"color:#00B050;\"><I><span style=\"font-family:'Arial Unicode MS';\"><span style=\"font-size:12px;\">6</span></span></I></span></B><B><span style=\"color:#00B0F0;\"><I><span style=\"font-family:'Arial Unicode MS';\"><span style=\"font-size:12px;\"> 7 </span></span></I></span></B><B><span style=\"color:#0070C0;\"><I><span style=\"font-family:'Arial Unicode MS';\"><span style=\"font-size:12px;\">8</span></span></I></span></B><B><span style=\"color:#00B0F0;\"><I><span style=\"font-family:'Arial Unicode MS';\"><span style=\"font-size:12px;\"> </span></span></I></span></B><B><span style=\"color:#002060;\"><I><span style=\"font-family:'Arial Unicode MS';\"><span style=\"font-size:12px;\">9</span></span></I></span></B><B><span style=\"color:#00B0F0;\"><I><span style=\"font-family:'Arial Unicode MS';\"><span style=\"font-size:12px;\"> </span></span></I></span></B><B><span style=\"color:#7030A0;\"><I><span style=\"font-family:'Arial Unicode MS';\"><span style=\"font-size:12px;\">10</span></span></I></span></B>";

                //sHtmlValue = "<B><span style=\"color:#FF0000;\"><span style=\"font-family:'Calibri Light';\"><span style=\"font-size:16px;\">This is </span></span></span></B><B><span style=\"color:#00B050;\"><I><span style=\"font-family:'Calibri Light';\"><span style=\"font-size:12px;\">Test Step1</span></span></I></span></B><span style=\"font-family:'Calibri Light';\"><span style=\"font-size:12px;\"> of Test Case1 for Test Pass1</span></span>";

                //sHtmlValue = "<B><span style=\"color:#FF0000;\"><span style=\"font-family:'Calibri Light';\"><span style=\"font-size:16px;\">This is </span></span></span></B><span style=\"font-family:'Calibri Light';\"><span style=\"font-size:12px;\">Test Step1 of Test Case1 for Test Pass1</span></span>";

                /*temp*/
                //sHtmlValue = sHtmlValue.Trim();

                //string newstr = AutoCloseHtmlTags(sHtmlValue);
                if (sHtmlValue.Contains("<p"))
                {
                    //sHtmlValue = sHtmlValue.Replace("<p>", "").Replace("</p>", "");
                    sHtmlValue = sHtmlValue.Replace("<p", "<span").Replace("<p>", "<span>").Replace("</p>", "</span>");
                }
                if (sHtmlValue.Contains("&nbsp;"))
                {
                    sHtmlValue = sHtmlValue.Replace("&nbsp;", " ");
                }
                if (sHtmlValue.Contains("<br>"))
                {
                    //sHtmlValue = sHtmlValue.Replace("<br>", "<br/>");
                    sHtmlValue = sHtmlValue.Replace("<br>", "\n").Replace("<br/>", "\n");
                }
                /**/
                //sHtmlValue = sHtmlValue.Replace("<o:p>", "").Replace("</o:p>", "");
                sHtmlValue = Regex.Replace(sHtmlValue, "<o:p>*</o:p>", "", RegexOptions.Singleline);
                sHtmlValue = sHtmlValue.Replace("<o:p>", "").Replace("</o:p>", "");
                sHtmlValue = sHtmlValue.Replace("<font", "<span").Replace("</font>", "</span>").Replace("<strong", "<b").Replace("</strong>", "</b>").Replace("<em", "<I").Replace("</em>", "</I>");
                sHtmlValue = Regex.Replace(sHtmlValue, "<!--.*?-->", "", RegexOptions.Singleline);
                //sHtmlValue = Regex.Replace(sHtmlValue, "<?xml:namespace * />", "", RegexOptions.Singleline);
                sHtmlValue = sHtmlValue.Replace("<?xml:namespace prefix = \"o\" ns = \"urn:schemas-microsoft-com:office:office\" />", "");
                //<?xml:namespace prefix = "o" ns = "urn:schemas-microsoft-com:office:office" />
                //if (sHtmlValue.Contains("<!--"))
                //{
                //    sHtmlValue = sHtmlValue.
                //}
                /**/
                if (sHtmlValue.Contains("<img"))
                {
                    /*creating proper img tag, i.e. adding < /> if it doesnot exist*/
                    Regex reImg = new Regex(@"<img\s[^>]*>", RegexOptions.IgnoreCase);
                    MatchCollection mc = reImg.Matches(sHtmlValue);
                    foreach (Match mImg in mc)
                    {
                        string sImgStr = mImg.Value;
                        if (!string.IsNullOrEmpty(sImgStr))
                        {
                            int lidx = sImgStr.LastIndexOf(">");
                            //if (sImgStr.ElementAt(lidx - 1) != '/')
                            //{

                            //}
                            char[] iStr = sImgStr.ToCharArray();
                            char flagSlash = 'N';
                            for (int i = iStr.Length - 1; i > 0; i--)
                            {
                                if ((iStr[i] == '"' || iStr[i] == '\''))
                                {
                                    break;
                                }
                                if (iStr[i] == '/')
                                {
                                    flagSlash = 'Y';
                                    break;
                                }
                            }
                            if (flagSlash == 'N')
                            {
                                string sImgStr2 = sImgStr.Remove(sImgStr.Length - 1, 1) + "/>";/*replacing last character with />*/
                                sHtmlValue = sHtmlValue.Replace(sImgStr, sImgStr2);
                            }
                        }
                    }

                    /*mixed string*/
                    if (sHtmlValue.ElementAt(0) != '<')
                    {
                        string sPlainText = sHtmlValue.Substring(0, sHtmlValue.IndexOf("<"));
                        string RichText = sHtmlValue.Substring(sHtmlValue.IndexOf("<"));

                        string sPlainText2 = "<span>" + sPlainText + "</span>";
                        sHtmlValue = sPlainText2 + RichText;
                    }
                    if (sHtmlValue.ElementAt((sHtmlValue.Length - 1)) != '>' && sHtmlValue.ElementAt((sHtmlValue.Length - 1)) != '\n')
                    {
                        string sPlainText = sHtmlValue.Substring(sHtmlValue.LastIndexOf(">") + 1, (sHtmlValue.Length - (sHtmlValue.LastIndexOf(">") + 1)));
                        //string RichText = sHtmlValue.Substring((sHtmlValue.Length - (sHtmlValue.LastIndexOf(">") + 2)), (sHtmlValue.LastIndexOf(">") + 1));
                        string RichText = sHtmlValue.Substring(sHtmlValue.IndexOf("<img"), (sHtmlValue.LastIndexOf(">") + 1));
                        string sPlainText2 = "<span>" + sPlainText + "</span>";
                        sHtmlValue = RichText + sPlainText2;
                    }

                }
                /*temp*/


                /**/
                sHtmlValue = "<Html>" + sHtmlValue + "</Html>";
                XmlDocument doc = new XmlDocument();
                try
                {
                    doc.LoadXml(sHtmlValue);
                }
                catch (Exception)
                {
                    sHtmlValue = wellformedxml(sHtmlValue);
                    doc.LoadXml(sHtmlValue);
                }


                XmlElement RootElement = doc.DocumentElement;
                if (RootElement.HasChildNodes)
                {
                    int ChildCount = RootElement.ChildNodes.Count;

                    XmlNodeList elementList = RootElement.ChildNodes;
                    //Dictionary<Object, List<XmlAttributeCollection>> lstMain = new Dictionary<Object, List<XmlAttributeCollection>>();
                    List<KeyValuePair<string, List<XmlAttributeCollection>>> lstMain = new List<KeyValuePair<string, List<XmlAttributeCollection>>>();
                    //var list = new List<KeyValuePair<string,List<XmlAttributeCollection>>>()
                    for (int i = 0; i < ChildCount; i++)
                    {
                        //if (i==6)
                        {
                            XmlNode node = elementList[i];
                            if (node.HasChildNodes)
                            {
                                //getElements(ref lstMain, node, ref doc);
                                //getElements(ref lstMain, node, ref doc, rowIndex, ref rowNew, ref listImageInfo, ref cell, ref columns1, ref celldata);


                                //object keyValPair = getElements(node, ref doc, rowIndex, ref rowNew, ref listImageInfo, ref cell, ref columns1, ref celldata);
                                //lstMain.Add((KeyValuePair<string, List<XmlAttributeCollection>>)keyValPair);
                                lstMain.AddRange(getElements(node, ref doc, rowIndex, ref rowNew, ref listImageInfo, ref cell, ref columns1, ref celldata));


                            }
                            else
                            {
                                //getElements(ref lstMain, node, ref doc);
                                //getElements(ref lstMain, node, ref doc, rowIndex, ref rowNew, ref listImageInfo, ref cell, ref columns1, ref celldata);

                                //object keyValPair = getElements(node, ref doc, rowIndex, ref rowNew, ref listImageInfo, ref cell, ref columns1, ref celldata);
                                //lstMain.Add((KeyValuePair<string, List<XmlAttributeCollection>>)keyValPair);
                                lstMain.AddRange(getElements(node, ref doc, rowIndex, ref rowNew, ref listImageInfo, ref cell, ref columns1, ref celldata));
                            }
                        }
                    }
                    /*SharedStringTable*/

                    /**/
                    /*Creating Shared String Item for shared string table*/
                    sharedItem = new SharedStringItem();
                    sharedItem = CreateSharedStringItem(lstMain);

                    /**/
                }
            }
            catch (Exception ex)
            {

            }
            return sharedItem;
        }

        private static string wellformedxml(string sHtmlValue)
        {
            try
            {
                int cntSpan = 0;
                int cntSpanClose = 0;
                int cntP = 0;
                int cntPClose = 0;
                StringBuilder sb = new StringBuilder();
                string[] arr = sHtmlValue.Split(new char[] { '<' }, StringSplitOptions.RemoveEmptyEntries);
                List<int> arrIdx = new List<int>();
                for (int i = 0; i < arr.Length; i++)
                {
                    if (arr[i].Contains("span") && !arr[i].Contains("/span>"))
                    {
                        int c1 = new Regex(Regex.Escape("span")).Matches(arr[i]).Count;
                        cntSpan += c1;
                    }
                    if (arr[i].Contains("/span>"))
                    {
                        int c2 = new Regex(Regex.Escape("/span>")).Matches(arr[i]).Count;
                        cntSpanClose += c2;
                    }
                    if (cntSpanClose <= cntSpan)
                    {
                        sb.Append("<" + arr[i]);
                    }
                    else
                    {
                        sb.Append("");
                        cntSpanClose = cntSpan;
                    }
                }
                sHtmlValue = sb.ToString();
            }
            catch (Exception ex)
            {

            }
            return sHtmlValue;
        }

        //private static string wellformedxml(string sHtmlValue)
        //{
        //    try
        //    {
        //        int cntSpan = 0;
        //        int cntSpanClose = 0;
        //        int cntP = 0;
        //        int cntPClose = 0;

        //        cntSpan = new Regex(Regex.Escape("<span")).Matches(sHtmlValue).Count;
        //        cntSpanClose = new Regex(Regex.Escape("</span>")).Matches(sHtmlValue).Count;
        //        cntP = new Regex(Regex.Escape("<p")).Matches(sHtmlValue).Count;
        //        cntPClose = new Regex(Regex.Escape("</p>")).Matches(sHtmlValue).Count;

        //        if (cntSpanClose != cntSpan)
        //        {
        //            if (cntSpanClose > cntSpan)
        //            {
        //                int diff = cntSpanClose - cntSpan;
        //                for (int q = 0; q < diff; q++)
        //                {
        //                    int lIdx = sHtmlValue.Trim().LastIndexOf("</span></span>");
        //                    sHtmlValue = sHtmlValue.Remove(lIdx, "</span>".Length);
        //                }
        //            }else
        //            if (cntSpanClose < cntSpan)
        //            {
        //                int diff = cntSpan - cntSpanClose;
        //                for (int q = 0; q < diff; q++)
        //                {
        //                    int lIdx = sHtmlValue.Trim().LastIndexOf("<span><span>");
        //                    sHtmlValue = sHtmlValue.Remove(lIdx, "<span>".Length);
        //                }
        //            }
        //        }
        //        if (cntP != cntPClose)
        //        {
        //            if (cntPClose > cntP)
        //            {
        //                int diff = cntPClose - cntP;
        //                for (int q = 0; q < diff; q++)
        //                {
        //                    int lIdx = sHtmlValue.Trim().LastIndexOf("</p></p>");
        //                    sHtmlValue = sHtmlValue.Remove(lIdx, "</p>".Length);
        //                }
        //            }else
        //            if (cntPClose < cntP)
        //            {
        //                int diff = cntP - cntPClose;
        //                for (int q = 0; q < diff; q++)
        //                {
        //                    int lIdx = sHtmlValue.Trim().LastIndexOf("<p><p>");
        //                    sHtmlValue = sHtmlValue.Remove(lIdx, "<p>".Length);
        //                }
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return sHtmlValue;
        //}

        public static string AutoCloseHtmlTags(string inputHtml)
        {
            var regexStartTag = new Regex(@"<(!--\u002E\u002E\u002E--|!DOCTYPE|a|abbr|" +
                  @"acronym|address|applet|area|article|aside|audio|b|base|basefont|bdi|bdo|big" +
                  @"|blockquote|body|br|button|canvas|caption|center|cite|code|col|colgroup|" +
                  @"command|datalist|dd|del|details|dfn|dialog|dir|div|dl|dt|em|embed|fieldset|" +
                  @"figcaption|figure|font|footer|form|frame|frameset|h1> to <h6|head|" +
                  @"header|hr|html|i|iframe|img|input|ins|kbd|keygen|label|legend|li|link|" +
                  @"map|mark|menu|meta|meter|nav|noframes|noscript|object|ol|optgroup|option|" +
                  @"output|p|param|pre|progress|q|rp|rt|ruby|s|samp|script|section|select|small|" +
                  @"source|span|strike|strong|style|sub|summary|sup|table|tbody|td|textarea|" +
                  @"tfoot|th|thead|time|title|tr|track|tt|u|ul|var|video|wbr)(\s\w+.*(\u0022|'))?>");
            var startTagCollection = regexStartTag.Matches(inputHtml);
            var regexCloseTag = new Regex(@"</(!--\u002E\u002E\u002E--|!DOCTYPE|a|abbr|" +
                  @"acronym|address|applet|area|article|aside|audio|b|base|basefont|bdi|bdo|" +
                  @"big|blockquote|body|br|button|canvas|caption|center|cite|code|col|colgroup|" +
                  @"command|datalist|dd|del|details|dfn|dialog|dir|div|dl|dt|em|embed|fieldset|" +
                  @"figcaption|figure|font|footer|form|frame|frameset|h1> to <h6|head|header" +
                  @"|hr|html|i|iframe|img|input|ins|kbd|keygen|label|legend|li|link|map|mark|menu|" +
                  @"meta|meter|nav|noframes|noscript|object|ol|optgroup|option|output|p|param|pre|" +
                  @"progress|q|rp|rt|ruby|s|samp|script|section|select|small|source|span|strike|" +
                  @"strong|style|sub|summary|sup|table|tbody|td|textarea|tfoot|th|thead|" +
                  @"time|title|tr|track|tt|u|ul|var|video|wbr)>");
            var closeTagCollection = regexCloseTag.Matches(inputHtml);
            var startTagList = new List<string>();
            var closeTagList = new List<string>();
            var resultClose = "";
            foreach (Match startTag in startTagCollection)
            {
                startTagList.Add(startTag.Value);
            }
            foreach (Match closeTag in closeTagCollection)
            {
                closeTagList.Add(closeTag.Value);
            }
            startTagList.Reverse();
            for (int i = 0; i < closeTagList.Count; i++)
            {
                if (startTagList[i] != closeTagList[i])
                {
                    int indexOfSpace = startTagList[i].IndexOf(
                             " ", System.StringComparison.Ordinal);
                    if (startTagList[i].Contains(" "))
                    {
                        startTagList[i].Remove(indexOfSpace);
                    }
                    startTagList[i] = startTagList[i].Replace("<", "</");
                    resultClose += startTagList[i] + ">";
                    resultClose = resultClose.Replace(">>", ">");
                }
            }
            return inputHtml + resultClose;
        }

        /*Creating Shared String Item and adding Runs*/
        //private static SharedStringItem CreateSharedStringItem(Dictionary<Object, List<XmlAttributeCollection>> lstMain)
        private static SharedStringItem CreateSharedStringItem(List<KeyValuePair<string, List<XmlAttributeCollection>>> lstMain)
        {
            SharedStringItem newSSI = new SharedStringItem();
            try
            {
                List<XmlAttributeCollection> lstAttColln = null;
                foreach (var item in lstMain)
                {
                    Text txtKeyVal = new Text() { Space = SpaceProcessingModeValues.Preserve };
                    txtKeyVal.Text = Convert.ToString(item.Key);
                    lstAttColln = new List<XmlAttributeCollection>();
                    lstAttColln.AddRange(item.Value);
                    newSSI.Append(CreateRuns(txtKeyVal, lstAttColln));
                }

                //var list = new List<KeyValuePair<string,List<XmlAttributeCollection>>>();
            }
            catch (Exception ex)
            {

            }
            return newSSI;
        }

        private static Run CreateRuns(Text StrValue, List<XmlAttributeCollection> lstRunProp)
        {
            Run newRun = null;
            try
            {
                newRun = new Run();
                if (StrValue.Text.Trim() == "_~#*~") { StrValue.Text = " "; }
                newRun.Text = (StrValue);

                RunProperties rp = new RunProperties();
                //rp.Append(new Bold());
                //rp.Append(new FontSize() { Val = 20D });
                //rp.Append(new Color() { Rgb = "00FF55" });
                //rp.Append(new RunFont() { Val = "Arial" });

                for (int i = 0; i < lstRunProp.Count; i++)
                {
                    string property = "";
                    try
                    {
                        property = lstRunProp[i][0].Value;
                        if (!property.Contains(":") && (!"BOLD,ITALICS,UNDERLINE,UL".Contains(property)))
                        {
                            property = lstRunProp[i][0].Name + ":" + lstRunProp[i][0].Value;
                        }
                    }
                    catch (Exception ex1)
                    {
                        continue;
                    }

                    /* if (!string.IsNullOrEmpty(property))
                     {
                        // property = property.Replace("'", "").Replace("\"", "").Replace(";", "").Replace("#", "").Replace("px", "").Replace("pt", "");
                         property = property.Replace("'", "").Replace("\"", "").Replace("#", "").Replace("px", "").Replace("pt", "");

                         if (property.Contains("BOLD"))
                         {
                             rp.Append(new Bold());
                         }else
                         if (property.Contains("ITALICS"))
                         {
                             rp.Append(new Italic());
                         }
                         else
                         if (property.Contains("UNDERLINE"))
                         {
                             rp.Append(new Underline());
                         }
                         else
                         if (property.Contains("http") || property.Contains("www"))
                         {
                             if (property.Contains("href:"))
                             {
                                 property = property.Replace("href:", "");
                             }
                             string displayStr = lstRunProp[i][0].OwnerElement.InnerText;
                             Uri u = new Uri(property);
                             rp.Append(new Hyperlink() { Location = u.AbsoluteUri, Display = u.OriginalString });
                         }
                         else if (property.Contains("font-style") || property.Contains("font-family") )
                         {
                             rp.Append(new RunFont() { Val = property.Split(new char[] { ':' })[1] });
                         }
                         else if (property.Contains("font-size") )
                         {
                             rp.Append(new FontSize() { Val = Convert.ToDouble(isNumeric(property.Split(new char[] { ':' })[1])) });
                         }
                         else if (property.Contains("text-decoration"))
                         {
                             rp.Append(new RunFont() { Val = property.Split(new char[] { ':' })[1] });
                         }
                         else if (property.Contains("face"))
                         {
                             //font-family: Wingdings; mso-fareast-font-family: Wingdings; mso-bidi-font-family: Wingdings;
                             string[] fontfam = property.Split(new char[] { ';' });
                             string sfontfamily = "";
                             for (int i1 = 0; i1 < fontfam.Length; i1++)
                             {
                                 if (fontfam[i1].Contains("face"))
                                 {
                                     sfontfamily = fontfam[i1].Split(new char[] { ':' })[1];
                                 }
                                 //if (fontfam[i1].Contains("font-family"))
                                 //{
                                 //    sfontfamily = fontfam[i1].Split(new char[] { ':' })[1];
                                 //}
                             }
                             if (!string.IsNullOrEmpty(sfontfamily))
                             {
                                 rp.Append(new RunFont() { Val = sfontfamily });
                             }
                         }
                         else if (property.Contains("size"))
                         {
                             rp.Append(new FontSize() { Val = Convert.ToDouble(isNumeric(property.Split(new char[] { ':' })[1])) });
                         }
                         else if (property.Contains("color"))
                         {
                             if (property.Contains("rgb"))
                             {
                                 string color = property.Split(new char[] { ':' })[1];
                                 color = color.Replace("rgb(", "").Replace(")", "");

                                 int r = Convert.ToInt32(isNumeric(color.Split(new char[] { ',' })[0]));
                                 int g = Convert.ToInt32(isNumeric(color.Split(new char[] { ',' })[1]));
                                 int b = Convert.ToInt32(isNumeric(color.Split(new char[] { ',' })[2]));

                                 color = System.Drawing.ColorTranslator.FromHtml(string.Format("#{0:X2}{1:X2}{2:X2}", r, g, b)).ToString();
                                 rp.Append(new Color() { Rgb = color });

                             }
                             else
                             {
                                 rp.Append(new Color() { Rgb = property.Split(new char[] { ':' })[1] });
                             }

                         }

                     }*/
                    if (!string.IsNullOrEmpty(property))
                    {
                        // property = property.Replace("'", "").Replace("\"", "").Replace(";", "").Replace("#", "").Replace("px", "").Replace("pt", "");
                        property = property.Replace("'", "").Replace("\"", "").Replace("#", "").Replace("px", "").Replace("pt", "");
                        string[] arrProp = property.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                        for (int cnt = 0; cnt < arrProp.Count(); cnt++)
                        {

                            if (arrProp[cnt].Contains("BOLD"))
                            {
                                rp.Append(new Bold());
                            }
                            else
                                if (arrProp[cnt].Contains("ITALICS"))
                                {
                                    rp.Append(new Italic());
                                }
                                else
                                    if (arrProp[cnt].Contains("UNDERLINE"))
                                    {
                                        rp.Append(new Underline());
                                    }
                                    else
                                        if (arrProp[cnt].Contains("http") || arrProp[cnt].Contains("www"))
                                        {
                                            if (arrProp[cnt].Contains("href:"))
                                            {
                                                arrProp[cnt] = arrProp[cnt].Replace("href:", "");
                                            }
                                            string displayStr = lstRunProp[i][0].OwnerElement.InnerText;
                                            Uri u = new Uri(property);
                                            rp.Append(new Color() { Rgb = "0563c1"/*"0000FF"*/ });
                                            rp.Append(new ObjectAnchor(new Hyperlink() { Location = u.AbsoluteUri, Display = u.OriginalString }));
                                            //rp.Append(new Hyperlink() { Location = u.AbsoluteUri, Display = u.OriginalString });

                                        }
                                        else
                                            if (arrProp[cnt].Contains("font-style") || arrProp[cnt].Contains("font-family"))
                                            {
                                                rp.Append(new RunFont() { Val = arrProp[cnt].Split(new char[] { ':' })[1] });
                                            }
                                            else
                                                if (arrProp[cnt].Contains("font-size"))
                                                {
                                                    rp.Append(new FontSize() { Val = Convert.ToDouble(isNumeric(arrProp[cnt].Split(new char[] { ':' })[1])) });
                                                }
                                                else
                                                    if (arrProp[cnt].Contains("text-decoration"))
                                                    {
                                                        rp.Append(new RunFont() { Val = arrProp[cnt].Split(new char[] { ':' })[1] });
                                                    }
                                                    else
                                                        if (arrProp[cnt].Contains("face"))
                                                        {
                                                            //font-family: Wingdings; mso-fareast-font-family: Wingdings; mso-bidi-font-family: Wingdings;
                                                            string[] fontfam = arrProp[cnt].Split(new char[] { ';' });
                                                            string sfontfamily = "";
                                                            for (int i1 = 0; i1 < fontfam.Length; i1++)
                                                            {
                                                                if (fontfam[i1].Contains("face"))
                                                                {
                                                                    sfontfamily = fontfam[i1].Split(new char[] { ':' })[1];
                                                                }
                                                                //if (fontfam[i1].Contains("font-family"))
                                                                //{
                                                                //    sfontfamily = fontfam[i1].Split(new char[] { ':' })[1];
                                                                //}
                                                            }
                                                            if (!string.IsNullOrEmpty(sfontfamily))
                                                            {
                                                                rp.Append(new RunFont() { Val = sfontfamily });
                                                            }
                                                        }
                                                        else
                                                            if (arrProp[cnt].Contains("size"))
                                                            {
                                                                //string[] subprop = arrProp[cnt].Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                                                                //string subproperty = (from s in subprop where s.Contains("color") select s).ToString();
                                                                double SizeVal = Convert.ToDouble(isNumeric(arrProp[cnt].Split(new char[] { ':' })[1]));
                                                                if (SizeVal <= 7 && SizeVal >= 1)
                                                                {
                                                                    /*http://home.earthlink.net/~silvermaplesoft/standards/size_heading.html */
                                                                    switch (SizeVal.ToString())
                                                                    {
                                                                        case "1": SizeVal = 8; break;
                                                                        case "2": SizeVal = 10; break;
                                                                        case "3": SizeVal = 12; break;
                                                                        case "4": SizeVal = 16; break;
                                                                        case "5": SizeVal = 18; break;
                                                                        case "6": SizeVal = 24; break;
                                                                        case "7": SizeVal = 36; break;
                                                                        default: SizeVal = 12;
                                                                            break;
                                                                    }
                                                                }
                                                                rp.Append(new FontSize() { Val = SizeVal });
                                                            }
                                                            else if (arrProp[cnt].Contains("background"))
                                                            {
                                                                try
                                                                {
                                                                    if (arrProp[cnt].Contains("rgb"))
                                                                    {
                                                                        string color = arrProp[cnt].Split(new char[] { ':' })[1];
                                                                        color = color.Replace("rgb(", "").Replace(")", "");

                                                                        int r = Convert.ToInt32(isNumeric(color.Split(new char[] { ',' })[0]));
                                                                        int g = Convert.ToInt32(isNumeric(color.Split(new char[] { ',' })[1]));
                                                                        int b = Convert.ToInt32(isNumeric(color.Split(new char[] { ',' })[2]));

                                                                        //  color = System.Drawing.ColorTranslator.FromHtml(string.Format("#{0:X2}{1:X2}{2:X2}", r, g, b)).ToString();
                                                                        color = string.Format("{0:X2}{1:X2}{2:X2}", r, g, b);
                                                                        //rp.Append(new BackgroundColor() { Rgb = color });

                                                                        //rp.Append(new PatternFill(new BackgroundColor() { Rgb = color }));
                                                                        rp.Append(new Fill(new BackgroundColor() { Rgb = color }));


                                                                    }
                                                                    else
                                                                    {
                                                                        string colVal = arrProp[cnt].Split(new char[] { ':' })[1].Trim();
                                                                        //rp.Append(new Color() { Rgb = arrProp[cnt].Split(new char[] { ':' })[1] });
                                                                        bool isHex = System.Text.RegularExpressions.Regex.IsMatch(colVal, @"\A\b[0-9a-fA-F]+\b\Z");
                                                                        if (isHex == true)
                                                                        {
                                                                            //rp.Append(new BackgroundColor() { Rgb = colVal });
                                                                            //rp.Append(new PatternFill(new BackgroundColor() { Rgb = color }));
                                                                            rp.Append(new Fill(new BackgroundColor() { Rgb = colVal }));
                                                                        }
                                                                        else
                                                                        {

                                                                            System.Drawing.ColorConverter cc = new System.Drawing.ColorConverter();
                                                                            object oColor = cc.ConvertFromString(colVal);
                                                                            System.Drawing.Color oCol = (System.Drawing.Color)oColor;

                                                                            int r = Convert.ToInt32(oCol.R);
                                                                            int g = Convert.ToInt32(oCol.G);
                                                                            int b = Convert.ToInt32(oCol.B);

                                                                            // string color = System.Drawing.ColorTranslator.FromHtml(string.Format("#{0:X2}{1:X2}{2:X2}", r, g, b)).ToString();
                                                                            string color = string.Format("{0:X2}{1:X2}{2:X2}", r, g, b);
                                                                            //rp.Append(new BackgroundColor() { Rgb = color });
                                                                            //rp.Append(new PatternFill(new BackgroundColor() { Rgb = color }));
                                                                            rp.Append(new Fill(new BackgroundColor() { Rgb = color }));
                                                                        }
                                                                    }
                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                    continue;
                                                                }
                                                            }
                                                            else
                                                                if (arrProp[cnt].Contains("color") && !arrProp[cnt].Contains("mso"))
                                                                {
                                                                    try
                                                                    {
                                                                        if (arrProp[cnt].Contains("rgb"))
                                                                        {
                                                                            string color = arrProp[cnt].Split(new char[] { ':' })[1];
                                                                            color = color.Replace("rgb(", "").Replace(")", "");

                                                                            int r = Convert.ToInt32(isNumeric(color.Split(new char[] { ',' })[0]));
                                                                            int g = Convert.ToInt32(isNumeric(color.Split(new char[] { ',' })[1]));
                                                                            int b = Convert.ToInt32(isNumeric(color.Split(new char[] { ',' })[2]));

                                                                            //  color = System.Drawing.ColorTranslator.FromHtml(string.Format("#{0:X2}{1:X2}{2:X2}", r, g, b)).ToString();
                                                                            color = string.Format("{0:X2}{1:X2}{2:X2}", r, g, b);
                                                                            rp.Append(new Color() { Rgb = color });

                                                                        }
                                                                        else
                                                                        {
                                                                            string colVal = arrProp[cnt].Split(new char[] { ':' })[1].Trim();
                                                                            //rp.Append(new Color() { Rgb = arrProp[cnt].Split(new char[] { ':' })[1] });
                                                                            bool isHex = System.Text.RegularExpressions.Regex.IsMatch(colVal, @"\A\b[0-9a-fA-F]+\b\Z");
                                                                            if (isHex == true)
                                                                            {
                                                                                rp.Append(new Color() { Rgb = colVal });
                                                                            }
                                                                            else
                                                                            {

                                                                                System.Drawing.ColorConverter cc = new System.Drawing.ColorConverter();
                                                                                object oColor = cc.ConvertFromString(colVal);
                                                                                System.Drawing.Color oCol = (System.Drawing.Color)oColor;

                                                                                int r = Convert.ToInt32(oCol.R);
                                                                                int g = Convert.ToInt32(oCol.G);
                                                                                int b = Convert.ToInt32(oCol.B);

                                                                                // string color = System.Drawing.ColorTranslator.FromHtml(string.Format("#{0:X2}{1:X2}{2:X2}", r, g, b)).ToString();
                                                                                string color = string.Format("{0:X2}{1:X2}{2:X2}", r, g, b);
                                                                                rp.Append(new Color() { Rgb = color });
                                                                            }
                                                                        }
                                                                    }
                                                                    catch (Exception ex)
                                                                    {
                                                                        continue;
                                                                    }
                                                                    //string[] subprop = property.Split(new string[] {";"}, StringSplitOptions.RemoveEmptyEntries);
                                                                    //string subproperty = (from s in subprop where s.Contains("color") select s).ToString();
                                                                    //if (subproperty.Contains("rgb"))
                                                                    //{
                                                                    //    string color = subproperty.Split(new char[] { ':' })[1];
                                                                    //    color = color.Replace("rgb(", "").Replace(")", "");

                                                                    //    int r = Convert.ToInt32(isNumeric(color.Split(new char[] { ',' })[0]));
                                                                    //    int g = Convert.ToInt32(isNumeric(color.Split(new char[] { ',' })[1]));
                                                                    //    int b = Convert.ToInt32(isNumeric(color.Split(new char[] { ',' })[2]));

                                                                    //    color = System.Drawing.ColorTranslator.FromHtml(string.Format("#{0:X2}{1:X2}{2:X2}", r, g, b)).ToString();
                                                                    //    rp.Append(new Color() { Rgb = color });

                                                                    //}
                                                                    //else
                                                                    //{
                                                                    //    rp.Append(new Color() { Rgb = subproperty.Split(new char[] { ':' })[1] });
                                                                    //}

                                                                }

                        }
                    }
                }
                newRun.RunProperties = rp;

            }
            catch (Exception ex)
            {

            }
            return newRun;
        }

        private static String ColorHexConverter(System.Drawing.Color c)
        {
            String rtn = String.Empty;
            try
            {
                rtn = "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
            }
            catch (Exception ex)
            {
                //doing nothing
            }

            return rtn;
        }

        //private static void getElements(ref List<KeyValuePair<string,List<XmlAttributeCollection>>> lstMain, XmlNode parentElement, ref XmlDocument doc)
        //private static void getElements(ref List<KeyValuePair<string, List<XmlAttributeCollection>>> lstMain, XmlNode parentElement, ref XmlDocument doc, int rowIndex, ref Row rowNew, ref List<ImageInfo> listImageInfo, ref Cell cell, ref Columns columns1, ref CellData celldata)
        //private static object getElements(XmlNode parentElement, ref XmlDocument doc, int rowIndex, ref Row rowNew, ref List<ImageInfo> listImageInfo, ref Cell cell, ref Columns columns1, ref CellData celldata)
        private static List<KeyValuePair<string, List<XmlAttributeCollection>>> getElements(XmlNode parentElement, ref XmlDocument doc, int rowIndex, ref Row rowNew, ref List<ImageInfo> listImageInfo, ref Cell cell, ref Columns columns1, ref CellData celldata)
        {
            object keyVal = null;
            List<KeyValuePair<string, List<XmlAttributeCollection>>> oList = new List<KeyValuePair<string, List<XmlAttributeCollection>>>();
            try
            {
                string val = "";
                List<XmlAttributeCollection> attr = new List<XmlAttributeCollection>();
                if (parentElement.Name.Trim().ToLower() == "span" && !parentElement.InnerXml.Contains("<img"))
                {
                    attr.Add(parentElement.Attributes);
                }
                else
                    if (parentElement.Name.Trim().ToLower() == "a")
                    {
                        attr.Add(parentElement.Attributes);
                    }
                    else
                        if (parentElement.Name.Trim().ToLower() == "img" || (parentElement.Name.Trim().ToLower() == "span" && parentElement.InnerXml.Contains("<img")))
                        {
                            string sVal = "";
                            string sImgStr2 = "";
                            //object brk = "break";
                            // attr.Add((XmlAttributeCollection)brk);
                            if ((parentElement.Name.Trim().ToLower() == "span" && parentElement.InnerXml.Contains("<img")))
                            {
                                if (parentElement.InnerXml.Contains("<img"))
                                {
                                    /*creating proper img tag, i.e. adding < /> if it doesnot exist*/
                                    Regex reImg_1 = new Regex(@"<img\s[^>]*>", RegexOptions.IgnoreCase);
                                    MatchCollection mc = reImg_1.Matches(parentElement.InnerXml);

                                    foreach (Match mImg in mc)
                                    {
                                        string sImgStr = mImg.Value;
                                        if (!string.IsNullOrEmpty(sImgStr))
                                        {
                                            int lidx = sImgStr.LastIndexOf(">");
                                            //if (sImgStr.ElementAt(lidx - 1) != '/')
                                            //{

                                            //}
                                            char[] iStr = sImgStr.ToCharArray();
                                            char flagSlash = 'N';
                                            for (int i = iStr.Length - 1; i > 0; i--)
                                            {
                                                if ((iStr[i] == '"' || iStr[i] == '\''))
                                                {
                                                    break;
                                                }
                                                if (iStr[i] == '/')
                                                {
                                                    flagSlash = 'Y';
                                                    break;
                                                }
                                            }
                                            if (flagSlash == 'N')
                                            {
                                                sImgStr2 += sImgStr.Remove(sImgStr.Length - 1, 1) + "/> \n";/*replacing last character with />*/
                                            }
                                            else
                                            {
                                                sImgStr2 += sImgStr + "\n";
                                            }
                                        }
                                    }
                                }
                                sVal = sImgStr2;
                            }
                            else
                            {
                                sVal = parentElement.OuterXml;
                            }
                            if ((sVal).Contains("<img") && (!(sVal).Contains("width") && !(sVal).Contains("height")))
                            {
                                sVal = sVal.Replace("<img", "<img width=\"400\" height=\"350\" ");
                            }


                            //string src = parentElement.Attributes["src"].Value.Replace("data:image/png;base64,", "");
                            string src = sVal.Replace("data:image/png;base64,", "");

                            int maxHeight = 0; int maxWidth = 0; string imgType = string.Empty; string imgBase64String = string.Empty;

                            Regex reImg = new Regex(@"<img\s[^>]*>", RegexOptions.IgnoreCase);
                            Regex reHeight = new Regex(@"height=(?:(['""])(?<height>(?:(?!\1).)*)\1|(?<height>[^\s>]+))", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                            Regex reWidth = new Regex(@"width=(?:(['""])(?<width>(?:(?!\1).)*)\1|(?<width>[^\s>]+))", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                            Regex reSrc = new Regex(@"src=(?:(['""])(?<src>(?:(?!\1).)*)\1|(?<src>[^\s>]+))", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                            Regex reStyle = new Regex(@"style=\s*([^""]*)", RegexOptions.IgnoreCase | RegexOptions.Singleline);

                            if (parentElement.Attributes["style"] != null)
                            {
                                //string myval = parentElement.Attributes["style"].Value;
                                string myval = sVal;
                                string sWidth = "";
                                string sHeight = "";
                                if (!string.IsNullOrEmpty(myval))
                                {
                                    string[] arr;
                                    if (myval.Contains(";"))
                                    {
                                        arr = myval.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                                        foreach (var item in arr)
                                        {
                                            if (item.Contains("width"))
                                            {
                                                sWidth = item.Split(new char[] { ':' })[1].Replace("px", "");
                                            }
                                            if (item.Contains("height"))
                                            {
                                                sHeight = item.Split(new char[] { ':' })[1].Replace("px", "");
                                            }

                                            if (item.Contains("base64,"))
                                            {
                                                src = item.Replace("base64,", "").Replace("\" />", "").Replace("\"/>", "").Replace("\n", "");
                                            }
                                        }
                                        maxWidth = Convert.ToInt16(sWidth);
                                        maxWidth = Convert.ToInt16(Math.Round(maxWidth * (72f / 96f)));//pixel to point
                                        maxHeight = Convert.ToInt16(sHeight);
                                        maxHeight = Convert.ToInt16(Math.Round(maxHeight * (72f / 96f)));//pixel to point
                                    }
                                }
                                imgType = "png";
                                imgBase64String = src;

                            }
                            else
                            {
                                MatchCollection mc = reImg.Matches(sVal);
                                foreach (Match mImg in mc)
                                {
                                    int flag = 0;
                                    //Console.WriteLine("    img tag: {0}", mImg.Groups[0].Value);
                                    if (reHeight.IsMatch(mImg.Groups[0].Value))
                                    {
                                        flag = 1;
                                        Match mHeight = reHeight.Match(mImg.Groups[0].Value);
                                        maxHeight = Convert.ToInt16(mHeight.Groups["height"].Value);
                                        maxHeight = Convert.ToInt16(Math.Round(maxHeight * (72f / 96f)));//pixel to point
                                    }
                                    if (reWidth.IsMatch(mImg.Groups[0].Value))
                                    {
                                        flag = 1;
                                        Match mWidth = reWidth.Match(mImg.Groups[0].Value);
                                        maxWidth = Convert.ToInt16(mWidth.Groups["width"].Value);
                                        maxWidth = Convert.ToInt16(Math.Round(maxWidth * (72f / 96f)));//pixel to point
                                    }
                                    if (reHeight.IsMatch(mImg.Groups[0].Value))
                                    {
                                        flag = 1;
                                        Match mSrc = reSrc.Match(mImg.Groups[0].Value);
                                        string imgSrc = Convert.ToString(mSrc.Groups["src"].Value);
                                        int indexStart = imgSrc.IndexOf('/');
                                        int length = imgSrc.IndexOf(';') - imgSrc.IndexOf('/');
                                        imgType = imgSrc.Substring(indexStart + 1, length - 1);
                                        int index1 = imgSrc.IndexOf(',');
                                        imgBase64String = "\n" + imgSrc.Substring(index1 + 1);
                                    }
                                    if (flag == 0)
                                    {
                                        string myval = sVal;
                                        string sWidth = "";
                                        string sHeight = "";
                                        if (!string.IsNullOrEmpty(myval))
                                        {
                                            string[] arr;
                                            if (myval.Contains(";"))
                                            {
                                                arr = myval.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                                                foreach (var item in arr)
                                                {
                                                    if (item.Contains("width"))
                                                    {
                                                        sWidth = item.Split(new char[] { ':' })[1].Replace("px", "");
                                                    }
                                                    if (item.Contains("height"))
                                                    {
                                                        sHeight = item.Split(new char[] { ':' })[1].Replace("px", "");
                                                    }
                                                    if (item.Contains("base64,"))
                                                    {
                                                        //Match mSrc = reSrc.Match(mImg.Groups[0].Value);
                                                        //string imgSrc = Convert.ToString(mSrc.Groups["src"].Value);
                                                        src = item.Replace("base64,", "").Replace("\" />", "").Replace("\"/>", "").Replace("\n", "");
                                                    }
                                                }
                                                maxWidth = Convert.ToInt16(sWidth);
                                                maxWidth = Convert.ToInt16(Math.Round(maxWidth * (72f / 96f)));//pixel to point
                                                maxHeight = Convert.ToInt16(sHeight);
                                                maxHeight = Convert.ToInt16(Math.Round(maxHeight * (72f / 96f)));//pixel to point
                                            }
                                        }
                                        imgType = "png";
                                        imgBase64String = src;
                                    }
                                }


                            }


                            ImageInfo objImg = new ImageInfo();

                            objImg.rowNumber = rowIndex;
                            //objImg.cellNumber = Convert.ToInt16(celldata.cellName.GetTypeCode());
                            objImg.cellNumber = Convert.ToInt16(celldata.cellName);
                            objImg.imgBase64 = imgBase64String;
                            objImg.imgHeight = maxHeight;
                            objImg.imgWidth = maxWidth;
                            objImg.imgType = imgType;
                            listImageInfo.Add(objImg);

                            //adjust excel row and column as per inserted image
                            rowNew.Height = Convert.ToDouble(maxHeight);
                            rowNew.CustomHeight = true;

                            Column col1 = columns1.ChildElements[Convert.ToInt16(celldata.cellName) - 1] as Column;
                            //calculate column width as per img width 
                            double cusWidth = (((double)maxWidth / (double)7 * 256) - (128 / 7)) / 256;
                            if (col1.Width < Convert.ToDouble(cusWidth))
                            {
                                col1.Width = Convert.ToDouble(cusWidth);
                            }
                            //unlock cell style//added
                            if (celldata.isDataValidate)
                                cell.StyleIndex = (UInt32Value)10U;
                            //unlock cell style
                            cell.StyleIndex = (UInt32Value)10U;

                        }
                        /* else
                         if (parentElement.Name.Trim().ToLower() == "a")
                         {
                             // Hyperlink h = new Hyperlink();
                             //h.Location = property;
                             //h.Display = property;
                            
                             //rp.Append(h);

                             Uri u = new Uri("www.google.co.in");
                            
                         }*/
                        else
                        {

                            if ("B,I,U,UL".Contains(parentElement.Name.Trim().ToUpper()))
                            {
                                //XmlAttribute newAttr = doc.CreateAttribute("style");
                                XmlAttribute newAttr = doc.CreateAttribute("style", "");
                                switch (parentElement.Name.Trim().ToUpper())
                                {

                                    case "B": newAttr.Value = "BOLD"; break;
                                    case "I": newAttr.Value = "ITALICS"; break;
                                    case "U": newAttr.Value = "UNDERLINE"; break;
                                    case "UL": newAttr.Value = "UL"; break;
                                    //case "A": newAttr.Value = "ANCHOR"; break;
                                    //default: newAttr.Value = string.Empty;
                                    //    break;
                                }

                                parentElement.Attributes.Append(newAttr);
                                attr.Add(parentElement.Attributes);
                                //xc.Append(newAttr);
                            }
                        }

                string name = parentElement.Name;
                /*//temp comment 26 march
                if (parentElement.InnerXml.Contains("mso-list:"))
                {
                    int ChildCount = parentElement.ChildNodes.Count;
                    if (ChildCount > 0)
                    {
                        XmlNodeList elementList = parentElement.ChildNodes;
                        //val = childEleBullets(elementList, ref attr);
                        oList.AddRange(childEleBullets(elementList, ref attr, ref doc));
                    }
                    else
                    {
                        val = parentElement.InnerText;
                        if (val.Trim() == "") { val = "_~#*~"; }
                        KeyValuePair<string, List<XmlAttributeCollection>> kv = new KeyValuePair<string, List<XmlAttributeCollection>>(val, attr);
                        oList.Add(kv);
                    }
                }
                else*/
                {
                    int ChildCount = parentElement.ChildNodes.Count;
                    //XmlElement[] elementList = new XmlElement[ChildCount];
                    if (ChildCount > 0)
                    {
                        XmlNodeList elementList = parentElement.ChildNodes;
                        //val = childEle(elementList, ref attr);
                        if (attr != null && attr.Count == 0 && parentElement.Attributes != null && parentElement.Attributes.Count > 0)
                        {
                            attr.Add(parentElement.Attributes);
                            //parentElement.Attributes
                        }
                        oList.AddRange(childEle(elementList, ref attr, ref doc));
                    }
                    else
                    {
                        val = parentElement.InnerText;
                        if (val.Trim() == "") { val = "_~#*~"; }
                        if (/*"§,·,•".Contains(val) && !"\n".Contains(val)*/ val.Contains("•") || val.Contains("·") || val.Contains("§")) { val = "\n" + val.Replace("·", "   ● ").Replace("§", "   ● ").Replace("•", "   ● "); }
                        KeyValuePair<string, List<XmlAttributeCollection>> kv = new KeyValuePair<string, List<XmlAttributeCollection>>(val, attr);
                        keyVal = kv;
                        oList.Add(kv);
                    }
                }

            }
            catch (Exception ex)
            {
                return null;
            }
            return oList;
        }


        /*private static string childEle(XmlNodeList NodeList, ref List<XmlAttributeCollection> attr)
        {
            string val = "";
            
            string innerVal = "";
            try
            {
                for (int i = 0; i < NodeList.Count; i++)
                {
                    if (NodeList[i].Attributes != null)
                        attr.Add(NodeList[i].Attributes);
                    if (i == NodeList.Count - 1)
                    {
                        if (NodeList[i].Name == "li")
                        {
                            val = "\n" + "   ● " + NodeList[i].InnerText;
                        }
                        else
                        {
                            val = NodeList[i].InnerText;
                        }
                    }
                    if (NodeList[i].HasChildNodes)
                    {
                        XmlNodeList NodeList_child = NodeList[i].ChildNodes;
                        //string rVal = "";
                        childEle(NodeList_child, ref attr);
                    }
                    //if (NodeList[i].HasChildNodes)
                    //{
                    //    XmlNodeList NodeList_child = NodeList[i].ChildNodes;

                    //    foreach (XmlNode item in NodeList_child)
                    //    {
                    //        if (!item.InnerXml.Contains("<"))
                    //        {
                    //            val = item.InnerText;
                    //        }
                    //        if (item.Attributes != null)
                    //        {
                    //            attr.Add(item.Attributes);
                    //        }
                    //    }
                    //}
                }
            }
            catch (Exception ex)
            {

            }
            return val;
        }*/

        private static List<KeyValuePair<string, List<XmlAttributeCollection>>> childEle(XmlNodeList NodeList, ref List<XmlAttributeCollection> attr, ref XmlDocument doc)
        {
            List<KeyValuePair<string, List<XmlAttributeCollection>>> lstKeyValue = new List<KeyValuePair<string, List<XmlAttributeCollection>>>();
            List<XmlAttributeCollection> newAttr = new List<XmlAttributeCollection>();
            string val = "";
            List<XmlAttributeCollection> parentColl = new List<XmlAttributeCollection>();
            try
            {
                for (int i = 0; i < NodeList.Count; i++)
                {
                    if (NodeList[i].HasChildNodes)
                    {
                        newAttr = new List<XmlAttributeCollection>();
                        if (attr != null && attr.Count > 0)
                        {
                            newAttr.AddRange(attr);
                            attr.RemoveRange(0, attr.Count);/*remove attributes once applied */
                        }
                        if (NodeList[i].Attributes != null)
                        {
                            newAttr.Add(NodeList[i].Attributes);
                        }

                        /**/
                        if ("B,I,U,UL".Contains(NodeList[i].Name.Trim().ToUpper()))
                        {
                            XmlAttribute Attr = doc.CreateAttribute("style", "");
                            switch (NodeList[i].Name.Trim().ToUpper())
                            {

                                case "B": Attr.Value = "BOLD"; break;
                                case "I": Attr.Value = "ITALICS"; break;
                                case "U": Attr.Value = "UNDERLINE"; break;
                                case "UL": Attr.Value = "UL"; break;
                                //case "A": newAttr.Value = "ANCHOR"; break;
                                //default: newAttr.Value = string.Empty;
                                //    break;
                            }

                            NodeList[i].Attributes.Append(Attr);
                            newAttr.Add(NodeList[i].Attributes);
                        }
                        /**/



                        XmlNodeList NodeList_child = NodeList[i].ChildNodes;

                        foreach (XmlNode item in NodeList_child)
                        {
                            if (!item.InnerXml.Contains("<"))
                            {
                                if (item.ParentNode.Name.ToLower() == "li")
                                {
                                    val = "\n" + "   ● " + item.InnerText;
                                }
                                else
                                {
                                    val = item.InnerText;
                                }
                                if (val.Trim() == "") { val = "_~#*~"; }


                                if (parentColl.Count > 0)
                                {
                                    /* attr.AddRange(parentColl);
                                     attr.Add(item.Attributes);
                                     parentColl.RemoveRange(0, parentColl.Count);*/
                                    newAttr.Add(item.Attributes);
                                }
                                else
                                {
                                    if (item.Attributes != null)
                                    {
                                        //attr.Add(item.Attributes);
                                        newAttr.Add(item.Attributes);
                                    }
                                }

                                if ("§,·".Contains(val) && !"\n".Contains(val)) { val = "\n" + val.Replace("·", "   ● ").Replace("§", "   ● "); }
                                KeyValuePair<string, List<XmlAttributeCollection>> oKeyValue = new KeyValuePair<string, List<XmlAttributeCollection>>(val/* + " "*/, newAttr);
                                lstKeyValue.Add(oKeyValue);
                                newAttr = new List<XmlAttributeCollection>();
                            }
                            else
                            {
                                //parentColl.Add(item.Attributes);
                                if (item.HasChildNodes)
                                {
                                    int cnt1 = item.ChildNodes.Count;
                                    XmlNodeList child_list = item.ChildNodes;
                                    if (item.Attributes != null)
                                    {
                                        /**/
                                        if ("B,I,U,UL".Contains(item.Name.Trim().ToUpper()))
                                        {
                                            XmlAttribute Attr = doc.CreateAttribute("style", "");
                                            switch (item.Name.Trim().ToUpper())
                                            {
                                                case "B": Attr.Value = "BOLD"; break;
                                                case "I": Attr.Value = "ITALICS"; break;
                                                case "U": Attr.Value = "UNDERLINE"; break;
                                                case "UL": Attr.Value = "UL"; break;
                                                //case "A": newAttr.Value = "ANCHOR"; break;
                                                //default: newAttr.Value = string.Empty;
                                                //    break;
                                            }
                                            item.Attributes.Append(Attr);
                                            newAttr.Add(item.Attributes);
                                        }
                                        else
                                        {
                                            /**/
                                            //attr.Add(item.Attributes);
                                            newAttr.Add(item.Attributes);
                                        }
                                    }
                                    for (int i2 = 0; i2 < child_list.Count; i2++)
                                    {
                                        if (child_list[i2].Attributes != null)
                                        {
                                            /**/
                                            if (i2 != 0 && item.Attributes != null && item.Attributes.Count > 0)
                                            {
                                                newAttr.Add(item.Attributes);
                                            }
                                            if ("B,I,U,UL".Contains(child_list[i2].Name.Trim().ToUpper()))
                                            {
                                                XmlAttribute Attr = doc.CreateAttribute("style", "");
                                                switch (child_list[i2].Name.Trim().ToUpper())
                                                {
                                                    case "B": Attr.Value = "BOLD"; break;
                                                    case "I": Attr.Value = "ITALICS"; break;
                                                    case "U": Attr.Value = "UNDERLINE"; break;
                                                    case "UL": Attr.Value = "UL"; break;
                                                    //case "A": newAttr.Value = "ANCHOR"; break;
                                                    //default: newAttr.Value = string.Empty;
                                                    //    break;
                                                }

                                                child_list[i2].Attributes.Append(Attr);
                                                newAttr.Add(child_list[i2].Attributes);
                                            }
                                            else
                                            {
                                                /**/
                                                //attr.Add(item.Attributes);
                                                newAttr.Add(child_list[i2].Attributes);
                                            }
                                        }
                                        if (child_list[i2].HasChildNodes)
                                        {
                                            int cnt = child_list[i2].ChildNodes.Count;

                                            if (cnt > 0)
                                            {
                                                XmlNodeList i2ChildList = child_list[i2].ChildNodes;
                                                // newAttr = new List<XmlAttributeCollection>();
                                                #region comm1

                                                //for (int i3 = 0; i3 < cnt; i3++)
                                                //{
                                                //    if (innerChildList[i3].NextSibling != null)
                                                //    {
                                                //        if ("B,I,U,UL".Contains(innerChildList[i3].Name.ToUpper()))
                                                //        {
                                                //            XmlAttribute Attr = doc.CreateAttribute("style", "");
                                                //            switch (innerChildList[i3].Name.Trim().ToUpper())
                                                //            {
                                                //                case "B": Attr.Value = "BOLD"; break;
                                                //                case "I": Attr.Value = "ITALICS"; break;
                                                //                case "U": Attr.Value = "UNDERLINE"; break;
                                                //                case "UL": Attr.Value = "UL"; break;
                                                //                //case "A": newAttr.Value = "ANCHOR"; break;
                                                //                //default: newAttr.Value = string.Empty;
                                                //                //    break;
                                                //            }

                                                //            innerChildList[i3].Attributes.Append(Attr);
                                                //            newAttr.Add(innerChildList[i3].Attributes);
                                                //        }
                                                //        /**/
                                                //        if (i3 != 0 && child_list[i2].Attributes != null && child_list[i2].Attributes.Count > 0)
                                                //        {
                                                //            newAttr.Add(child_list[i2].Attributes);
                                                //        }
                                                //        /**/
                                                //        if (innerChildList[i3].NextSibling.InnerXml != innerChildList[i3].InnerXml)
                                                //        {
                                                //            val = innerChildList[i3].InnerText;
                                                //            if (val.Trim() == "") { val = "_~#*~"; }
                                                //            if (innerChildList[i3].Attributes != null)
                                                //            {
                                                //                //attr.Add(innerChildList[i3].Attributes);
                                                //                newAttr.Add(innerChildList[i3].Attributes);
                                                //            }
                                                //            KeyValuePair<string, List<XmlAttributeCollection>> oKeyValue = new KeyValuePair<string, List<XmlAttributeCollection>>(val + " ", newAttr);
                                                //            lstKeyValue.Add(oKeyValue);
                                                //            newAttr = new List<XmlAttributeCollection>();
                                                //        }
                                                //        else
                                                //        {
                                                //            continue;
                                                //        }
                                                //    }
                                                //    else
                                                //    {
                                                //        if ("B,I,U,UL".Contains(innerChildList[i3].Name.ToUpper()))
                                                //        {
                                                //            XmlAttribute Attr = doc.CreateAttribute("style", "");
                                                //            switch (innerChildList[i3].Name.Trim().ToUpper())
                                                //            {
                                                //                case "B": Attr.Value = "BOLD"; break;
                                                //                case "I": Attr.Value = "ITALICS"; break;
                                                //                case "U": Attr.Value = "UNDERLINE"; break;
                                                //                case "UL": Attr.Value = "UL"; break;
                                                //                //case "A": newAttr.Value = "ANCHOR"; break;
                                                //                //default: newAttr.Value = string.Empty;
                                                //                //    break;
                                                //            }

                                                //            innerChildList[i3].Attributes.Append(Attr);
                                                //            newAttr.Add(innerChildList[i3].Attributes);
                                                //        }
                                                //        else
                                                //        {

                                                //        }
                                                //        /**/
                                                //        if (i3 != 0 && child_list[i2].Attributes != null && child_list[i2].Attributes.Count > 0)
                                                //        {
                                                //            newAttr.Add(child_list[i2].Attributes);
                                                //        }
                                                //        /**/
                                                //        val = innerChildList[i3].InnerText;
                                                //        if (val.Trim() == "") { val = "_~#*~"; }
                                                //        if (innerChildList[i3].Attributes != null)
                                                //        {
                                                //            //attr.Add(innerChildList[i3].Attributes);
                                                //            newAttr.Add(innerChildList[i3].Attributes);
                                                //        }
                                                //        KeyValuePair<string, List<XmlAttributeCollection>> oKeyValue = new KeyValuePair<string, List<XmlAttributeCollection>>(val + " ", newAttr);
                                                //        lstKeyValue.Add(oKeyValue);
                                                //        newAttr = new List<XmlAttributeCollection>();
                                                //    }
                                                //}
                                                #endregion
                                                for (int i3 = 0; i3 < i2ChildList.Count; i3++)
                                                {
                                                    if (i2ChildList[i3].Attributes != null)
                                                    {
                                                        /**/
                                                        if (i3 != 0 && child_list[i2].Attributes != null && child_list[i2].Attributes.Count > 0)
                                                        {
                                                            newAttr.Add(child_list[i2].Attributes);
                                                        }
                                                        if ("B,I,U,UL".Contains(i2ChildList[i3].Name.Trim().ToUpper()))
                                                        {
                                                            XmlAttribute Attr = doc.CreateAttribute("style", "");
                                                            switch (i2ChildList[i3].Name.Trim().ToUpper())
                                                            {
                                                                case "B": Attr.Value = "BOLD"; break;
                                                                case "I": Attr.Value = "ITALICS"; break;
                                                                case "U": Attr.Value = "UNDERLINE"; break;
                                                                case "UL": Attr.Value = "UL"; break;
                                                                //case "A": newAttr.Value = "ANCHOR"; break;
                                                                //default: newAttr.Value = string.Empty;
                                                                //    break;
                                                            }

                                                            i2ChildList[i3].Attributes.Append(Attr);
                                                            newAttr.Add(i2ChildList[i3].Attributes);
                                                        }
                                                        else
                                                        {
                                                            /**/
                                                            //attr.Add(item.Attributes);
                                                            newAttr.Add(i2ChildList[i3].Attributes);
                                                        }
                                                        #region oldAttr
                                                        if (child_list[i2].Attributes != null && child_list[i2].Attributes.Count > 0)/*new in else*/
                                                        {
                                                            newAttr.Add(child_list[i2].Attributes);
                                                        }
                                                        if (item.Attributes != null && item.Attributes.Count > 0)/*new in else*/
                                                        {
                                                            newAttr.Add(item.Attributes);
                                                        }
                                                        #endregion
                                                        if (i2ChildList[i3].HasChildNodes)
                                                        {
                                                            int childI3 = i2ChildList[i3].ChildNodes.Count;
                                                            XmlNodeList i3ChildList = i2ChildList[i3].ChildNodes;
                                                            for (int i4 = 0; i4 < i3ChildList.Count; i4++)
                                                            {
                                                                if (i3ChildList[i4].Attributes != null)
                                                                {
                                                                    if (i4 != 0 && i2ChildList[i3].Attributes != null && i2ChildList[i3].Attributes.Count > 0)
                                                                    {
                                                                        newAttr.Add(i2ChildList[i3].Attributes);
                                                                    }
                                                                    if ("B,I,U,UL".Contains(i3ChildList[i4].Name.Trim().ToUpper()))
                                                                    {
                                                                        XmlAttribute Attr = doc.CreateAttribute("style", "");
                                                                        switch (i3ChildList[i4].Name.Trim().ToUpper())
                                                                        {
                                                                            case "B": Attr.Value = "BOLD"; break;
                                                                            case "I": Attr.Value = "ITALICS"; break;
                                                                            case "U": Attr.Value = "UNDERLINE"; break;
                                                                            case "UL": Attr.Value = "UL"; break;
                                                                        }
                                                                        i3ChildList[i4].Attributes.Append(Attr);
                                                                        newAttr.Add(i3ChildList[i4].Attributes);
                                                                    }
                                                                    else
                                                                    {
                                                                        newAttr.Add(i3ChildList[i4].Attributes);
                                                                    }
                                                                    #region oldAttr
                                                                    if (i2ChildList[i3].Attributes != null && i2ChildList[i3].Attributes.Count > 0)/*new in else*/
                                                                    {
                                                                        newAttr.Add(i2ChildList[i3].Attributes);
                                                                    }
                                                                    if (child_list[i2].Attributes != null && child_list[i2].Attributes.Count > 0)/*new in else*/
                                                                    {
                                                                        newAttr.Add(child_list[i2].Attributes);
                                                                    }
                                                                    if (item.Attributes != null && item.Attributes.Count > 0)/*new in else*/
                                                                    {
                                                                        newAttr.Add(item.Attributes);
                                                                    }
                                                                    #endregion
                                                                    if (i3ChildList[i4].HasChildNodes)
                                                                    {
                                                                        XmlNodeList i4ChildList = i3ChildList[i4].ChildNodes;
                                                                        for (int i5 = 0; i5 < i4ChildList.Count; i5++)
                                                                        {
                                                                            if (i4ChildList[i5].Attributes != null)
                                                                            {
                                                                                if (i5 != 0 && i3ChildList[i4].Attributes != null && i3ChildList[i4].Attributes.Count > 0)
                                                                                {
                                                                                    newAttr.Add(i3ChildList[i4].Attributes);
                                                                                }
                                                                                if ("B,I,U,UL".Contains(i4ChildList[i5].Name.Trim().ToUpper()))
                                                                                {
                                                                                    XmlAttribute Attr = doc.CreateAttribute("style", "");
                                                                                    switch (i4ChildList[i5].Name.Trim().ToUpper())
                                                                                    {
                                                                                        case "B": Attr.Value = "BOLD"; break;
                                                                                        case "I": Attr.Value = "ITALICS"; break;
                                                                                        case "U": Attr.Value = "UNDERLINE"; break;
                                                                                        case "UL": Attr.Value = "UL"; break;
                                                                                    }
                                                                                    i4ChildList[i5].Attributes.Append(Attr);
                                                                                    newAttr.Add(i4ChildList[i5].Attributes);
                                                                                }
                                                                                else
                                                                                {
                                                                                    newAttr.Add(i4ChildList[i5].Attributes);
                                                                                }
                                                                                #region oldAttr
                                                                                if (i3ChildList[i4].Attributes != null && i3ChildList[i4].Attributes.Count > 0)/*new in else*/
                                                                                {
                                                                                    newAttr.Add(i3ChildList[i4].Attributes);
                                                                                }
                                                                                if (i2ChildList[i3].Attributes != null && i2ChildList[i3].Attributes.Count > 0)/*new in else*/
                                                                                {
                                                                                    newAttr.Add(i2ChildList[i3].Attributes);
                                                                                }
                                                                                if (child_list[i2].Attributes != null && child_list[i2].Attributes.Count > 0)/*new in else*/
                                                                                {
                                                                                    newAttr.Add(child_list[i2].Attributes);
                                                                                }
                                                                                if (item.Attributes != null && item.Attributes.Count > 0)/*new in else*/
                                                                                {
                                                                                    newAttr.Add(item.Attributes);
                                                                                }
                                                                                #endregion
                                                                                if (i4ChildList[i5].HasChildNodes)
                                                                                {
                                                                                    XmlNodeList i5ChildList = i4ChildList[i5].ChildNodes;
                                                                                    for (int i6 = 0; i6 < i5ChildList.Count; i6++)
                                                                                    {
                                                                                        if (i5ChildList[i6].Attributes != null)
                                                                                        {
                                                                                            if (i6 != 0 && i4ChildList[i5].Attributes != null && i4ChildList[i5].Attributes.Count > 0)
                                                                                            {
                                                                                                newAttr.Add(i4ChildList[i4].Attributes);
                                                                                            }

                                                                                            if ("B,I,U,UL".Contains(i5ChildList[i6].Name.Trim().ToUpper()))
                                                                                            {
                                                                                                XmlAttribute Attr = doc.CreateAttribute("style", "");
                                                                                                switch (i5ChildList[i6].Name.Trim().ToUpper())
                                                                                                {
                                                                                                    case "B": Attr.Value = "BOLD"; break;
                                                                                                    case "I": Attr.Value = "ITALICS"; break;
                                                                                                    case "U": Attr.Value = "UNDERLINE"; break;
                                                                                                    case "UL": Attr.Value = "UL"; break;
                                                                                                }
                                                                                                i5ChildList[i6].Attributes.Append(Attr);
                                                                                                newAttr.Add(i5ChildList[i6].Attributes);
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                newAttr.Add(i5ChildList[i6].Attributes);
                                                                                            }
                                                                                            #region oldAttr
                                                                                            if (i3ChildList[i4].Attributes != null && i3ChildList[i4].Attributes.Count > 0)/*new in else*/
                                                                                            {
                                                                                                newAttr.Add(i3ChildList[i4].Attributes);
                                                                                            }
                                                                                            if (i2ChildList[i3].Attributes != null && i2ChildList[i3].Attributes.Count > 0)/*new in else*/
                                                                                            {
                                                                                                newAttr.Add(i2ChildList[i3].Attributes);
                                                                                            }
                                                                                            if (child_list[i2].Attributes != null && child_list[i2].Attributes.Count > 0)/*new in else*/
                                                                                            {
                                                                                                newAttr.Add(child_list[i2].Attributes);
                                                                                            }
                                                                                            if (item.Attributes != null && item.Attributes.Count > 0)/*new in else*/
                                                                                            {
                                                                                                newAttr.Add(item.Attributes);
                                                                                            }
                                                                                            #endregion
                                                                                            if (i5ChildList[i6].ChildNodes != null)
                                                                                            {

                                                                                                XmlNodeList i6ChildList = i5ChildList[i6].ChildNodes;
                                                                                                for (int i7 = 0; i7 < i6ChildList.Count; i7++)
                                                                                                {

                                                                                                    if (i6ChildList[i7].NextSibling != null)
                                                                                                    {
                                                                                                        if ("B,I,U,UL".Contains(i6ChildList[i7].Name.ToUpper()))
                                                                                                        {
                                                                                                            XmlAttribute Attr = doc.CreateAttribute("style", "");
                                                                                                            switch (i6ChildList[i7].Name.Trim().ToUpper())
                                                                                                            {
                                                                                                                case "B": Attr.Value = "BOLD"; break;
                                                                                                                case "I": Attr.Value = "ITALICS"; break;
                                                                                                                case "U": Attr.Value = "UNDERLINE"; break;
                                                                                                                case "UL": Attr.Value = "UL"; break;
                                                                                                                //case "A": newAttr.Value = "ANCHOR"; break;
                                                                                                                //default: newAttr.Value = string.Empty;
                                                                                                                //    break;
                                                                                                            }

                                                                                                            i6ChildList[i7].Attributes.Append(Attr);
                                                                                                            newAttr.Add(i6ChildList[i7].Attributes);
                                                                                                        }
                                                                                                        /**/
                                                                                                        if (i7 != 0 && i5ChildList[i6].Attributes != null && i5ChildList[i6].Attributes.Count > 0)
                                                                                                        {
                                                                                                            newAttr.Add(i5ChildList[i6].Attributes);
                                                                                                        }
                                                                                                        /**/
                                                                                                        if (i6ChildList[i7].NextSibling.InnerXml != i6ChildList[i7].InnerXml)
                                                                                                        {
                                                                                                            val = i6ChildList[i7].InnerText;
                                                                                                            if (val.Trim() == "") { val = "_~#*~"; }
                                                                                                            if (i6ChildList[i7].Attributes != null)
                                                                                                            {
                                                                                                                //attr.Add(innerChildList[i3].Attributes);
                                                                                                                newAttr.Add(i6ChildList[i7].Attributes);
                                                                                                            }
                                                                                                            #region oldAttr
                                                                                                            if (i4ChildList[i5].Attributes != null && i4ChildList[i5].Attributes.Count > 0)/*new in else*/
                                                                                                            {
                                                                                                                newAttr.Add(i4ChildList[i5].Attributes);
                                                                                                            }
                                                                                                            if (i3ChildList[i4].Attributes != null && i3ChildList[i4].Attributes.Count > 0)/*new in else*/
                                                                                                            {
                                                                                                                newAttr.Add(i3ChildList[i4].Attributes);
                                                                                                            }
                                                                                                            if (i2ChildList[i3].Attributes != null && i2ChildList[i3].Attributes.Count > 0)/*new in else*/
                                                                                                            {
                                                                                                                newAttr.Add(i2ChildList[i3].Attributes);
                                                                                                            }
                                                                                                            if (child_list[i2].Attributes != null && child_list[i2].Attributes.Count > 0)/*new in else*/
                                                                                                            {
                                                                                                                newAttr.Add(child_list[i2].Attributes);
                                                                                                            }
                                                                                                            if (item.Attributes != null && item.Attributes.Count > 0)/*new in else*/
                                                                                                            {
                                                                                                                newAttr.Add(item.Attributes);
                                                                                                            }
                                                                                                            #endregion
                                                                                                            if ("§,·".Contains(val)) { val = "\n" + val.Replace("·", "   ● ").Replace("§", "   ● "); }
                                                                                                            KeyValuePair<string, List<XmlAttributeCollection>> oKeyValue = new KeyValuePair<string, List<XmlAttributeCollection>>(val /* + " "*/, newAttr);
                                                                                                            lstKeyValue.Add(oKeyValue);
                                                                                                            newAttr = new List<XmlAttributeCollection>();
                                                                                                        }
                                                                                                        else
                                                                                                        {
                                                                                                            continue;
                                                                                                        }
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        if ("B,I,U,UL".Contains(i6ChildList[i7].Name.ToUpper()))
                                                                                                        {
                                                                                                            XmlAttribute Attr = doc.CreateAttribute("style", "");
                                                                                                            switch (i6ChildList[i7].Name.Trim().ToUpper())
                                                                                                            {
                                                                                                                case "B": Attr.Value = "BOLD"; break;
                                                                                                                case "I": Attr.Value = "ITALICS"; break;
                                                                                                                case "U": Attr.Value = "UNDERLINE"; break;
                                                                                                                case "UL": Attr.Value = "UL"; break;
                                                                                                                //case "A": newAttr.Value = "ANCHOR"; break;
                                                                                                                //default: newAttr.Value = string.Empty;
                                                                                                                //    break;
                                                                                                            }

                                                                                                            i6ChildList[i7].Attributes.Append(Attr);
                                                                                                            newAttr.Add(i6ChildList[i7].Attributes);
                                                                                                        }
                                                                                                        else
                                                                                                        {

                                                                                                        }
                                                                                                        /**/
                                                                                                        if (i7 != 0 && i5ChildList[i6].Attributes != null && i5ChildList[i6].Attributes.Count > 0)
                                                                                                        {
                                                                                                            newAttr.Add(i5ChildList[i6].Attributes);
                                                                                                        }
                                                                                                        /**/
                                                                                                        val = i6ChildList[i7].InnerText;
                                                                                                        if (val.Trim() == "") { val = "_~#*~"; }
                                                                                                        if (i5ChildList[i6].Attributes != null)/*new in else*/
                                                                                                        {
                                                                                                            newAttr.Add(i5ChildList[i6].Attributes);
                                                                                                        }
                                                                                                        if (i6ChildList[i7].Attributes != null)
                                                                                                        {
                                                                                                            //attr.Add(innerChildList[i3].Attributes);
                                                                                                            newAttr.Add(i6ChildList[i7].Attributes);
                                                                                                        }
                                                                                                        #region oldAttr
                                                                                                        if (i4ChildList[i5].Attributes != null && i4ChildList[i5].Attributes.Count > 0)/*new in else*/
                                                                                                        {
                                                                                                            newAttr.Add(i4ChildList[i5].Attributes);
                                                                                                        }
                                                                                                        if (i3ChildList[i4].Attributes != null && i3ChildList[i4].Attributes.Count > 0)/*new in else*/
                                                                                                        {
                                                                                                            newAttr.Add(i3ChildList[i4].Attributes);
                                                                                                        }
                                                                                                        if (i2ChildList[i3].Attributes != null && i2ChildList[i3].Attributes.Count > 0)/*new in else*/
                                                                                                        {
                                                                                                            newAttr.Add(i2ChildList[i3].Attributes);
                                                                                                        }
                                                                                                        if (child_list[i2].Attributes != null && child_list[i2].Attributes.Count > 0)/*new in else*/
                                                                                                        {
                                                                                                            newAttr.Add(child_list[i2].Attributes);
                                                                                                        }
                                                                                                        if (item.Attributes != null && item.Attributes.Count > 0)/*new in else*/
                                                                                                        {
                                                                                                            newAttr.Add(item.Attributes);
                                                                                                        }
                                                                                                        #endregion

                                                                                                        if ("§,·".Contains(val)) { val = "\n" + val.Replace("·", "   ● ").Replace("§", "   ● "); }
                                                                                                        KeyValuePair<string, List<XmlAttributeCollection>> oKeyValue = new KeyValuePair<string, List<XmlAttributeCollection>>(val /* + " "*/, newAttr);
                                                                                                        lstKeyValue.Add(oKeyValue);
                                                                                                        newAttr = new List<XmlAttributeCollection>();
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                            else
                                                                                            {

                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            val = i5ChildList[i6].InnerText;
                                                                                            if (val.Trim() == "") { val = "_~#*~"; }
                                                                                            if (i4ChildList[i5].Attributes != null)/*new in else*/
                                                                                            {
                                                                                                newAttr.Add(i4ChildList[i5].Attributes);
                                                                                            }
                                                                                            #region oldAttr
                                                                                            if (i3ChildList[i4].Attributes != null && i3ChildList[i4].Attributes.Count > 0)/*new in else*/
                                                                                            {
                                                                                                newAttr.Add(i3ChildList[i4].Attributes);
                                                                                            }
                                                                                            if (i2ChildList[i3].Attributes != null && i2ChildList[i3].Attributes.Count > 0)/*new in else*/
                                                                                            {
                                                                                                newAttr.Add(i2ChildList[i3].Attributes);
                                                                                            }
                                                                                            if (child_list[i2].Attributes != null && child_list[i2].Attributes.Count > 0)/*new in else*/
                                                                                            {
                                                                                                newAttr.Add(child_list[i2].Attributes);
                                                                                            }
                                                                                            if (item.Attributes != null && item.Attributes.Count > 0)/*new in else*/
                                                                                            {
                                                                                                newAttr.Add(item.Attributes);
                                                                                            }
                                                                                            #endregion
                                                                                            if (i5ChildList[i6].Attributes != null)
                                                                                            {
                                                                                                /**/
                                                                                                if ("B,I,U,UL".Contains(i5ChildList[i6].Name.Trim().ToUpper()))
                                                                                                {
                                                                                                    XmlAttribute Attr = doc.CreateAttribute("style", "");
                                                                                                    switch (i5ChildList[i6].Name.Trim().ToUpper())
                                                                                                    {
                                                                                                        case "B": Attr.Value = "BOLD"; break;
                                                                                                        case "I": Attr.Value = "ITALICS"; break;
                                                                                                        case "U": Attr.Value = "UNDERLINE"; break;
                                                                                                        case "UL": Attr.Value = "UL"; break;
                                                                                                    }
                                                                                                    i5ChildList[i6].Attributes.Append(Attr);
                                                                                                    newAttr.Add(i5ChildList[i6].Attributes);
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    newAttr.Add(i5ChildList[i6].Attributes);
                                                                                                }
                                                                                            }

                                                                                            if ("§,·".Contains(val)) { val = "\n" + val; }
                                                                                            KeyValuePair<string, List<XmlAttributeCollection>> oKeyValue = new KeyValuePair<string, List<XmlAttributeCollection>>(val /* + " "*/, newAttr);
                                                                                            lstKeyValue.Add(oKeyValue);
                                                                                            newAttr = new List<XmlAttributeCollection>();
                                                                                        }

                                                                                    }
                                                                                }


                                                                            }
                                                                            else
                                                                            {
                                                                                val = i4ChildList[i5].InnerText;
                                                                                if (val.Trim() == "") { val = "_~#*~"; }
                                                                                if (i3ChildList[i4].Attributes != null)/*new in else*/
                                                                                {
                                                                                    newAttr.Add(i3ChildList[i4].Attributes);
                                                                                }
                                                                                #region oldAttr
                                                                                if (i2ChildList[i3].Attributes != null && i2ChildList[i3].Attributes.Count > 0)/*new in else*/
                                                                                {
                                                                                    newAttr.Add(i2ChildList[i3].Attributes);
                                                                                }
                                                                                if (child_list[i2].Attributes != null && child_list[i2].Attributes.Count > 0)/*new in else*/
                                                                                {
                                                                                    newAttr.Add(child_list[i2].Attributes);
                                                                                }
                                                                                if (item.Attributes != null && item.Attributes.Count > 0)/*new in else*/
                                                                                {
                                                                                    newAttr.Add(item.Attributes);
                                                                                }
                                                                                #endregion
                                                                                if (i4ChildList[i5].Attributes != null)
                                                                                {
                                                                                    /**/
                                                                                    if ("B,I,U,UL".Contains(i4ChildList[i5].Name.Trim().ToUpper()))
                                                                                    {
                                                                                        XmlAttribute Attr = doc.CreateAttribute("style", "");
                                                                                        switch (i4ChildList[i5].Name.Trim().ToUpper())
                                                                                        {
                                                                                            case "B": Attr.Value = "BOLD"; break;
                                                                                            case "I": Attr.Value = "ITALICS"; break;
                                                                                            case "U": Attr.Value = "UNDERLINE"; break;
                                                                                            case "UL": Attr.Value = "UL"; break;
                                                                                        }
                                                                                        i4ChildList[i5].Attributes.Append(Attr);
                                                                                        newAttr.Add(i4ChildList[i5].Attributes);
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        newAttr.Add(i4ChildList[i5].Attributes);
                                                                                    }
                                                                                }

                                                                                if ("§,·".Contains(val)) { val = "\n" + val.Replace("·", "   ● ").Replace("§", "   ● "); }
                                                                                KeyValuePair<string, List<XmlAttributeCollection>> oKeyValue = new KeyValuePair<string, List<XmlAttributeCollection>>(val/* + " "*/, newAttr);
                                                                                lstKeyValue.Add(oKeyValue);
                                                                                newAttr = new List<XmlAttributeCollection>();
                                                                            }
                                                                        }
                                                                    }

                                                                }
                                                                else
                                                                {
                                                                    val = i3ChildList[i4].InnerText;
                                                                    if (val.Trim() == "") { val = "_~#*~"; }
                                                                    if (i2ChildList[i3].Attributes != null)/*new in else*/
                                                                    {
                                                                        newAttr.Add(i2ChildList[i3].Attributes);
                                                                    }
                                                                    #region oldAttr
                                                                    if (child_list[i2].Attributes != null && child_list[i2].Attributes.Count > 0)/*new in else*/
                                                                    {
                                                                        newAttr.Add(child_list[i2].Attributes);
                                                                    }
                                                                    if (item.Attributes != null && item.Attributes.Count > 0)/*new in else*/
                                                                    {
                                                                        newAttr.Add(item.Attributes);
                                                                    }
                                                                    #endregion
                                                                    if (i3ChildList[i4].Attributes != null)
                                                                    {
                                                                        /**/
                                                                        if ("B,I,U,UL".Contains(i3ChildList[i4].Name.Trim().ToUpper()))
                                                                        {
                                                                            XmlAttribute Attr = doc.CreateAttribute("style", "");
                                                                            switch (i3ChildList[i4].Name.Trim().ToUpper())
                                                                            {
                                                                                case "B": Attr.Value = "BOLD"; break;
                                                                                case "I": Attr.Value = "ITALICS"; break;
                                                                                case "U": Attr.Value = "UNDERLINE"; break;
                                                                                case "UL": Attr.Value = "UL"; break;
                                                                                //case "A": newAttr.Value = "ANCHOR"; break;
                                                                                //default: newAttr.Value = string.Empty;
                                                                                //    break;
                                                                            }
                                                                            i3ChildList[i4].Attributes.Append(Attr);
                                                                            newAttr.Add(i3ChildList[i4].Attributes);
                                                                        }
                                                                        else
                                                                        {
                                                                            /**/
                                                                            //attr.Add(child_list[i2].Attributes);
                                                                            newAttr.Add(i3ChildList[i4].Attributes);
                                                                        }
                                                                    }

                                                                    if ("§,·".Contains(val)) { val = "\n" + val.Replace("·", "   ● ").Replace("§", "   ● "); }
                                                                    KeyValuePair<string, List<XmlAttributeCollection>> oKeyValue = new KeyValuePair<string, List<XmlAttributeCollection>>(val /* + " "*/, newAttr);
                                                                    lstKeyValue.Add(oKeyValue);
                                                                    newAttr = new List<XmlAttributeCollection>();
                                                                }
                                                            }
                                                        }

                                                    }
                                                    else
                                                    {
                                                        val = i2ChildList[i3].InnerText;
                                                        if (val.Trim() == "") { val = "_~#*~"; }
                                                        #region oldAttr
                                                        if (child_list[i2].Attributes != null && child_list[i2].Attributes.Count > 0)/*new in else*/
                                                        {
                                                            newAttr.Add(child_list[i2].Attributes);
                                                        }

                                                        if (item.Attributes != null && item.Attributes.Count > 0)/*new in else*/
                                                        {
                                                            newAttr.Add(item.Attributes);
                                                        }
                                                        #endregion
                                                        if (i2ChildList[i3].Attributes != null)
                                                        {
                                                            /**/
                                                            if ("B,I,U,UL".Contains(i2ChildList[i3].Name.Trim().ToUpper()))
                                                            {
                                                                XmlAttribute Attr = doc.CreateAttribute("style", "");
                                                                switch (i2ChildList[i3].Name.Trim().ToUpper())
                                                                {
                                                                    case "B": Attr.Value = "BOLD"; break;
                                                                    case "I": Attr.Value = "ITALICS"; break;
                                                                    case "U": Attr.Value = "UNDERLINE"; break;
                                                                    case "UL": Attr.Value = "UL"; break;
                                                                    //case "A": newAttr.Value = "ANCHOR"; break;
                                                                    //default: newAttr.Value = string.Empty;
                                                                    //    break;
                                                                }
                                                                i2ChildList[i3].Attributes.Append(Attr);
                                                                newAttr.Add(i2ChildList[i3].Attributes);
                                                            }
                                                            else
                                                            {
                                                                /**/
                                                                //attr.Add(child_list[i2].Attributes);
                                                                newAttr.Add(i2ChildList[i3].Attributes);
                                                            }
                                                        }

                                                        if ("§,·".Contains(val)) { val = "\n" + val.Replace("·", "   ● ").Replace("§", "   ● "); }
                                                        KeyValuePair<string, List<XmlAttributeCollection>> oKeyValue = new KeyValuePair<string, List<XmlAttributeCollection>>(val /* + " "*/, newAttr);
                                                        lstKeyValue.Add(oKeyValue);
                                                        newAttr = new List<XmlAttributeCollection>();
                                                    }
                                                }

                                            }

                                            //val = item.InnerText;
                                            //if (item.Attributes != null)
                                            //{
                                            //    attr.Add(item.Attributes);
                                            //}
                                        }
                                        else
                                        {
                                            val = child_list[i2].InnerText;
                                            if (val.Trim() == "") { val = "_~#*~"; }
                                            if (item.Attributes != null)/*new in else*/
                                            {
                                                newAttr.Add(item.Attributes);
                                            }
                                            if (child_list[i2].Attributes != null)
                                            {
                                                /**/
                                                if ("B,I,U,UL".Contains(child_list[i2].Name.Trim().ToUpper()))
                                                {
                                                    XmlAttribute Attr = doc.CreateAttribute("style", "");
                                                    switch (child_list[i2].Name.Trim().ToUpper())
                                                    {
                                                        case "B": Attr.Value = "BOLD"; break;
                                                        case "I": Attr.Value = "ITALICS"; break;
                                                        case "U": Attr.Value = "UNDERLINE"; break;
                                                        case "UL": Attr.Value = "UL"; break;
                                                        //case "A": newAttr.Value = "ANCHOR"; break;
                                                        //default: newAttr.Value = string.Empty;
                                                        //    break;
                                                    }
                                                    child_list[i2].Attributes.Append(Attr);
                                                    newAttr.Add(child_list[i2].Attributes);
                                                }
                                                else
                                                {
                                                    /**/
                                                    //attr.Add(child_list[i2].Attributes);
                                                    newAttr.Add(child_list[i2].Attributes);
                                                }
                                            }

                                            if ("§,·".Contains(val)) { val = "\n" + val.Replace("·", "   ● ").Replace("§", "   ● "); }
                                            KeyValuePair<string, List<XmlAttributeCollection>> oKeyValue = new KeyValuePair<string, List<XmlAttributeCollection>>(val/* + " "*/, newAttr);
                                            lstKeyValue.Add(oKeyValue);
                                            newAttr = new List<XmlAttributeCollection>();
                                        }
                                    }
                                }
                                else
                                {
                                    newAttr = new List<XmlAttributeCollection>();

                                    val = item.InnerText;
                                    if (val.Trim() == "") { val = "_~#*~"; }
                                    if (item.Attributes != null)
                                    {
                                        /**/
                                        if ("B,I,U,UL".Contains(item.Name.Trim().ToUpper()))
                                        {
                                            XmlAttribute Attr = doc.CreateAttribute("style", "");
                                            switch (item.Name.Trim().ToUpper())
                                            {
                                                case "B": Attr.Value = "BOLD"; break;
                                                case "I": Attr.Value = "ITALICS"; break;
                                                case "U": Attr.Value = "UNDERLINE"; break;
                                                case "UL": Attr.Value = "UL"; break;
                                                //case "A": newAttr.Value = "ANCHOR"; break;
                                                //default: newAttr.Value = string.Empty;
                                                //    break;
                                            }
                                            item.Attributes.Append(Attr);
                                            newAttr.Add(item.Attributes);
                                        }
                                        else
                                        {
                                            /**/
                                            //   attr.Add(item.Attributes);
                                            newAttr.Add(item.Attributes);
                                        }
                                    }
                                    if ("§,·".Contains(val)) { val = "\n" + val.Replace("·", "   ● ").Replace("§", "   ● "); }
                                    KeyValuePair<string, List<XmlAttributeCollection>> oKeyValue = new KeyValuePair<string, List<XmlAttributeCollection>>(val /* + " "*/, newAttr);
                                    lstKeyValue.Add(oKeyValue);
                                    newAttr = new List<XmlAttributeCollection>();
                                }

                            }
                        }
                    }
                    else
                    {
                        if (attr != null && attr.Count > 0)
                        {
                            newAttr.AddRange(attr);
                            attr.RemoveRange(0, attr.Count);/*remove attributes once applied */
                        }
                        if (NodeList[i].Attributes != null)
                        {
                            newAttr.Add(NodeList[i].Attributes);
                        }


                        /**/
                        if ("B,I,U,UL".Contains(NodeList[i].Name.Trim().ToUpper()))
                        {
                            XmlAttribute Attr = doc.CreateAttribute("style", "");
                            switch (NodeList[i].Name.Trim().ToUpper())
                            {

                                case "B": Attr.Value = "BOLD"; break;
                                case "I": Attr.Value = "ITALICS"; break;
                                case "U": Attr.Value = "UNDERLINE"; break;
                                case "UL": Attr.Value = "UL"; break;
                                //case "A": newAttr.Value = "ANCHOR"; break;
                                //default: newAttr.Value = string.Empty;
                                //    break;
                            }

                            NodeList[i].Attributes.Append(Attr);
                            newAttr.Add(NodeList[i].Attributes);
                        }
                        /**/


                        //if (i == NodeList.Count - 1)
                        {
                            if (NodeList[i].ParentNode.Name.ToLower() == "li")
                            {
                                val = "\n" + "   ● " + NodeList[i].InnerText;
                            }
                            else
                            {
                                if (NodeList[i].ParentNode.Name.ToLower() == "a")
                                {
                                    //if (attr.Count == 0) {
                                    //    XmlAttribute Attr = doc.CreateAttribute("href", "");
                                    //    XmlAttribute Attr1 = doc.CreateAttribute("color", "");

                                    //    Attr.Value = NodeList[i].InnerText;
                                    //    Attr1.Value = "#0563c1";
                                    //    if (NodeList[i].Attributes == null)
                                    //    {

                                    //    }
                                    //    NodeList[i].Attributes.Append(Attr);
                                    //    NodeList[i].Attributes.Append(Attr1);
                                    //    newAttr.Add(NodeList[i].Attributes);


                                    //}
                                    val = NodeList[i].InnerText;
                                }
                                else
                                {
                                    val = NodeList[i].InnerText;
                                }

                            }
                            if (val.Trim() == "") { val = "_~#*~"; }
                            if (("§,·".Contains(val) && !"\n".Contains(val)) || val.Contains("•")) { val = "\n" + val.Replace("·", "   ● ").Replace("§", "   ● ").Replace("§", "   ● "); }
                            KeyValuePair<string, List<XmlAttributeCollection>> oKeyValue = new KeyValuePair<string, List<XmlAttributeCollection>>(val/* + " "*/, newAttr);
                            lstKeyValue.Add(oKeyValue);
                            newAttr = new List<XmlAttributeCollection>();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return lstKeyValue;
        }
        private static string getInnerchildEle(XmlNodeList NodeList, ref List<XmlAttributeCollection> attr, ref string rVal)
        {
            string tempNode = "";
            string name = "";
            object oa = new object();
            foreach (XmlNode item in NodeList)
            {

                oa = (object)item.Attributes;
                tempNode += item.InnerText;
                name += item.Name;
            }

            string val = "";
            try
            {

                foreach (XmlNode item in NodeList)
                {
                    if (!item.InnerXml.Contains("<"))
                    {
                        val = item.InnerText;
                    }
                    if (item.Attributes != null)
                    {

                    }

                }


                /*for (int i = 0; i < NodeList.Count; i++)
                {
                    if (NodeList[i].Attributes != null)
                        attr.Add(NodeList[i].Attributes);
                    if (i == NodeList.Count - 1)
                    {
                        if (NodeList[i].Name == "li")
                        {
                            val = "\n" + "   ● " + NodeList[i].InnerText;
                        }
                        else
                        {
                            val = NodeList[i].LastChild.InnerText;
                        }
                    }
                    if (NodeList[i].HasChildNodes)
                    {
                        XmlNodeList NodeList_child = NodeList[i].ChildNodes;
                        getInnerchildEle(NodeList_child, ref attr,ref val);
                    }
                    else
                    {
                        rVal += val;
                    }
                }*/
            }
            catch (Exception ex)
            {

            }
            return val;
        }

        //private static string childEleBullets(XmlNodeList NodeList, ref List<XmlAttributeCollection> attr)
        //{
        //    string val = "";
        //    try
        //    {
        //        for (int i = 0; i < NodeList.Count; i++)
        //        {
        //            if (NodeList[i].LastChild != null && !string.IsNullOrEmpty(NodeList[i].LastChild.InnerText) && NodeList[i].LastChild.InnerText != "·")
        //            {
        //                val += "\n" + "   ● " + NodeList[i].LastChild.InnerText;

        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    return val;
        //}

        private static List<KeyValuePair<string, List<XmlAttributeCollection>>> childEleBullets(XmlNodeList NodeList, ref List<XmlAttributeCollection> attr, ref XmlDocument doc)
        {
            string val = "";
            List<KeyValuePair<string, List<XmlAttributeCollection>>> olist = new List<KeyValuePair<string, List<XmlAttributeCollection>>>();
            try
            {
                List<XmlAttributeCollection> lstAttr = new List<XmlAttributeCollection>();
                for (int i = 0; i < NodeList.Count; i++)
                {
                    //if(NodeList[i].Attributes != null && NodeList[i].Attributes.Count > 0)
                    //    lstAttr.Add(NodeList[i].Attributes);
                    //if (NodeList[i].HasChildNodes)
                    //{

                    //}

                    if ((NodeList[i].LastChild != null && !string.IsNullOrEmpty(NodeList[i].LastChild.InnerText) && !"§,·".Contains(NodeList[i].LastChild.InnerText)))
                    {

                        #region BoldIta
                        if ("B,I,U,UL".Contains(NodeList[i].LastChild.Name.Trim().ToUpper()))
                        {
                            XmlAttribute Attr = doc.CreateAttribute("style", "");
                            switch (NodeList[i].LastChild.Name.Trim().ToUpper())
                            {

                                case "B": Attr.Value = "BOLD"; break;
                                case "I": Attr.Value = "ITALICS"; break;
                                case "U": Attr.Value = "UNDERLINE"; break;
                                case "UL": Attr.Value = "UL"; break;
                                //case "A": newAttr.Value = "ANCHOR"; break;
                                //default: newAttr.Value = string.Empty;
                                //    break;
                            }

                            NodeList[i].LastChild.Attributes.Append(Attr);
                            lstAttr.Add(NodeList[i].LastChild.Attributes);
                        }
                        #endregion

                        val += "\n" + "   ● " + NodeList[i].LastChild.InnerText;

                        if (NodeList[i].LastChild.ParentNode.Attributes != null)
                        {
                            lstAttr.Add(NodeList[i].LastChild.ParentNode.Attributes);
                            #region BoldIta
                            if ("B,I,U,UL".Contains(NodeList[i].LastChild.ParentNode.Name.Trim().ToUpper()))
                            {
                                XmlAttribute Attr = doc.CreateAttribute("style", "");
                                switch (NodeList[i].LastChild.ParentNode.Name.Trim().ToUpper())
                                {

                                    case "B": Attr.Value = "BOLD"; break;
                                    case "I": Attr.Value = "ITALICS"; break;
                                    case "U": Attr.Value = "UNDERLINE"; break;
                                    case "UL": Attr.Value = "UL"; break;
                                }

                                NodeList[i].LastChild.ParentNode.Attributes.Append(Attr);
                                lstAttr.Add(NodeList[i].LastChild.ParentNode.Attributes);
                            }
                            #endregion

                            if (NodeList[i].LastChild.ParentNode.ParentNode != null && NodeList[i].LastChild.ParentNode.ParentNode.Attributes != null)
                            {
                                #region BoldIta
                                if ("B,I,U,UL".Contains(NodeList[i].LastChild.ParentNode.ParentNode.Name.Trim().ToUpper()))
                                {
                                    XmlAttribute Attr = doc.CreateAttribute("style", "");
                                    switch (NodeList[i].LastChild.ParentNode.ParentNode.Name.Trim().ToUpper())
                                    {

                                        case "B": Attr.Value = "BOLD"; break;
                                        case "I": Attr.Value = "ITALICS"; break;
                                        case "U": Attr.Value = "UNDERLINE"; break;
                                        case "UL": Attr.Value = "UL"; break;
                                    }

                                    NodeList[i].LastChild.ParentNode.ParentNode.Attributes.Append(Attr);
                                    lstAttr.Add(NodeList[i].LastChild.ParentNode.ParentNode.Attributes);
                                }
                                #endregion

                                lstAttr.Add(NodeList[i].LastChild.ParentNode.ParentNode.Attributes);
                            }
                            if (NodeList[i].LastChild.ParentNode.ParentNode.ParentNode != null && NodeList[i].LastChild.ParentNode.ParentNode.ParentNode.Attributes != null)
                            {
                                lstAttr.Add(NodeList[i].LastChild.ParentNode.ParentNode.ParentNode.Attributes);

                                #region BoldIta
                                if ("B,I,U,UL".Contains(NodeList[i].LastChild.ParentNode.ParentNode.ParentNode.Name.Trim().ToUpper()))
                                {
                                    XmlAttribute Attr = doc.CreateAttribute("style", "");
                                    switch (NodeList[i].LastChild.ParentNode.ParentNode.ParentNode.Name.Trim().ToUpper())
                                    {

                                        case "B": Attr.Value = "BOLD"; break;
                                        case "I": Attr.Value = "ITALICS"; break;
                                        case "U": Attr.Value = "UNDERLINE"; break;
                                        case "UL": Attr.Value = "UL"; break;
                                    }

                                    NodeList[i].LastChild.ParentNode.ParentNode.ParentNode.Attributes.Append(Attr);
                                    lstAttr.Add(NodeList[i].LastChild.ParentNode.ParentNode.ParentNode.Attributes);
                                }
                                #endregion
                            }
                            if (NodeList[i].LastChild.ParentNode.ParentNode.ParentNode.ParentNode != null && NodeList[i].LastChild.ParentNode.ParentNode.ParentNode.ParentNode.Attributes != null)
                            {
                                lstAttr.Add(NodeList[i].LastChild.ParentNode.ParentNode.ParentNode.ParentNode.Attributes);

                                #region BoldIta
                                if ("B,I,U,UL".Contains(NodeList[i].LastChild.ParentNode.ParentNode.ParentNode.ParentNode.Name.Trim().ToUpper()))
                                {
                                    XmlAttribute Attr = doc.CreateAttribute("style", "");
                                    switch (NodeList[i].LastChild.ParentNode.ParentNode.ParentNode.ParentNode.Name.Trim().ToUpper())
                                    {

                                        case "B": Attr.Value = "BOLD"; break;
                                        case "I": Attr.Value = "ITALICS"; break;
                                        case "U": Attr.Value = "UNDERLINE"; break;
                                        case "UL": Attr.Value = "UL"; break;
                                    }

                                    NodeList[i].LastChild.ParentNode.ParentNode.ParentNode.ParentNode.Attributes.Append(Attr);
                                    lstAttr.Add(NodeList[i].LastChild.ParentNode.ParentNode.ParentNode.ParentNode.Attributes);
                                }
                                #endregion
                            }
                        }
                        if (NodeList[i].LastChild.Attributes != null && NodeList[i].LastChild.Attributes.Count > 0)
                        {
                            lstAttr.Add(NodeList[i].LastChild.Attributes);
                        }
                        if (NodeList[i].LastChild.HasChildNodes)
                        {
                            int childCnt = NodeList[i].LastChild.ChildNodes.Count;
                            for (int j = 0; j < childCnt; j++)
                            {
                                if (NodeList[i].LastChild.ChildNodes[j].Attributes != null && NodeList[i].LastChild.ChildNodes[j].Attributes.Count > 0)
                                    lstAttr.Add(NodeList[i].LastChild.ChildNodes[j].Attributes);
                            }
                        }


                    }
                }
                KeyValuePair<string, List<XmlAttributeCollection>> kv = new KeyValuePair<string, List<XmlAttributeCollection>>(val, attr);
                olist.Add(kv);
            }
            catch (Exception ex)
            {
                return null;
            }
            return olist;
        }




        private static string isNumeric(string num)
        {
            try
            {
                if (string.IsNullOrEmpty(num))
                {
                    return "0";
                }
                else if (num.Contains("."))
                {
                    return Convert.ToDouble(num).ToString();
                }
                else
                {
                    return Convert.ToInt32(num).ToString();
                }
            }
            catch (Exception ex)
            {
                return "0";
            }
        }

        public static WorkbookPart CreateSharedWorkbookPart(SpreadsheetDocument spreadsheetDocument)
        {
            WorkbookPart mExcelWorkbookPart = null;
            try
            {
                mExcelWorkbookPart = spreadsheetDocument.WorkbookPart;
                if (mExcelWorkbookPart == null)
                { // Add a WorkbookPart to the document.
                    mExcelWorkbookPart = spreadsheetDocument.AddWorkbookPart();
                    mExcelWorkbookPart.Workbook = new Workbook();
                }
            }
            catch (Exception ex)
            {

            }
            return mExcelWorkbookPart;
        }

        public static SharedStringTablePart CreateSharedStringTablePart(WorkbookPart mExcelWorkbookPart)
        {
            SharedStringTablePart sstPart = null;
            try
            {
                sstPart = mExcelWorkbookPart.SharedStringTablePart;
                if (sstPart == null) { sstPart = mExcelWorkbookPart.AddNewPart<SharedStringTablePart>(); }
            }
            catch (Exception ex)
            {

            }
            return sstPart;
        }
        #endregion
    }
}