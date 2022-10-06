using Newtonsoft.Json;
using System.Collections.Generic;
using System.Windows;
using TechresStandaloneSale.Helpers;

namespace TechresStandaloneSale.Models
{
    public class Order
    {
        public SettingData currentSetting = (SettingData)Utils.Utils.GetCacheValue(Constants.CURRENT_SETTING);
        
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("vat")]
        public decimal Vat { get; set; }

        [JsonProperty("table_id")]
        public long TableId { get; set; }

        [JsonProperty("table_name")]
        public string TableName { get; set; }

        [JsonProperty("vat_amount")]
        public decimal VatAmount { get; set; }

        [JsonProperty("discount_percent")]
        public decimal DiscountPercent { get; set; }

        [JsonProperty("discount_amount")]
        public decimal DiscountAmount { get; set; }

        [JsonProperty("discount_type")]
        public int DiscountType { get; set; }

        [JsonProperty("total_amount")]
        public decimal TotalAmount { get; set; }

        [JsonProperty("cash_amount")]
        public decimal CashAmount { get; set; }

        [JsonProperty("bank_amount")]
        public decimal BankAmount { get; set; }
        [JsonProperty("transfer_amount")]
        public decimal TransferAmount { get; set; }

        [JsonProperty("order_status")]
        public int OrderStatus { get; set; }

        [JsonProperty("move_from_tables")]
        public List<string> MoveFromTables { get; set; }

        [JsonProperty("table_merge_list_name")]
        public List<string> TableMergeListName { get; set; }

        [JsonProperty("employee")]
        public Employee Employee { get; set; }

        [JsonProperty("customer")]
        public Customer Customer { get; set; }

        [JsonProperty("using_slot")]
        public long CustomerSlotNumber { get; set; }
        [JsonProperty("total_order_detail_customer_request")]
        public int TotalOrderDetailCustomerRequest { get; set; }
        [JsonProperty("payment_date")]
        public string PaymentDate { get; set; }
        [JsonProperty("payment_method_id")]
        public int PaymentMethodId { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }

        [JsonProperty("using_time_minutes")]
        public long UsingTimeMinutes { get; set; }
        [JsonProperty("using_time_minutes_string")]
        public string UsingTimeMinutesString { get; set; }
        [JsonProperty("is_online")]
        public long IsOnline { get; set; }
        [JsonProperty("membership_point_used")]
        public long MembershipPointUsed { get; set; }

        [JsonProperty("membership_total_point_used")]
        public long MembershipTotalPointUsed { get; set; }

        [JsonProperty("membership_point_used_amount")]
        public decimal MembershipPointUsedAmount { get; set; }

        [JsonProperty("shipping_receiver_name")]
        public string ShippingReceiverName { get; set; }

        [JsonProperty("shipping_address")]
        public string ShippingAddress { get; set; }

        [JsonProperty("shipping_phone")]
        public string ShippingPhone { get; set; }

