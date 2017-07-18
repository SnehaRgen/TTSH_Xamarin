using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DocumentFormat.OpenXml.Packaging;
using Ap = DocumentFormat.OpenXml.ExtendedProperties;
using Vt = DocumentFormat.OpenXml.VariantTypes;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Spreadsheet;
using A = DocumentFormat.OpenXml.Drawing;
using System.Data;
using TTSH.Entity;
using System.IO;
using Xdr = DocumentFormat.OpenXml.Drawing.Spreadsheet;
using System.Text.RegularExpressions;



namespace TTSHWCFLayer.Excel
{
    public class GenerateDynamicExcel
    {
        // Creates a SpreadsheetDocument.
        public void CreatePackage(string filePath, List<EntityDynamicExcel> listExcelSheetdata)
        {
            using (SpreadsheetDocument package = SpreadsheetDocument.Create(filePath, SpreadsheetDocumentType.Workbook))
            {
                CreateParts(package, listExcelSheetdata);
            }
        }

        // Adds child parts and generates content of the specified part.
        private void CreateParts(SpreadsheetDocument document, List<EntityDynamicExcel> listExcelSheetdata)
        {
            WorkbookPart workbookPart1 = document.AddWorkbookPart();
            GenerateWorkbookPart1Content(workbookPart1, listExcelSheetdata);

            /*prasanna*/
            
            SharedStringTablePart sstablePart = null;
            var cntSharedItem = listExcelSheetdata.SelectMany(z => z.listRowData).SelectMany(y => y.listData).Count(z => z.cellType.ToString() == "sharedStringType");
            if (cntSharedItem > 0)
            {
                //sstablePart = ExportExcel.CreateSharedStringTablePart(workbookPart1);
            }
            /**/

            int _sheetCount = listExcelSheetdata.Count;
            for (int i = 1; i <= _sheetCount; i++)
            {
                WorksheetPart worksheetParti = workbookPart1.AddNewPart<WorksheetPart>("rId" + i);
                GenerateWorksheetPart1Content(worksheetParti, listExcelSheetdata[i - 1], ref sstablePart, ref workbookPart1);
            }

            //SharedStringTablePart sharedStringTablePart1 = workbookPart1.AddNewPart<SharedStringTablePart>("rId6");
            //GenerateSharedStringTablePart1Content(sharedStringTablePart1);

            WorkbookStylesPart workbookStylesPart1 = workbookPart1.AddNewPart<WorkbookStylesPart>("rId" + (_sheetCount + 1));
            GenerateWorkbookStylesPart1Content(workbookStylesPart1);

            //SetPackageProperties(document);
        }

        // Generates content of workbookPart1.
        private void GenerateWorkbookPart1Content(WorkbookPart workbookPart1, List<EntityDynamicExcel> listExcelSheetdata)
        {
            Workbook workbook1 = new Workbook();
            workbook1.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
            FileVersion fileVersion1 = new FileVersion() { ApplicationName = "xl", LastEdited = "4", LowestEdited = "4", BuildVersion = "4506" };
            WorkbookProperties workbookProperties1 = new WorkbookProperties() { FilterPrivacy = true, DefaultThemeVersion = (UInt32Value)124226U };

            BookViews bookViews1 = new BookViews();
            WorkbookView workbookView1 = new WorkbookView() { XWindow = 240, YWindow = 105, WindowWidth = (UInt32Value)14805U, WindowHeight = (UInt32Value)8010U };

            bookViews1.Append(workbookView1);

            Sheets sheets1 = new Sheets();
            int _sheetIndex = 1;
            foreach (EntityDynamicExcel sheet in listExcelSheetdata)
            {
                Sheet sheet1 = new Sheet() { Name = sheet.sheetName, SheetId = (UInt32Value)(UInt32)_sheetIndex, Id = "rId" + _sheetIndex };
                sheets1.Append(sheet1);
                _sheetIndex++;
            }

            /*DefinedNames definedNames1 = new DefinedNames();
            DefinedName definedName1 = new DefinedName() { Name = "Status" };
            definedName1.Text = "Sheet1!$C$1,Sheet1!$D$1,Sheet1!$E$1";*/

            //definedNames1.Append(definedName1);
            //CalculationProperties calculationProperties1 = new CalculationProperties() { CalculationId = (UInt32Value)125725U };

            //workbook1.Append(fileVersion1);
            // workbook1.Append(workbookProperties1);
            // workbook1.Append(bookViews1);
            workbook1.Append(sheets1);
            //workbook1.Append(definedNames1);
            //workbook1.Append(calculationProperties1);

            workbookPart1.Workbook = workbook1;
        }

