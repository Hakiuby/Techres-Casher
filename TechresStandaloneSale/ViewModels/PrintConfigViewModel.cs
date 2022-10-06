using Caliburn.Micro;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Printing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using TechresStandaloneSale.Helpers;
using TechresStandaloneSale.Interface;
using TechresStandaloneSale.Models;
using TechresStandaloneSale.Models.Request;
using TechresStandaloneSale.Models.Response;
using TechresStandaloneSale.Services;
using TechresStandaloneSale.Template;
using TechresStandaloneSale.UserControlView;
using TechresStandaloneSale.Views;
using TechresStandaloneSale.Views.Dialogs;

namespace TechresStandaloneSale.ViewModels
{
    public class PrintConfigViewModel : BaseViewModel, ICacheService, IDeserializer, IErrorLogger
    {
        private int _height;
        public int height { get => _height; set { _height = value; OnPropertyChanged("height"); } }
        private ObservableCollection<Printer> _PrintBill = new ObservableCollection<Printer>();
        public ObservableCollection<Printer> PrintBill { get => _PrintBill; set { _PrintBill = value; OnPropertyChanged("PrintBill"); } }
        private ObservableCollection<Printer> _PrintFishTank = new ObservableCollection<Printer>();
        public ObservableCollection<Printer> PrintFishTank { get => _PrintFishTank; set { _PrintFishTank = value; OnPropertyChanged("PrintFishTank"); } }
        private ObservableCollection<Printer> _PrintQRCode = new ObservableCollection<Printer>();
        private ObservableCollection<Printer> _PrinterStamp; 
        public ObservableCollection<Printer> PrinterStamp
        {
            get => _PrinterStamp; 
            set
            {
                _PrinterStamp = value;
                OnPropertyChanged("PrinterStamp"); 
            }
        }
        public ObservableCollection<Printer> PrintQRCode { get => _PrintQRCode; set { _PrintQRCode = value; OnPropertyChanged("PrintQRCode"); } }
        private ObservableCollection<Printer> _PrintBranchQRCode = new ObservableCollection<Printer>();
        public ObservableCollection<Printer> PrintBranchQRCode { get => _PrintBranchQRCode; set { _PrintBranchQRCode = value; OnPropertyChanged("PrintBranchQRCode"); } }
        private ObservableCollection<Printer> _PrintEmployeeQRCode = new ObservableCollection<Printer>();
        public ObservableCollection<Printer> PrintEmployeeQRCode { get => _PrintEmployeeQRCode; set { _PrintEmployeeQRCode = value; OnPropertyChanged("PrintEmployeeQRCode"); } }
        private ObservableCollection<Printer> _PrinterNameList = new ObservableCollection<Printer>();
        public ObservableCollection<Printer> PrinterNameList { get => _PrinterNameList; set { _PrinterNameList = value; OnPropertyChanged("PrinterNameList"); } }

        private ObservableCollection<BasicModel> _SizeQRCode = new ObservableCollection<BasicModel>();
        public ObservableCollection<BasicModel> SizeQRCode { get => _SizeQRCode; set { _SizeQRCode = value; OnPropertyChanged("SizeQRCode"); } }
        private ObservableCollection<BasicModel> _SizeBranchQRCode = new ObservableCollection<BasicModel>();
        public ObservableCollection<BasicModel> SizeBranchQRCode { get => _SizeBranchQRCode; set { _SizeBranchQRCode = value; OnPropertyChanged("SizeBranchQRCode"); } }
        private ObservableCollection<BasicModel> _SizeEmployeeQRCode = new ObservableCollection<BasicModel>();
        public ObservableCollection<BasicModel> SizeEmployeeQRCode { get => _SizeEmployeeQRCode; set { _SizeEmployeeQRCode = value; OnPropertyChanged("SizeEmployeeQRCode"); } }

        private ObservableCollection<BasicModel> _SizeBill = new ObservableCollection<BasicModel>();
        public ObservableCollection<BasicModel> SizeBill { get => _SizeBill; set { _SizeBill = value; OnPropertyChanged("SizeBill"); } }
        private ObservableCollection<BasicModel> _SizeFishTank = new ObservableCollection<BasicModel>();
        public ObservableCollection<BasicModel> SizeFishTank { get => _SizeFishTank; set { _SizeFishTank = value; OnPropertyChanged("SizeFishTank"); } }
        private ObservableCollection<BasicModel> _SizeStamp; 
        public ObservableCollection<BasicModel> SizeStamp { get => _SizeStamp; set { _SizeStamp = value; OnPropertyChanged("SizeStamp"); } }
        private ObservableCollection<BasicModel> _PrinterPaperSizeList = new ObservableCollection<BasicModel>();
        public ObservableCollection<BasicModel> PrinterPaperSizeList { get => _PrinterPaperSizeList; set { _PrinterPaperSizeList = value; OnPropertyChanged("PrinterPaperSizeList"); } }

        private ObservableCollection<Kitchen> _RestaurantKitchenList = new ObservableCollection<Kitchen>();
        public ObservableCollection<Kitchen> RestaurantKitchenList { get => _RestaurantKitchenList; set { _RestaurantKitchenList = value; OnPropertyChanged("RestaurantKitchenList"); } }

        public DeviceClient deviceClient = new DeviceClient();

        private User user = (User)Utils.Utils.GetCacheValue(Constants.CURRENT_USER);
        private Printer _PrintFishTankItem { get; set; }
        public Printer PrintFishTankItem { get => _PrintFishTankItem; set { _PrintFishTankItem = value; OnPropertyChanged("PrintFishTankItem"); } }
        private Printer _PrintQRCodeItem { get; set; }
        public Printer PrintQRCodeItem { get => _PrintQRCodeItem; set { _PrintQRCodeItem = value; OnPropertyChanged("PrintQRCodeItem"); } }
        private Printer _PrintBranchQRCodeItem { get; set; }
        public Printer PrintBranchQRCodeItem { get => _PrintBranchQRCodeItem; set { _PrintBranchQRCodeItem = value; OnPropertyChanged("PrintBranchQRCodeItem"); } }
        private Printer _PrintEmployeeQRCodeItem { get; set; }
        public Printer PrintEmployeeQRCodeItem { get => _PrintEmployeeQRCodeItem; set { _PrintEmployeeQRCodeItem = value; OnPropertyChanged("PrintEmployeeQRCodeItem"); } }
        private Printer _PrintStampItem;
        public Printer PrintStampItem
        {
            get => _PrintStampItem;
            set
            {
                _PrintStampItem = value;
                OnPropertyChanged("PrintStampItem"); 
            }
        }
        private Printer _PrintBillItem { get; set; }
        public Printer PrintBillItem { get => _PrintBillItem; set { _PrintBillItem = value; OnPropertyChanged("PrintBillItem"); } }

