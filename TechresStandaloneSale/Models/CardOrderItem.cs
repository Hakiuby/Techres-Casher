using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TechresStandaloneSale.Helpers;
using TechresStandaloneSale.ViewModels.SettingS;

namespace TechresStandaloneSale.Models
{
    public class CardOrderItem
    {
        public long OrderId { get; set; }
        //  public string OrderStatusText { get; set; }
        public int OrderStatus { get; set; }
        public long CustomerSlot { get; set; }
        public string TableName { get; set; }
        public int TotalOrderDetailCustomerRequest { get; set; }
        public long TableId { get; set; }
        public string EmployeeName { get; set; }
        public long EmployeeId { get; set; }
        public long CustomerId { get; set; }
        public long PointOrder { get; set; }

        public bool OpenWindow
        {
            get; set;  
        }

       // public int IsTakeAway { get; set; }
        // public string MegerTable { get; set; }
        public List<string> MegerTableObject { get; set; }
        public decimal CashAmount { get; set; }
        public string UsingTime { get; set; }
        public long UsingTimeMinutes { get; set; }
        public string UsingTimePropertyChange 
        { 
            get
            {
                SettingCardOrderTimeCount setting = new SettingCardOrderTimeCount(UsingTimeMinutes);
                return setting.SystemTime;
            }
            set
            {
                UsingTimePropertyChange = value;
            }
        }
        public bool BtnSaveIsEnabled { get; set; }
        public bool BtnPrintIsEnabled { get; set; }
        public bool BtnDoneIsEnabled { get; set; }
        public bool IsOnline { get; set; }
        public bool IsTakeAway { get; set; }
        public decimal Amount { get; set; }
        public float Vat { get; set; }
        public decimal VatAmount { get; set; }
        public float DiscountPercent { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal ShippingFee { get; set; }
        public bool IsReturnDeposit { get; set; }
        public long BookingInforId { get; set; }
        public decimal BookingDepositAmount { get; set; }
        public int BookingDepositPaymentMethod { get; set; }
        public int FontTitle
        {
            get
            {
                if (SystemParameters.MaximizedPrimaryScreenWidth >= 1920)
                {
                    return 17;
                }
                else if (SystemParameters.MaximizedPrimaryScreenWidth >= 1680 && SystemParameters.MaximizedPrimaryScreenWidth < 1920)
                {
                    return 17;
                }
                else if (SystemParameters.MaximizedPrimaryScreenWidth >= 1600 && SystemParameters.MaximizedPrimaryScreenWidth < 1680)
                {
                    return 17;
                }
                else if (SystemParameters.MaximizedPrimaryScreenWidth >= 1440 && SystemParameters.MaximizedPrimaryScreenWidth < 1680)
                {
                    return 15;
                }
                else if (SystemParameters.MaximizedPrimaryScreenWidth >= 1400 && SystemParameters.MaximizedPrimaryScreenWidth < 1440)
                {
                    return 15;
                }
                else if (SystemParameters.MaximizedPrimaryScreenWidth >= 1366 && SystemParameters.MaximizedPrimaryScreenWidth < 1400)
                {
                    return 14;
                }
                else if (SystemParameters.MaximizedPrimaryScreenWidth >= 1360 && SystemParameters.MaximizedPrimaryScreenWidth < 1366)
                {
                    return 14;
                }
                else if (SystemParameters.MaximizedPrimaryScreenWidth >= 1280 && SystemParameters.MaximizedPrimaryScreenWidth < 1360)
                {
                    return 15;
                }
                else if (SystemParameters.MaximizedPrimaryScreenWidth >= 1024 && SystemParameters.MaximizedPrimaryScreenWidth < 1280)
                {
                    return 13;
                }
                else
                {
                    return 13;
                }
            }
            set
            {
                FontTitle = value;
            }
        }

        public int FontNumberTable
        {
            get
            {
                if (SystemParameters.MaximizedPrimaryScreenWidth >= 1920)
                {
                    return 30;
                }
                else if (SystemParameters.MaximizedPrimaryScreenWidth >= 1680 && SystemParameters.MaximizedPrimaryScreenWidth < 1920)
                {
                    return 27;
                }
                else if (SystemParameters.MaximizedPrimaryScreenWidth >= 1600 && SystemParameters.MaximizedPrimaryScreenWidth < 1680)
                {
                    return 27;
                }
                else if (SystemParameters.MaximizedPrimaryScreenWidth >= 1440 && SystemParameters.MaximizedPrimaryScreenWidth < 1680)
                {
                    return 24;
                }
                else if (SystemParameters.MaximizedPrimaryScreenWidth >= 1400 && SystemParameters.MaximizedPrimaryScreenWidth < 1440)
                {
                    return 24;
                }
                else if (SystemParameters.MaximizedPrimaryScreenWidth >= 1366 && SystemParameters.MaximizedPrimaryScreenWidth < 1400)
                {
                    return 21;
                }
                else if (SystemParameters.MaximizedPrimaryScreenWidth >= 1360 && SystemParameters.MaximizedPrimaryScreenWidth < 1366)
                {
                    return 21;
                }
                else if (SystemParameters.MaximizedPrimaryScreenWidth >= 1280 && SystemParameters.MaximizedPrimaryScreenWidth < 1360)
                {
                    return 21;
                }
                else if (SystemParameters.MaximizedPrimaryScreenWidth >= 1024 && SystemParameters.MaximizedPrimaryScreenWidth < 1280)
                {
                    return 20;
                }
                else
                {
                    return 20;
                }
            }
            set
            {
                FontNumberTable = value;
            }
        }

        public int FontMoney
        {
            get
            {
                if (SystemParameters.MaximizedPrimaryScreenWidth >= 1920)
                {
                    return 30;
                }
                else if (SystemParameters.MaximizedPrimaryScreenWidth >= 1680 && SystemParameters.MaximizedPrimaryScreenWidth < 1920)
                {
                    return 25;
                }
                else if (SystemParameters.MaximizedPrimaryScreenWidth >= 1600 && SystemParameters.MaximizedPrimaryScreenWidth < 1680)
                {
                    return 25;
                }
                else if (SystemParameters.MaximizedPrimaryScreenWidth >= 1440 && SystemParameters.MaximizedPrimaryScreenWidth < 1680)
                {
                    return 22;
                }
                else if (SystemParameters.MaximizedPrimaryScreenWidth >= 1400 && SystemParameters.MaximizedPrimaryScreenWidth < 1440)
                {
                    return 22;
                }
                else if (SystemParameters.MaximizedPrimaryScreenWidth >= 1366 && SystemParameters.MaximizedPrimaryScreenWidth < 1400)
                {
                    return 20;
                }
                else if (SystemParameters.MaximizedPrimaryScreenWidth >= 1360 && SystemParameters.MaximizedPrimaryScreenWidth < 1366)
                {
                    return 20;
                }
                else if (SystemParameters.MaximizedPrimaryScreenWidth >= 1280 && SystemParameters.MaximizedPrimaryScreenWidth < 1360)
                {
                    return 21;
                }
                else if (SystemParameters.MaximizedPrimaryScreenWidth >= 1024 && SystemParameters.MaximizedPrimaryScreenWidth < 1280)
                {
                    return 20;
                }
                else
                {
                    return 20;
                }
            }
            set
            {
                FontMoney = value;
            }
        }
        public int FontTime
        {
            get
            {
                if (SystemParameters.MaximizedPrimaryScreenWidth >= 1920)
                {
                    return 15;
                }
                else if (SystemParameters.MaximizedPrimaryScreenWidth >= 1680 && SystemParameters.MaximizedPrimaryScreenWidth < 1920)
                {
                    return 13;
                }
                else if (SystemParameters.MaximizedPrimaryScreenWidth >= 1600 && SystemParameters.MaximizedPrimaryScreenWidth < 1680)
                {
                    return 13;
                }
                else if (SystemParameters.MaximizedPrimaryScreenWidth >= 1440 && SystemParameters.MaximizedPrimaryScreenWidth < 1680)
                {
                    return 11;
                }
                else if (SystemParameters.MaximizedPrimaryScreenWidth >= 1400 && SystemParameters.MaximizedPrimaryScreenWidth < 1440)
                {
                    return 10;
                }
                else if (SystemParameters.MaximizedPrimaryScreenWidth >= 1366 && SystemParameters.MaximizedPrimaryScreenWidth < 1400)
                {
                    return 9;
                }
                else if (SystemParameters.MaximizedPrimaryScreenWidth >= 1360 && SystemParameters.MaximizedPrimaryScreenWidth < 1366)
                {
                    return 9;
                }
                else if (SystemParameters.MaximizedPrimaryScreenWidth >= 1280 && SystemParameters.MaximizedPrimaryScreenWidth < 1360)
                {
                    return 10;
                }
                else if (SystemParameters.MaximizedPrimaryScreenWidth >= 1024 && SystemParameters.MaximizedPrimaryScreenWidth < 1280)
                {
                    return 9;
                }
                else
                {
                    return 9;
                }
            }
            set
            {
                FontTime = value;
            }
        }

        public int FontTotal
        {
            get
            {
                if (SystemParameters.MaximizedPrimaryScreenWidth >= 1920)
                {
                    return 17;
                }
                else if (SystemParameters.MaximizedPrimaryScreenWidth >= 1680 && SystemParameters.MaximizedPrimaryScreenWidth < 1920)
                {
                    return 17;
                }
                else if (SystemParameters.MaximizedPrimaryScreenWidth >= 1600 && SystemParameters.MaximizedPrimaryScreenWidth < 1680)
                {
                    return 17;
                }
                else if (SystemParameters.MaximizedPrimaryScreenWidth >= 1440 && SystemParameters.MaximizedPrimaryScreenWidth < 1680)
                {
                    return 16;
                }
                else if (SystemParameters.MaximizedPrimaryScreenWidth >= 1400 && SystemParameters.MaximizedPrimaryScreenWidth < 1440)
                {
                    return 16;
                }
                else
                {
                    return 244;
                }
            }
            set
            {
                FontTotal = value;
            }
        }
        public long WidthCard
        {
            get
            {
                if (SystemParameters.MaximizedPrimaryScreenWidth >= 1920)
                {
                    return 302;
                }
                else if (SystemParameters.MaximizedPrimaryScreenWidth >= 1680 && SystemParameters.MaximizedPrimaryScreenWidth < 1920)
                {
                    return 317;
                }
                else if (SystemParameters.MaximizedPrimaryScreenWidth >= 1600 && SystemParameters.MaximizedPrimaryScreenWidth < 1680)
                {
                    return 302;
                }
                else if (SystemParameters.MaximizedPrimaryScreenWidth >= 1440 && SystemParameters.MaximizedPrimaryScreenWidth < 1680)
                {
                    return 269;
                }
                else if (SystemParameters.MaximizedPrimaryScreenWidth >= 1400 && SystemParameters.MaximizedPrimaryScreenWidth < 1440)
                {
                    return 263;
                }
                else if (SystemParameters.MaximizedPrimaryScreenWidth >= 1366 && SystemParameters.MaximizedPrimaryScreenWidth < 1400)
                {
                    return 255;
                }
                else if (SystemParameters.MaximizedPrimaryScreenWidth >= 1360 && SystemParameters.MaximizedPrimaryScreenWidth < 1366)
                {
                    return 254;
                }
                else if (SystemParameters.MaximizedPrimaryScreenWidth >= 1280 && SystemParameters.MaximizedPrimaryScreenWidth < 1360)
                {
                    return 300;
                }
                else if (SystemParameters.MaximizedPrimaryScreenWidth >= 1024 && SystemParameters.MaximizedPrimaryScreenWidth < 1280)
                {
                    return 235;
                }
                else
                {
                    return 243;
                }
            }
            set
            {
                WidthCard = value;
            }
        }
        public long HeightCard
        {
            get
            {
                if (SystemParameters.MaximizedPrimaryScreenWidth >= 1920)
                {
                    return 200;
                }
                else if (SystemParameters.MaximizedPrimaryScreenWidth >= 1680 && SystemParameters.MaximizedPrimaryScreenWidth < 1920)
                {
                    return 212;
                }
                else if (SystemParameters.MaximizedPrimaryScreenWidth >= 1600 && SystemParameters.MaximizedPrimaryScreenWidth < 1680)
                {
                    return 209;
                }
                else if (SystemParameters.MaximizedPrimaryScreenWidth >= 1440 && SystemParameters.MaximizedPrimaryScreenWidth < 1680)
                {
                    return 189;
                }
                else if (SystemParameters.MaximizedPrimaryScreenWidth >= 1400 && SystemParameters.MaximizedPrimaryScreenWidth < 1440)
                {
                    return 186;
                }
                else if (SystemParameters.MaximizedPrimaryScreenWidth >= 1366 && SystemParameters.MaximizedPrimaryScreenWidth < 1400)
                {
                    return 170;
                }
                else if (SystemParameters.MaximizedPrimaryScreenWidth >= 1360 && SystemParameters.MaximizedPrimaryScreenWidth < 1366)
                {
                    return 170;
                }
                else if (SystemParameters.MaximizedPrimaryScreenWidth >= 1280 && SystemParameters.MaximizedPrimaryScreenWidth < 1360)
                {
                    return 186;
                }
                else if (SystemParameters.MaximizedPrimaryScreenWidth >= 1024 && SystemParameters.MaximizedPrimaryScreenWidth < 1280)
                {
                    return 170;
                }
                else
                {
                    return 167;
                }
            }
            set
            {
                HeightCard = value;
            }
        }

        public string OrderCode
        {
            get
            {
                return string.Format("#{0}", OrderId);
            }
            set
            {
                OrderCode = value;
            }
        }
        public string MegerTable
        {
            get
            {
                if (MegerTableObject!= null)
                {
                    return MegerTableObject.Count > 0 ? "[" + Utils.Utils.convertFormListString(MegerTableObject) + "]" : "";
                }
                else
                {
                    return "";
                }
           
            }
            set
            {
                MegerTable = value;
            }
        }
        public string TableNameString
        {
            get
            {
                if (!string.IsNullOrEmpty(TableName))
                {
                    return TableName.Length>4 ? string.Format("{0}...", TableName.Substring(0,4).ToUpper()) : TableName.ToUpper();
                }
                else
                {
                    return "";
                }

            }
            set
            {
                TableNameString = value;
            }
        }
        public string CashAmountString
        {
            get
            {
                /*
                if (IsReturnDeposit)
                {
                    return Utils.Utils.FormatMoney(this.CashAmount + ShippingFee - BookingDepositAmount);

                }
                else
                {
                    return Utils.Utils.FormatMoney(this.CashAmount + ShippingFee);

                }
                */
                return Utils.Utils.FormatMoney(this.CashAmount + ShippingFee);
            }
            set
            {
                CashAmountString = value;
            }
        }
        public string OrderStatusText
        {
            get
            {
                if (OrderStatus == (int)(OrderStatusEnum.OPENING))
                {
                    return MessageValue.MESSAGE_ORDER_STATUS_OPENING_TEXT;
                }
                else if (OrderStatus == (int)(OrderStatusEnum.WAITING_PAYMENT))
                {
                    return MessageValue.MESSAGE_ORDER_STATUS_PAYMENT_TEXT;
                }
                else if (OrderStatus == (int)(OrderStatusEnum.WAITING_COMPLETE))
                {
                    return MessageValue.MESSAGE_ORDER_STATUS_COMPLETE_TEXT;
                }
                else if (OrderStatus == (int)(OrderStatusEnum.DELIVERING))
                {
                    return MessageValue.MESSAGE_ORDER_STATUS_DELIVERING_TEXT;
                }
                else
                {
                    return "";
                }
            }
            set
            {
                OrderStatusText = value;
            }
        }
        public BitmapImage ImageStatus
        {
            get
            {
                if (this.BookingInforId > 0)
                {
                    if (OrderStatus == (int)(OrderStatusEnum.OPENING))
                    {
                        return new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/ic-booking-watting.png"));
                    }
                    else if (OrderStatus == (int)(OrderStatusEnum.WAITING_PAYMENT))
                    {
                        return new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/ic-booking-pending.png", UriKind.RelativeOrAbsolute));
                    }
                    else if (OrderStatus == (int)(OrderStatusEnum.WAITING_COMPLETE) || OrderStatus == (int)(OrderStatusEnum.DELIVERING))
                    {
                        return new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/ic-booking-payment.png", UriKind.RelativeOrAbsolute));
                    }
                    else
                    {
                        return new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/ic-booking-pending.png"));
                    }
                }
                else
                {
                    if (OrderStatus == (int)(OrderStatusEnum.OPENING))
                    {
                        return new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/table_pending.png"));
                    }
                    else if (OrderStatus == (int)(OrderStatusEnum.WAITING_PAYMENT))
                    {
                        return new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/table-request-payment.png", UriKind.RelativeOrAbsolute));
                    }
                    else if (OrderStatus == (int)(OrderStatusEnum.WAITING_COMPLETE) || OrderStatus == (int)(OrderStatusEnum.DELIVERING))
                    {
                        return new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/table-waiting-payemt.png", UriKind.RelativeOrAbsolute));
                    }
                    else
                    {
                        return new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/table_pending.png"));
                    }
                }
                
            }
            set
            {
                ImageStatus = value;
            }
        }
        public Brush ChangeBackgroup
        {
            get
            {
                BrushConverter bc = new BrushConverter();
                if (OrderStatus == (int)(OrderStatusEnum.OPENING))
                {
                    return (Brush)bc.ConvertFrom(Constants.BLUE_TABLE_OPENING_COLOR);
                }
                else if (OrderStatus == (int)(OrderStatusEnum.WAITING_PAYMENT))
                {
                    return (Brush)bc.ConvertFrom(Constants.MAIN_COLOR_SYSTEM);
                }
                else if (OrderStatus == (int)(OrderStatusEnum.WAITING_COMPLETE) || OrderStatus == (int)(OrderStatusEnum.DELIVERING))
                {
                    return (Brush)bc.ConvertFrom(Constants.RED_GG_COLOR);
                }
                else
                {
                    return (Brush)bc.ConvertFrom(Constants.MAIN_COLOR_SYSTEM);
                }
            }
            set
            {
                ChangeBackgroup = value;
            }
        }

        public SettingData currentSetting = (SettingData)Utils.Utils.GetCacheValue(Constants.CURRENT_SETTING);
        public Visibility TableVisibility
        {
            get
            {
                if ((int)BranchTypeEnum.MEDIUM == currentSetting.BranchType)
                {
                    if ((int)BranchTypeOption.OPTIONTWO == currentSetting.BranchTypeOption)
                    {
                        return Visibility.Collapsed;
                    }
                    else
                    {
                        if(TableName == "MV")
                        return Visibility.Collapsed;
                    }
                }
                if (currentSetting.BranchType >= (int)BranchTypeEnum.LARGE || IsOnline)
                {

                    return Visibility.Collapsed;
                }
                else
                {
                    if (OrderStatus == (int)OrderStatusEnum.OPENING || OrderStatus == (int)OrderStatusEnum.WAITING_PAYMENT)
                    {
                        return Visibility.Visible;

                    }
                    else
                    {
                        return Visibility.Collapsed;

                    }
                }
            }
            set
            {
                TableVisibility = value;
            }
        }

        public Visibility MoveFoodVisibility //toan
        {
            get
            {
                if ((int)BranchTypeEnum.MEDIUM == currentSetting.BranchType)
                {
                    if ((int)BranchTypeOption.OPTIONTWO == currentSetting.BranchTypeOption)
                    {
                        if (OrderStatus == (int)OrderStatusEnum.WAITING_COMPLETE )
                        {
                            return Visibility.Collapsed;
                        }    
                        if (TableName == "MV")
                        {
                            return Visibility.Visible;
                        }     
                        else
                        {
                            return Visibility.Collapsed;
                        }    
                    }
                    else
                    {
                        if (OrderStatus == (int)OrderStatusEnum.WAITING_COMPLETE)
                        {
                            return Visibility.Collapsed;
                        }
                        if (TableName == "MV")
                            return Visibility.Visible;
                    }
                }
                if (currentSetting.BranchType >= (int)BranchTypeEnum.LARGE || IsOnline)
                {

                    return Visibility.Collapsed;
                }
                else
                {
                    if (OrderStatus == (int)OrderStatusEnum.OPENING || OrderStatus == (int)OrderStatusEnum.WAITING_PAYMENT)
                    {
                        return Visibility.Visible;

                    }
                    else
                    {
                        return Visibility.Collapsed;

                    }
                }
            }
            set
            {
                TableVisibility = value;
            }
        }

        public Visibility DebitVisibility
        {
            get
            {
                if (currentSetting.IsEnableTms &&( OrderStatus ==(int)OrderStatusEnum.WAITING_PAYMENT || OrderStatus == (int)OrderStatusEnum.WAITING_COMPLETE)
                    && currentSetting.ServiceRestaurantLevelId >= 5)
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Collapsed;
                }
            }
            set
            {
                DebitVisibility = value;
            }
        }
        public Visibility CustomerDebitVisibility
        {
            get
            {
                if (currentSetting.IsEnableMembershipCard && (OrderStatus == (int)OrderStatusEnum.WAITING_PAYMENT || OrderStatus == (int)OrderStatusEnum.WAITING_COMPLETE))
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Collapsed;
                }
            }
            set
            {
                CustomerDebitVisibility = value;
            }
        }
        public Visibility IsReturnDepositVisibility
        {
            get
            {
                if (this.IsReturnDeposit == true && this.BookingInforId > 0)
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Collapsed;
                }
            }
            set
            {
                IsReturnDepositVisibility = value;
            }
        }
        public string BookingDepositAmountString
        {
            get
            {
                return string.Concat(MessageValue.MESSAGE_FROM_CARD_ORDER_ITEM,Utils.Utils.FormatMoney(BookingDepositAmount));
            }
            set
            {
                BookingDepositAmountString = value;
            }
        }
        public Visibility TotalOrderDetailCustomerRequestVisibility
        {
            get
            {
                if(TotalOrderDetailCustomerRequest > 0)
                {
                    return Visibility.Visible;
                }
                return Visibility.Collapsed;
            }
            set
            {
                TotalOrderDetailCustomerRequestVisibility = value;
            }
        }        
    }
}