        // Generates content of worksheetPart1.
        private void GenerateWorksheetPart1Content(WorksheetPart worksheetPart3, EntityDynamicExcel _sheetData, ref SharedStringTablePart sstablePart, ref WorkbookPart mExcelWorkbookPart)
        {

            Worksheet worksheet3 = new Worksheet();
            worksheet3.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
            //SheetDimension sheetDimension3 = new SheetDimension() { Reference = "A1:E16" };
            SheetDimension sheetDimension3 = new SheetDimension() { Reference = _sheetData.sheetDimensionStart + ":" + _sheetData.sheetDimensionEnd };

            SheetViews sheetViews3 = new SheetViews();
            SheetView sheetView3 = new SheetView() { TabSelected = true, WorkbookViewId = (UInt32Value)0U };
            Selection selection2 = new Selection() { ActiveCell = "E2", SequenceOfReferences = new ListValue<StringValue>() { InnerText = "E2" } };
            sheetView3.Append(selection2);

            sheetViews3.Append(sheetView3);
            SheetFormatProperties sheetFormatProperties3 = new SheetFormatProperties() { DefaultRowHeight = 15D };

            Columns columns1 = new Columns();
            foreach (SheetColumns sheetcolumn in _sheetData.listSheetColumns)
            {
                Column column = new Column() { Min = (UInt32Value)(UInt32)sheetcolumn.columnIndex, Max = (UInt32Value)(UInt32)sheetcolumn.columnIndex, Width = sheetcolumn.columnWidth, CustomWidth = true,BestFit=true };
                columns1.Append(column);
            }

            SheetData sheetData3 = new SheetData();
            string cellValidateReference = string.Empty;
            List<ImageInfo> listImageInfo = new List<ImageInfo>();
            foreach (RowData row in _sheetData.listRowData)
            {
                List<CellData> listCellData = new List<CellData>();
                listCellData = row.listData;

                //Row row4 = new Row() { RowIndex = (UInt32Value)1U, Spans = new ListValue<StringValue>() { InnerText = "1:5" } };
                Row rowNew = new Row() { RowIndex = (UInt32Value)(UInt32)row.rowIndex, Spans = new ListValue<StringValue>() { InnerText = row.rowCellStartIndex + ":" + row.rowCellEndIndex } };

                foreach (CellData celldata in listCellData)
                {

                    Cell cell = new Cell();
                    
                    cell.CellReference = Convert.ToString(celldata.cellName) + row.rowIndex;     

                    if (celldata.isDataValidate)
                        cellValidateReference = cellValidateReference + " " + cell.CellReference;

                    if (Convert.ToString(celldata.cellType) == "textBoldDarkGreyFill")
                    {
                        cell.StyleIndex = (UInt32Value)12U;
                        cell.DataType = CellValues.String;
                    }
                    else if (Convert.ToString(celldata.cellType) == "textBoldLtGreyFill")
                    {
                        cell.StyleIndex = (UInt32Value)4U;
                        cell.DataType = CellValues.String;
                    }
                    else if (Convert.ToString(celldata.cellType) == "textBoldYellowFill")
                    {
                        cell.StyleIndex = (UInt32Value)2U;
                        cell.DataType = CellValues.String;
                    }
                    else if (Convert.ToString(celldata.cellType) == "textYellowFill")
                    {
                        cell.StyleIndex = (UInt32Value)1U;
                        cell.DataType = CellValues.String;
                    }
                    else if (Convert.ToString(celldata.cellType) == "NumberYellowFill")
                    {
                        cell.StyleIndex = (UInt32Value)1U;
                        cell.DataType = CellValues.Number;
                    }
                    else if (Convert.ToString(celldata.cellType) == "textBold")
                    {
                        cell.StyleIndex = (UInt32Value)3U;
                        cell.DataType = CellValues.String;
                    }
                    else if (Convert.ToString(celldata.cellType) == "textBoldOrangeFill")
                    {
                        cell.StyleIndex = (UInt32Value)5U;
                        cell.DataType = CellValues.String;
                    }
                    else if (Convert.ToString(celldata.cellType) == "textNoFill")
                    {
                        cell.DataType = CellValues.String;
                        cell.StyleIndex = (UInt32Value)0U;
                        //unlock cell style
                        if (celldata.isDataValidate)
                            cell.StyleIndex = (UInt32Value)10U;
                    }
                    else if (Convert.ToString(celldata.cellType) == "textNoFillUnLock")
                    {
                        cell.DataType = CellValues.String;
                        //unlock cell style
                        cell.StyleIndex = (UInt32Value)10U;                        
                    }
                    else if (Convert.ToString(celldata.cellType) == "NumberNoFill")
                    {
                        cell.DataType = CellValues.Number;
                    }
                    else if (Convert.ToString(celldata.cellType) == "NumberNoFillLtAlign")
                    {
                        cell.DataType = CellValues.Number;
                        cell.StyleIndex = (UInt32Value)0U;
                    }
                    else if (Convert.ToString(celldata.cellType) == "percent")
                    {
                        cell.DataType = CellValues.Number;
                        cell.StyleIndex = (UInt32Value)9U;
                    }
                    else if (Convert.ToString(celldata.cellType) == "percentBold")
                    {
                        cell.DataType = CellValues.Number;
                        cell.StyleIndex = (UInt32Value)13U;
                    }
                    else if (Convert.ToString(celldata.cellType) == "textBlueFill")
                    {
                        cell.DataType = CellValues.String;
                        cell.StyleIndex = (UInt32Value)11U;
                    }
                    else if (Convert.ToString(celldata.cellType) == "NumberBold")
                    {
                        cell.DataType = CellValues.Number;
                        cell.StyleIndex = (UInt32Value)3U;
                    }
                    else if (Convert.ToString(celldata.cellType) == "textHidden")
                    {
                        cell.DataType = CellValues.String;
                        cell.StyleIndex = (UInt32Value)6U;
                    }
                    else if (Convert.ToString(celldata.cellType) == "textNoFillRtAlign")
                    {
                        cell.DataType = CellValues.Number;
                        cell.StyleIndex = (UInt32Value)14U;
                    }
                    else if (Convert.ToString(celldata.cellType) == "imageFile")
                    {
                        int maxHeight = 0; int maxWidth = 0; string imgType = string.Empty; string imgBase64String = string.Empty;

                        Regex reImg = new Regex(@"<img\s[^>]*>", RegexOptions.IgnoreCase);
                        Regex reHeight = new Regex(@"height=(?:(['""])(?<height>(?:(?!\1).)*)\1|(?<height>[^\s>]+))", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                        Regex reWidth = new Regex(@"width=(?:(['""])(?<width>(?:(?!\1).)*)\1|(?<width>[^\s>]+))", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                        Regex reSrc = new Regex(@"src=(?:(['""])(?<src>(?:(?!\1).)*)\1|(?<src>[^\s>]+))", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                        MatchCollection mc = reImg.Matches(celldata.cellValue);
                        foreach (Match mImg in mc)
                        {
                            //Console.WriteLine("    img tag: {0}", mImg.Groups[0].Value);
                            if (reHeight.IsMatch(mImg.Groups[0].Value))
                            {
                                Match mHeight = reHeight.Match(mImg.Groups[0].Value);
                                maxHeight = Convert.ToInt16(mHeight.Groups["height"].Value);
                                maxHeight = Convert.ToInt16(Math.Round(maxHeight * (72f / 96f)));//pixel to point
                            }
                            if (reWidth.IsMatch(mImg.Groups[0].Value))
                            {
                                Match mWidth = reWidth.Match(mImg.Groups[0].Value);
                                maxWidth = Convert.ToInt16(mWidth.Groups["width"].Value);
                                maxWidth = Convert.ToInt16(Math.Round(maxWidth * (72f / 96f)));//pixel to point
                            }
                            if (reHeight.IsMatch(mImg.Groups[0].Value))
                            {
                                Match mSrc = reSrc.Match(mImg.Groups[0].Value);
                                string imgSrc = Convert.ToString(mSrc.Groups["src"].Value);
                                int indexStart = imgSrc.IndexOf('/');
                                int length = imgSrc.IndexOf(';') - imgSrc.IndexOf('/');
                                imgType = imgSrc.Substring(indexStart + 1, length - 1);
                                int index1 = imgSrc.IndexOf(',');
                                imgBase64String = imgSrc.Substring(index1 + 1);
                            }
                        }

                        ImageInfo objImg = new ImageInfo();
                        objImg.rowNumber = row.rowIndex;
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

                        //To fill the text contain with the image | Ejaz Waquif DT:18 Mar 2015
                        string textData = celldata.cellValue.Split(new string[] { "<img" }, StringSplitOptions.None)[0];
                        
                        if (textData != "")
                        {
                            ExportExcel expEx = new ExportExcel();
                        
                            cell.DataType = CellValues.String;
                            CellValue cellValue = new CellValue();

                            cellValue.Text = expEx.ConvertHtmlToPlainText(textData);

                            cell.Append(cellValue);

                        }
                        //End of To fill the text contain with the image | Ejaz Waquif DT:18 Mar 2015

                    }
                    if (Convert.ToString(celldata.cellType) == "sharedStringType")
                    {
                        /*shared string type*/
                        cell.DataType = CellValues.SharedString;
                        //if (celldata.cellValue.Contains("<span"))/*temporary delete this*/
                        {
                            celldata.cellValue = celldata.cellValue.Replace("\r\n", "").Replace("\n","");
                            /**/
                            //listImageInfo
                            //row
                            //rownew
                            //cell
                           // Column col1 = columns1.ChildElements[Convert.ToInt16(celldata.cellName) - 1] as Column;
                            
                            /**/
                            //SharedStringItem ssItem = ExportExcel.HtmlToRunProperties(celldata.cellValue);
                            int iRowIndex = row.rowIndex;
                            CellData newCellData = celldata;  
                            SharedStringItem ssItem = ExportExcel.HtmlToRunProperties(celldata.cellValue, iRowIndex, ref rowNew, ref listImageInfo, ref cell, ref columns1,  ref newCellData);
                            if (ssItem != null)
                            {
                                if (sstablePart.SharedStringTable == null)
                                {
                                    sstablePart.SharedStringTable = new SharedStringTable();
                                }
                                sstablePart.SharedStringTable.Append(ssItem);
                               // WorksheetPart worksheetPart = ExportExcel.InsertWorksheet(mExcelWorkbookPart);/*Code to add new sheet to workbook*/

                                int cnt = sstablePart.SharedStringTable.ChildElements.Count;
                                /*set val to and from sst*/

                                CellValue cellValue = new CellValue(Convert.ToString(cnt - 1));
                                //cellValue.Text = Convert.ToString(cnt - 1);
                                cell.Append(cellValue);
                            }
                        }
                    } else
                    if (Convert.ToString(celldata.cellType) != "imageFile")
                    {
                        CellValue cellValue = new CellValue();
                        cellValue.Text = celldata.cellValue;
                        cell.Append(cellValue);                        
                    }
                    
                    rowNew.Append(cell);

                }
                sheetData3.Append(rowNew);
            }

            worksheet3.Append(sheetDimension3);
            worksheet3.Append(sheetViews3);
            worksheet3.Append(sheetFormatProperties3);
            worksheet3.Append(columns1);
            worksheet3.Append(sheetData3);

            if (_sheetData.isSheetProtected)
            {
                //protect sheet review tab and set password 12345
                SheetProtection sheetProtection1 = new SheetProtection() { Password = "CA9C", Sheet = true, Objects = false, Scenarios = true,FormatCells = false, FormatColumns = false, FormatRows = false };
                worksheet3.Append(sheetProtection1);
            }
            if (!string.IsNullOrEmpty(cellValidateReference.Trim()))
            {
                //dropdown cell 'Pass,Fail,NotCompleted'
                DataValidations dataValidations1 = new DataValidations() { Count = (UInt32Value)1U };
                //DataValidation dataValidation1 = new DataValidation() { Type = DataValidationValues.List, AllowBlank = true, ShowInputMessage = true, ShowErrorMessage = true, Error = "Select value from dropdown.", SequenceOfReferences = new ListValue<StringValue>() { InnerText = "E13 E14 E16" } };
                DataValidation dataValidation1 = new DataValidation() { Type = DataValidationValues.List, AllowBlank = true, ShowInputMessage = true, ShowErrorMessage = true, Error = "Select value from dropdown.", SequenceOfReferences = new ListValue<StringValue>() { InnerText = (StringValue)cellValidateReference.Trim() } };
                Formula1 formula1 = new Formula1();
                formula1.Text = "\"Pass,Fail,Not Completed\"";
                dataValidation1.Append(formula1);
                dataValidations1.Append(dataValidation1);
                worksheet3.Append(dataValidations1);
            }
            
            worksheetPart3.Worksheet = worksheet3;

            foreach (ImageInfo img in listImageInfo)
            {
                try
                {
                
                    InsertImage(worksheetPart3, img.imgBase64,img.imgType,img.imgWidth,img.imgHeight ,img.cellNumber, img.rowNumber, 0, 0);//colnum,rownu,offsetx,offsety
                }
                catch (Exception ex)
                {
                    continue;
                }
            }
            
        }

        // Generates content of sharedStringTablePart1.
        private void GenerateSharedStringTablePart1Content(SharedStringTablePart sharedStringTablePart1)
        {
            SharedStringTable sharedStringTable1 = new SharedStringTable() { Count = (UInt32Value)34U, UniqueCount = (UInt32Value)26U };

            SharedStringItem sharedStringItem1 = new SharedStringItem();
            Text text1 = new Text();
            text1.Text = "ProjectName";

            sharedStringItem1.Append(text1);

            SharedStringItem sharedStringItem2 = new SharedStringItem();
            Text text2 = new Text();
            text2.Text = "VersionName";

            sharedStringItem2.Append(text2);

            SharedStringItem sharedStringItem3 = new SharedStringItem();
            Text text3 = new Text();
            text3.Text = "TestPassName";

            sharedStringItem3.Append(text3);

            SharedStringItem sharedStringItem4 = new SharedStringItem();
            Text text4 = new Text();
            text4.Text = "ManagerName";

            sharedStringItem4.Append(text4);

            SharedStringItem sharedStringItem5 = new SharedStringItem();
            Text text5 = new Text();
            text5.Text = "Tester";

            sharedStringItem5.Append(text5);

            SharedStringItem sharedStringItem6 = new SharedStringItem();
            Text text6 = new Text();
            text6.Text = "Role";

            sharedStringItem6.Append(text6);

            SharedStringItem sharedStringItem7 = new SharedStringItem();
            Text text7 = new Text();
            text7.Text = "TestCaseName";

            sharedStringItem7.Append(text7);

            SharedStringItem sharedStringItem8 = new SharedStringItem();
            Text text8 = new Text();
            text8.Text = "ETT";

            sharedStringItem8.Append(text8);

            SharedStringItem sharedStringItem9 = new SharedStringItem();
            Text text9 = new Text();
            text9.Text = "Note";

            sharedStringItem9.Append(text9);

            SharedStringItem sharedStringItem10 = new SharedStringItem();
            Text text10 = new Text();
            text10.Text = "Expected Result";

            sharedStringItem10.Append(text10);

            SharedStringItem sharedStringItem11 = new SharedStringItem();
            Text text11 = new Text();
            text11.Text = "Actual Result";

            sharedStringItem11.Append(text11);

            SharedStringItem sharedStringItem12 = new SharedStringItem();
            Text text12 = new Text();
            text12.Text = "Status";

            sharedStringItem12.Append(text12);

            SharedStringItem sharedStringItem13 = new SharedStringItem();
            Text text13 = new Text();
            text13.Text = "TestActionName";

            sharedStringItem13.Append(text13);

            SharedStringItem sharedStringItem14 = new SharedStringItem();
            Text text14 = new Text();
            text14.Text = "Sequence";

            sharedStringItem14.Append(text14);

            SharedStringItem sharedStringItem15 = new SharedStringItem();
            Text text15 = new Text();
            text15.Text = "TS1";

            sharedStringItem15.Append(text15);

            SharedStringItem sharedStringItem16 = new SharedStringItem();
            Text text16 = new Text();
            text16.Text = "EP";

            sharedStringItem16.Append(text16);

            SharedStringItem sharedStringItem17 = new SharedStringItem();
            Text text17 = new Text();
            text17.Text = "AR";

            sharedStringItem17.Append(text17);

            SharedStringItem sharedStringItem18 = new SharedStringItem();
            Text text18 = new Text();
            text18.Text = "Pass";

            sharedStringItem18.Append(text18);

            SharedStringItem sharedStringItem19 = new SharedStringItem();
            Text text19 = new Text();
            text19.Text = "Fail";

            sharedStringItem19.Append(text19);

            SharedStringItem sharedStringItem20 = new SharedStringItem();
            Text text20 = new Text();
            text20.Text = "NotCompleted";

            sharedStringItem20.Append(text20);

            SharedStringItem sharedStringItem21 = new SharedStringItem();
            Text text21 = new Text();
            text21.Text = "TS2";

            sharedStringItem21.Append(text21);

            SharedStringItem sharedStringItem22 = new SharedStringItem();
            Text text22 = new Text();
            text22.Text = "EP1";

            sharedStringItem22.Append(text22);

            SharedStringItem sharedStringItem23 = new SharedStringItem();
            Text text23 = new Text();
            text23.Text = "AR1";

            sharedStringItem23.Append(text23);

            SharedStringItem sharedStringItem24 = new SharedStringItem();
            Text text24 = new Text();
            text24.Text = "TS";

            sharedStringItem24.Append(text24);

            SharedStringItem sharedStringItem25 = new SharedStringItem();
            Text text25 = new Text();
            text25.Text = "EPP";

            sharedStringItem25.Append(text25);

            SharedStringItem sharedStringItem26 = new SharedStringItem();
            Text text26 = new Text();
            text26.Text = "ARR";

            sharedStringItem26.Append(text26);

            sharedStringTable1.Append(sharedStringItem1);
            sharedStringTable1.Append(sharedStringItem2);
            sharedStringTable1.Append(sharedStringItem3);
            sharedStringTable1.Append(sharedStringItem4);
            sharedStringTable1.Append(sharedStringItem5);
            sharedStringTable1.Append(sharedStringItem6);
            sharedStringTable1.Append(sharedStringItem7);
            sharedStringTable1.Append(sharedStringItem8);
            sharedStringTable1.Append(sharedStringItem9);
            sharedStringTable1.Append(sharedStringItem10);
            sharedStringTable1.Append(sharedStringItem11);
            sharedStringTable1.Append(sharedStringItem12);
            sharedStringTable1.Append(sharedStringItem13);
            sharedStringTable1.Append(sharedStringItem14);
            sharedStringTable1.Append(sharedStringItem15);
            sharedStringTable1.Append(sharedStringItem16);
            sharedStringTable1.Append(sharedStringItem17);
            sharedStringTable1.Append(sharedStringItem18);
            sharedStringTable1.Append(sharedStringItem19);
            sharedStringTable1.Append(sharedStringItem20);
            sharedStringTable1.Append(sharedStringItem21);
            sharedStringTable1.Append(sharedStringItem22);
            sharedStringTable1.Append(sharedStringItem23);
            sharedStringTable1.Append(sharedStringItem24);
            sharedStringTable1.Append(sharedStringItem25);
            sharedStringTable1.Append(sharedStringItem26);

            sharedStringTablePart1.SharedStringTable = sharedStringTable1;
        }

        // Generates content of workbookStylesPart1.
        private void GenerateWorkbookStylesPart1Content(WorkbookStylesPart workbookStylesPart1)
        {
            Stylesheet stylesheet1 = new Stylesheet();
            //for hidden cell custom cell format as ';;;'
            NumberingFormats numberingFormats1 = new NumberingFormats() { Count = (UInt32Value)1U };
            NumberingFormat numberingFormat1 = new NumberingFormat() { NumberFormatId = (UInt32Value)164U, FormatCode = ";;;" };
            numberingFormats1.Append(numberingFormat1);

            Fonts fonts1 = new Fonts() { Count = (UInt32Value)4U };

            //Normal fontID 0
            Font font1 = new Font();
            FontSize fontSize1 = new FontSize() { Val = 11D };
            //Color color1 = new Color() { Theme = (UInt32Value)1U };
            Color color1 = new Color() { Rgb="000000" };
            FontName fontName1 = new FontName() { Val = "Calibri" };
            FontFamilyNumbering fontFamilyNumbering1 = new FontFamilyNumbering() { Val = 2 };
            FontScheme fontScheme1 = new FontScheme() { Val = FontSchemeValues.Minor };

            font1.Append(fontSize1);
            font1.Append(color1);
            font1.Append(fontName1);
            font1.Append(fontFamilyNumbering1);
            font1.Append(fontScheme1);

            //black bold fontID 1
            Font font2 = new Font();
            Bold bold1 = new Bold();
            FontSize fontSize2 = new FontSize() { Val = 11D };
            //Color color2 = new Color() { Theme = (UInt32Value)1U };
            Color color2 = new Color() { Rgb="000000" };
            FontName fontName2 = new FontName() { Val = "Calibri" };
            FontFamilyNumbering fontFamilyNumbering2 = new FontFamilyNumbering() { Val = 2 };
            FontScheme fontScheme2 = new FontScheme() { Val = FontSchemeValues.Minor };

            font2.Append(bold1);
            font2.Append(fontSize2);
            font2.Append(color2);
            font2.Append(fontName2);
            font2.Append(fontFamilyNumbering2);
            font2.Append(fontScheme2);

            //White Normal fontID 2
            Font font3 = new Font();
            FontSize fontSize3 = new FontSize() { Val = 11D };
            //Color color3 = new Color() { Indexed = (UInt32Value)9U };
            Color color3 = new Color() { Rgb="FFFFFF" };
            FontName fontName3 = new FontName() { Val = "Calibri" };
            FontFamilyNumbering fontFamilyNumbering3 = new FontFamilyNumbering() { Val = 2 };
            FontScheme fontScheme4 = new FontScheme() { Val = FontSchemeValues.Minor };

            font3.Append(fontSize3);
            font3.Append(color3);
            font3.Append(fontName3);
            font3.Append(fontFamilyNumbering3);
            font3.Append(fontScheme4);

            //White Bold fontID 3
            Font font4 = new Font();
            Bold bold4 = new Bold();
            FontSize fontSize4 = new FontSize() { Val = 11D };
            //Color color4 = new Color() { Indexed = (UInt32Value)9U };
            Color color4 = new Color() { Rgb="FFFFFF"};
            FontName fontName4 = new FontName() { Val = "Calibri" };
            FontFamilyNumbering fontFamilyNumbering4 = new FontFamilyNumbering() { Val = 2 };
            FontScheme fontScheme5 = new FontScheme() { Val = FontSchemeValues.Minor };

            font4.Append(bold4);
            font4.Append(fontSize4);
            font4.Append(color4);
            font4.Append(fontName4);
            font4.Append(fontFamilyNumbering4);
            font4.Append(fontScheme5);

            fonts1.Append(font1);
            fonts1.Append(font2);
            fonts1.Append(font3);
            fonts1.Append(font4);

            Fills fills1 = new Fills() { Count = (UInt32Value)7U };

            //fillId 0
            Fill fill1 = new Fill();
            PatternFill patternFill1 = new PatternFill() { PatternType = PatternValues.None };
            fill1.Append(patternFill1);

            //fillId 1
            Fill fill2 = new Fill();
            PatternFill patternFill2 = new PatternFill() { PatternType = PatternValues.Gray125 };
            fill2.Append(patternFill2);

            //yellow fillID 2
            Fill fill3 = new Fill();
            PatternFill patternFill3 = new PatternFill() { PatternType = PatternValues.Solid };
            //ForegroundColor foregroundColor1 = new ForegroundColor() { Rgb = "FFFFFF00" };
            ForegroundColor foregroundColor1 = new ForegroundColor() { Rgb = "FFFF99" };
            BackgroundColor backgroundColor1 = new BackgroundColor() { Indexed = (UInt32Value)64U };
            
            patternFill3.Append(foregroundColor1);
            patternFill3.Append(backgroundColor1);
            fill3.Append(patternFill3);

            //ltGrey fillID 3
            Fill fill4 = new Fill();
            PatternFill patternFill4 = new PatternFill() { PatternType = PatternValues.Solid };
            ForegroundColor foregroundColor2 = new ForegroundColor() { Theme = (UInt32Value)0U, Tint = -0.249977111117893D };
            BackgroundColor backgroundColor2 = new BackgroundColor() { Indexed = (UInt32Value)64U };
            patternFill4.Append(foregroundColor2);
            patternFill4.Append(backgroundColor2);
            fill4.Append(patternFill4);

            //Orange fillID 4
            Fill fill5 = new Fill();
            PatternFill patternFill5 = new PatternFill() { PatternType = PatternValues.Solid };
            //ForegroundColor foregroundColor3 = new ForegroundColor() { Theme = (UInt32Value)9U, Tint = -0.249977111117893D };
            //ForegroundColor foregroundColor3 = new ForegroundColor() { Rgb ="FF6666" };//pink
            ForegroundColor foregroundColor3 = new ForegroundColor() { Rgb = "FFCC99" };//lt orange
            BackgroundColor backgroundColor3 = new BackgroundColor() { Indexed = (UInt32Value)64U };
            patternFill5.Append(foregroundColor3);
            patternFill5.Append(backgroundColor3);
            fill5.Append(patternFill5);

            //Blue fillID 5
            Fill fill6 = new Fill();
            PatternFill patternFill6 = new PatternFill() { PatternType = PatternValues.Solid };
            ForegroundColor foregroundColor6 = new ForegroundColor() { Indexed = (UInt32Value)30U };
            BackgroundColor backgroundColor6 = new BackgroundColor() { Indexed = (UInt32Value)64U };
            patternFill6.Append(foregroundColor6);
            patternFill6.Append(backgroundColor6);
            fill6.Append(patternFill6);

            //Dark Grey fillID 6
            Fill fill7 = new Fill();
            PatternFill patternFill7 = new PatternFill() { PatternType = PatternValues.Solid };
            //ForegroundColor foregroundColor7 = new ForegroundColor() { Rgb="666666" };
            ForegroundColor foregroundColor7 = new ForegroundColor() { Rgb = "6C6C89" };
            BackgroundColor backgroundColor7 = new BackgroundColor() { Indexed = (UInt32Value)64U };
            patternFill7.Append(foregroundColor7);
            patternFill7.Append(backgroundColor7);
            fill7.Append(patternFill7);

            fills1.Append(fill1);
            fills1.Append(fill2);
            fills1.Append(fill3);
            fills1.Append(fill4);
            fills1.Append(fill5);
            fills1.Append(fill6);
            fills1.Append(fill7);
            Borders borders1 = new Borders() { Count = (UInt32Value)1U };

            Border border1 = new Border();
            LeftBorder leftBorder1 = new LeftBorder();
            RightBorder rightBorder1 = new RightBorder();
            TopBorder topBorder1 = new TopBorder();
            BottomBorder bottomBorder1 = new BottomBorder();
            DiagonalBorder diagonalBorder1 = new DiagonalBorder();

            border1.Append(leftBorder1);
            border1.Append(rightBorder1);
            border1.Append(topBorder1);
            border1.Append(bottomBorder1);
            border1.Append(diagonalBorder1);

            borders1.Append(border1);

            CellStyleFormats cellStyleFormats1 = new CellStyleFormats() { Count = (UInt32Value)1U };
            CellFormat cellFormat1 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)0U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)0U };
            cellStyleFormats1.Append(cellFormat1);

            CellFormats cellFormats1 = new CellFormats() { Count = (UInt32Value)14U };
            //styleIndex 0 used for TextNoFill
            CellFormat cellFormat2 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)0U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U };
            Alignment alignment2 = new Alignment() { Vertical = VerticalAlignmentValues.Top, WrapText = true, Horizontal = HorizontalAlignmentValues.Left };
            cellFormat2.Append(alignment2);