        private Printer _SizeQRCodeText { get; set; }
        public Printer SizeQRCodeText { get => _SizeQRCodeText; set { _SizeQRCodeText = value; } }
        private BasicModel _SizeQRCodeItem { get; set; }
        public BasicModel SizeQRCodeItem { get => _SizeQRCodeItem; set { _SizeQRCodeItem = value; OnPropertyChanged("SizeQRCodeItem"); } }
        private BasicModel _SizeBranchQRCodeItem { get; set; }
        public BasicModel SizeBranchQRCodeItem { get => _SizeBranchQRCodeItem; set { _SizeBranchQRCodeItem = value; OnPropertyChanged("SizeBranchQRCodeItem"); } }
        private BasicModel _SizeEmployeeQRCodeItem { get; set; }
        public BasicModel SizeEmployeeQRCodeItem { get => _SizeEmployeeQRCodeItem; set { _SizeEmployeeQRCodeItem = value; OnPropertyChanged("SizeEmployeeQRCodeItem"); } }
        private BasicModel _SizeBillItem { get; set; }
        public BasicModel SizeBillItem { get => _SizeBillItem; set { _SizeBillItem = value; OnPropertyChanged("SizeBillItem"); } }
        private BasicModel _SizeFishTankItem { get; set; }
        public BasicModel SizeFishTankItem { get => _SizeFishTankItem; set { _SizeFishTankItem = value; OnPropertyChanged("SizeFishTankItem"); } }
        private BasicModel _SizeStampItem; 
        public BasicModel SizeStampItem { get => _SizeStampItem; set { _SizeStampItem = value; OnPropertyChanged("SizeStampItem"); } }

        public ICommand SaveCommand { get; set; }

        public ICommand IsSeaFoodCommand { get; set; }

        public ICommand IsBillCommand { get; set; }

        public ICommand IsCookCommand { get; set; }

        public ICommand IsDrinkCommand { get; set; }

        public ICommand IsBBQCommand { get; set; }

        public ICommand IsQRCodeCommand { get; set; }
        public ICommand IsFishTankCommand { get; set; }
        public ICommand IsBranchQRCodeCommand { get; set; }
        public ICommand IsEmployeeQRCodeCommand { get; set; }
        public ICommand PreviewBillCommand { get; set; }
        public ICommand PreviewCommand { get; set; }
        public ICommand PreviewQRCodeCommand { get; set; }
        public ICommand PreviewBranchQRCodeCommand { get; set; }
        public ICommand PreviewEmployeeQRCodeCommand { get; set; }
        public ICommand PrintTestCommand { get; set; }
        public ICommand PrintTestBillCommand { get; set; }
        public ICommand PrintTestQRCodeCommand { get; set; }
        public ICommand PrintTestBranchQRCodeCommand { get; set; }
        public ICommand PrintTestEmployeeQRCodeCommand { get; set; }
        public ICommand IsCheckCommand { get; set; }
        public ICommand PreviewFishTankCommand { get; set; }
        public ICommand PrintTestFishTankCommand { get; set; }
        public ICommand StampPrintCommand { get; set; }

        private bool _StampPrintChecked; 
        public bool StampPrintChecked
        {
            get => _StampPrintChecked;
            set
            {
                _StampPrintChecked = value;
                OnPropertyChanged("StampPrintChecked"); 
            }
        }
        private bool _FishTankChecked;
        public bool FishTankChecked
        {
            get
            {
                return _FishTankChecked;
            }
            set
            {
                _FishTankChecked = value;

                OnPropertyChanged("FishTankChecked");
            }
        }
        private bool _QRCodeChecked;
        public bool QRCodeChecked
        {
            get
            {
                return _QRCodeChecked;
            }
            set
            {
                _QRCodeChecked = value;

                OnPropertyChanged("QRCodeChecked");
            }
        }
        private bool _BranchQRCodeChecked;
        public bool BranchQRCodeChecked
        {
            get
            {
                return _BranchQRCodeChecked;
            }
            set
            {
                _BranchQRCodeChecked = value;

                OnPropertyChanged("BranchQRCodeChecked");
            }
        }
        private bool _EmployeeQRCodeChecked;
        public bool EmployeeQRCodeChecked
        {
            get
            {
                return _EmployeeQRCodeChecked;
            }
            set
            {
                _EmployeeQRCodeChecked = value;

                OnPropertyChanged("EmployeeQRCodeChecked");
            }
        }


        private bool _BillChecked;
        public bool BillChecked
        {
            get
            {
                return _BillChecked;
            }
            set
            {
                _BillChecked = value;

                OnPropertyChanged("BillChecked");
            }
        }
        private Visibility _PrintCasherVisibility;
        public Visibility PrintCasherVisibility
        {
            get
            {
                return _PrintCasherVisibility;
            }
            set
            {
                _PrintCasherVisibility = value;

                OnPropertyChanged("PrintCasherVisibility");
            }
        }

        private Visibility _PrintFishTankVisibility;
        public Visibility PrintFishTankVisibility
        {
            get
            {
                return _PrintFishTankVisibility;
            }
            set
            {
                _PrintFishTankVisibility = value;

                OnPropertyChanged("PrintFishTankVisibility");
            }
        }
        private Visibility _PrintQRCodeVisibility;
        public Visibility PrintQRCodeVisibility
        {
            get
            {
                return _PrintQRCodeVisibility;
            }
            set
            {
                _PrintQRCodeVisibility = value;

                OnPropertyChanged("PrintQRCodeVisibility");
            }
        }
        private Visibility _PrintBranchQRCodeVisibility;
        public Visibility PrintBranchQRCodeVisibility
        {
            get
            {
                return _PrintBranchQRCodeVisibility;
            }
            set
            {
                _PrintBranchQRCodeVisibility = value;

                OnPropertyChanged("PrintBranchQRCodeVisibility");
            }
        }
        private Visibility _PrintEmployeeQRCodeVisibility;
        public Visibility PrintEmployeeQRCodeVisibility
        {
            get
            {
                return _PrintEmployeeQRCodeVisibility;
            }
            set
            {
                _PrintEmployeeQRCodeVisibility = value;

                OnPropertyChanged("PrintEmployeeQRCodeVisibility");
            }
        }