        [JsonProperty("shipping_fee")]
        public decimal ShippingFee { get; set; }
        [JsonProperty("is_take_away")]
        public long IsTakeAway { get; set; }
        [JsonProperty("is_return_deposit")]
        public long IsReturnDeposit { get; set; }
        [JsonProperty("booking_infor_id")]
        public long BookingInforId { get; set; }
        [JsonProperty("booking_deposit_amount")]
        public decimal BookingDepositAmount { get; set; }
        [JsonProperty("booking_deposit_payment_method")]
        public int BookingDepositPaymentMethod { get; set; }
        public string TotalAmountString
        {
            get
            {

                return Utils.Utils.FormatMoney(this.TotalAmount);
            }
            set
            {
                TotalAmountString = value;
            }
        }
        public string TransferAmountString
        {
            get
            {

                return Utils.Utils.FormatMoney(this.TransferAmount);
            }
            set
            {
                TransferAmountString = value;
            }
        }
        public string BankAmountString
        {
            get
            {

                return Utils.Utils.FormatMoney(this.BankAmount);
            }
            set
            {
                BankAmountString = value;
            }
        }
        public string CashAmountString
        {
            get
            {

                return Utils.Utils.FormatMoney(this.CashAmount);
            }
            set
            {
                CashAmountString = value;
            }
        }
        public Visibility BtnActiveVATVisibility
        {
            get
            {
                if (Utils.Utils.FormatMoneyDecimal(AmountVatString) > 0 || TotalAmount == 0)
                {
                    return Visibility.Hidden;
                }
                else
                {
                    return Visibility.Visible;
                }
            }
            set
            {
                BtnActiveVATVisibility = value;
            }
        }
        public Visibility DebitVisibility
        {
            get
            {
                if (currentSetting.BranchType >= (int)BranchTypeEnum.LARGE && (this.OrderStatus == (int)OrderStatusEnum.WAITING_PAYMENT || this.OrderStatus == (int)OrderStatusEnum.WAITING_COMPLETE))
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Hidden;
                }
            }
            set
            {
                DebitVisibility = value;
            }
        }
        public string AmountString
        {
            get
            {
                return Utils.Utils.FormatMoney( this.Amount);
            }
            set
            {
                AmountString = value;
            }
        }
        public string AmountVatString
        {
            get => Utils.Utils.FormatMoney(this.VatAmount); 
            set
            {
                AmountVatString = value; 
            }
        }

        public string TableMergeListNameString
        {
            get
            {
                if (TableMergeListName != null && TableMergeListName.Count > 0)
                {
                    string str = TableMergeListName[0];
                    for (int i = 1; i < TableMergeListName.Count; i++)
                    {
                        str = str + "," + TableMergeListName[i];
                    }
                    return str;
                }
                else return "";


            }
            set
            {
                TableMergeListNameString = value;
            }
        }

        public string OrderCode
        {
            get
            {
                return string.Format("#{0}", Id);
            }
            set
            {
                OrderCode = value;
            }
        }
        public string CreatedAtHour
        {
            get
            {
                //05/06/2019 09:06
                // DateTime date = DateTime.ParseExact(CreatedAt, "yyyy-MM-dd HH:mm", null);
                string hour = CreatedAt.Substring(CreatedAt.Length - 6, 6);
                return hour;
            }
            set
            {
                CreatedAtHour = value;
            }
        }
        public string UpdatedAtHour
        {
            get
            {

                // string hour  = date.Hour + ":" + date.Minute;
                string hour = UpdatedAt.Substring(UpdatedAt.Length - 6, 6);

                return hour;
            }
            set
            {
                UpdatedAtHour = value;
            }
        }

        public string OrderStatusString
        {
            get
            {
                switch (this.OrderStatus)
                {
                    case (int)OrderStatusEnum.OPENING:
                        return MessageValue.MESSAGE_ORDER_OPENING;
                    case (int)OrderStatusEnum.WAITING_PAYMENT:
                        return MessageValue.MESSAGE_ORDER_PAYMENT;
                    case (int)OrderStatusEnum.DONE:
                        return MessageValue.MESSAGE_ORDER_DONE_ENUM;
                    case (int)OrderStatusEnum.MERGED:
                        return MessageValue.MESSAGE_ORDER_MERGED;
                    case (int)OrderStatusEnum.WAITING_COMPLETE:
                        return MessageValue.MESSAGE_ORDER_COMPLETE;
                    case (int)OrderStatusEnum.DEBIT:
                        return MessageValue.MESSAGE_ORDER_DEBIT;
                    default:
                        return "Bàn trống";
                }
            }
            set
            {
                OrderStatusString = value;
            }
        }
        public string PaymentMethodString
        {
            get
            {
                switch (this.PaymentMethodId)
                {
                    case (int)PaymentMethodEnum.CASH:
                        return MessageValue.MESSAGE_FROM_END_WORKING_SESION_CASH_MONEY;
                    case (int)PaymentMethodEnum.BANK:
                        return MessageValue.MESSAGE_FROM_END_WORKING_SESION_BANK_MONEY;
                    case (int)PaymentMethodEnum.CASH_AND_BANK:
                        return MessageValue.MESSAGE_FROM_END_WORKING_SESION_CASH_AND_BANK_MONEY;
                    case (int)PaymentMethodEnum.MEMBERSHIP_CARD:
                        return MessageValue.MESSAGE_FROM_END_WORKING_SESION_MEMBER_POINT;
                    case (int)PaymentMethodEnum.UNKNOW:
                        return MessageValue.MESSAGE_FROM_END_WORKING_SESION_UNKNOW_MONEY;
                    case (int)PaymentMethodEnum.TRANSFER:
                        return MessageValue.MESSAGE_FROM_END_WORKING_SESION_TRANSFER_ORDER;                
                    default:
                        return MessageValue.MESSAGE_FROM_END_WORKING_SESION_CASH_AND_TRANSFER_MONEY;
                }
            }
            set
            {
                PaymentMethodString = value;
            }
        }

    }
}
