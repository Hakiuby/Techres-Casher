using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TechresStandaloneSale.Models.Response
{
    public class CustomerDebtResponse : BaseResponse
    {
        [JsonProperty("data")]
        public List<CustomerDebtData> Data { get; set; }
    }
    public class CustomerDebtData
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("note")]
        public string Note { get; set; }

        [JsonProperty("restaurant_id")]
        public long RestaurantId { get; set; }

        [JsonProperty("branch_id")]
        public long BranchId { get; set; }

        [JsonProperty("customer_id")]
        public long CustomerId { get; set; }

        [JsonProperty("order_id")]
        public long OrderId { get; set; }

        [JsonProperty("total_amount")]
        public decimal TotalAmount { get; set; }

        [JsonProperty("vat_amount")]
        public decimal VatAmount { get; set; }

        [JsonProperty("discount_amount")]
        public decimal DiscountAmount { get; set; }

        [JsonProperty("debt_time")]
        public string DebtTime { get; set; }

        [JsonProperty("payment_time")]
        public string PaymentTime { get; set; }

        [JsonProperty("is_paid")]
        public int IsPaid { get; set; }

        [JsonProperty("employee_confirm_id")]
        public long EmployeeConfirmId { get; set; }

        [JsonProperty("employee_recover_debt_id")]
        public long EmployeeRecoverDebtId { get; set; }

        [JsonProperty("customer_name")]
        public string CustomerName { get; set; }

        [JsonProperty("customer_address_full_text")]
        public string CustomerAddressFullText { get; set; }

        [JsonProperty("customer_phone")]
        public string CustomerPhone { get; set; }
        public string TotalAmountString
        {
            get
            {
                return Utils.Utils.FormatMoney(this.TotalAmount);
            }
            set
            {
                this.TotalAmountString = value;
            }
        }
        public string StatusPaidString
        {
            get
            {
                if(this.IsPaid == 1)
                {
                    return "Đã thanh toán";
                }
                else
                {
                    return "Chưa thanh toán";
                }
            }
            set
            {
                this.TotalAmountString = value;
            }
        }
        public Visibility ShowCheckBox
        {
            get
            {
                if (this.IsPaid == 1)
                {
                    return Visibility.Collapsed;
                }
                else
                {
                    return Visibility.Visible;
                }
            }
            set
            {
                this.ShowCheckBox = value;
            }
        }
        public bool IsSelected { get; set; }       
    }
}