            //textYellowFill cell styleIndex 1
            CellFormat cellFormat_YellowFill = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)0U, FillId = (UInt32Value)2U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFill = true };
            Alignment alignment = new Alignment() { Vertical = VerticalAlignmentValues.Top, WrapText = true,Horizontal=HorizontalAlignmentValues.Left};
            cellFormat_YellowFill.Append(alignment);


            //textBoldYellowFill cell styleIndex 2
            CellFormat cellFormat_YellowFillBold = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)1U, FillId = (UInt32Value)2U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFont = true, ApplyFill = true };
            Alignment alignmentYB = new Alignment() { Vertical = VerticalAlignmentValues.Top, WrapText = true, Horizontal = HorizontalAlignmentValues.Left };
            cellFormat_YellowFillBold.Append(alignmentYB);

            //textBold and //NumberBold styleIndex 3
            CellFormat cellFormat_TextNumberBold = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)1U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFont = true, ApplyFill = true };
            Alignment alignmentBT = new Alignment() { Vertical = VerticalAlignmentValues.Top, WrapText = true};
            cellFormat_TextNumberBold.Append(alignmentBT);

            //textBoldDarkGreyFill styleIndex 4
            CellFormat cellFormat_DarkGreyFillBold = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)1U, FillId = (UInt32Value)3U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFont = true, ApplyFill = true };
            Alignment alignmentDarkGreyFillBold = new Alignment() { Vertical = VerticalAlignmentValues.Top, WrapText = true };//Code modified to allow text wrap |Ejaz Waquif DT:23/Mar/2015
            cellFormat_DarkGreyFillBold.Append(alignmentDarkGreyFillBold);

            //textBoldOrangeFill cell styleIndex 5
            CellFormat cellFormat_OrangeFillBold = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)1U, FillId = (UInt32Value)4U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFont = true, ApplyFill = true };

            //hidden cell styleIndex 6
            CellFormat cellFormat_Hidden = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)2U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyProtection = true };
            Protection protection1 = new Protection() { Hidden = true };
            cellFormat_Hidden.Append(protection1);

            //styleIndex 7
            CellFormat cellFormat9 = new CellFormat() { NumberFormatId = (UInt32Value)164U, FontId = (UInt32Value)0U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyNumberFormat = true };

            //percentage cell styleIndex 8
            CellFormat cellFormat10 = new CellFormat() { NumberFormatId = (UInt32Value)164U, FontId = (UInt32Value)0U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyNumberFormat = true, ApplyProtection = true };

            //percent cell styleIndex 9
            CellFormat cellFormat_Percent = new CellFormat() { NumberFormatId = (UInt32Value)9U, FontId = (UInt32Value)0U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyNumberFormat = true };
            Alignment alignmentPercent = new Alignment() { Vertical = VerticalAlignmentValues.Top, WrapText = true };
            cellFormat_Percent.Append(alignmentPercent);

            //unlocked cell styleIndex 10
            CellFormat cellFormat_unlock = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)0U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyProtection = true };

            Alignment alignment15 = new Alignment() { Vertical = VerticalAlignmentValues.Top, WrapText = true,Horizontal=HorizontalAlignmentValues.Left };
            cellFormat_unlock.Append(alignment15);
            Protection protection2 = new Protection() { Locked = false };
            cellFormat_unlock.Append(protection2);
                        
            //textblue fill styleIndex 11
            CellFormat cellFormat_BlueFill = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)2U, FillId = (UInt32Value)5U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFont = true, ApplyFill = true, ApplyAlignment = true };
            Alignment alignment13 = new Alignment() { Vertical = VerticalAlignmentValues.Top,WrapText=true };
            cellFormat_BlueFill.Append(alignment13);

            //DarkGrey fill styleIndex 12
            CellFormat cellFormat_DarkGreyFill = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)3U, FillId = (UInt32Value)6U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFont = true, ApplyFill = true, ApplyAlignment = true };
            Alignment alignment14 = new Alignment() { Vertical = VerticalAlignmentValues.Top, WrapText = true };
            cellFormat_DarkGreyFill.Append(alignment14);

            //percentBold cell styleIndex 13
            CellFormat cellFormat_PercentBold = new CellFormat() { NumberFormatId = (UInt32Value)9U, FontId = (UInt32Value)1U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyNumberFormat = true, ApplyFont = true };
            Alignment alignmentPercentBold = new Alignment() { Vertical = VerticalAlignmentValues.Top, WrapText = true };
            cellFormat_PercentBold.Append(alignmentPercentBold);

            //styleIndex 14 used for TextNoFill Right align
            CellFormat cellFormat_RightAlign = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)0U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyAlignment = true, ApplyNumberFormat = true };
            Alignment alignmentRight = new Alignment() { Vertical = VerticalAlignmentValues.Top, WrapText = true, Horizontal = HorizontalAlignmentValues.Right };
            cellFormat_RightAlign.Append(alignmentRight);






            cellFormats1.Append(cellFormat2);
            cellFormats1.Append(cellFormat_YellowFill);
            cellFormats1.Append(cellFormat_YellowFillBold);
            cellFormats1.Append(cellFormat_TextNumberBold);
            cellFormats1.Append(cellFormat_DarkGreyFillBold);
            cellFormats1.Append(cellFormat_OrangeFillBold);
            cellFormats1.Append(cellFormat_Hidden);
            cellFormats1.Append(cellFormat9);
            cellFormats1.Append(cellFormat10);
            cellFormats1.Append(cellFormat_Percent);
            cellFormats1.Append(cellFormat_unlock);
            cellFormats1.Append(cellFormat_BlueFill);
            cellFormats1.Append(cellFormat_DarkGreyFill);
            cellFormats1.Append(cellFormat_PercentBold);
            cellFormats1.Append(cellFormat_RightAlign);

            CellStyles cellStyles1 = new CellStyles() { Count = (UInt32Value)1U };
            CellStyle cellStyle1 = new CellStyle() { Name = "Normal", FormatId = (UInt32Value)0U, BuiltinId = (UInt32Value)0U };

            cellStyles1.Append(cellStyle1);
            DifferentialFormats differentialFormats1 = new DifferentialFormats() { Count = (UInt32Value)0U };
            TableStyles tableStyles1 = new TableStyles() { Count = (UInt32Value)0U, DefaultTableStyle = "TableStyleMedium9", DefaultPivotStyle = "PivotStyleLight16" };

            Colors colors1 = new Colors();

            MruColors mruColors1 = new MruColors();
            Color color = new Color() { Rgb = "FFFFFF00" };

            mruColors1.Append(color);

            colors1.Append(mruColors1);

            stylesheet1.Append(numberingFormats1);
            stylesheet1.Append(fonts1);
            stylesheet1.Append(fills1);
            stylesheet1.Append(borders1);
            stylesheet1.Append(cellStyleFormats1);
            stylesheet1.Append(cellFormats1);
            stylesheet1.Append(cellStyles1);
            stylesheet1.Append(differentialFormats1);
            stylesheet1.Append(tableStyles1);
            stylesheet1.Append(colors1);

            workbookStylesPart1.Stylesheet = stylesheet1;
        }

        /// <summary>
        /// Takes base64 img string and convert it to image file then insert it on excel row and cell number given to it        
        /// </summary>
        /// <param name="worksheetPart"></param>
        /// <param name="imageFileHtml"></param>
        /// <param name="colNumber"></param>
        /// <param name="rowNumber"></param>
        /// <param name="offsetX"></param>
        /// <param name="offsetY"></param>
        private void InsertImage(WorksheetPart worksheetPart, string imgBase64String, string imgType, int maxWidth, int maxHeight, int colNumber, int rowNumber, int offsetX, int offsetY)
        {
            //string filename = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath + "SampleTemplate\\Feedback_Rating.png";
            //imageFileHtml = @"<p>&nbsp;</p><img width='300' height='275' src='data:image/png;base64,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'><br>";
            //string imgBase64String = "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";
            
            //string imageFileName = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath + "SampleTemplate\\Image_" + Guid.NewGuid() + "."+imgType;
            //Considering only png format
            string imageFileName = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath + "SampleTemplate\\Image_" + Guid.NewGuid() + ".png";

            File.WriteAllBytes(imageFileName, Convert.FromBase64String(imgBase64String));

            //System.Drawing.Image imgFile = System.Drawing.Image.FromFile(imageFileName);
            //int wd = imgFile.Width;
            //int ht = imgFile.Height;
            
            try
            {
                if (!string.IsNullOrEmpty(imgBase64String) && !string.IsNullOrEmpty(imgType))
                {
                    var drawingsPart = worksheetPart.DrawingsPart ?? worksheetPart.AddNewPart<DrawingsPart>();

                    if (!worksheetPart.Worksheet.ChildElements.OfType<Drawing>().Any())
                    {
                        worksheetPart.Worksheet.Append(new Drawing { Id = worksheetPart.GetIdOfPart(drawingsPart) });
                    }

                    if (drawingsPart.WorksheetDrawing == null)
                    {
                        drawingsPart.WorksheetDrawing = new Xdr.WorksheetDrawing();
                    }

                    var worksheetDrawing = drawingsPart.WorksheetDrawing;

                    //assuming image is in png format
                    var imagePart = drawingsPart.AddImagePart(ImagePartType.Png);
                    
                    using (var stream = new FileStream(imageFileName, FileMode.Open))
                    {
                        imagePart.FeedData(stream);
                    }

                    var positioningEMU = new ImagePositioningEMU(imageFileName, maxWidth, maxHeight, offsetX, offsetY);

                    var extentsCx = positioningEMU.Width;
                    long extentsCy = positioningEMU.Height;

                    if (extentsCy > positioningEMU.MaxHeight || extentsCx > positioningEMU.MaxWidth)
                    {
                        var scaleX = (double)extentsCx / (double)positioningEMU.MaxWidth;
                        var scaleY = (double)extentsCy / (double)positioningEMU.MaxHeight;
                        var scale = Math.Max(scaleX, scaleY);
                        extentsCx = (int)((double)extentsCx / scale);
                        extentsCy = (int)((double)extentsCy / scale);
                    }

                    var colOffset = positioningEMU.OffsetX;
                    var rowOffset = positioningEMU.OffsetY;

                    var nvps = worksheetDrawing.Descendants<Xdr.NonVisualDrawingProperties>();
                    var nvpId = nvps.Count() > 0 ?
                        (UInt32Value)worksheetDrawing.Descendants<Xdr.NonVisualDrawingProperties>().Max(p => p.Id.Value) + 1 :
                        1U;

                    var oneCellAnchor = new Xdr.OneCellAnchor(
                        new Xdr.FromMarker
                        {
                            ColumnId = new Xdr.ColumnId((colNumber - 1).ToString()),
                            RowId = new Xdr.RowId((rowNumber - 1).ToString()),
                            ColumnOffset = new Xdr.ColumnOffset(colOffset.ToString()),
                            RowOffset = new Xdr.RowOffset(rowOffset.ToString())
                        },
                        new Xdr.Extent { Cx = extentsCx, Cy = extentsCy },
                        new Xdr.Picture(
                            new Xdr.NonVisualPictureProperties(
                                new Xdr.NonVisualDrawingProperties { Id = nvpId, Name = "Picture " + nvpId, Description = imageFileName },
                                new Xdr.NonVisualPictureDrawingProperties(new A.PictureLocks { NoChangeAspect = true })
                            ),
                            new Xdr.BlipFill(
                                new A.Blip { Embed = drawingsPart.GetIdOfPart(imagePart), CompressionState = A.BlipCompressionValues.Print },
                                new A.Stretch(new A.FillRectangle())
                            ),
                            new Xdr.ShapeProperties(
                                new A.Transform2D(
                                    new A.Offset { X = 0, Y = 0 },
                                    new A.Extents { Cx = extentsCx, Cy = extentsCy }
                                ),
                                new A.PresetGeometry { Preset = A.ShapeTypeValues.Rectangle }
                            )
                        ),
                        new Xdr.ClientData()
                    );

                    worksheetDrawing.Append(oneCellAnchor);
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

        

    }

    public class ImagePositioningEMU
    {
        private readonly int _maxWidth;
        private readonly int _maxHeight;

        private readonly int _width;
        private readonly int _height;

        private readonly int _offsetX;
        private readonly int _offsetY;

        private readonly float _verticalResolution;
        private readonly float _horizontalResolution;

        public int MaxWidth
        {
            get { return ConvertToEmu(_maxWidth, _horizontalResolution); }
        }

        public int MaxHeight
        {
            get { return ConvertToEmu(_maxHeight, _verticalResolution); }
        }

        public int Width
        {
            get { return ConvertToEmu(_width, _horizontalResolution); }
        }

        public int Height
        {
            get { return ConvertToEmu(_height, _verticalResolution); }
        }

        public int OffsetX
        {
            get { return ConvertToEmu(_offsetX, _horizontalResolution); }
        }

        public int OffsetY
        {
            get { return ConvertToEmu(_offsetY, _verticalResolution); }
        }

        private int ConvertToEmu(int pixels, float resolution)
        {
            return (int)(914400 * pixels / resolution);
        }

        public ImagePositioningEMU(string fileName, int maxWidth, int maxHeight, int offsetX, int offsetY)
        {
            _maxWidth = maxWidth;
            _maxHeight = maxHeight;
            _offsetX = offsetX;
            _offsetY = offsetY;

            using (var bitmap = new System.Drawing.Bitmap(fileName))
            {
                _width = bitmap.Width;
                _height = bitmap.Height;
                _horizontalResolution = bitmap.HorizontalResolution;
                _verticalResolution = bitmap.VerticalResolution;
            }
        }
    }

}