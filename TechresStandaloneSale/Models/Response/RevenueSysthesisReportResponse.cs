

using Newtonsoft.Json;
using System.Collections.Generic;

namespace TechresStandaloneSale.Models.Response
{
    public class RevenueSysthesisReportResponse : BaseResponse
    {
        [JsonProperty("data")]
        public RevenueSysthesisReportData Data { get; set; }
    }
    public class RevenueSysthesisReportData
    {
        [JsonProperty("total_sale_revenue")]
        public decimal TotalSaleRevenue { get; set; }

        [JsonProperty("total_revenue")]
        public decimal TotalRevenue { get; set; }

        [JsonProperty("total_gift")]
        public decimal TotalGift { get; set; }

        [JsonProperty("total_discount")]
        public decimal TotalDiscount { get; set; }

        [JsonProperty("total_vat")]
        public decimal TotalVat { get; set; }

        [JsonProperty("bank_amount")]
        public decimal BankAmount { get; set; }

        [JsonProperty("cash_amount")]
        public decimal CashAmount { get; set; }
        [JsonProperty("transfer_amount")]
        public decimal TransferAmount { get; set; }

        [JsonProperty("membership_point_used")]
        public decimal MembershipPointUsed { get; set; }
        [JsonProperty("membership_alo_point_used")]
        public decimal MembershipAloPointUsed { get; set; }
        [JsonProperty("membership_promotion_point_used")]
        public decimal MembershipPromotionPointUsed { get; set; }
        [JsonProperty("membership_accumulate_point_used")]
        public decimal MembershipAccumulatePointUsed { get; set; }
        [JsonProperty("membership_total_point_used")]
        public decimal MembershipTotalPointUsed { get; set; }

        [JsonProperty("total_order")]
        public decimal TotalOrder { get; set; }

        [JsonProperty("revenue_exclude_vat")]
        public decimal RevenueExcludeVat { get; set; }
        [JsonProperty("index")]
        public string Index { get; set; }
        [JsonIgnore]
        public string IndexString { get; set; }
        [JsonProperty("detail")]
        public List<RevenueSysthesisReportData> Detail { get; set; }

        public string RevenueExcludeVatFormart
        {
            get
            {
                return Utils.Utils.FormatMoney(this.RevenueExcludeVat);

            }
            set
            {
                RevenueExcludeVatFormart = value;
            }
        }
        public string TotalSaleRevenueFormart
        {
            get
            {
                return Utils.Utils.FormatMoney(this.TotalSaleRevenue);
            }
            set
            {
                TotalSaleRevenueFormart = value;
            }
        }
        public string TotalRevenueFormart
        {
            get
            {
                return Utils.Utils.FormatMoney(this.TotalRevenue);
            }
            set
            {
                TotalRevenueFormart = value;
            }
        }
        public string TotalGiftFormart
        {
            get
            {
                return Utils.Utils.FormatMoney(this.TotalGift);
            }
            set
            {
                TotalGiftFormart = value;
            }
        }
        public string TotalDiscountFormart
        {
            get
            {
                return Utils.Utils.FormatMoney(this.TotalDiscount);
            }
            set
            {
                TotalDiscountFormart = value;
            }
        }
        public string TotalVatFormart
        {
            get
            {
                return Utils.Utils.FormatMoney(this.TotalVat);
            }
            set
            {
                TotalVatFormart = value;
            }
        }
        public string BankAmountFormart
        {
            get
            {
                return Utils.Utils.FormatMoney(this.BankAmount);
            }
            set
            {
                BankAmountFormart = value;
            }
        }
        public string CashAmountFormart
        {
            get
            {
                return Utils.Utils.FormatMoney(this.CashAmount);
            }
            set
            {
                CashAmountFormart = value;
            }
        }
        public string TransferAmountFormat
        {
            get
            {
                return Utils.Utils.FormatMoney(this.TransferAmount);
            }
            set
            {
                TransferAmountFormat = value;
            }
        }
    }
}
