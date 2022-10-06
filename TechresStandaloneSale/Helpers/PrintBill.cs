using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Linq;
using System.Printing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using TechresStandaloneSale.Interface;
using TechresStandaloneSale.Models;
using TechresStandaloneSale.Models.Response;
using TechresStandaloneSale.Template;
using TechresStandaloneSale.ViewModels;

namespace TechresStandaloneSale.Helpers
{
    public class PrintBill
    {
        public class PrintText : BaseViewModel, ICacheService, IDeserializer, IErrorLogger
        {
            Restaurant restaurant = (Restaurant)Utils.Utils.GetCacheValue(Constants.CURRENT_RESTAURANT);
            Branch branch = (Branch)Utils.Utils.GetCacheValue(Constants.CURRENT_BRANCH);
            User currentUser = (User)Utils.Utils.GetCacheValue(Constants.CURRENT_USER);
            SettingData currentSetting = (SettingData)Utils.Utils.GetCacheValue(Constants.CURRENT_SETTING);
            public void PrintFoodCook(string PrintName, List<OrderDetailPrint> orderDetails, string TableName, string OrderCode, string EmployeeName, int size)
            {
                if(TableName == null)
                {
                    TableName = "Mang về";
                }
                try
                {
                    System.Windows.Controls.PrintDialog dialog = new System.Windows.Controls.PrintDialog();

                    if (!string.IsNullOrEmpty(PrintName))
                    {
                        dialog.PrintQueue = new PrintQueue(new PrintServer(), PrintName);
                    }
                    if (size == (int)SizePrintEnum.SIZE_80MM)
                    {
                        PrintFoodCook printFoodCook = new PrintFoodCook();
                        printFoodCook.Margin = new Thickness(15, 0, 0, 0);
                        printFoodCook.FoodCall.Text = "MÓN MỚI";
                        printFoodCook.TableName.Text = TableName.Contains("Chọn bàn") ? "" : TableName;

                        printFoodCook.Time.Text = Utils.Utils.GetDateHourFormatVN(DateTime.Now);
                        printFoodCook.OrderCode.Text = OrderCode;
                        printFoodCook.EmployeeName.Text = string.IsNullOrEmpty(EmployeeName) ? currentUser.Name : EmployeeName;

                        List<Models.OrderDetailPrint> f = orderDetails.Where(x => x.Quantity != 0).ToList();
                        if (f.Count > 0)
                        {
                            for (int i = 1; i <= f.Count; i++)
                            {

                                TableRow currentRow = new TableRow();
                                TableRow NoteRow = new TableRow();
                                List<TableRow> tableRows = new List<TableRow>();
                                currentRow.FontSize = 14;
                                currentRow.Background = System.Windows.Media.Brushes.White;
                                Paragraph foodName = new Paragraph();
                                foodName.Inlines.Add(new TextBlock()
                                {
                                    Text = Utils.Utils.ConvertToUpperAndLower(f[i - 1].FoodNameString),
                                    TextWrapping = TextWrapping.Wrap
                                });
                                if (f[i - 1].OrderDetailAdditions != null && f[i - 1].OrderDetailAdditions.Count > 0)
                                {
                                    for (int j = 0; j < f[i - 1].OrderDetailAdditions.Count; j++)
                                    {

                                        TableRow tableRow = new TableRow();
                                        tableRow.FontSize = 13;
                                        tableRow.Background = System.Windows.Media.Brushes.White;
                                        Paragraph tablerow = new Paragraph();
                                        tablerow.Inlines.Add(new TextBlock()
                                        {
                                            Text = f[i - 1].OrderDetailAdditions[j].FoodNameAdditionPrintBill,
                                            TextWrapping = TextWrapping.Wrap,
                                            FontStyle = FontStyles.Italic,
                                        });
                                        tablerow.TextAlignment = System.Windows.TextAlignment.Left;
                                        tablerow.Margin = new Thickness(15, 0, 0, 0);
                                        TableCell cellAddition = new TableCell(tablerow);
                                        cellAddition.ColumnSpan = 3;
                                        tableRow.Cells.Add(cellAddition);
                                        tableRows.Add(tableRow);
                                    }
                                }
                                if (!string.IsNullOrEmpty(f[i - 1].Note))
                                {
                                    NoteRow.FontSize = 13;
                                    NoteRow.Background = System.Windows.Media.Brushes.White;
                                    Paragraph note = new Paragraph();
                                    note.Inlines.Add(new TextBlock()
                                    {
                                        Text = "* " + f[i - 1].Note,
                                        TextWrapping = TextWrapping.Wrap,
                                        FontStyle = FontStyles.Italic,
                                    });
                                    note.TextAlignment = System.Windows.TextAlignment.Left;
                                    note.Margin = new Thickness(15, 0, 0, 0);
                                    TableCell cellNote = new TableCell(note);
                                    cellNote.ColumnSpan = 3;
                                    NoteRow.Cells.Add(cellNote);
                                }
                                foodName.TextAlignment = System.Windows.TextAlignment.Left;
                                foodName.Margin = new Thickness(15, 0, 0, 0);

                                TableCell cell = new TableCell(foodName);
                                // cell.BorderThickness = new Thickness(0, 1, 0, 0);
                                cell.ColumnSpan = 3;
                                // cell.BorderBrush = System.Windows.Media.Brushes.Black;
                                // cell.LineHeight = 50;
                                currentRow.Cells.Add(cell);

                                Paragraph quantity = new Paragraph();

                                quantity.Inlines.Add(new Run("x "));
                                quantity.Inlines.Add(new Run
                                {
                                    Text = (f[i - 1].Quantity - f[i - 1].PrintedQuantity).ToString(),
                                    FontSize = 16,
                                    FontWeight = FontWeights.Bold
                                });
                                //quantity.Margin = new Thickness(0, 3, 0, 0);
                                quantity.TextAlignment = System.Windows.TextAlignment.Center;
                                TableCell cellQuantity = new TableCell(quantity);
                                //  cellQuantity.BorderThickness = new Thickness(1, 1, 0, 0);
                                cellQuantity.ColumnSpan = 1;
                                //  cellQuantity.BorderBrush = System.Windows.Media.Brushes.Black;
                                currentRow.Cells.Add(cellQuantity);

                                printFoodCook.TableRow.RowGroups[0].Rows.Add(currentRow);
                                printFoodCook.TableRow.RowGroups[0].Rows.Add(NoteRow);
                                for (int k = 1; k <= tableRows.Count(); k++)
                                {
                                    printFoodCook.TableRow.RowGroups[0].Rows.Add(tableRows[k - 1]);
                                }
                                if (i != f.Count)
                                {
                                    TableRow tableRow = new TableRow();
                                    tableRow.FontSize = 10;
                                    tableRow.Background = System.Windows.Media.Brushes.White;
                                    Paragraph paragraph = new Paragraph(new Run(Utils.Utils.GetLine80()));
                                    TableCell cellLine = new TableCell(paragraph);
                                    cellLine.ColumnSpan = 4;
                                    tableRow.Cells.Add(cellLine);
                                    printFoodCook.TableRow.RowGroups[0].Rows.Add(tableRow);

                                }
                                printFoodCook.billDocument.MaxPageWidth = 290;
                            }
                        }

                        FlowDocument doc = printFoodCook.billDocument;
                        doc.PagePadding = new Thickness(0);

                        IDocumentPaginatorSource idSource = doc;

                        if(PrintName != "")
                        {
                            dialog.PrintDocument(idSource.DocumentPaginator, "");
                        }    
                        
                        
                    }
                    else if (size == (int)SizePrintEnum.SIZE_58MM)
                    {
                        PrintFoodCook58MM printFoodCook = new PrintFoodCook58MM();

                        printFoodCook.FoodCall.Text = "MÓN MỚI";
                        printFoodCook.TableName.Text = TableName.Contains("Chọn bàn") ? "" : TableName;
                        printFoodCook.Time.Text = Utils.Utils.GetDateHourFormatVN(DateTime.Now);
                        printFoodCook.OrderCode.Text = OrderCode;
                        printFoodCook.EmployeeName.Text = string.IsNullOrEmpty(EmployeeName) ? currentUser.Name : EmployeeName;
                        List<Models.OrderDetailPrint> f = orderDetails.Where(x => x.Quantity != 0).ToList();
                        if (f.Count > 0)
                        {
                            for (int i = 1; i <= f.Count; i++)
                            {

                                TableRow currentRow = new TableRow();
                                TableRow NoteRow = new TableRow();
                                List<TableRow> tableRows = new List<TableRow>();
                                currentRow.FontSize = 12;
                                currentRow.Background = System.Windows.Media.Brushes.White;
                                Paragraph foodName = new Paragraph();
                                foodName.Inlines.Add(new TextBlock()
                                {
                                    Text = Utils.Utils.ConvertToUpperAndLower(f[i - 1].FoodNameString),
                                    TextWrapping = TextWrapping.Wrap

                                });
                                if (f[i - 1].OrderDetailAdditions != null && f[i - 1].OrderDetailAdditions.Count > 0)
                                {
                                    for (int j = 0; j < f[i - 1].OrderDetailAdditions.Count; j++)
                                    {
                                        TableRow tableRow = new TableRow();
                                        tableRow.FontSize = 11;
                                        tableRow.Background = System.Windows.Media.Brushes.White;
                                        Paragraph tablerow = new Paragraph();
                                        tablerow.Inlines.Add(new TextBlock()
                                        {
                                            Text = f[i - 1].OrderDetailAdditions[j].FoodNameAdditionPrintBill,
                                            TextWrapping = TextWrapping.Wrap,
                                            FontStyle = FontStyles.Italic,
                                        });
                                        tablerow.TextAlignment = System.Windows.TextAlignment.Left;
                                        tablerow.Margin = new Thickness(5, 0, 0, 0);
                                        TableCell cellAddition = new TableCell(tablerow);
                                        cellAddition.ColumnSpan = 3;
                                        tableRow.Cells.Add(cellAddition);
                                        tableRows.Add(tableRow);
                                    }
                                }
                                if (!string.IsNullOrEmpty(f[i - 1].Note))
                                {
                                    NoteRow.FontSize = 11;
                                    NoteRow.Background = System.Windows.Media.Brushes.White;
                                    Paragraph note = new Paragraph();
                                    note.Inlines.Add(new TextBlock()
                                    {
                                        Text = "* " + f[i - 1].Note,
                                        TextWrapping = TextWrapping.Wrap,
                                        FontStyle = FontStyles.Italic,
                                    });
                                    note.TextAlignment = System.Windows.TextAlignment.Left;
                                    note.Margin = new Thickness(5, 0, 0, 0);
                                    TableCell cellNote = new TableCell(note);
                                    cellNote.ColumnSpan = 3;
                                    NoteRow.Cells.Add(cellNote);
                                }
                                foodName.TextAlignment = System.Windows.TextAlignment.Left;
                                foodName.Margin = new Thickness(5, 0, 0, 0);

                                TableCell cell = new TableCell(foodName);
                                // cell.BorderThickness = new Thickness(0, 1, 0, 0);
                                cell.ColumnSpan = 3;
                                // cell.BorderBrush = System.Windows.Media.Brushes.Black;
                                // cell.LineHeight = 50;
                                currentRow.Cells.Add(cell);

                                Paragraph quantity = new Paragraph();
                                quantity.Inlines.Add(new Run("x "));
                                quantity.Inlines.Add(new Run
                                {
                                    Text = (f[i - 1].Quantity - f[i - 1].PrintedQuantity).ToString(),
                                    FontSize = 14,
                                    FontWeight = FontWeights.Bold
                                });
                                //quantity.Margin = new Thickness(0, 3, 0, 0);
                                quantity.TextAlignment = System.Windows.TextAlignment.Center;
                                TableCell cellQuantity = new TableCell(quantity);
                                //  cellQuantity.BorderThickness = new Thickness(1, 1, 0, 0);
                                cellQuantity.ColumnSpan = 1;
                                //  cellQuantity.BorderBrush = System.Windows.Media.Brushes.Black;
                                currentRow.Cells.Add(cellQuantity);

                                printFoodCook.TableRow.RowGroups[0].Rows.Add(currentRow);
                                printFoodCook.TableRow.RowGroups[0].Rows.Add(NoteRow);
                                for (int k = 1; k <= tableRows.Count(); k++)
                                {
                                    printFoodCook.TableRow.RowGroups[0].Rows.Add(tableRows[k - 1]);
                                }
                                if (i != f.Count)
                                {
                                    TableRow tableRow = new TableRow();
                                    tableRow.FontSize = 8;
                                    tableRow.Background = System.Windows.Media.Brushes.White;
                                    Paragraph paragraph = new Paragraph(new Run(Utils.Utils.GetLine80()));
                                    TableCell cellLine = new TableCell(paragraph);
                                    cellLine.ColumnSpan = 4;
                                    tableRow.Cells.Add(cellLine);
                                    printFoodCook.TableRow.RowGroups[0].Rows.Add(tableRow);

                                }
                                printFoodCook.billDocument.MaxPageWidth = 210;
                            }
                        }

                        FlowDocument doc = printFoodCook.billDocument;
                        doc.PagePadding = new Thickness(0);

                        IDocumentPaginatorSource idSource = doc;

                        dialog.PrintDocument(idSource.DocumentPaginator, "");
                    }

                }
                catch (Exception ex)
                {

                    WriteLog.logs(ex.Message);
                }

            }