        private Visibility _PrintBillVisibility;
        public Visibility PrintBillVisibility
        {
            get
            {
                return _PrintBillVisibility;
            }
            set
            {
                _PrintBillVisibility = value;

                OnPropertyChanged("PrintBillVisibility");
            }
        }
        private Visibility _KitchentPrintVisibility;
        public Visibility KitchentPrintVisibility
        {
            get
            {
                return _KitchentPrintVisibility;
            }
            set
            {
                _KitchentPrintVisibility = value;

                OnPropertyChanged("KitchentPrintVisibility");
            }
        }
        private User currentUser = (User)Utils.Utils.GetCacheValue(Constants.CURRENT_USER);
        private SettingData currentSetting = (SettingData)Utils.Utils.GetCacheValue(Constants.CURRENT_SETTING);
        private Kitchen currentKitchen = Utils.Utils.AsObject<Kitchen>(Properties.Settings.Default.CurrentKitchen);
        private void GetRestaurantKitchen()
        {
            if (RestaurantKitchenList == null)
            {
                RestaurantKitchenList = new ObservableCollection<Kitchen>();
            }
            else
            {
                RestaurantKitchenList.Clear();
            }
            KitchenClient client = new KitchenClient(this, this, this);
            KitchenResponse response = client.GetKitchenResponses((int)currentUser.RestaurantBrandId,currentUser.BranchId, -1);
            if (response != null && response.Status == (int)ResponseEnum.OK)
            {
                foreach (Kitchen kitchen in response.Data)
                {
                    if (kitchen.PrinterPaperSize == (int)SizePrintEnum.SIZE_58MM || kitchen.PrinterPaperSize == (int)SizePrintEnum.SIZE_80MM)
                    {
                        kitchen.PrinterPaperSizeItem = PrinterPaperSizeList.Where(x => x.Value == kitchen.PrinterPaperSize).FirstOrDefault();
                    }
                    else
                    {
                        kitchen.PrinterPaperSizeItem = PrinterPaperSizeList[1];
                    }
                    if (string.IsNullOrEmpty(kitchen.PrinterName))
                    {
                        kitchen.PrinterNameItem = PrinterNameList[0];
                    }
                    else
                    {
                        kitchen.PrinterNameItem = PrinterNameList.Where(x => x.PrintName == kitchen.PrinterName).FirstOrDefault();
                    }
                    RestaurantKitchenList.Add(kitchen);
                }
            }
        }
        public void CheckRestaurantKitchen()
        {
            KitchentPrintVisibility = Visibility.Collapsed;
            PrintCasherVisibility = Visibility.Collapsed;
            if (currentSetting.BranchType == (int)BranchTypeEnum.MEDIUM && Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.CASHIER_ACCESS), currentUser.Permissions))
            {
                KitchentPrintVisibility = Visibility.Visible;
                PrintCasherVisibility = Visibility.Visible;
                GetRestaurantKitchen();
            }
            else if (currentSetting.BranchType == (int)BranchTypeEnum.SMALL && Utils.Utils.CheckListPermissionsEmployee(new List<string>() { Enum.GetName(typeof(TechresEnum), TechresEnum.CASHIER_ACCESS), Enum.GetName(typeof(TechresEnum), TechresEnum.OWNER) }, currentUser.Permissions))
            {
                KitchentPrintVisibility = Visibility.Visible;
                PrintCasherVisibility = Visibility.Visible;
                GetRestaurantKitchen();
            }
            else if (currentSetting.BranchType >= (int)BranchTypeEnum.LARGE && Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.CASHIER_ACCESS), currentUser.Permissions))
            {
                PrintCasherVisibility = Visibility.Visible;
              //  KitchentPrintVisibility = Visibility.Visible;
            }
            else if (Utils.Utils.CheckListPermissionsEmployee(new List<string>() { Enum.GetName(typeof(TechresEnum), TechresEnum.CHEF_COOK_ACCESS), Enum.GetName(typeof(TechresEnum), TechresEnum.CHEF_BBQ_ACCESS) }, currentUser.Permissions))
            {
                KitchentPrintVisibility = Visibility.Visible;
                if (RestaurantKitchenList == null)
                {
                    RestaurantKitchenList = new ObservableCollection<Kitchen>();
                }
                else
                {
                    RestaurantKitchenList.Clear();
                }
                if (currentKitchen != null)
                {
                    if (currentKitchen.PrinterPaperSize == (int)SizePrintEnum.SIZE_58MM || currentKitchen.PrinterPaperSize == (int)SizePrintEnum.SIZE_80MM)
                    {
                        currentKitchen.PrinterPaperSizeItem = PrinterPaperSizeList.Where(x => x.Value == currentKitchen.PrinterPaperSize).FirstOrDefault();
                    }
                    else
                    {
                        currentKitchen.PrinterPaperSizeItem = PrinterPaperSizeList[1];
                    }
                    if (string.IsNullOrEmpty(currentKitchen.PrinterName))
                    {
                        currentKitchen.PrinterNameItem = PrinterNameList[0];
                    }
                    else
                    {
                        currentKitchen.PrinterNameItem = PrinterNameList.Where(x => x.PrintName == currentKitchen.PrinterName).FirstOrDefault();
                    }
                    RestaurantKitchenList.Add(currentKitchen);
                }
            }
            else
            {
                KitchentPrintVisibility = Visibility.Collapsed;
                PrintCasherVisibility = Visibility.Collapsed;
                //PrintBillVisibility = Visibility.Collapsed;
                //PrintBranchQRCodeVisibility = Visibility.Collapsed;
                //PrintEmployeeQRCodeVisibility = Visibility.Collapsed;
                //PrintFishTankVisibility = Visibility.Collapsed;
                //PrintQRCodeVisibility = Visibility.Collapsed;
            }
        }
        public void GetAllPrintName()
        {
            foreach (Printer l in Helpers.PrintConfig.FindAllPrint())
            {
                PrintBill.Add(l);
                PrinterNameList.Add(l);
                PrintQRCode.Add(l);
                PrintBranchQRCode.Add(l);
                PrintEmployeeQRCode.Add(l);
                PrintFishTank.Add(l);
            }
        }
        public void SetSizePrint()
        {
            if (PrinterPaperSizeList == null)
            {
                PrinterPaperSizeList = new ObservableCollection<BasicModel>();
            }
            else
            {
                PrinterPaperSizeList.Clear();
            }
            PrinterPaperSizeList.Add(new BasicModel((int)SizePrintEnum.SIZE_58MM, MessageValue.MESSAGE_FROM_BILL_SIZE_58MM));
            PrinterPaperSizeList.Add(new BasicModel((int)SizePrintEnum.SIZE_80MM, MessageValue.MESSAGE_FROM_BILL_SIZE_80MM));

            if (SizeBill == null)
            {
                SizeBill = new ObservableCollection<BasicModel>();
            }
            else
            {
                SizeBill.Clear();
            }
            SizeBill.Add(new BasicModel((int)SizePrintEnum.SIZE_58MM, MessageValue.MESSAGE_FROM_BILL_SIZE_58MM));
            SizeBill.Add(new BasicModel((int)SizePrintEnum.SIZE_80MM, MessageValue.MESSAGE_FROM_BILL_SIZE_80MM));

            if (SizeQRCode == null)
            {
                SizeQRCode = new ObservableCollection<BasicModel>();
            }
            else
            {
                SizeQRCode.Clear();
            }
            SizeQRCode.Add(new BasicModel((int)SizePrintEnum.SIZE_58MM, MessageValue.MESSAGE_FROM_BILL_SIZE_58MM));
            SizeQRCode.Add(new BasicModel((int)SizePrintEnum.SIZE_80MM, MessageValue.MESSAGE_FROM_BILL_SIZE_80MM));
            if (SizeBranchQRCode == null)
            {
                SizeBranchQRCode = new ObservableCollection<BasicModel>();
            }
            else
            {
                SizeBranchQRCode.Clear();
            }
            SizeBranchQRCode.Add(new BasicModel((int)SizePrintEnum.SIZE_58MM, MessageValue.MESSAGE_FROM_BILL_SIZE_58MM));
            SizeBranchQRCode.Add(new BasicModel((int)SizePrintEnum.SIZE_80MM, MessageValue.MESSAGE_FROM_BILL_SIZE_80MM));
            if (SizeEmployeeQRCode == null)
            {
                SizeEmployeeQRCode = new ObservableCollection<BasicModel>();
            }
            else
            {
                SizeEmployeeQRCode.Clear();
            }
            SizeEmployeeQRCode.Add(new BasicModel((int)SizePrintEnum.SIZE_58MM, MessageValue.MESSAGE_FROM_BILL_SIZE_58MM));
            SizeEmployeeQRCode.Add(new BasicModel((int)SizePrintEnum.SIZE_80MM, MessageValue.MESSAGE_FROM_BILL_SIZE_80MM));
            if (SizeFishTank == null)
            {
                SizeFishTank = new ObservableCollection<BasicModel>();
            }
            else
            {
                SizeFishTank.Clear();
            }
            SizeFishTank.Add(new BasicModel((int)SizePrintEnum.SIZE_58MM, MessageValue.MESSAGE_FROM_BILL_SIZE_58MM));
            SizeFishTank.Add(new BasicModel((int)SizePrintEnum.SIZE_80MM, MessageValue.MESSAGE_FROM_BILL_SIZE_80MM));
        }
        public PrintConfigViewModel()
        {
            try
            {
                height = 120; 
                SetSizePrint();
                GetAllPrintName();
                DeviceConfigWrapper CacheDevice = deviceClient.RealConfigs();
                if (CacheDevice != null)
                {

                    if (CacheDevice.IsQRCode == true)
                    {
                        QRCodeChecked = true;
                        PrintQRCodeVisibility = Visibility.Visible;
                        if (!string.IsNullOrEmpty(CacheDevice.QrCodePrinter))
                        {
                            PrintQRCodeItem = PrintQRCode.Where(x => x.PrintName == CacheDevice.QrCodePrinter).FirstOrDefault();
                        }
                        if (CacheDevice.QrCodeSize > 0)
                        {
                            SizeQRCodeItem = SizeQRCode.Where(x => x.Value == CacheDevice.QrCodeSize).FirstOrDefault();
                        }
                        else
                        {
                            SizeQRCodeItem = SizeQRCode[1];
                        }
                    }
                    else
                    {
                        QRCodeChecked = false;
                        PrintQRCodeVisibility = Visibility.Hidden;
                    }
                    if (CacheDevice.IsBranchQRCode == true)
                    {
                        BranchQRCodeChecked = true;
                        PrintBranchQRCodeVisibility = Visibility.Visible;
                        if (!string.IsNullOrEmpty(CacheDevice.BranchQrCodePrinter))
                        {
                            PrintBranchQRCodeItem = PrintBranchQRCode.Where(x => x.PrintName == CacheDevice.BranchQrCodePrinter).FirstOrDefault();
                        }
                        if (CacheDevice.QrCodeSize > 0)
                        {
                            SizeBranchQRCodeItem = SizeBranchQRCode.Where(x => x.Value == CacheDevice.BranchQrCodeSize).FirstOrDefault();
                        }
                        else
                        {
                            SizeBranchQRCodeItem = SizeBranchQRCode[1];
                        }
                    }
                    else
                    {
                        BranchQRCodeChecked = false;
                        PrintBranchQRCodeVisibility = Visibility.Hidden;
                    }
                    if (CacheDevice.IsEmployeeQRCode == true)
                    {
                        EmployeeQRCodeChecked = true;
                        PrintEmployeeQRCodeVisibility = Visibility.Visible;
                        if (!string.IsNullOrEmpty(CacheDevice.EmployeeQrCodePrinter))
                        {
                            PrintEmployeeQRCodeItem = PrintEmployeeQRCode.Where(x => x.PrintName == CacheDevice.EmployeeQrCodePrinter).FirstOrDefault();
                        }
                        if (CacheDevice.QrCodeSize > 0)
                        {
                            SizeEmployeeQRCodeItem = SizeEmployeeQRCode.Where(x => x.Value == CacheDevice.EmployeeQrCodeSize).FirstOrDefault();
                        }
                        else
                        {
                            SizeEmployeeQRCodeItem = SizeEmployeeQRCode[1];
                        }
                    }
                    else
                    {
                        EmployeeQRCodeChecked = false;
                        PrintEmployeeQRCodeVisibility = Visibility.Hidden;
                    }
                    if (CacheDevice.IsCasher == true)
                    {
                        BillChecked = true;
                        PrintBillVisibility = Visibility.Visible;
                        if (!string.IsNullOrEmpty(CacheDevice.CasherPrinter))
                        {
                            //    this.PrintBill.Add(CacheDevice.CasherPrinter);
                            PrintBillItem = PrintBill.Where(x => x.PrintName == CacheDevice.CasherPrinter).FirstOrDefault();
                        }
                        if (CacheDevice.CasherSize > 0)
                        {
                            SizeBillItem = SizeBill.Where(x => x.Value == CacheDevice.CasherSize).FirstOrDefault();
                        }
                        else
                        {
                            SizeBillItem = SizeBill[1];
                        }
                    }
                    else
                    {
                        BillChecked = false;
                        PrintBillVisibility = Visibility.Hidden;
                    }
                    if (CacheDevice.IsFishtank == true)
                    {
                        FishTankChecked = true;
                        PrintFishTankVisibility = Visibility.Visible;
                        if (!string.IsNullOrEmpty(CacheDevice.FishtankPrinter))
                        {
                            //    this.PrintBill.Add(CacheDevice.CasherPrinter);
                            PrintFishTankItem = PrintFishTank.Where(x => x.PrintName == CacheDevice.FishtankPrinter).FirstOrDefault();
                        }
                        if (CacheDevice.FishtankSize > 0)
                        {
                            SizeFishTankItem = SizeFishTank.Where(x => x.Value == CacheDevice.FishtankSize).FirstOrDefault();
                        }
                        else
                        {
                            SizeFishTankItem = SizeFishTank[1];
                        }
                    }
                    else
                    {
                        FishTankChecked = false;
                        PrintFishTankVisibility = Visibility.Hidden;
                    }
                    if(CacheDevice.IsStamp == true)
                    {
                        StampPrintChecked = true;
                        if (!string.IsNullOrEmpty(CacheDevice.StampPrinter))
                        {
                            PrintStampItem = PrinterStamp.Where(x => x.PrintName == CacheDevice.StampPrinter).FirstOrDefault();
                        }
                        if (CacheDevice.StampSize > 0)
                        {
                            SizeStampItem = SizeStamp.Where(x => x.Value == CacheDevice.StampSize).FirstOrDefault(); 
                        }
                    }
                    else
                    {

                    }
                }
                else
                {
                    QRCodeChecked = false;
                    PrintQRCodeVisibility = Visibility.Hidden;
                    BranchQRCodeChecked = false;
                    PrintBranchQRCodeVisibility = Visibility.Hidden;
                    EmployeeQRCodeChecked = false;
                    PrintEmployeeQRCodeVisibility = Visibility.Hidden;
                    BillChecked = false;
                    PrintBillVisibility = Visibility.Hidden;
                    FishTankChecked = false;
                    PrintFishTankVisibility = Visibility.Hidden;
                }
                CheckRestaurantKitchen();

                IsQRCodeCommand = new RelayCommand<ToggleButton>((p) => { return true; }, p =>
                {
                    if (p.IsChecked.Value == true)
                    {
                        PrintQRCodeVisibility = Visibility.Visible;
                        PrintQRCodeItem = PrintQRCode[0];
                        SizeQRCodeItem = SizeQRCode[1];
                    }
                    else
                    {
                        PrintQRCodeVisibility = Visibility.Collapsed;
                    }
                });
                IsFishTankCommand = new RelayCommand<ToggleButton>((p) => { return true; }, p =>
                {
                    if (p.IsChecked.Value == true)
                    {
                        PrintFishTankVisibility = Visibility.Visible;
                        PrintFishTankItem = PrintQRCode[0];
                        SizeFishTankItem = SizeQRCode[1];
                    }
                    else
                    {
                        PrintFishTankVisibility = Visibility.Collapsed;
                    }
                });
                IsBranchQRCodeCommand = new RelayCommand<ToggleButton>((p) => { return true; }, p =>
                {
                    if (p.IsChecked.Value == true)
                    {
                        PrintBranchQRCodeVisibility = Visibility.Visible;
                        PrintBranchQRCodeItem = PrintBranchQRCode[0];
                        SizeBranchQRCodeItem = SizeBranchQRCode[1];
                    }
                    else
                    {
                        PrintBranchQRCodeVisibility = Visibility.Collapsed;
                    }
                });
                IsEmployeeQRCodeCommand = new RelayCommand<ToggleButton>((p) => { return true; }, p =>
                {
                    if (p.IsChecked.Value == true)
                    {
                        PrintEmployeeQRCodeVisibility = Visibility.Visible;
                        PrintEmployeeQRCodeItem = PrintEmployeeQRCode[0];
                        SizeEmployeeQRCodeItem = SizeEmployeeQRCode[1];
                    }
                    else
                    {
                        PrintEmployeeQRCodeVisibility = Visibility.Collapsed;
                    }
                });
                IsCheckCommand = new RelayCommand<Kitchen>((p) => { return true; }, p =>
                {
                    int index = RestaurantKitchenList.IndexOf(p);
                    RestaurantKitchenList.Remove(p);
                    RestaurantKitchenList.Insert(index, p);
                });
                IsBillCommand = new RelayCommand<ToggleButton>((p) => { return true; }, p =>
                {
                    if (p.IsChecked.Value == true)
                    {
                        PrintBillVisibility = Visibility.Visible;
                        PrintBillItem = PrintBill[0];
                        SizeBillItem = SizeBill[1];
                    }
                    else
                    {
                        PrintBillVisibility = Visibility.Collapsed;
                    }
                });
                PreviewBillCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
                {
                    string name = PrintBillItem.PrintName;
                    int type = 1;
                    PreviewPrintViewWindow PreviewPrint = new PreviewPrintViewWindow();
                    PreviewPrint.DataContext = new PreviewPrintViewModels(user.EmployeeRoleName, name, type, SizeBillItem.Value);
                    PreviewPrint.ShowDialog();
                });
                PreviewCommand = new RelayCommand<Kitchen>((p) => { return true; }, p =>
                {
                    string name = p.PrinterName;
                    int type = 2;
                    PreviewPrintViewWindow PreviewPrint = new PreviewPrintViewWindow();
                    PreviewPrint.DataContext = new PreviewPrintViewModels(user.EmployeeRoleName, name, type, p.PrinterPaperSizeItem.Value);
                    PreviewPrint.ShowDialog();
                });
                PreviewQRCodeCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
                {
                    string name = PrintQRCodeItem.PrintName;
                    int type = 3;
                    PreviewPrintViewWindow PreviewPrint = new PreviewPrintViewWindow();
                    PreviewPrint.DataContext = new PreviewPrintViewModels(user.EmployeeRoleName, name, type, SizeQRCodeItem.Value);
                    PreviewPrint.ShowDialog();
                });
                PreviewBranchQRCodeCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
                {
                    string name = PrintBranchQRCodeItem.PrintName;
                    int type = 7;
                    PreviewPrintViewWindow PreviewPrint = new PreviewPrintViewWindow();
                    PreviewPrint.DataContext = new PreviewPrintViewModels(user.EmployeeRoleName, name, type, SizeBranchQRCodeItem.Value);
                    PreviewPrint.ShowDialog();
                });
                PreviewEmployeeQRCodeCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
                {
                    string name = PrintEmployeeQRCodeItem.PrintName;
                    int type = 8;
                    PreviewPrintViewWindow PreviewPrint = new PreviewPrintViewWindow();
                    PreviewPrint.DataContext = new PreviewPrintViewModels(user.EmployeeRoleName, name, type, SizeEmployeeQRCodeItem.Value);
                    PreviewPrint.ShowDialog();
                });
                PreviewFishTankCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
                {
                    string name = PrintFishTankItem.PrintName;
                    int type = 2;
                    PreviewPrintViewWindow PreviewPrint = new PreviewPrintViewWindow();
                    PreviewPrint.DataContext = new PreviewPrintViewModels(user.EmployeeRoleName, name, type, SizeFishTankItem.Value);
                    PreviewPrint.ShowDialog();
                });
                PrintTestQRCodeCommand = new RelayCommand<UserControl>((p) => { return true; }, (p) =>
                {
                    System.Windows.Controls.PrintDialog dialog = new System.Windows.Controls.PrintDialog();
                    DeviceClient deviceClient = new DeviceClient();
                    DeviceConfigWrapper device = deviceClient.RealConfigs();
                    if (device != null && device.IsQRCode && !string.IsNullOrEmpty(device.QrCodePrinter))
                    {
                        if (!string.IsNullOrEmpty(PrintQRCodeItem.PrintName))
                        {
                            dialog.PrintQueue = new PrintQueue(new PrintServer(), PrintQRCodeItem.PrintName);
                        }
                        MemoryStream ms = new MemoryStream();
                        string qrcode = new QRCodeHelper(MessageValue.MESSAGE_FROM_SETTING_PRINT_TECHRES).GenerateUrl();
                        using (WebClient webClient = new WebClient())
                        {
                            using (Stream stream = webClient.OpenRead(qrcode))
                            {
                                using (Bitmap bitmap = new Bitmap(stream))
                                {
                                    stream.Flush();
                                    stream.Close();
                                    bitmap.Save(ms, ImageFormat.Png);
                                }
                            }
                        }
                        if (SizeQRCodeItem.Value == MessageValue.MESSAGE_FROM_SETTING_PRINT_PAPER_SIZE_80_VALUE)
                        {
                            PrintQrCodeCheckIn print = new PrintQrCodeCheckIn();
                            ms.Position = 0;
                            BitmapImage bi = new BitmapImage();
                            bi.BeginInit();
                            bi.StreamSource = ms;
                            bi.EndInit();
                            print.ImageQrCode.Source = bi;
                            print.Title.Text = MessageValue.MESSAGE_FROM_CHECK_IN_QR;
                            print.RestaurantName.Text = string.Format("{0}", MessageValue.MESSAGE_FROM_SETTING_PRINT_TECHRES);
                            FlowDocument doc = print.InfoDocument;
                            doc.PagePadding = new Thickness(0);
                            //doc.ColumnWidth = dialog.PrintableAreaWidth;
                            doc.PageHeight = dialog.PrintableAreaHeight;
                            IDocumentPaginatorSource idSource = doc;
                            dialog.PrintDocument(idSource.DocumentPaginator, "");
                        }
                        else
                        {
                            PrintQrCodeCheckIn58MM print = new PrintQrCodeCheckIn58MM();
                            ms.Position = 0;
                            BitmapImage bi = new BitmapImage();
                            bi.BeginInit();
                            bi.StreamSource = ms;
                            bi.EndInit();
                            print.ImageQrCode.Source = bi;
                            print.Title.Text = MessageValue.MESSAGE_FROM_CHECK_IN_QR;
                            print.RestaurantName.Text = string.Format("{0}", MessageValue.MESSAGE_FROM_SETTING_PRINT_TECHRES);
                            FlowDocument doc = print.InfoDocument;
                            doc.PagePadding = new Thickness(0);
                            doc.ColumnWidth = dialog.PrintableAreaWidth;
                            doc.PageHeight = dialog.PrintableAreaHeight;
                            IDocumentPaginatorSource idSource = doc;
                            dialog.PrintDocument(idSource.DocumentPaginator, "");
                        }
                    }
                });
                PrintTestBranchQRCodeCommand = new RelayCommand<UserControl>((p) => { return true; }, (p) =>
                {
                    System.Windows.Controls.PrintDialog dialog = new System.Windows.Controls.PrintDialog();
                    DeviceClient deviceClient = new DeviceClient();
                    DeviceConfigWrapper device = deviceClient.RealConfigs();
                    MemoryStream ms = new MemoryStream();
                    string qrcode = new QRCodeHelper(MessageValue.MESSAGE_FROM_SETTING_PRINT_TECHRES).GenerateUrl();
                    using (WebClient webClient = new WebClient())
                    {
                        using (Stream stream = webClient.OpenRead(qrcode))
                        {
                            using (Bitmap bitmap = new Bitmap(stream))
                            {
                                stream.Flush();
                                stream.Close();
                                bitmap.Save(ms, ImageFormat.Png);
                            }
                        }
                    }
                    if (SizeBranchQRCodeItem.Value == MessageValue.MESSAGE_FROM_SETTING_PRINT_PAPER_SIZE_80_VALUE)
                    {
                        PrintQrCodeCheckIn print = new PrintQrCodeCheckIn();
                        ms.Position = 0;
                        BitmapImage bi = new BitmapImage();
                        bi.BeginInit();
                        bi.StreamSource = ms;
                        bi.EndInit();
                        print.ImageQrCode.Source = bi;
                        print.Title.Text = MessageValue.MESSAGE_FROM_CREATE_CUSTOMER_QR;
                        print.RestaurantName.Text = string.Format("{0}", MessageValue.MESSAGE_FROM_SETTING_PRINT_TECHRES);
                        FlowDocument doc = print.InfoDocument;
                        doc.PagePadding = new Thickness(0);
                        //doc.ColumnWidth = dialog.PrintableAreaWidth;
                        doc.PageHeight = dialog.PrintableAreaHeight;
                        IDocumentPaginatorSource idSource = doc;
                        dialog.PrintDocument(idSource.DocumentPaginator, "");
                    }
                    else
                    {
                        PrintQrCodeCheckIn58MM print = new PrintQrCodeCheckIn58MM();
                        ms.Position = 0;
                        BitmapImage bi = new BitmapImage();
                        bi.BeginInit();
                        bi.StreamSource = ms;
                        bi.EndInit();
                        print.ImageQrCode.Source = bi;
                        print.Title.Text = MessageValue.MESSAGE_FROM_CREATE_CUSTOMER_QR;
                        print.RestaurantName.Text = string.Format("{0}", MessageValue.MESSAGE_FROM_SETTING_PRINT_TECHRES);
                        FlowDocument doc = print.InfoDocument;
                        doc.PagePadding = new Thickness(0);
                        doc.ColumnWidth = dialog.PrintableAreaWidth;
                        doc.PageHeight = dialog.PrintableAreaHeight;
                        IDocumentPaginatorSource idSource = doc;
                        dialog.PrintDocument(idSource.DocumentPaginator, "");
                    }

                });
                PrintTestEmployeeQRCodeCommand = new RelayCommand<UserControl>((p) => { return true; }, (p) =>
                {
                    MemoryStream ms = new MemoryStream();
                    string qrcode = new QRCodeHelper(MessageValue.MESSAGE_FROM_SETTING_PRINT_TECHRES).GenerateUrl();
                    using (WebClient webClient = new WebClient())
                    {
                        using (Stream stream = webClient.OpenRead(qrcode))
                        {
                            using (Bitmap bitmap = new Bitmap(stream))
                            {
                                stream.Flush();
                                stream.Close();
                                bitmap.Save(ms, ImageFormat.Png);
                            }
                        }
                    }
                    BitmapImage bi = new BitmapImage();
                    bi.BeginInit();
                    bi.StreamSource = ms;
                    bi.EndInit();
                    System.Windows.Controls.PrintDialog dialog = new System.Windows.Controls.PrintDialog();

                    DeviceClient deviceClient = new DeviceClient();
                    DeviceConfigWrapper device = deviceClient.RealConfigs();
                    dialog.PrintQueue = new PrintQueue(new PrintServer(), PrintEmployeeQRCodeItem.PrintName);

                    if (SizeEmployeeQRCodeItem.Value == MessageValue.MESSAGE_FROM_SETTING_PRINT_PAPER_SIZE_80_VALUE)
                    {
                        PrintQrCode print = new PrintQrCode();
                        ms.Position = 0;

                        print.ImageQrCode.Source = bi;
                        print.restaurantName.Text = MessageValue.MESSAGE_FROM_SETTING_PRINT_RESTAURANT_NAME;
                        print.EmployeeName.Text = MessageValue.MESSAGE_FROM_SETTING_PRINT_RESTAURANT_NAME;
                        print.Username.Text = MessageValue.MESSAGE_FROM_SETTING_PRINT_RESTAURANT_NAME;
                        print.Password.Text = MessageValue.MESSAGE_FROM_SETTING_PRINT_RESTAURANT_NAME;
                        FlowDocument doc = print.InfoDocument;
                        doc.PagePadding = new Thickness(0);

                        doc.ColumnWidth = dialog.PrintableAreaWidth;
                        doc.PageHeight = dialog.PrintableAreaHeight;

                        IDocumentPaginatorSource idSource = doc;
                        dialog.PrintDocument(idSource.DocumentPaginator, "");
                    }
                    else
                    {
                        PrintQrCode58MM print = new PrintQrCode58MM();
                        ms.Position = 0;

                        print.ImageQrCode.Source = bi;
                        print.restaurantName.Text = MessageValue.MESSAGE_FROM_SETTING_PRINT_RESTAURANT_NAME;
                        print.EmployeeName.Text = MessageValue.MESSAGE_FROM_SETTING_PRINT_RESTAURANT_NAME;
                        print.Username.Text = MessageValue.MESSAGE_FROM_SETTING_PRINT_RESTAURANT_NAME;
                        print.Password.Text = MessageValue.MESSAGE_FROM_SETTING_PRINT_RESTAURANT_NAME;
                        FlowDocument doc = print.InfoDocument;
                        doc.PagePadding = new Thickness(0);

                        doc.ColumnWidth = dialog.PrintableAreaWidth;
                        doc.PageHeight = dialog.PrintableAreaHeight;

                        IDocumentPaginatorSource idSource = doc;
                        dialog.PrintDocument(idSource.DocumentPaginator, "");
                    }
                });
                PrintTestBillCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
                {
                    Models.BillResponse temp = new Models.BillResponse
                    {
                        Quantity = 2,
                        Name = MessageValue.MESSAGE_FROM_SETTING_PRINT_BBQ_TEST_1,
                        Price = 100000,
                        TotalPriceInlcudeAdditionFoods = 200000,
                    };
                    Models.BillResponse temp2 = new Models.BillResponse
                    {
                        Quantity = 3,
                        Name = MessageValue.MESSAGE_FROM_SETTING_PRINT_BBQ_TEST_2,
                        Price = 150000,
                        TotalPriceInlcudeAdditionFoods = 450000,
                    };
                    List<Models.BillResponse> temp3 = new List<Models.BillResponse>();
                    temp3.Add(temp);
                    temp3.Add(temp2);
                    OrderBillData test = new OrderBillData
                    {
                        Id = 1,
                        TableName = MessageValue.MESSAGE_FROM_SETTING_PRINT_TABLE_TEST,
                        UpdatedAt = MessageValue.MESSAGE_FROM_SETTING_PRINT_TITLE_TEST,
                        EmployeeName = MessageValue.MESSAGE_FROM_SETTING_PRINT_EMPLOYEE_TEST,
                        CashierName = MessageValue.MESSAGE_FROM_SETTING_PRINT_CASHIER_TEST,
                        CustomerSlotNumber = 2,
                        CreatedAt = Utils.Utils.GetDateFormatVN(DateTime.Now),
                        Foods = temp3,
                        DiscountType = 1,
                        DiscountPercent = 10,
                        DiscountAmount = 50000,
                        Amount = 650000,
                        TotalAmount = 600000,
                    };
                    OrderItemResponse PrintTest = new OrderItemResponse
                    {
                        Data = test,
                    };
                    string mode = MessageValue.MESSAGE_FROM_SETTING_PRINT_TITLE_TEST;
                    Helpers.PrintBill.PrintText pt = new Helpers.PrintBill.PrintText();
                    Branch currentBranch = (Branch)Utils.Utils.GetCacheValue(Constants.CURRENT_BRANCH);
                    pt.Print(mode, PrintTest, PrintBillItem.PrintName, SizeBillItem.Value, currentBranch);
                });
                PrintTestCommand = new RelayCommand<Kitchen>((p) => { return true; }, p =>
                {
                    List<Kitchen> CurrentKitchen = new List<Kitchen>();
                    CurrentKitchen = Utils.Utils.AsObjectList<Kitchen>(Properties.Settings.Default.RestaurantKitchenPlacePrint);
                    var kitchen = CurrentKitchen.Where(x => x.Id == p.Id).FirstOrDefault();
                    Models.OrderDetailPrint temp = new Models.OrderDetailPrint
                    {
                        Id = 1,
                        Quantity = 2,
                        FoodName = MessageValue.MESSAGE_FROM_SETTING_PRINT_BBQ_TEST_1,
                    };
                    Models.OrderDetailPrint temp2 = new Models.OrderDetailPrint
                    {
                        Id = 1,
                        Quantity = 3,
                        FoodName = MessageValue.MESSAGE_FROM_SETTING_PRINT_BBQ_TEST_2,
                    };
                    List<Models.OrderDetailPrint> temp3 = new List<Models.OrderDetailPrint>();
                    temp3.Add(temp);
                    temp3.Add(temp2);
                    Helpers.PrintBill.PrintText pt = new Helpers.PrintBill.PrintText();
                    pt.PrintFoodCook(kitchen.PrinterName, temp3, MessageValue.MESSAGE_FROM_SETTING_PRINT_TABLE_TEST, MessageValue.MESSAGE_FROM_SETTING_PRINT_TABLE_ID_TEST, MessageValue.MESSAGE_FROM_SETTING_PRINT_CASHIER_TEST, p.PrinterPaperSizeItem.Value);
                });
                PrintTestFishTankCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
                {
                    Models.OrderDetailPrint temp = new Models.OrderDetailPrint
                    {
                        Id = 1,
                        Quantity = 2,
                        FoodName = MessageValue.MESSAGE_FROM_SETTING_PRINT_BBQ_TEST_1,
                    };
                    Models.OrderDetailPrint temp2 = new Models.OrderDetailPrint
                    {
                        Id = 1,
                        Quantity = 3,
                        FoodName = MessageValue.MESSAGE_FROM_SETTING_PRINT_BBQ_TEST_2,
                    };
                    List<Models.OrderDetailPrint> temp3 = new List<Models.OrderDetailPrint>();
                    temp3.Add(temp);
                    temp3.Add(temp2);
                    Helpers.PrintBill.PrintText pt = new Helpers.PrintBill.PrintText();
                    pt.PrintFoodCook(PrintFishTankItem.PrintName, temp3, MessageValue.MESSAGE_FROM_SETTING_PRINT_TABLE_TEST, MessageValue.MESSAGE_FROM_SETTING_PRINT_TABLE_ID_TEST, MessageValue.MESSAGE_FROM_SETTING_PRINT_CASHIER_TEST, SizeFishTankItem.Value);
                });
                StampPrintCommand = new RelayCommand<UserControl>((p) => { return true; }, p => {
                    Models.OrderDetailPrint temp = new Models.OrderDetailPrint
                    {
                        Id = 1,
                        Quantity = 2, 
                        FoodName = "Trà sữa truyền thống",
                    };
                    Helpers.PrintBill.PrintText pt = new Helpers.PrintBill.PrintText();
                    //pt.PrintFoodStamp()
                    
                }); 
                SaveCommand = new RelayCommand<System.Windows.Controls.UserControl>((p) => { return true; }, p =>
                {
                    DeviceConfigWrapper wrapper = deviceClient.RealConfigs();
                    if (wrapper == null)
                    {
                        wrapper = new DeviceConfigWrapper();
                    }
                    if (QRCodeChecked)
                    {
                        wrapper.IsQRCode = QRCodeChecked;
                        if (PrintQRCodeItem != null)
                        {
                            wrapper.QrCodePrinter = PrintQRCodeItem.PrintName;
                            wrapper.QrCodeSize = SizeQRCodeItem.Value;
                        }
                        else
                        {
                            NotificationMessage.Error("Bạn cần chọn đủ máy in trước khi lưu lại !"); 
                        }
                    }
                    else
                    {
                        wrapper.IsQRCode = QRCodeChecked;
                        wrapper.QrCodePrinter = "";
                        wrapper.QrCodeSize = 0;
                    }
                    if (BranchQRCodeChecked)
                    {
                        wrapper.IsBranchQRCode = BranchQRCodeChecked;
                        wrapper.BranchQrCodePrinter = PrintBranchQRCodeItem.PrintName;
                        wrapper.BranchQrCodeSize = SizeBranchQRCodeItem.Value;
                    }
                    else
                    {
                        wrapper.IsBranchQRCode = BranchQRCodeChecked;
                        wrapper.QrCodePrinter = "";
                        wrapper.QrCodeSize = 0;
                    }
                    if (EmployeeQRCodeChecked)
                    {
                        wrapper.IsEmployeeQRCode = EmployeeQRCodeChecked;
                        wrapper.EmployeeQrCodePrinter = PrintEmployeeQRCodeItem.PrintName;
                        wrapper.EmployeeQrCodeSize = SizeEmployeeQRCodeItem.Value;
                    }
                    else
                    {
                        wrapper.IsEmployeeQRCode = EmployeeQRCodeChecked;
                        wrapper.QrCodePrinter = "";
                        wrapper.QrCodeSize = 0;
                    }
                    if (BillChecked)
                    {
                        wrapper.IsCasher = BillChecked;
                        wrapper.CasherPrinter = PrintBillItem.PrintName;
                        wrapper.CasherSize = SizeBillItem.Value;
                    }
                    else
                    {
                        wrapper.IsCasher = BillChecked;
                        wrapper.CasherPrinter = "";
                        wrapper.CasherSize = 0;
                    }
                    if (FishTankChecked)
                    {
                        wrapper.IsFishtank = FishTankChecked;
                        wrapper.FishtankPrinter = PrintFishTankItem.PrintName;
                        wrapper.FishtankSize = SizeFishTankItem.Value;
                    }
                    else
                    {
                        wrapper.IsFishtank = FishTankChecked;
                        wrapper.FishtankPrinter = "";
                        wrapper.FishtankSize = 0;
                    }
                    deviceClient.SaveConfigs(wrapper);
                    List<UpdatePrinterKitchen> kitchens = new List<UpdatePrinterKitchen>();

                    foreach (Kitchen k in RestaurantKitchenList)
                    {
                        //if (k.IsHavePrinter)
                        if (k.IsHavePrinter && k.PrinterNameItem != null) // Dat
                        {

                            kitchens.Add(new UpdatePrinterKitchen(k.Id, k.PrinterNameItem.PrintName, k.PrinterNameItem.IpAddress, k.PrinterNameItem.Port, k.PrinterPaperSizeItem.Value, k.IsHavePrinter));

                        }
                        else
                        {
                            kitchens.Add(new UpdatePrinterKitchen(k.Id, string.Empty, string.Empty, string.Empty, 0, k.IsHavePrinter));

                        }
                        //if (k.Id != 0)
                        //{

                        //}
                    }
                    UpdatePrinterKitchenWrapper updatePrinterKitchenWrapper = new UpdatePrinterKitchenWrapper(kitchens);
                    KitchenClient client = new KitchenClient(this, this, this);
                    KitchenResponse response = client.UpdateRestaurantKitchenPlate(updatePrinterKitchenWrapper);
                    if (response != null && response.Status == (int)ResponseEnum.OK)
                    {
                        //Kitchen kitchen = response.Data.Where(x => currentKitchen.Id == x.Id).FirstOrDefault();
                        //if (kitchen!= null)
                        //{
                        //    Properties.Settings.Default.CurrentKitchen = Utils.Utils.AsJson<Kitchen>(kitchen);

                        //}
                        #region Dat
                        Kitchen kitchen = response.Data.Where(x => currentKitchen == x).FirstOrDefault();
                        Kitchen kitchen1 = RestaurantKitchenList.Where(x => x.Id == response.Data[0].Id).FirstOrDefault();
                        if(kitchen1 != null)
                        {
                            response.Data[0].Name = kitchen1.Name;
                        }
                        if (kitchen != null)
                        {
                            Properties.Settings.Default.CurrentKitchen = Utils.Utils.AsJson<Kitchen>(kitchen);
                        }
                        Properties.Settings.Default.RestaurantKitchenPlacePrint = Utils.Utils.AsJsonList<Kitchen>(response.Data);
                        if (Utils.Utils.CheckListPermissionsEmployee(new List<string>() { Enum.GetName(typeof(TechresEnum), TechresEnum.CHEF_COOK_ACCESS), Enum.GetName(typeof(TechresEnum), TechresEnum.CHEF_BBQ_ACCESS) }, currentUser.Permissions))
                        {
                            Properties.Settings.Default.CurrentKitchen = Utils.Utils.AsJson<Kitchen>(response.Data[0]);
                        }
                        Properties.Settings.Default.Save();
                        #endregion
                        NotificationMessage.Infomation(MessageValue.MESSAGE_RESPONSE_SUCCESS);
                    }
                });
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }

        FrameworkElement GetWindowParent(System.Windows.Controls.UserControl p)
        {
            FrameworkElement parent = p;

            while (parent.Parent != null)
            {
                parent = parent.Parent as FrameworkElement;

            }

            return parent;
        }
        public void LogError(Exception ex, string infoMessage)
        {
            throw new NotImplementedException();
        }

        public T Deserialize<T>(IRestResponse response)
        {
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                dynamic jsonResponse = JsonConvert.DeserializeObject(response.Content);
                if (jsonResponse.status == 200)
                {
                    T check = jsonResponse.ToObject<T>();
                    if (check != null)
                    {
                        return check;
                    }
                }
                else
                {
                    NotificationMessage.Error(jsonResponse.message);
                }
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                NotificationMessage.Warning(MessageValue.FORBIDDEN);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                NotificationMessage.Error(MessageValue.INTERNAL_SERVER_ERROR);
            }
            else
            {
                NotificationMessage.Error(response.ErrorMessage);
            }
            return default(T);
        }

        public T Get<T>(string cacheKey) where T : class
        {
            throw new NotImplementedException();
        }


        public void Set(string cacheKey, object item, int minutes)
        {
            throw new NotImplementedException();
        }

    }
}