            public void PrintFoodCookByMobliePrint(string PrintName, List<OrderDetail> orderDetails, string TableName, string OrderCode, string EmployeeName, int size)
            {
                try
                {
                    Application.Current.Dispatcher.Invoke((Action)delegate
                    {
                        System.Windows.Controls.PrintDialog dialog = new System.Windows.Controls.PrintDialog();

                        if (!string.IsNullOrEmpty(PrintName))
                        {
                            dialog.PrintQueue = new PrintQueue(new PrintServer(), PrintName);
                        }
                        if (size == (int)SizePrintEnum.SIZE_80MM)
                        {
                            PrintFoodCook printFoodCook = new PrintFoodCook();

                            printFoodCook.FoodCall.Text = "MÓN MỚI";
                            printFoodCook.TableName.Text = TableName.Contains("Chọn bàn") ? "" : TableName;
                            printFoodCook.Time.Text = Utils.Utils.GetDateHourFormatVN(DateTime.Now);
                            printFoodCook.OrderCode.Text = OrderCode;
                            printFoodCook.EmployeeName.Text = string.IsNullOrEmpty(EmployeeName) ? currentUser.Name : EmployeeName;
                            List<OrderDetail> f = orderDetails.Where(x => x.Quantity != 0).ToList();
                            if (f.Count > 0)
                            {
                                for (int i = 1; i <= f.Count; i++)
                                {

                                    TableRow currentRow = new TableRow();
                                    TableRow NoteRow = new TableRow();
                                    List<TableRow> tableRows = new List<TableRow>();
                                    currentRow.FontSize = 14;
                                    currentRow.Background = System.Windows.Media.Brushes.White;
                                    Paragraph foodName = new Paragraph();
                                    foodName.Inlines.Add(new TextBlock()
                                    {
                                        Text = Utils.Utils.ConvertToUpperAndLower(f[i - 1].FoodNameString),
                                        TextWrapping = TextWrapping.Wrap

                                    });
                                    if (f[i - 1].OrderDetailAdditions != null && f[i - 1].OrderDetailAdditions.Count > 0)
                                    {
                                        for (int j = 0; j < f[i - 1].OrderDetailAdditions.Count; j++)
                                        {
                                            TableRow tableRow = new TableRow();
                                            tableRow.FontSize = 13;
                                            tableRow.Background = System.Windows.Media.Brushes.White;
                                            Paragraph tablerow = new Paragraph();
                                            tablerow.Inlines.Add(new TextBlock()
                                            {
                                                Text = f[i - 1].OrderDetailAdditions[j].FoodNameValue,
                                                TextWrapping = TextWrapping.Wrap,
                                                FontStyle = FontStyles.Italic,
                                            });
                                            tablerow.TextAlignment = System.Windows.TextAlignment.Left;
                                            tablerow.Margin = new Thickness(5, 0, 0, 0);
                                            TableCell cellAddition = new TableCell(tablerow);
                                            cellAddition.ColumnSpan = 3;
                                            tableRow.Cells.Add(cellAddition);
                                            tableRows.Add(tableRow);
                                        }
                                    }
                                    if (!string.IsNullOrEmpty(f[i - 1].Note))
                                    {
                                        NoteRow.FontSize = 13;
                                        NoteRow.Background = System.Windows.Media.Brushes.White;
                                        Paragraph note = new Paragraph();
                                        note.Inlines.Add(new TextBlock()
                                        {
                                            Text = "* " + f[i - 1].Note,
                                            TextWrapping = TextWrapping.Wrap,
                                            FontStyle = FontStyles.Italic,
                                        });
                                        note.TextAlignment = System.Windows.TextAlignment.Left;
                                        note.Margin = new Thickness(5, 0, 0, 0);
                                        TableCell cellNote = new TableCell(note);
                                        cellNote.ColumnSpan = 3;
                                        NoteRow.Cells.Add(cellNote);
                                    }
                                    foodName.TextAlignment = System.Windows.TextAlignment.Left;
                                    foodName.Margin = new Thickness(5, 0, 0, 0);

                                    TableCell cell = new TableCell(foodName);
                                    // cell.BorderThickness = new Thickness(0, 1, 0, 0);
                                    cell.ColumnSpan = 3;
                                    // cell.BorderBrush = System.Windows.Media.Brushes.Black;
                                    // cell.LineHeight = 50;
                                    currentRow.Cells.Add(cell);

                                    Paragraph quantity = new Paragraph();
                                    quantity.Inlines.Add(new Run("x "));
                                    quantity.Inlines.Add(new Run
                                    {
                                        Text = f[i - 1].Quantity.ToString(),
                                        FontSize = 16,
                                        FontWeight = FontWeights.Bold
                                    });
                                    //quantity.Margin = new Thickness(0, 3, 0, 0);
                                    quantity.TextAlignment = System.Windows.TextAlignment.Center;
                                    TableCell cellQuantity = new TableCell(quantity);
                                    //  cellQuantity.BorderThickness = new Thickness(1, 1, 0, 0);
                                    cellQuantity.ColumnSpan = 1;
                                    //  cellQuantity.BorderBrush = System.Windows.Media.Brushes.Black;
                                    currentRow.Cells.Add(cellQuantity);

                                    printFoodCook.TableRow.RowGroups[0].Rows.Add(currentRow);
                                    printFoodCook.TableRow.RowGroups[0].Rows.Add(NoteRow);
                                    for (int k = 1; k <= tableRows.Count(); k++)
                                    {
                                        printFoodCook.TableRow.RowGroups[0].Rows.Add(tableRows[k - 1]);
                                    }
                                    if (i != f.Count)
                                    {
                                        TableRow tableRow = new TableRow();
                                        tableRow.FontSize = 10;
                                        tableRow.Background = System.Windows.Media.Brushes.White;
                                        Paragraph paragraph = new Paragraph(new Run(Utils.Utils.GetLine80()));
                                        TableCell cellLine = new TableCell(paragraph);
                                        cellLine.ColumnSpan = 4;
                                        tableRow.Cells.Add(cellLine);
                                        printFoodCook.TableRow.RowGroups[0].Rows.Add(tableRow);

                                    }
                                    printFoodCook.billDocument.MaxPageWidth = 295;
                                }
                            }

                            FlowDocument doc = printFoodCook.billDocument;
                            doc.PagePadding = new Thickness(0);

                            IDocumentPaginatorSource idSource = doc;

                            dialog.PrintDocument(idSource.DocumentPaginator, "");
                        }
                        else if (size == (int)SizePrintEnum.SIZE_58MM)
                        {
                            PrintFoodCook58MM printFoodCook = new PrintFoodCook58MM();

                            printFoodCook.FoodCall.Text = "MÓN MỚI";
                            printFoodCook.TableName.Text = TableName.Contains("Chọn bàn") ? "" : TableName;
                            printFoodCook.Time.Text = Utils.Utils.GetDateHourFormatVN(DateTime.Now);
                            printFoodCook.OrderCode.Text = OrderCode;
                            printFoodCook.EmployeeName.Text = string.IsNullOrEmpty(EmployeeName) ? currentUser.Name : EmployeeName;
                            List<OrderDetail> f = orderDetails.Where(x => x.Quantity != 0).ToList();
                            if (f.Count > 0)
                            {
                                for (int i = 1; i <= f.Count; i++)
                                {

                                    TableRow currentRow = new TableRow();
                                    TableRow NoteRow = new TableRow();
                                    List<TableRow> tableRows = new List<TableRow>();
                                    currentRow.FontSize = 12;
                                    currentRow.Background = System.Windows.Media.Brushes.White;
                                    Paragraph foodName = new Paragraph();
                                    foodName.Inlines.Add(new TextBlock()
                                    {
                                        Text = Utils.Utils.ConvertToUpperAndLower(f[i - 1].FoodNameString),
                                        TextWrapping = TextWrapping.Wrap

                                    });
                                    if (f[i - 1].OrderDetailAdditions != null && f[i - 1].OrderDetailAdditions.Count > 0)
                                    {
                                        for (int j = 0; j < f[i - 1].OrderDetailAdditions.Count; j++)
                                        {
                                            TableRow tableRow = new TableRow();
                                            tableRow.FontSize = 11;
                                            tableRow.Background = System.Windows.Media.Brushes.White;
                                            Paragraph tablerow = new Paragraph();
                                            tablerow.Inlines.Add(new TextBlock()
                                            {
                                                Text = f[i - 1].OrderDetailAdditions[j].FoodNameValue,
                                                TextWrapping = TextWrapping.Wrap,
                                                FontStyle = FontStyles.Italic,
                                            });
                                            tablerow.TextAlignment = System.Windows.TextAlignment.Left;
                                            tablerow.Margin = new Thickness(5, 0, 0, 0);
                                            TableCell cellAddition = new TableCell(tablerow);
                                            cellAddition.ColumnSpan = 3;
                                            tableRow.Cells.Add(cellAddition);
                                            tableRows.Add(tableRow);
                                        }
                                    }
                                    if (!string.IsNullOrEmpty(f[i - 1].Note))
                                    {
                                        NoteRow.FontSize = 11;
                                        NoteRow.Background = System.Windows.Media.Brushes.White;
                                        Paragraph note = new Paragraph();
                                        note.Inlines.Add(new TextBlock()
                                        {
                                            Text = "* " + f[i - 1].Note,
                                            TextWrapping = TextWrapping.Wrap,
                                            FontStyle = FontStyles.Italic,
                                        });
                                        note.TextAlignment = System.Windows.TextAlignment.Left;
                                        note.Margin = new Thickness(5, 0, 0, 0);
                                        TableCell cellNote = new TableCell(note);
                                        cellNote.ColumnSpan = 3;
                                        NoteRow.Cells.Add(cellNote);
                                    }
                                    foodName.TextAlignment = System.Windows.TextAlignment.Left;
                                    foodName.Margin = new Thickness(5, 0, 0, 0);

                                    TableCell cell = new TableCell(foodName);
                                    // cell.BorderThickness = new Thickness(0, 1, 0, 0);
                                    cell.ColumnSpan = 3;
                                    // cell.BorderBrush = System.Windows.Media.Brushes.Black;
                                    // cell.LineHeight = 50;
                                    currentRow.Cells.Add(cell);

                                    Paragraph quantity = new Paragraph();
                                    quantity.Inlines.Add(new Run("x "));
                                    quantity.Inlines.Add(new Run
                                    {
                                        Text = f[i - 1].Quantity.ToString(),
                                        FontSize = 14,
                                        FontWeight = FontWeights.Bold
                                    });
                                    //quantity.Margin = new Thickness(0, 3, 0, 0);
                                    quantity.TextAlignment = System.Windows.TextAlignment.Center;
                                    TableCell cellQuantity = new TableCell(quantity);
                                    //  cellQuantity.BorderThickness = new Thickness(1, 1, 0, 0);
                                    cellQuantity.ColumnSpan = 1;
                                    //  cellQuantity.BorderBrush = System.Windows.Media.Brushes.Black;
                                    currentRow.Cells.Add(cellQuantity);

                                    printFoodCook.TableRow.RowGroups[0].Rows.Add(currentRow);
                                    printFoodCook.TableRow.RowGroups[0].Rows.Add(NoteRow);
                                    for (int k = 1; k <= tableRows.Count(); k++)
                                    {
                                        printFoodCook.TableRow.RowGroups[0].Rows.Add(tableRows[k - 1]);
                                    }
                                    if (i != f.Count)
                                    {
                                        TableRow tableRow = new TableRow();
                                        tableRow.FontSize = 8;
                                        tableRow.Background = System.Windows.Media.Brushes.White;
                                        Paragraph paragraph = new Paragraph(new Run(Utils.Utils.GetLine80()));
                                        TableCell cellLine = new TableCell(paragraph);
                                        cellLine.ColumnSpan = 4;
                                        tableRow.Cells.Add(cellLine);
                                        printFoodCook.TableRow.RowGroups[0].Rows.Add(tableRow);

                                    }
                                    printFoodCook.billDocument.MaxPageWidth = 210;
                                }
                            }

                            FlowDocument doc = printFoodCook.billDocument;
                            doc.PagePadding = new Thickness(0);

                            IDocumentPaginatorSource idSource = doc;

                            dialog.PrintDocument(idSource.DocumentPaginator, "");
                        }

                    });
                }
                catch (Exception ex)
                {

                    WriteLog.logs(ex.Message);
                }

            }
            public void PrintSeaFood(string PrintName, SingleOrderDetailResponse od, long size)
            {
                try
                {
                    System.Windows.Controls.PrintDialog dialog = new System.Windows.Controls.PrintDialog();

                    if (!string.IsNullOrEmpty(PrintName))
                    {
                        dialog.PrintQueue = new PrintQueue(new PrintServer(), PrintName);
                    }
                    if (size == (int)SizePrintEnum.SIZE_80MM)
                    {
                        FoodBill foodBill = new FoodBill();
                        foodBill.ContentFood.Text = "MÓN MỚI";
                        foodBill.FoodName.Text = Utils.Utils.ConvertToUpperAndLower(od.Data.FoodName);
                        foodBill.Quantity.Text = od.Data.Quantity.ToString();
                        foodBill.TableName.Text = od.Data.TableName;
                        foodBill.EmployeeName.Text = od.Data.EmployeeName;
                        foodBill.ComingTime.Text = od.Data.CreatedAtHour;
                        foodBill.ComingDay.Text = od.Data.CreatedAtDay;
                        foodBill.Note.Text = od.Data.Note;
                        foodBill.billDocument.MaxPageWidth = 295;
                        FlowDocument doc = foodBill.billDocument;
                        doc.PagePadding = new Thickness(0);
                        doc.ColumnWidth = dialog.PrintableAreaWidth;
                        doc.PageHeight = dialog.PrintableAreaHeight;
                        IDocumentPaginatorSource idSource = doc;
                        dialog.PrintDocument(idSource.DocumentPaginator, "");
                    }
                    else if (size == (int)SizePrintEnum.SIZE_58MM)
                    {
                        FoodBill58MM foodBill = new FoodBill58MM();
                        foodBill.ContentFood.Text = "MÓN MỚI";
                        foodBill.FoodName.Text = Utils.Utils.ConvertToUpperAndLower(od.Data.FoodName);
                        foodBill.Quantity.Text = od.Data.Quantity.ToString();
                        foodBill.TableName.Text = od.Data.TableName;
                        foodBill.EmployeeName.Text = od.Data.EmployeeName;
                        foodBill.ComingTime.Text = od.Data.CreatedAtHour;
                        foodBill.ComingDay.Text = od.Data.CreatedAtDay;
                        foodBill.Note.Text = od.Data.Note;
                        foodBill.billDocument.MaxPageWidth = 210;
                        FlowDocument doc = foodBill.billDocument;
                        doc.PagePadding = new Thickness(0);
                        doc.ColumnWidth = dialog.PrintableAreaWidth;
                        doc.PageHeight = dialog.PrintableAreaHeight;
                        IDocumentPaginatorSource idSource = doc;
                        dialog.PrintDocument(idSource.DocumentPaginator, "");
                    }

                }
                catch (Exception ex)
                {
                    WriteLog.logs(ex.Message);
                }
            }
            public void PrintFoodCancel(SingleOrderDetailResponse od, string PrintName, long size)
            {
                try
                {
                    System.Windows.Controls.PrintDialog dialog = new System.Windows.Controls.PrintDialog();

                    if (!string.IsNullOrEmpty(PrintName))
                    {
                        dialog.PrintQueue = new PrintQueue(new PrintServer(), PrintName);
                    }
                    if (size == (int)SizePrintEnum.SIZE_80MM)
                    {
                        FoodBill foodBill = new FoodBill();
                        foodBill.ContentFood.Text = "PHIẾU HỦY MÓN";
                        foodBill.FoodName.Text = Utils.Utils.ConvertToUpperAndLower(od.Data.FoodName);
                        foodBill.FoodName.TextDecorations = TextDecorations.Strikethrough;
                        foodBill.Quantity.Text = od.Data.Quantity.ToString();
                        foodBill.Quantity.TextDecorations = TextDecorations.Strikethrough;
                        foodBill.TableName.Text = od.Data.TableName;
                        foodBill.EmployeeName.Text = od.Data.EmployeeName;
                        foodBill.ComingTime.Text = od.Data.CreatedAtHour;
                        foodBill.ComingDay.Text = od.Data.CreatedAtDay;
                        foodBill.Note.Text = od.Data.Note;
                        foodBill.billDocument.MaxPageWidth = 295;

                        FlowDocument doc = foodBill.billDocument;
                        doc.PagePadding = new Thickness(0);


                        doc.ColumnWidth = dialog.PrintableAreaWidth;
                        doc.PageHeight = dialog.PrintableAreaHeight;

                        IDocumentPaginatorSource idSource = doc;


                        dialog.PrintDocument(idSource.DocumentPaginator, "");
                    }
                    else if (size == (int)SizePrintEnum.SIZE_58MM)
                    {
                        FoodBill58MM foodBill = new FoodBill58MM();
                        foodBill.ContentFood.Text = "PHIẾU HỦY MÓN";
                        foodBill.FoodName.Text = Utils.Utils.ConvertToUpperAndLower(od.Data.FoodName);
                        foodBill.FoodName.TextDecorations = TextDecorations.Strikethrough;
                        foodBill.Quantity.Text = od.Data.Quantity.ToString();
                        foodBill.Quantity.TextDecorations = TextDecorations.Strikethrough;
                        foodBill.TableName.Text = od.Data.TableName;
                        foodBill.EmployeeName.Text = od.Data.EmployeeName;
                        foodBill.ComingTime.Text = od.Data.CreatedAtHour;
                        foodBill.ComingDay.Text = od.Data.CreatedAtDay;
                        foodBill.Note.Text = od.Data.Note;
                        foodBill.billDocument.MaxPageWidth = 210;

                        FlowDocument doc = foodBill.billDocument;
                        doc.PagePadding = new Thickness(0);


                        doc.ColumnWidth = dialog.PrintableAreaWidth;
                        doc.PageHeight = dialog.PrintableAreaHeight;

                        IDocumentPaginatorSource idSource = doc;


                        dialog.PrintDocument(idSource.DocumentPaginator, "");
                    }
                }
                catch (Exception ex)
                {
                    WriteLog.logs(ex.Message);
                }

            }
            public void PrintFoodCancel(OrderDetailPrint od, string PrintName, int size)
            {
                try
                {
                    System.Windows.Controls.PrintDialog dialog = new System.Windows.Controls.PrintDialog();

                    if (!string.IsNullOrEmpty(PrintName))
                    {
                        dialog.PrintQueue = new PrintQueue(new PrintServer(), PrintName);
                    }
                    if (size == (int)SizePrintEnum.SIZE_80MM)
                    {
                        FoodBill foodBill = new FoodBill();
                        foodBill.ContentFood.Text = "PHIẾU HỦY MÓN";
                        foodBill.FoodName.Text = Utils.Utils.ConvertToUpperAndLower(od.FoodName);
                        foodBill.FoodName.TextDecorations = TextDecorations.Strikethrough;
                        foodBill.Quantity.Text = od.Quantity.ToString();
                        foodBill.Quantity.TextDecorations = TextDecorations.Strikethrough;
                        foodBill.TableName.Text = od.TableName;
                        //foodBill.TableName.TextDecorations = TextDecorations.Strikethrough;
                        foodBill.EmployeeName.Text = od.EmployeeName;
                        foodBill.ComingTime.Text = od.CreatedAtHour;
                        foodBill.ComingDay.Text = od.CreatedAtDay;
                        foodBill.Note.Text = od.Note;
                        foodBill.billDocument.MaxPageWidth = 295;

                        FlowDocument doc = foodBill.billDocument;
                        doc.PagePadding = new Thickness(0);


                        doc.ColumnWidth = dialog.PrintableAreaWidth;
                        doc.PageHeight = dialog.PrintableAreaHeight;

                        IDocumentPaginatorSource idSource = doc;


                        dialog.PrintDocument(idSource.DocumentPaginator, "");
                    }
                    else if (size == (int)SizePrintEnum.SIZE_58MM)
                    {
                        FoodBill58MM foodBill = new FoodBill58MM();
                        foodBill.ContentFood.Text = "MÓN HỦY";
                        foodBill.FoodName.Text = Utils.Utils.ConvertToUpperAndLower(od.FoodName);
                        foodBill.FoodName.TextDecorations = TextDecorations.Strikethrough;
                        foodBill.Quantity.Text = od.Quantity.ToString();
                        foodBill.Quantity.TextDecorations = TextDecorations.Strikethrough;
                        foodBill.TableName.Text = od.TableName;
                        foodBill.TableName.TextDecorations = TextDecorations.Strikethrough;
                        foodBill.EmployeeName.Text = od.EmployeeName;
                        foodBill.ComingTime.Text = od.CreatedAtHour;
                        foodBill.ComingDay.Text = od.CreatedAtDay;
                        foodBill.Note.Text = od.Note;
                        foodBill.billDocument.MaxPageWidth = 210;

                        FlowDocument doc = foodBill.billDocument;
                        doc.PagePadding = new Thickness(0);


                        doc.ColumnWidth = dialog.PrintableAreaWidth;
                        doc.PageHeight = dialog.PrintableAreaHeight;

                        IDocumentPaginatorSource idSource = doc;


                        dialog.PrintDocument(idSource.DocumentPaginator, "");
                    }
                }
                catch (Exception ex)
                {

                    Console.Write(ex.Message);
                }

            }
            public void PrintSeaFoodMove(SingleOrderDetailResponse od, string PrintName, decimal oldQuantity, decimal quantity, long size)
            {
                try
                {
                    System.Windows.Controls.PrintDialog dialog = new System.Windows.Controls.PrintDialog();

                    if (!string.IsNullOrEmpty(PrintName))
                    {
                        dialog.PrintQueue = new PrintQueue(new PrintServer(), PrintName);
                    }
                    if (size == (int)SizePrintEnum.SIZE_80MM)
                    {
                        MoveFoodBill foodBill = new MoveFoodBill();
                        foodBill.ContentFood.Text = "THAY ĐỔI";
                        foodBill.FoodName.Text = Utils.Utils.ConvertToUpperAndLower(od.Data.FoodName);
                        foodBill.Quantity.Text = (oldQuantity + quantity).ToString();
                        foodBill.OldQuantity.Text = oldQuantity.ToString();
                        foodBill.OldQuantity.TextDecorations = TextDecorations.Strikethrough;
                        foodBill.QuantityContent.TextDecorations = TextDecorations.Strikethrough;
                        foodBill.TableName.Text = od.Data.TableName;
                        foodBill.EmployeeName.Text = od.Data.EmployeeName;
                        foodBill.ComingTime.Text = od.Data.CreatedAtHour;
                        foodBill.ComingDay.Text = od.Data.CreatedAtDay;
                        foodBill.Note.Text = od.Data.Note;
                        foodBill.billDocument.MaxPageWidth = 295;

                        FlowDocument doc = foodBill.billDocument;
                        doc.PagePadding = new Thickness(0);

                        doc.ColumnWidth = dialog.PrintableAreaWidth;
                        doc.PageHeight = dialog.PrintableAreaHeight;

                        IDocumentPaginatorSource idSource = doc;

                        dialog.PrintDocument(idSource.DocumentPaginator, "");
                    }
                    else if (size == (int)SizePrintEnum.SIZE_58MM)
                    {
                        MoveFoodBill58MM foodBill = new MoveFoodBill58MM();
                        foodBill.ContentFood.Text = "THAY ĐỔI";
                        foodBill.FoodName.Text = Utils.Utils.ConvertToUpperAndLower(od.Data.FoodName);
                        foodBill.Quantity.Text = (oldQuantity + quantity).ToString();
                        foodBill.OldQuantity.Text = oldQuantity.ToString();
                        foodBill.OldQuantity.TextDecorations = TextDecorations.Strikethrough;
                        foodBill.QuantityContent.TextDecorations = TextDecorations.Strikethrough;
                        foodBill.TableName.Text = od.Data.TableName;
                        foodBill.EmployeeName.Text = od.Data.EmployeeName;
                        foodBill.ComingTime.Text = od.Data.CreatedAtHour;
                        foodBill.ComingDay.Text = od.Data.CreatedAtDay;
                        foodBill.Note.Text = od.Data.Note;
                        foodBill.billDocument.MaxPageWidth = 210;

                        FlowDocument doc = foodBill.billDocument;
                        doc.PagePadding = new Thickness(0);

                        doc.ColumnWidth = dialog.PrintableAreaWidth;
                        doc.PageHeight = dialog.PrintableAreaHeight;

                        IDocumentPaginatorSource idSource = doc;

                        dialog.PrintDocument(idSource.DocumentPaginator, "");
                    }

                }
                catch (Exception ex)
                {

                    Console.Write(ex.Message);
                }

            }
            public void PrintFoodMove(OrderDetailPrint od, string PrintName, string EmployeeName, string TableName, int size)
            {
                try
                {
                    Application.Current.Dispatcher.Invoke((Action)delegate
                    {
                        System.Windows.Controls.PrintDialog dialog = new System.Windows.Controls.PrintDialog();

                        if (!string.IsNullOrEmpty(PrintName))
                        {
                            dialog.PrintQueue = new PrintQueue(new PrintServer(), PrintName);
                        }
                        if (size == (int)SizePrintEnum.SIZE_80MM)
                        {
                            MoveFoodBill foodBill = new MoveFoodBill();
                            foodBill.ContentFood.Text = "PHIẾU TRẢ";
                            foodBill.FoodName.Text = Utils.Utils.ConvertToUpperAndLower(od.FoodName);
                            foodBill.Quantity.Text = od.Quantity.ToString();
                            foodBill.OldQuantity.Text = od.PrintedQuantity.ToString();
                            foodBill.OldQuantity.TextDecorations = TextDecorations.Strikethrough;
                            foodBill.QuantityContent.TextDecorations = TextDecorations.Strikethrough;
                            foodBill.TableName.Text = TableName;
                            foodBill.EmployeeName.Text = EmployeeName;
                            foodBill.ComingTime.Text = Utils.Utils.GetHourFormatVN(DateTime.Now);
                            foodBill.ComingDay.Text = Utils.Utils.GetDateFormatVN(DateTime.Now);
                            foodBill.Note.Text = od.Note;
                            foodBill.billDocument.MaxPageWidth = 295;

                            FlowDocument doc = foodBill.billDocument;
                            doc.PagePadding = new Thickness(0);

                            doc.ColumnWidth = dialog.PrintableAreaWidth;
                            doc.PageHeight = dialog.PrintableAreaHeight;

                            IDocumentPaginatorSource idSource = doc;

                            dialog.PrintDocument(idSource.DocumentPaginator, "");
                        }
                        else
                        {
                            MoveFoodBill58MM foodBill = new MoveFoodBill58MM();
                            foodBill.ContentFood.Text = "PHIẾU TRẢ";
                            foodBill.FoodName.Text = Utils.Utils.ConvertToUpperAndLower(od.FoodName);
                            foodBill.Quantity.Text = od.Quantity.ToString();
                            foodBill.OldQuantity.Text = od.OldQuantity.ToString();
                            foodBill.OldQuantity.TextDecorations = TextDecorations.Strikethrough;
                            foodBill.QuantityContent.TextDecorations = TextDecorations.Strikethrough;
                            foodBill.TableName.Text = TableName;
                            foodBill.EmployeeName.Text = EmployeeName;
                            foodBill.ComingTime.Text = Utils.Utils.GetHourFormatVN(DateTime.Now);
                            foodBill.ComingDay.Text = Utils.Utils.GetDateFormatVN(DateTime.Now);
                            foodBill.Note.Text = od.Note;
                            foodBill.billDocument.MaxPageWidth = 210;

                            FlowDocument doc = foodBill.billDocument;
                            doc.PagePadding = new Thickness(0);

                            doc.ColumnWidth = dialog.PrintableAreaWidth;
                            doc.PageHeight = dialog.PrintableAreaHeight;

                            IDocumentPaginatorSource idSource = doc;

                            dialog.PrintDocument(idSource.DocumentPaginator, "");
                        }

                    });


                }
                catch (Exception ex)
                {

                    Console.Write(ex.Message);
                }
            }
            public void PrintFoodChangeOfMobliePrint(OrderDetail od, string PrintName, int size)
            {
                try
                {
                    Application.Current.Dispatcher.Invoke((Action)delegate
                    {
                        System.Windows.Controls.PrintDialog dialog = new System.Windows.Controls.PrintDialog();

                        if (!string.IsNullOrEmpty(PrintName))
                        {
                            dialog.PrintQueue = new PrintQueue(new PrintServer(), PrintName);
                        }
                        if (size == (int)SizePrintEnum.SIZE_80MM)
                        {
                            MoveFoodBill foodBill = new MoveFoodBill();
                            foodBill.ContentFood.Text = "THAY ĐỔI";
                            foodBill.FoodName.Text = Utils.Utils.ConvertToUpperAndLower(od.FoodName);
                            foodBill.Quantity.Text = od.Quantity.ToString();
                            foodBill.OldQuantity.Text = od.PrintQuantity.ToString();
                            foodBill.OldQuantity.TextDecorations = TextDecorations.Strikethrough;
                            foodBill.QuantityContent.TextDecorations = TextDecorations.Strikethrough;
                            foodBill.TableName.Text = od.TableName;
                            foodBill.EmployeeName.Text = od.EmployeeName;
                            foodBill.ComingTime.Text = Utils.Utils.GetHourFormatVN(DateTime.Now);
                            foodBill.ComingDay.Text = Utils.Utils.GetDateFormatVN(DateTime.Now);
                            foodBill.Note.Text = od.Note;
                            foodBill.billDocument.MaxPageWidth = 295;
                            FlowDocument doc = foodBill.billDocument;
                            doc.PagePadding = new Thickness(0);
                            doc.ColumnWidth = dialog.PrintableAreaWidth;
                            doc.PageHeight = dialog.PrintableAreaHeight;
                            IDocumentPaginatorSource idSource = doc;
                            dialog.PrintDocument(idSource.DocumentPaginator, "");
                        }
                        else
                        {
                            MoveFoodBill58MM foodBill = new MoveFoodBill58MM();
                            foodBill.ContentFood.Text = "THAY ĐỔI";
                            foodBill.FoodName.Text = Utils.Utils.ConvertToUpperAndLower(od.FoodName);
                            foodBill.Quantity.Text = od.Quantity.ToString();
                            foodBill.OldQuantity.Text = od.PrintQuantity.ToString();
                            foodBill.OldQuantity.TextDecorations = TextDecorations.Strikethrough;
                            foodBill.QuantityContent.TextDecorations = TextDecorations.Strikethrough;
                            foodBill.TableName.Text = od.TableName;
                            foodBill.EmployeeName.Text = od.EmployeeName;
                            foodBill.ComingTime.Text = Utils.Utils.GetHourFormatVN(DateTime.Now);
                            foodBill.ComingDay.Text = Utils.Utils.GetDateFormatVN(DateTime.Now);
                            foodBill.Note.Text = od.Note;
                            foodBill.billDocument.MaxPageWidth = 210;
                            FlowDocument doc = foodBill.billDocument;
                            doc.PagePadding = new Thickness(0);
                            doc.ColumnWidth = dialog.PrintableAreaWidth;
                            doc.PageHeight = dialog.PrintableAreaHeight;
                            IDocumentPaginatorSource idSource = doc;
                            dialog.PrintDocument(idSource.DocumentPaginator, "");
                        }


                    });


                }
                catch (Exception ex)
                {

                    Console.Write(ex.Message);
                }
            }
            public void PrintCook(OrderDetail od, string PrintName, long size)
            {
                try
                {
                    System.Windows.Controls.PrintDialog dialog = new System.Windows.Controls.PrintDialog();
                    if (!string.IsNullOrEmpty(PrintName))
                    {
                        dialog.PrintQueue = new PrintQueue(new PrintServer(), PrintName);
                    }
                    if (size == (int)SizePrintEnum.SIZE_80MM)
                    {
                        FoodBill foodBill = new FoodBill();
                        foodBill.AdditionFood.ItemsSource = od.OrderDetailAdditions;
                        foodBill.FoodName.Text = Utils.Utils.ConvertToUpperAndLower(od.FoodName);
                        foodBill.Quantity.Text = od.Quantity.ToString();
                        foodBill.TableName.Text = od.TableName;
                        foodBill.EmployeeName.Text = od.EmployeeName;
                        foodBill.ComingTime.Text = od.CreatedAtHour;
                        foodBill.ComingDay.Text = od.CreatedAtDay;
                        foodBill.Note.Text = od.Note;

                        foodBill.billDocument.MaxPageWidth = 295;
                        foodBill.billDocument.MaxPageHeight = 3276;
                        FlowDocument doc = foodBill.billDocument;
                        doc.PagePadding = new Thickness(0);

                        doc.ColumnWidth = dialog.PrintableAreaWidth;
                        doc.PageHeight = dialog.PrintableAreaHeight;

                        IDocumentPaginatorSource idSource = doc;
                        dialog.PrintDocument(idSource.DocumentPaginator, "");
                    }
                    else if (size == (int)SizePrintEnum.SIZE_58MM)
                    {
                        FoodBill58MM foodBill = new FoodBill58MM();
                        foodBill.AdditionFood.ItemsSource = od.OrderDetailAdditions;
                        foodBill.FoodName.Text = Utils.Utils.ConvertToUpperAndLower(od.FoodName);
                        foodBill.Quantity.Text = od.Quantity.ToString();
                        foodBill.TableName.Text = od.TableName;
                        foodBill.EmployeeName.Text = od.EmployeeName;
                        foodBill.ComingTime.Text = od.CreatedAtHour;
                        foodBill.ComingDay.Text = od.CreatedAtDay;
                        foodBill.Note.Text = od.Note;

                        foodBill.billDocument.MaxPageWidth = 210;
                        foodBill.billDocument.MaxPageHeight = 3276;
                        FlowDocument doc = foodBill.billDocument;
                        doc.PagePadding = new Thickness(0);

                        doc.ColumnWidth = dialog.PrintableAreaWidth;
                        doc.PageHeight = dialog.PrintableAreaHeight;

                        IDocumentPaginatorSource idSource = doc;
                        dialog.PrintDocument(idSource.DocumentPaginator, "");
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }

            }
            public void PrintViewTallying(List<Money> MoneyList, string AmountTmp, string EmployeeName, string PrintName, string branch, string beginAmount, string currentAmount, int size, string DifferenceAmount, string SpendAmount,string TipAmount)
            {
                try
                {
                    System.Windows.Controls.PrintDialog dialog = new System.Windows.Controls.PrintDialog();
                    if (!string.IsNullOrEmpty(PrintName))
                    {
                        dialog.PrintQueue = new PrintQueue(new PrintServer(), PrintName);
                    }


                    dialog.MinPage = 1;
                    dialog.MaxPage = 1;
                    dialog.PageRangeSelection = PageRangeSelection.AllPages;

                    dialog.UserPageRangeEnabled = false;

                    if (size == (int)SizePrintEnum.SIZE_80MM)
                    {
                        PrintViewTallying bill = new PrintViewTallying();
                        DateTime dateTime = DateTime.Now;

                        if (dateTime.Hour >= 0 && dateTime.Hour <= currentSetting.HourToTakeReport)
                        {
                            bill.Day.Text = Utils.Utils.GetDateHourFormatVN(dateTime.AddDays(-1));
                        }
                        else
                        {
                            bill.Day.Text = Utils.Utils.GetDateHourFormatVN(dateTime);
                        }
                        bill.branch.Text = branch;
                        bill.BeginAmount.Text = beginAmount;
                        bill.SpendingAmountApp.Text = SpendAmount;
                        bill.CurrentAmountApp.Text = currentAmount;
                        bill.TipAmount.Text = TipAmount;
                        bill.EmployeeName.Text = string.IsNullOrEmpty(EmployeeName) ? "" : EmployeeName;
                        bill.AmountApp.Text = string.IsNullOrEmpty(AmountTmp) ? "0" : AmountTmp;
                        bill.DifferenceAmount.Text = string.IsNullOrEmpty(DifferenceAmount) ? "" : DifferenceAmount;
                        for (int i = 1; i <= MoneyList.Count; i++)
                        {
                            TableRow currentRow = new TableRow();
                            currentRow.FontSize = 12;
                            currentRow.Background = System.Windows.Media.Brushes.White;
                            Paragraph denominations = new Paragraph(new Run(Utils.Utils.ConvertToUpperAndLower(MoneyList[i - 1].DenominationsFormat)));
                            denominations.TextAlignment = System.Windows.TextAlignment.Left;
                            denominations.Margin = new Thickness(5, 3, 0, 3);
                            TableCell cell = new TableCell(denominations);
                            cell.ColumnSpan = 2;
                            currentRow.Cells.Add(cell);
                            Paragraph quantity = new Paragraph();
                            quantity.Inlines.Add(new Run
                            {
                                Text = MoneyList[i - 1].Quantity.ToString(),
                                FontSize = 14,
                                FontWeight = FontWeights.Bold,
                            });
                            quantity.TextAlignment = System.Windows.TextAlignment.Center;
                            quantity.Margin = new Thickness(0, 3, 0, 3);
                            TableCell cellQuantity = new TableCell(quantity);
                            cellQuantity.ColumnSpan = 1;
                            currentRow.Cells.Add(cellQuantity);
                            Paragraph UnitPrice = new Paragraph(new Run(MoneyList[i - 1].AmountFormat));
                            UnitPrice.TextAlignment = System.Windows.TextAlignment.Center;
                            UnitPrice.Margin = new Thickness(0, 3, 0, 3);
                            TableCell cellUnitPrice = new TableCell(UnitPrice);
                            cellUnitPrice.ColumnSpan = 2;
                            currentRow.Cells.Add(cellUnitPrice);
                            bill.TableRow.RowGroups[0].Rows.Add(currentRow);
                        }

                        TableRow AllRow = new TableRow();
                        AllRow.FontSize = 16;
                        AllRow.Background = System.Windows.Media.Brushes.White;
                        Paragraph Alldenominations = new Paragraph();
                        Alldenominations.Inlines.Add(new Run
                        {
                            Text = "TC: ",
                            FontSize = 16,
                            FontWeight = FontWeights.Bold,
                        });

                        Alldenominations.TextAlignment = System.Windows.TextAlignment.Left;
                        Alldenominations.Margin = new Thickness(5, 3, 0, 3);
                        TableCell Allcell = new TableCell(Alldenominations);
                        Allcell.ColumnSpan = 2;
                        AllRow.Cells.Add(Allcell);
                        Paragraph Allquantity = new Paragraph();
                        Allquantity.Inlines.Add(new Run
                        {
                            Text = MoneyList.Sum(x => x.Quantity).ToString(),
                            FontSize = 16,
                            FontWeight = FontWeights.Bold,
                        });
                        Allquantity.TextAlignment = System.Windows.TextAlignment.Center;
                        Allquantity.Margin = new Thickness(0, 3, 0, 3);
                        TableCell AllcellQuantity = new TableCell(Allquantity);
                        AllcellQuantity.ColumnSpan = 1;
                        AllRow.Cells.Add(AllcellQuantity);
                        Paragraph AllUnitPrice = new Paragraph();
                        AllUnitPrice.Inlines.Add(new Run
                        {
                            Text = Utils.Utils.FormatMoney(MoneyList.Sum(x => x.Amount)),
                            FontSize = 16,
                            FontWeight = FontWeights.Bold,
                        });
                        AllUnitPrice.TextAlignment = System.Windows.TextAlignment.Center;
                        AllUnitPrice.Margin = new Thickness(0, 3, 0, 3);
                        TableCell AllcellUnitPrice = new TableCell(AllUnitPrice);
                        AllcellUnitPrice.ColumnSpan = 2;
                        AllRow.Cells.Add(AllcellUnitPrice);
                        bill.TableRow.RowGroups[0].Rows.Add(AllRow);
                        bill.billDocument.MaxPageWidth = 295;
                        bill.billDocument.MaxPageHeight = 3276;

                        FlowDocument doc = bill.billDocument;
                        doc.PagePadding = new Thickness(10);


                        doc.ColumnWidth = dialog.PrintableAreaWidth;
                        doc.PageHeight = dialog.PrintableAreaHeight;
                        IDocumentPaginatorSource idSource = doc;

                        dialog.PrintDocument(idSource.DocumentPaginator, "Invoice");
                    }
                    else if (size == (int)SizePrintEnum.SIZE_58MM)
                    {
                        PrintViewTallying bill = new PrintViewTallying();
                        DateTime dateTime = DateTime.Now;
                        if (dateTime.Hour >= 0 && dateTime.Hour <= currentSetting.HourToTakeReport)
                        {
                            bill.Day.Text = Utils.Utils.GetDateFormatVN(dateTime.AddDays(-1));
                        }
                        else
                        {
                            bill.Day.Text = Utils.Utils.GetDateFormatVN(dateTime);
                        }
                        bill.branch.Text = branch;
                        bill.BeginAmount.Text = beginAmount;
                        bill.CurrentAmountApp.Text = currentAmount;
                        bill.EmployeeName.Text = string.IsNullOrEmpty(EmployeeName) ? "" : EmployeeName;
                        bill.AmountApp.Text = string.IsNullOrEmpty(AmountTmp) ? "0" : AmountTmp;
                        for (int i = 1; i <= MoneyList.Count; i++)
                        {
                            TableRow currentRow = new TableRow();
                            currentRow.FontSize = 10;
                            currentRow.Background = System.Windows.Media.Brushes.White;
                            Paragraph denominations = new Paragraph(new Run(Utils.Utils.ConvertToUpperAndLower(MoneyList[i - 1].DenominationsFormat)));
                            denominations.TextAlignment = System.Windows.TextAlignment.Left;
                            denominations.Margin = new Thickness(5, 3, 0, 3);
                            TableCell cell = new TableCell(denominations);
                            cell.ColumnSpan = 2;
                            currentRow.Cells.Add(cell);
                            Paragraph quantity = new Paragraph();
                            quantity.Inlines.Add(new Run
                            {
                                Text = MoneyList[i - 1].Quantity.ToString(),
                                FontSize = 12,
                                FontWeight = FontWeights.Bold,
                            });
                            quantity.TextAlignment = System.Windows.TextAlignment.Center;
                            quantity.Margin = new Thickness(0, 3, 0, 3);
                            TableCell cellQuantity = new TableCell(quantity);
                            cellQuantity.ColumnSpan = 1;
                            currentRow.Cells.Add(cellQuantity);
                            Paragraph UnitPrice = new Paragraph(new Run(MoneyList[i - 1].AmountFormat));
                            UnitPrice.TextAlignment = System.Windows.TextAlignment.Center;
                            UnitPrice.Margin = new Thickness(0, 3, 0, 3);
                            TableCell cellUnitPrice = new TableCell(UnitPrice);
                            cellUnitPrice.ColumnSpan = 2;
                            currentRow.Cells.Add(cellUnitPrice);
                            bill.TableRow.RowGroups[0].Rows.Add(currentRow);
                        }

                        TableRow AllRow = new TableRow();
                        AllRow.FontSize = 14;
                        AllRow.Background = System.Windows.Media.Brushes.White;
                        Paragraph Alldenominations = new Paragraph();
                        Alldenominations.Inlines.Add(new Run
                        {
                            Text = "TC: ",
                            FontSize = 14,
                            FontWeight = FontWeights.Bold,
                        });
                        Alldenominations.TextAlignment = System.Windows.TextAlignment.Left;
                        Alldenominations.Margin = new Thickness(5, 3, 0, 3);
                        TableCell Allcell = new TableCell(Alldenominations);
                        Allcell.ColumnSpan = 2;
                        AllRow.Cells.Add(Allcell);
                        Paragraph Allquantity = new Paragraph();
                        Allquantity.Inlines.Add(new Run
                        {
                            Text = MoneyList.Sum(x => x.Quantity).ToString(),
                            FontSize = 14,
                            FontWeight = FontWeights.Bold,
                        });
                        Allquantity.TextAlignment = System.Windows.TextAlignment.Center;
                        Allquantity.Margin = new Thickness(0, 3, 0, 3);
                        TableCell AllcellQuantity = new TableCell(Allquantity);
                        AllcellQuantity.ColumnSpan = 1;
                        AllRow.Cells.Add(AllcellQuantity);
                        Paragraph AllUnitPrice = new Paragraph();
                        AllUnitPrice.Inlines.Add(new Run
                        {
                            Text = Utils.Utils.FormatMoney(MoneyList.Sum(x => x.Amount)),
                            FontSize = 14,
                            FontWeight = FontWeights.Bold,
                        });
                        AllUnitPrice.TextAlignment = System.Windows.TextAlignment.Center;
                        AllUnitPrice.Margin = new Thickness(0, 3, 0, 3);
                        TableCell AllcellUnitPrice = new TableCell(AllUnitPrice);
                        AllcellUnitPrice.ColumnSpan = 2;
                        AllRow.Cells.Add(AllcellUnitPrice);
                        bill.TableRow.RowGroups[0].Rows.Add(AllRow);
                        bill.billDocument.MaxPageWidth = 210;
                        bill.billDocument.MaxPageHeight = 3276;

                        FlowDocument doc = bill.billDocument;
                        doc.PagePadding = new Thickness(10);


                        doc.ColumnWidth = dialog.PrintableAreaWidth;
                        doc.PageHeight = dialog.PrintableAreaHeight;
                        IDocumentPaginatorSource idSource = doc;

                        dialog.PrintDocument(idSource.DocumentPaginator, "Invoice");
                    }

                }
                catch (Exception ex)
                {
                    WriteLog.logs(ex.Message);
                }

            }
            public void PrintBillTMP(OrderItemResponse order, string PrintName, int size)
            {
                try
                {
                    System.Windows.Controls.PrintDialog dialog = new System.Windows.Controls.PrintDialog();
                    if (!string.IsNullOrEmpty(PrintName))
                    {
                        dialog.PrintQueue = new PrintQueue(new PrintServer(), PrintName);
                    }


                    dialog.MinPage = 1;
                    dialog.MaxPage = 1;
                    dialog.PageRangeSelection = PageRangeSelection.AllPages;

                    dialog.UserPageRangeEnabled = false;

                    PrinterSettings pst = new PrinterSettings();
                    pst.PrinterName = PrintName;
                    if (size == (int)SizePrintEnum.SIZE_80MM)
                    {
                        BillTmp bill = new BillTmp();
                        bill.TableName.Text = string.Format("{0} - #{1} ", order.Data.TableName, order.Data.Id.ToString());
                        bill.RestaurantName.Text = branch.Name;
                        bill.RestaurantAddress.Text = branch.Address;
                        bill.EmployeeName.Text = currentUser.Name;
                        bill.ComingTime.Text = order.Data.UpdatedAt;
                        bill.ModePrint.Text = "PHIẾU TẠM";
                        bill.EmployeeSaleName.Text = order.Data.EmployeeName;
                        bill.CustomerSlot.Text = order.Data.CustomerSlotNumber.ToString();
                        List<Models.BillResponse> f = order.Data.Foods.Where(x => x.Quantity != 0).ToList();
                        for (int i = 1; i <= f.Count; i++)
                        {
                            if (i != 1)
                            {
                                TableRow tableRow = new TableRow();
                                tableRow.Background = System.Windows.Media.Brushes.White;
                                Paragraph paragraph = new Paragraph(new Run(Utils.Utils.GetLine80()));
                                TableCell cellLine = new TableCell(paragraph);
                                //cellLine.IsFocused = true;
                                cellLine.ColumnSpan = 4;
                                cellLine.LineHeight = 1;
                                cellLine.FontSize = 16;
                                tableRow.Cells.Add(cellLine);
                                bill.TableRow.RowGroups[0].Rows.Add(tableRow);
                            }
                            TableRow currentRow = new TableRow();
                            currentRow.FontSize = 12;
                            currentRow.Background = System.Windows.Media.Brushes.White;
                            Paragraph foodName = new Paragraph(new Run(Utils.Utils.ConvertToUpperAndLower(f[i - 1].Name)));
                            foodName.TextAlignment = System.Windows.TextAlignment.Left;
                            foodName.Margin = new Thickness(5, 0, 0, 0);
                            TableCell cell = new TableCell(foodName);
                            cell.ColumnSpan = 3;
                            currentRow.Cells.Add(cell);
                            Paragraph quantity = new Paragraph();
                            quantity.Inlines.Add(new Run(" x"));
                            quantity.Inlines.Add(new Run
                            {
                                Text = f[i - 1].Quantity.ToString(),
                                FontSize = 14,
                                FontWeight = FontWeights.Bold
                            });
                            //quantity.Inlines.Add(new Run(" x"));
                            quantity.TextAlignment = System.Windows.TextAlignment.Center;
                            TableCell cellQuantity = new TableCell(quantity);
                            cellQuantity.ColumnSpan = 1;
                            currentRow.Cells.Add(cellQuantity);
                            bill.TableRow.RowGroups[0].Rows.Add(currentRow);
                        }
                        bill.billDocument.MaxPageWidth = 295;
                        bill.billDocument.MaxPageHeight = 3276;

                        FlowDocument doc = bill.billDocument;
                        doc.PagePadding = new Thickness(10);


                        doc.ColumnWidth = dialog.PrintableAreaWidth;
                        doc.PageHeight = dialog.PrintableAreaHeight;
                        IDocumentPaginatorSource idSource = doc;

                        dialog.PrintDocument(idSource.DocumentPaginator, "Invoice");
                    }
                    else if (size == (int)SizePrintEnum.SIZE_58MM)
                    {

                        BillTmp58MM bill = new BillTmp58MM();
                        bill.TableName.Text = string.Format("{0} - #{1} ", order.Data.TableName, order.Data.Id.ToString());
                        bill.RestaurantName.Text = branch.Name;
                        bill.RestaurantAddress.Text = branch.Address;
                        bill.EmployeeName.Text = currentUser.Name;
                        bill.ComingTime.Text = order.Data.UpdatedAt;
                        bill.ModePrint.Text = "PHIẾU TẠM";
                        bill.EmployeeSaleName.Text = order.Data.EmployeeName;
                        bill.CustomerSlot.Text = order.Data.CustomerSlotNumber.ToString();
                        List<Models.BillResponse> f = order.Data.Foods.Where(x => x.Quantity != 0).ToList();
                        for (int i = 1; i <= f.Count; i++)
                        {
                            if (i != 1)
                            {
                                TableRow tableRow = new TableRow();
                                tableRow.Background = System.Windows.Media.Brushes.White;
                                Paragraph paragraph = new Paragraph(new Run(Utils.Utils.GetLine80()));
                                TableCell cellLine = new TableCell(paragraph);
                                //cellLine.IsFocused = true;
                                cellLine.ColumnSpan = 4;
                                cellLine.LineHeight = 1;
                                cellLine.FontSize = 14;
                                tableRow.Cells.Add(cellLine);
                                bill.TableRow.RowGroups[0].Rows.Add(tableRow);
                            }
                            TableRow currentRow = new TableRow();
                            currentRow.FontSize = 10;
                            currentRow.Background = System.Windows.Media.Brushes.White;
                            Paragraph foodName = new Paragraph(new Run(Utils.Utils.ConvertToUpperAndLower(f[i - 1].Name)));
                            foodName.TextAlignment = System.Windows.TextAlignment.Left;
                            foodName.Margin = new Thickness(5, 0, 0, 0);
                            TableCell cell = new TableCell(foodName);
                            cell.ColumnSpan = 3;
                            currentRow.Cells.Add(cell);
                            Paragraph quantity = new Paragraph();
                            quantity.Inlines.Add(new Run(" x"));
                            quantity.Inlines.Add(new Run
                            {
                                Text = f[i - 1].Quantity.ToString(),
                                FontSize = 12,
                                FontWeight = FontWeights.Bold
                            });
                            //quantity.Inlines.Add(new Run(" x"));
                            quantity.TextAlignment = System.Windows.TextAlignment.Center;
                            TableCell cellQuantity = new TableCell(quantity);
                            cellQuantity.ColumnSpan = 1;
                            currentRow.Cells.Add(cellQuantity);
                            bill.TableRow.RowGroups[0].Rows.Add(currentRow);
                        }
                        bill.billDocument.MaxPageWidth = 210;
                        bill.billDocument.MaxPageHeight = 3276;

                        FlowDocument doc = bill.billDocument;
                        doc.PagePadding = new Thickness(10);


                        doc.ColumnWidth = dialog.PrintableAreaWidth;
                        doc.PageHeight = dialog.PrintableAreaHeight;
                        IDocumentPaginatorSource idSource = doc;

                        dialog.PrintDocument(idSource.DocumentPaginator, "Invoice");
                    }

                }
                catch (Exception ex)
                {
                    WriteLog.logs(ex.Message);
                }

            }
            public void PrintTmpSalaryEmployee(EmployeeAdvancedSalary e, string PrintName, int size)
            {
                try
                {
                    System.Windows.Controls.PrintDialog dialog = new System.Windows.Controls.PrintDialog();
                    if (!string.IsNullOrEmpty(PrintName))
                    {
                        dialog.PrintQueue = new PrintQueue(new PrintServer(), PrintName);
                    }

                    dialog.MinPage = 1;
                    dialog.MaxPage = 1;
                    dialog.PageRangeSelection = PageRangeSelection.AllPages;
                    dialog.UserPageRangeEnabled = false;
                    PrinterSettings pst = new PrinterSettings();
                    pst.PrinterName = PrintName;
                    if (size == (int)SizePrintEnum.SIZE_80MM)
                    {
                        PrintTmpSalaryEmployee print = new PrintTmpSalaryEmployee();
                        print.branchName.Text = e.BranchName;
                        print.ModePrint.Text = "PHIẾU ỨNG LƯƠNG";
                        print.month.Text = string.Format("THÁNG {0}", Utils.Utils.GetStringFormatDateTimeHour(e.PaidAt).Month);
                        print.EmployeeName.Text = e.Employee.Name;
                        print.RoleName.Text = e.Employee.RoleName;
                        print.Amount.Text = e.AmountString;
                        print.EmployeeAddition.Text = e.EmployeeApproved.Name;
                        print.EmployeePaid.Text = currentUser.Name;
                        print.ComingTime.Text = e.PaidAt;
                        print.billDocument.MaxPageWidth = 295;
                        print.billDocument.MaxPageHeight = 3276;

                        FlowDocument doc = print.billDocument;
                        doc.PagePadding = new Thickness(10);


                        doc.ColumnWidth = dialog.PrintableAreaWidth;
                        doc.PageHeight = dialog.PrintableAreaHeight;
                        IDocumentPaginatorSource idSource = doc;

                        dialog.PrintDocument(idSource.DocumentPaginator, "Invoice");
                    }
                    else if (size == (int)SizePrintEnum.SIZE_58MM)
                    {
                        PrintTmpSalaryEmployee58MM print = new PrintTmpSalaryEmployee58MM();
                        print.branchName.Text = e.BranchName;
                        print.ModePrint.Text = "PHIẾU ỨNG LƯƠNG";
                        print.month.Text = string.Format("THÁNG {0}", Utils.Utils.GetStringFormatDateTimeHour(e.PaidAt).Month);
                        print.EmployeeName.Text = e.Employee.Name;
                        print.RoleName.Text = e.Employee.RoleName;
                        print.Amount.Text = e.AmountString;
                        print.EmployeeAddition.Text = e.EmployeeApproved.Name;
                        print.EmployeePaid.Text = currentUser.Name;
                        print.ComingTime.Text = e.PaidAt;
                        print.billDocument.MaxPageWidth = 210;
                        print.billDocument.MaxPageHeight = 3276;

                        FlowDocument doc = print.billDocument;
                        doc.PagePadding = new Thickness(10);


                        doc.ColumnWidth = dialog.PrintableAreaWidth;
                        doc.PageHeight = dialog.PrintableAreaHeight;
                        IDocumentPaginatorSource idSource = doc;

                        dialog.PrintDocument(idSource.DocumentPaginator, "Invoice");
                    }
                }
                catch (Exception ex)
                {
                    WriteLog.logs(ex.Message);
                }
            }
            public void Print(string mode, OrderItemResponse order, string PrintName, int size, Branch branch)
            {
                try
                {
                    System.Windows.Controls.PrintDialog dialog = new System.Windows.Controls.PrintDialog();
                    if (!string.IsNullOrEmpty(PrintName))
                    {
                        dialog.PrintQueue = new PrintQueue(new PrintServer(), PrintName);
                    }
                    dialog.MinPage = 1;
                    dialog.MaxPage = 1;
                    dialog.PageRangeSelection = PageRangeSelection.AllPages;
                    dialog.UserPageRangeEnabled = false;
                    PrinterSettings pst = new PrinterSettings();
                    pst.PrinterName = PrintName;
                    if (size == (int)SizePrintEnum.SIZE_80MM)
                    {
                        Template.Bill bill = new Template.Bill();
                        if (currentSetting.IsPrintBillLogo)
                        {
                            bill.Logo.Visibility = Visibility.Visible;
                            // BitmapImage bitmapImage = new BitmapImage(new Uri(Environment.CurrentDirectory + @"\logo_branch.png"));
                            //bill.Logo.Source = bitmapImage;
                        }
                        else
                        {
                            bill.Logo.Visibility = Visibility.Collapsed;
                        }
                        bill.RestaurantName.Text = branch.Name;
                        bill.RestaurantAddress.Text = branch.Address;
                        //if (order.Data.MembershipTotalPointUsed > 0)
                        //{
                        //    bill.Point.Text = Utils.Utils.FormatMoney(order.Data.MembershipTotalPointUsed);
                        //}
                        bill.Point.Text = Utils.Utils.FormatMoney(order.Data.MembershipPromotionPointUsed + order.Data.MembershipPointUsed + order.Data.MembershipAccumulatePointUsed);
                        if (order.Data.IsOnline == Constants.STATUS || order.Data.IsTakeAway == Constants.STATUS || order.Data.TableId == 0)
                        {
                            //if (order.Data.IsTakeAway == Constants.STATUS)
                            //{
                            //    bill.OrderName.Text = "(Đặt Online)";
                            //}
                            if (order.Data.IsOnline == Constants.STATUS)
                            {
                                bill.OrderName.Text = "(Mang Về)";
                            }
                            if(order.Data.TableId == 0)
                            {
                                bill.OrderName.Text = "(Mang đi)"; 
                            }
                            bill.contentCustomerSlot.Visibility = Visibility.Collapsed;
                            bill.CustomerSlot.Visibility = Visibility.Collapsed;
                            bill.TableTitle.Text = MessageValue.MESSAGE_FROM_RECEIVERS_PERSON;
                            bill.TableName.Text = order.Data.ShippingReceiverName;
                            bill.CashierTitle.Text = MessageValue.MESSAGE_FROM_PHONE;
                            bill.EmployeeTitle.Text = string.Format("{0} {1}", MessageValue.MESSAGE_FROM_RECEIVERS_ADDRESS, order.Data.ShippingAddress);
                            bill.CashierName.Text = order.Data.ShippingPhone;
                            bill.EmployeeName.Text = "";
                            bill.ShipTitle.Visibility = Visibility.Visible;
                            bill.ShipAmount.Text = Utils.Utils.FormatMoney(order.Data.ShippingFee) + "đ";
                            bill.billDocument.Blocks.Remove(bill.CustomerInFMtion);
                            bill.billDocument.Blocks.Remove(bill.EmployeeInFMtion);
                        }
                        else
                        {
                            bill.OrderName.Text = "(Tại Quán)";
                            bill.TableTitle.Text = MessageValue.MESSAGE_FROM_VIEW_DETAIL_ORDER_TABLE;
                            if (!string.IsNullOrEmpty(order.Data.TableName))
                            {
                                bill.TableName.Text = string.Format("{0} - #{1} ", order.Data.TableName, order.Data.Id.ToString());
                            }
                            else
                            {
                                bill.TableName.Text = string.Format("#{0}", order.Data.Id.ToString());
                            }
                            if (order.Data.CustomerSlotNumber > 0)
                            {
                                bill.contentCustomerSlot.Visibility = Visibility.Visible;
                                bill.CustomerSlot.Visibility = Visibility.Visible;
                                bill.CustomerSlot.Text = order.Data.CustomerSlotNumber.ToString();
                            }
                            else
                            {
                                bill.contentCustomerSlot.Visibility = Visibility.Collapsed;
                                bill.CustomerSlot.Visibility = Visibility.Collapsed;
                            }
                            bill.EmployeeGiveQrCodeTitle.Text = MessageValue.MESSAGE_FROM_VIEW_DETAIL_EMPLOYRR_GIVE_QRCODE;
                            if (order.Data.EmployeeGiveQrCodeId > 0)
                            {
                                bill.EmployeeGiveQrCodeName.Text = order.Data.EmployeeGiveQrCodeName;
                            }
                            bill.CashierTitle.Text = MessageValue.MESSAGE_FROM_VIEW_DETAIL_ORDER_CASHIER;
                            bill.EmployeeTitle.Text = MessageValue.MESSAGE_FROM_NVKD;
                            bill.CashierName.Text = currentUser.Name;
                            bill.EmployeeName.Text = order.Data.EmployeeName;
                            bill.ShipTitle.Visibility = Visibility.Collapsed;
                            bill.ShipAmount.Visibility = Visibility.Collapsed;
                            if (string.IsNullOrEmpty(order.Data.CustomerName))
                            {
                                bill.billDocument.Blocks.Remove(bill.CustomerInFMtion);
                                //bill.CustomerTitle.Visibility = Visibility.Collapsed;
                                //bill.CusTomerName.Visibility = Visibility.Collapsed;
                            }
                            else
                            {
                                bill.CustomerTitle.Text = MessageValue.MESSAGE_FROM_CUSTOMER_FITER;
                                bill.CusTomerName.Text = order.Data.CustomerName;
                            }
                        }
                        DateTime dateTime = Utils.Utils.GetStringFormatDateTimeHour(order.Data.PaymentDate);
                        bill.ComingTime.Text = string.Format("{0} - {1}", order.Data.CreatedAt, Utils.Utils.GetHourFormatVN(dateTime));
                        if (order.Data.ShippingFee > 0)
                        {
                            bill.TotalPayment.Text = Utils.Utils.FormatMoney(order.Data.TotalAmount + order.Data.ShippingFee) + " đ";
                        }
                        else
                        {
                            bill.TotalPayment.Text = Utils.Utils.FormatMoney(order.Data.TotalAmount) + " đ";
                        }
                        if (order.Data.BookingDepositAmount > 0)
                        {
                            //if (order.Data.IsReturnDeposit == (int)StatusEnum.YES)
                            //{
                                bill.BookingAmount.Text = Utils.Utils.FormatMoney(order.Data.BookingDepositAmount) + " đ";
                                bill.TotalPayment.Text = Utils.Utils.FormatMoney(order.Data.TotalAmount - order.Data.BookingDepositAmount) + " đ";
                            //}
                            //else
                            //{
                            //    bill.BookingTitle.Visibility = Visibility.Collapsed;
                            //    bill.BookingAmount.Visibility = Visibility.Collapsed;
                            //    bill.TotalPayment.Text = Utils.Utils.FormatMoney(order.Data.TotalAmount) + " đ";
                            //}
                        }
                        else
                        {
                            bill.BookingTitle.Visibility = Visibility.Collapsed;
                            bill.BookingAmount.Visibility = Visibility.Collapsed;
                        }
                        bill.ModePrint.Text = mode;
                        bill.Sale.Text = Utils.Utils.FormatMoney(order.Data.DiscountAmount) + " đ";
                        bill.VAT.Text = Utils.Utils.FormatMoney(order.Data.VatAmount) + " đ";
                        bill.GiftAmount.Text = Utils.Utils.FormatMoney(order.Data.Foods.Where(x=>x.IsGift == 1).Sum(t=>t.TotalPriceInlcudeAdditionFoods)) + " đ";
                        bill.TempAmount.Text = Utils.Utils.FormatMoney(order.Data.Amount + order.Data.TotalGiftFoodAmount) + " đ";
                        bill.PhoneRes.Text = "ĐT: " + branch.Phone;

                        // bill.VATNumber.Text = "VAT:   " + order.Data.Vat + "%";
                        bill.VATNumber.Text = "VAT:   ";
                        bill.ServiceChargeAmount.Text = "Phí phục vụ: ";
                        bill.TIP.Text = Utils.Utils.FormatMoney(order.Data.ServiceChargeAmount) + " đ";

                        if (order.Data.DiscountType == (int)DiscountEnum.ALL_BILL)
                        {
                            bill.Discount.Text = "Giảm giá hóa đơn:       " + order.Data.DiscountPercent + "%";
                        }
                        else
                            if (order.Data.DiscountType == (int)DiscountEnum.FOOD_BILL)
                        {
                            bill.Discount.Text = "Giảm giá món ăn:       " + order.Data.DiscountPercent + "%";
                        }
                        else
                            if (order.Data.DiscountType == (int)DiscountEnum.DRINK_BILL)
                        {
                            bill.Discount.Text = "Giảm giá nước uống:       " + order.Data.DiscountPercent + "%";
                        }
                        List<Models.BillResponse> f = order.Data.Foods.Where(x => x.Quantity != 0).ToList();
                        for (int i = 1; i <= f.Count; i++)
                        {
                            TableRow currentRow = new TableRow();
                            List<TableRow> tableRows = new List<TableRow>();
                            currentRow.FontSize = 12;
                            currentRow.Background = System.Windows.Media.Brushes.White;
                            Paragraph foodName = new Paragraph(new Run(Utils.Utils.ConvertToUpperAndLower(f[i - 1].Name)));
                            foodName.TextAlignment = System.Windows.TextAlignment.Left;
                            foodName.Margin = new Thickness(5, 3, 0, 3);
                            TableCell cell = new TableCell(foodName);
                            cell.ColumnSpan = 3;
                            //cell.BorderBrush = new System.Windows.Media.SolidColorBrush(Colors.Black);
                            //cell.BorderThickness = new Thickness(0, 0, 0, 1);
                            currentRow.Cells.Add(cell);

                            if (f[i - 1].OrderDetailAdditions != null && f[i - 1].OrderDetailAdditions.Count > 0)
                            {
                                for (int j = 0; j < f[i - 1].OrderDetailAdditions.Count; j++)
                                {
                                    TableRow tableRow = new TableRow();
                                    tableRow.FontSize = 13;
                                    tableRow.Background = System.Windows.Media.Brushes.White;
                                    Paragraph tablerow = new Paragraph();
                                    tablerow.Inlines.Add(new TextBlock()
                                    {
                                        Text = f[i - 1].OrderDetailAdditions[j].FoodNameAdditionPrintBill1,
                                        TextWrapping = TextWrapping.Wrap,
                                        FontStyle = FontStyles.Italic,
                                    });
                                    tablerow.TextAlignment = System.Windows.TextAlignment.Left;
                                    tablerow.Margin = new Thickness(5, 0, 0, 0);
                                    TableCell cellAddition = new TableCell(tablerow);
                                    cellAddition.ColumnSpan = 6;
                                    tableRow.Cells.Add(cellAddition);
                                    tableRows.Add(tableRow);
                                }

                            }
                            Paragraph quantity = new Paragraph();
                            quantity.Inlines.Add(new Run
                            {
                                Text = f[i - 1].Quantity.ToString(),
                                FontSize = 14,
                                FontWeight = FontWeights.Bold,
                            });
                            quantity.Inlines.Add(new Run(" x"));
                            quantity.TextAlignment = System.Windows.TextAlignment.Center;
                            quantity.Margin = new Thickness(0, 3, 0, 3);
                            TableCell cellQuantity = new TableCell(quantity);
                            cellQuantity.ColumnSpan = 1;
                            //cellQuantity.BorderBrush = new System.Windows.Media.SolidColorBrush(Colors.Black);
                            //cellQuantity.BorderThickness = new Thickness(0, 0, 0, 1);
                            currentRow.Cells.Add(cellQuantity);

                            Paragraph UnitPrice;
                            if (f[i - 1].IsPurchaseByPoint == 1)
                            {
                                UnitPrice = new Paragraph(new Run(string.Format("{0:0,0}", f[i - 1].UnitPurchasePoint)));
                            }
                            else
                            {
                                UnitPrice = new Paragraph(new Run(string.Format("{0:0,0}", f[i - 1].Price)));
                            }
                            //   UnitPrice.Margin = new Thickness(0, 3, 0, 0);
                            UnitPrice.TextAlignment = System.Windows.TextAlignment.Center;
                            UnitPrice.Margin = new Thickness(0, 3, 0, 3);
                            TableCell cellUnitPrice = new TableCell(UnitPrice);
                            cellUnitPrice.ColumnSpan = 2;
                            //cellUnitPrice.BorderBrush = new System.Windows.Media.SolidColorBrush(Colors.Black);
                            //cellUnitPrice.BorderThickness = new Thickness(0, 0, 0, 1);
                            currentRow.Cells.Add(cellUnitPrice);


                            Paragraph totalPrice;
                            if (f[i - 1].IsGift == 1)
                            {
                                //totalPrice = new Paragraph(new Run(string.Format("🎁{0}", Utils.Utils.FormatMoney(f[i - 1].TotalPriceInlcudeAdditionFoods))));
                                totalPrice = new Paragraph(new Run(string.Format("🎁")));
                            }
                            else
                            {
                                if (f[i - 1].IsPurchaseByPoint == 1)
                                {
                                    totalPrice = new Paragraph(new Run(string.Format("{0}          {1}", Utils.Utils.FormatMoney(f[i - 1].TotalPriceInlcudeAdditionFoods), f[i - 1].PurchasePointString)));
                                }
                                else
                                {
                                    totalPrice = new Paragraph(new Run(Utils.Utils.FormatMoney(f[i - 1].TotalPriceInlcudeAdditionFoods)));
                                }
                            }
                            totalPrice.TextAlignment = System.Windows.TextAlignment.Center;
                            totalPrice.Margin = new Thickness(0, 3, 5, 3);

                            TableCell cellPrice = new TableCell(totalPrice);
                            //cellPrice.BorderBrush = new System.Windows.Media.SolidColorBrush(Colors.Black);
                            //cellPrice.BorderThickness = new Thickness(0, 0, 0, 1);
                            cellPrice.ColumnSpan = 2;

                            currentRow.Cells.Add(cellPrice);

                            bill.TableRow.RowGroups[0].Rows.Add(currentRow);
                            for (int k = 1; k <= tableRows.Count(); k++)
                            {
                                bill.TableRow.RowGroups[0].Rows.Add(tableRows[k - 1]);
                            }
                        }

                        bill.billDocument.MaxPageWidth = 295;
                        bill.billDocument.MaxPageHeight = 3276;
                        FlowDocument doc = bill.billDocument;
                        doc.PagePadding = new Thickness(10);
                        doc.ColumnWidth = dialog.PrintableAreaWidth;
                        doc.PageHeight = dialog.PrintableAreaHeight;
                        IDocumentPaginatorSource idSource = doc;
                        dialog.PrintDocument(idSource.DocumentPaginator, "Invoice");
                    }
                    else if (size == (int)SizePrintEnum.SIZE_58MM)
                    {
                        Bill58MM bill = new Bill58MM();
                        if (currentSetting.IsPrintBillLogo)
                        {
                            bill.Logo.Visibility = Visibility.Visible;
                            BitmapImage bitmapImage = new BitmapImage(new Uri(Environment.CurrentDirectory + @"\logo_branch.png"));
                            bill.Logo.Source = bitmapImage;
                        }
                        else
                        {
                            bill.Logo.Visibility = Visibility.Collapsed;
                        }
                        bill.RestaurantName.Text = branch.Name;
                        bill.RestaurantAddress.Text = branch.Address;
                        bill.point.Text = Utils.Utils.FormatMoney(order.Data.MembershipTotalPointUsed);
                        if (order.Data.IsOnline == Constants.STATUS || order.Data.IsTakeAway == Constants.STATUS)
                        {
                            bill.contentCustomerSlot.Visibility = Visibility.Collapsed;
                            bill.CustomerSlot.Visibility = Visibility.Collapsed;
                            bill.TableTitle.Text = MessageValue.MESSAGE_FROM_RECEIVERS_PERSON;
                            bill.TableName.Text = order.Data.ShippingReceiverName;
                            bill.CashierTitle.Text = MessageValue.MESSAGE_FROM_PHONE;
                            bill.EmployeeTitle.Text = MessageValue.MESSAGE_FROM_RECEIVERS_ADDRESS;
                            bill.CashierName.Text = order.Data.ShippingPhone;
                            bill.EmployeeName.Text = order.Data.ShippingAddress;
                            bill.ShipTitle.Visibility = Visibility.Visible;
                            bill.ShipAmount.Text = Utils.Utils.FormatMoney(order.Data.ShippingFee) + "đ";
                        }
                        else
                        {
                            bill.TableTitle.Text = MessageValue.MESSAGE_FROM_VIEW_DETAIL_ORDER_TABLE;

                            if (!string.IsNullOrEmpty(order.Data.TableName))
                            {
                                bill.TableName.Text = string.Format("{0} - #{1} ", order.Data.TableName, order.Data.Id.ToString());
                            }
                            else
                            {
                                bill.TableName.Text = string.Format("#{0}", order.Data.Id.ToString());
                            }

                            bill.CustomerSlot.Text = order.Data.CustomerSlotNumber.ToString();
                            bill.CashierTitle.Text = MessageValue.MESSAGE_FROM_VIEW_DETAIL_ORDER_CASHIER;
                            bill.EmployeeTitle.Text = MessageValue.MESSAGE_FROM_NVKD;
                            bill.CashierName.Text = order.Data.CashierName;
                            bill.EmployeeName.Text = order.Data.EmployeeName;
                            bill.ShipTitle.Visibility = Visibility.Collapsed;
                            bill.ShipAmount.Visibility = Visibility.Collapsed;
                            if ( string.IsNullOrEmpty(order.Data.CustomerName))
                            {
                                bill.CustomerTitle.Visibility = Visibility.Visible;
                                bill.CusTomerName.Visibility = Visibility.Collapsed;
                            }
                            else
                            {
                                bill.CustomerTitle.Text = MessageValue.MESSAGE_FROM_CUSTOMER_FITER;
                                bill.CusTomerName.Text = order.Data.CustomerName;
                            }
                        }
                        bill.EmployeeGiveQrCodeTitle.Text = MessageValue.MESSAGE_FROM_VIEW_DETAIL_EMPLOYRR_GIVE_QRCODE;
                        if ( order.Data.EmployeeGiveQrCodeId > 0)
                        {
                            bill.EmployeeGiveQrCodeName.Text = order.Data.EmployeeGiveQrCodeName;
                        }
                        DateTime dateTime = Utils.Utils.GetStringFormatDateTimeHour(order.Data.PaymentDate);
                        bill.ComingTime.Text = string.Format("{0} - {1}", order.Data.CreatedAt, Utils.Utils.GetHourFormatVN(dateTime));
                        if (order.Data.ShippingFee > 0)
                        {
                            bill.TotalPayment.Text = Utils.Utils.FormatMoney(order.Data.TotalAmount + order.Data.ShippingFee) + " đ";
                        }
                        else
                        {
                            bill.TotalPayment.Text = Utils.Utils.FormatMoney(order.Data.TotalAmount) + " đ";
                        }
                        bill.ModePrint.Text = mode;

                        bill.Sale.Text = Utils.Utils.FormatMoney(order.Data.DiscountAmount) + " đ";
                        bill.VAT.Text = Utils.Utils.FormatMoney(order.Data.VatAmount) + " đ";
                        bill.GiftAmount.Text = Utils.Utils.FormatMoney(order.Data.TotalGiftFoodAmount) + " đ";
                        bill.TempAmount.Text = Utils.Utils.FormatMoney(order.Data.Amount + order.Data.TotalGiftFoodAmount) + " đ";
                        bill.PhoneRes.Text = "ĐT: " + branch.Phone;

                        bill.VATNumber.Text = "VAT:   " + order.Data.Vat + "%";
                        if (order.Data.DiscountType == (int)DiscountEnum.ALL_BILL)
                        {
                            bill.Discount.Text = "Giảm giá hóa đơn:       " + order.Data.DiscountPercent + "%";
                        }
                        else
                            if (order.Data.DiscountType == (int)DiscountEnum.FOOD_BILL)
                        {
                            bill.Discount.Text = "Giảm giá món ăn:       " + order.Data.DiscountPercent + "%";
                        }
                        else
                            if (order.Data.DiscountType == (int)DiscountEnum.DRINK_BILL)
                        {
                            bill.Discount.Text = "Giảm giá nước uống:       " + order.Data.DiscountPercent + "%";
                        }
                        List<Models.BillResponse> f = order.Data.Foods.Where(x => x.Quantity != 0).ToList();
                        for (int i = 1; i <= f.Count; i++)
                        {
                            TableRow currentRow = new TableRow();
                            List<TableRow> tableRows = new List<TableRow>();
                            currentRow.FontSize = 10;
                            currentRow.Background = System.Windows.Media.Brushes.White;
                            Paragraph foodName = new Paragraph(new Run(Utils.Utils.ConvertToUpperAndLower(f[i - 1].Name)));
                            foodName.TextAlignment = System.Windows.TextAlignment.Left;
                            foodName.Margin = new Thickness(5, 3, 0, 3);
                            TableCell cell = new TableCell(foodName);
                            cell.ColumnSpan = 3;
                            currentRow.Cells.Add(cell);
                            if (f[i - 1].OrderDetailAdditions != null && f[i - 1].OrderDetailAdditions.Count > 0)
                            {
                                for (int j = 0; j < f[i - 1].OrderDetailAdditions.Count; j++)
                                {
                                    TableRow tableRow = new TableRow();
                                    tableRow.FontSize = 11;
                                    tableRow.Background = System.Windows.Media.Brushes.White;
                                    Paragraph tablerow = new Paragraph();
                                    tablerow.Inlines.Add(new TextBlock()
                                    {
                                        Text = f[i - 1].OrderDetailAdditions[j].FoodNameAdditionPrintBill,
                                        TextWrapping = TextWrapping.Wrap,
                                        FontStyle = FontStyles.Italic,
                                    });
                                    tablerow.TextAlignment = System.Windows.TextAlignment.Left;
                                    tablerow.Margin = new Thickness(5, 0, 0, 0);
                                    TableCell cellAddition = new TableCell(tablerow);
                                    cellAddition.ColumnSpan = 6;
                                    tableRow.Cells.Add(cellAddition);
                                    tableRows.Add(tableRow);
                                }
                            }
                            Paragraph quantity = new Paragraph();
                            quantity.Inlines.Add(new Run
                            {
                                Text = f[i - 1].Quantity.ToString(),
                                FontSize = 12,
                                FontWeight = FontWeights.Bold,
                            });
                            quantity.Inlines.Add(new Run(" x"));
                            quantity.TextAlignment = System.Windows.TextAlignment.Center;
                            quantity.Margin = new Thickness(0, 3, 0, 3);
                            TableCell cellQuantity = new TableCell(quantity);
                            cellQuantity.ColumnSpan = 1;
                            currentRow.Cells.Add(cellQuantity);
                            Paragraph UnitPrice = new Paragraph(new Run(string.Format("{0:0,0}", f[i - 1].Price)));
                            UnitPrice.TextAlignment = System.Windows.TextAlignment.Center;
                            UnitPrice.Margin = new Thickness(0, 3, 0, 3);
                            TableCell cellUnitPrice = new TableCell(UnitPrice);
                            cellUnitPrice.ColumnSpan = 2;
                            currentRow.Cells.Add(cellUnitPrice);
                            Paragraph totalPrice;
                            if (f[i - 1].IsGift == 1)
                            {
                                totalPrice = new Paragraph(new Run(string.Format("🎁{0}", Utils.Utils.FormatMoney(f[i - 1].TotalPriceInlcudeAdditionFoods))));
                            }
                            else
                            {
                                totalPrice = new Paragraph(new Run(Utils.Utils.FormatMoney(f[i - 1].TotalPriceInlcudeAdditionFoods)));
                            }
                            totalPrice.TextAlignment = System.Windows.TextAlignment.Center;
                            totalPrice.Margin = new Thickness(0, 3, 5, 3);
                            TableCell cellPrice = new TableCell(totalPrice);
                            cellPrice.ColumnSpan = 2;
                            currentRow.Cells.Add(cellPrice);
                            bill.TableRow.RowGroups[0].Rows.Add(currentRow);
                            for (int k = 1; k <= tableRows.Count(); k++)
                            {
                                bill.TableRow.RowGroups[0].Rows.Add(tableRows[k - 1]);
                            }
                        }
                        bill.billDocument.MaxPageWidth = 210;
                        bill.billDocument.MaxPageHeight = 3276;
                        FlowDocument doc = bill.billDocument;
                        doc.PagePadding = new Thickness(10);
                        doc.ColumnWidth = dialog.PrintableAreaWidth;
                        doc.PageHeight = dialog.PrintableAreaHeight;
                        IDocumentPaginatorSource idSource = doc;
                        dialog.PrintDocument(idSource.DocumentPaginator, "Invoice");
                    }

                }
                catch (Exception ex)
                {
                    WriteLog.logs(ex.Message);
                }

            }
            public void PrintBooking(string mode, Models.Booking booking, string PrintName, int size, Branch branch, bool IsReturnDeposit)
            {
                try
                {
                    System.Windows.Controls.PrintDialog dialog = new System.Windows.Controls.PrintDialog();
                    if (!string.IsNullOrEmpty(PrintName))
                    {
                        dialog.PrintQueue = new PrintQueue(new PrintServer(), PrintName);
                    }
                    dialog.MinPage = 1;
                    dialog.MaxPage = 1;
                    dialog.PageRangeSelection = PageRangeSelection.AllPages;
                    dialog.UserPageRangeEnabled = false;
                    PrinterSettings pst = new PrinterSettings();
                    pst.PrinterName = PrintName;
                    if (size == (int)SizePrintEnum.SIZE_80MM)
                    {
                        Template.Booking bill = new Template.Booking();
                        if (currentSetting.IsPrintBillLogo)
                        {
                            bill.Logo.Visibility = Visibility.Visible;
                            BitmapImage bitmapImage = new BitmapImage(new Uri(Environment.CurrentDirectory + @"\logo_branch.png"));
                            if (bitmapImage != null)
                            {
                                bill.Logo.Source = bitmapImage;
                            }
                            else
                            {
                                bill.Logo.Visibility = Visibility.Collapsed;
                            }
                        }
                        else
                        {
                            bill.Logo.Visibility = Visibility.Collapsed;
                        }
                        bill.ModePrint.Text = mode;
                        bill.RestaurantName.Text = branch.Name;
                        bill.RestaurantAddress.Text = branch.Address;

                        bill.TimeBooking.Text = booking.BookingTime;
                        bill.CustomerName.Text = booking.CustomerName;
                        bill.PhoneNumber.Text = booking.CustomerPhone;
                        bill.NumberSlot.Text = booking.NumberSlot.ToString();
                        //if (booking.Tables != null)
                        if (booking.Tables != null && booking.Tables.Count > 0) // Dat
                        {
                            bill.AreaName.Text = booking.Tables[0].AreaNameBooking;
                            bill.TableName.Text = booking.TableFormatString;
                        }
                        else
                        {
                            bill.AreaTitle.Visibility = Visibility.Collapsed;
                        }
                        if (IsReturnDeposit)
                        {
                            bill.DepositAmount.Text = booking.DepositString;
                            bill.ReturnDepositAmount.Text = booking.ReturnDepositString;
                            bill.AmountTitle.Visibility = Visibility.Collapsed;
                            bill.TableFood.Visibility = Visibility.Collapsed;
                            bill.LineOne.Visibility = Visibility.Collapsed;
                            bill.LineTwo.Visibility = Visibility.Collapsed;
                        }
                        else
                        {
                            bill.DepositAmount.Text = booking.DepositString;
                            bill.ReturnDepositAmount.Visibility = Visibility.Collapsed;
                            bill.DepositTitle.Visibility = Visibility.Collapsed;
                            List<FoodRequest> f = booking.Foods.Where(x => x.Quantity != 0).ToList();
                            if (f.Count > 0 && f != null)
                            {
                                for (int i = 1; i <= f.Count; i++)
                                {
                                    TableRow currentRow = new TableRow();
                                    List<TableRow> tableRows = new List<TableRow>();
                                    currentRow.FontSize = 12;
                                    currentRow.Background = System.Windows.Media.Brushes.White;
                                    Paragraph foodName = new Paragraph(new Run(Utils.Utils.ConvertToUpperAndLower(f[i - 1].Name)));
                                    foodName.TextAlignment = System.Windows.TextAlignment.Left;
                                    foodName.Margin = new Thickness(5, 3, 0, 3);
                                    TableCell cell = new TableCell(foodName);
                                    cell.ColumnSpan = 3;
                                    currentRow.Cells.Add(cell);

                                    Paragraph quantity = new Paragraph();
                                    quantity.Inlines.Add(new Run
                                    {
                                        Text = f[i - 1].Quantity.ToString(),
                                        FontSize = 14,
                                        FontWeight = FontWeights.Bold,
                                    });
                                    quantity.Inlines.Add(new Run(" x"));
                                    quantity.TextAlignment = System.Windows.TextAlignment.Center;
                                    quantity.Margin = new Thickness(0, 3, 0, 3);
                                    TableCell cellQuantity = new TableCell(quantity);
                                    cellQuantity.ColumnSpan = 1;
                                    currentRow.Cells.Add(cellQuantity);

                                    Paragraph UnitPrice = new Paragraph(new Run(string.Format("{0:0,0}", f[i - 1].PriceString)));
                                    UnitPrice.TextAlignment = System.Windows.TextAlignment.Center;
                                    UnitPrice.Margin = new Thickness(0, 3, 0, 3);
                                    TableCell cellUnitPrice = new TableCell(UnitPrice);
                                    cellUnitPrice.ColumnSpan = 2;
                                    currentRow.Cells.Add(cellUnitPrice);
                                    Paragraph totalPrice = new Paragraph(new Run(Utils.Utils.FormatMoney(f[i - 1].TotalAmountString)));
                                    totalPrice.TextAlignment = System.Windows.TextAlignment.Center;
                                    totalPrice.Margin = new Thickness(0, 3, 5, 3);

                                    TableCell cellPrice = new TableCell(totalPrice);
                                    cellPrice.ColumnSpan = 2;

                                    currentRow.Cells.Add(cellPrice);

                                    bill.TableRow.RowGroups[0].Rows.Add(currentRow);
                                    for (int k = 1; k <= tableRows.Count(); k++)
                                    {
                                        bill.TableRow.RowGroups[0].Rows.Add(tableRows[k - 1]);
                                    }
                                }
                            }
                            else
                            {
                                bill.AmountTitle.Visibility = Visibility.Collapsed;
                                bill.TableFood.Visibility = Visibility.Collapsed;
                                bill.LineOne.Visibility = Visibility.Collapsed;
                                bill.LineTwo.Visibility = Visibility.Collapsed;
                            }
                        }
                        bill.TotalAmount.Text = booking.TotalAmountString;
                        bill.PhoneRes.Text = "ĐT: " + branch.Phone;
                        bill.billDocument.MaxPageWidth = 295;
                        bill.billDocument.MaxPageHeight = 3276;
                        FlowDocument doc = bill.billDocument;
                        doc.PagePadding = new Thickness(10);
                        doc.ColumnWidth = dialog.PrintableAreaWidth;
                        doc.PageHeight = dialog.PrintableAreaHeight;
                        IDocumentPaginatorSource idSource = doc;
                        dialog.PrintDocument(idSource.DocumentPaginator, "Invoice");
                    }
                    else if (size == (int)SizePrintEnum.SIZE_58MM)
                    {
                        Template.Booking58MM bill = new Template.Booking58MM();
                        if (currentSetting.IsPrintBillLogo)
                        {
                            bill.Logo.Visibility = Visibility.Visible;
                            BitmapImage bitmapImage = new BitmapImage(new Uri(Environment.CurrentDirectory + @"\logo_branch.png"));
                            bill.Logo.Source = bitmapImage;
                        }
                        else
                        {
                            bill.Logo.Visibility = Visibility.Collapsed;
                        }
                        bill.ModePrint.Text = mode;
                        bill.RestaurantName.Text = branch.Name;
                        bill.RestaurantAddress.Text = branch.Address;

                        bill.TimeBooking.Text = booking.BookingTime;
                        bill.CustomerName.Text = booking.CustomerName;
                        bill.PhoneNumber.Text = booking.CustomerPhone;
                        bill.NumberSlot.Text = booking.NumberSlot.ToString();

                        if (IsReturnDeposit)
                        {
                            bill.DepositAmount.Text = booking.DepositString;
                            bill.ReturnDepositAmount.Text = booking.ReturnDepositString;
                            bill.AmountTitle.Visibility = Visibility.Collapsed;
                            bill.TableFood.Visibility = Visibility.Collapsed;
                            bill.LineOne.Visibility = Visibility.Collapsed;
                            bill.LineTwo.Visibility = Visibility.Collapsed;
                        }
                        else
                        {
                            bill.DepositAmount.Text = booking.DepositString;
                            bill.ReturnDepositAmount.Visibility = Visibility.Collapsed;
                            bill.DepositTitle.Visibility = Visibility.Collapsed;
                            List<FoodRequest> f = booking.Foods.Where(x => x.Quantity != 0).ToList();
                            if (f.Count > 0 && f != null)
                            {
                                for (int i = 1; i <= f.Count; i++)
                                {
                                    TableRow currentRow = new TableRow();
                                    List<TableRow> tableRows = new List<TableRow>();
                                    currentRow.FontSize = 12;
                                    currentRow.Background = System.Windows.Media.Brushes.White;
                                    Paragraph foodName = new Paragraph(new Run(Utils.Utils.ConvertToUpperAndLower(f[i - 1].Name)));
                                    foodName.TextAlignment = System.Windows.TextAlignment.Left;
                                    foodName.Margin = new Thickness(5, 3, 0, 3);
                                    TableCell cell = new TableCell(foodName);
                                    cell.ColumnSpan = 3;
                                    currentRow.Cells.Add(cell);

                                    Paragraph quantity = new Paragraph();
                                    quantity.Inlines.Add(new Run
                                    {
                                        Text = f[i - 1].Quantity.ToString(),
                                        FontSize = 14,
                                        FontWeight = FontWeights.Bold,
                                    });
                                    quantity.Inlines.Add(new Run(" x"));
                                    quantity.TextAlignment = System.Windows.TextAlignment.Center;
                                    quantity.Margin = new Thickness(0, 3, 0, 3);
                                    TableCell cellQuantity = new TableCell(quantity);
                                    cellQuantity.ColumnSpan = 1;
                                    currentRow.Cells.Add(cellQuantity);

                                    Paragraph UnitPrice = new Paragraph(new Run(string.Format("{0:0,0}", f[i - 1].PriceString)));
                                    UnitPrice.TextAlignment = System.Windows.TextAlignment.Center;
                                    UnitPrice.Margin = new Thickness(0, 3, 0, 3);
                                    TableCell cellUnitPrice = new TableCell(UnitPrice);
                                    cellUnitPrice.ColumnSpan = 2;
                                    currentRow.Cells.Add(cellUnitPrice);
                                    Paragraph totalPrice = new Paragraph(new Run(Utils.Utils.FormatMoney(f[i - 1].TotalAmountString)));
                                    totalPrice.TextAlignment = System.Windows.TextAlignment.Center;
                                    totalPrice.Margin = new Thickness(0, 3, 5, 3);

                                    TableCell cellPrice = new TableCell(totalPrice);
                                    cellPrice.ColumnSpan = 2;

                                    currentRow.Cells.Add(cellPrice);

                                    bill.TableRow.RowGroups[0].Rows.Add(currentRow);
                                    for (int k = 1; k <= tableRows.Count(); k++)
                                    {
                                        bill.TableRow.RowGroups[0].Rows.Add(tableRows[k - 1]);
                                    }
                                }
                            }
                            else
                            {
                                bill.AmountTitle.Visibility = Visibility.Collapsed;
                                bill.TableFood.Visibility = Visibility.Collapsed;
                                bill.LineOne.Visibility = Visibility.Collapsed;
                                bill.LineTwo.Visibility = Visibility.Collapsed;
                            }
                        }
                        bill.TotalAmount.Text = booking.TotalAmountString;
                        bill.PhoneRes.Text = "ĐT: " + branch.Phone;
                        bill.billDocument.MaxPageWidth = 295;
                        bill.billDocument.MaxPageHeight = 3276;
                        FlowDocument doc = bill.billDocument;
                        doc.PagePadding = new Thickness(10);
                        doc.ColumnWidth = dialog.PrintableAreaWidth;
                        doc.PageHeight = dialog.PrintableAreaHeight;
                        IDocumentPaginatorSource idSource = doc;
                        dialog.PrintDocument(idSource.DocumentPaginator, "Invoice");
                    }

                }
                catch (Exception ex)
                {
                    WriteLog.logs(ex.Message);
                }

            }
            public PrintText() { }
            public T Deserialize<T>(IRestResponse response)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    dynamic jsonResponse = JsonConvert.DeserializeObject(response.Content);
                    if (jsonResponse.status == 200)
                    {
                        T orderResponse = jsonResponse.data.ToObject<T>();
                        return orderResponse;
                    }
                }
                return default(T);
            }

            public T Get<T>(string cacheKey) where T : class
            {
                throw new NotImplementedException();
            }

            public void LogError(Exception ex, string infoMessage)
            {
                throw new NotImplementedException();
            }

            public void Set(string cacheKey, object item, int minutes)
            {
                throw new NotImplementedException();
            }
        }
    }
}
