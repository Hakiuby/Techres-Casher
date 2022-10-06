using Newtonsoft.Json;
using System.Collections.Generic;

namespace TechresStandaloneSale.Models.Response
{
    public class SysthesisReportOrdersResponse : BaseResponse
    {
        [JsonProperty("data")]
        public SysthesisReportOrdersData Data { get; set; }
    }

    public class SysthesisReportOrdersData
    {

        [JsonProperty("limit")]
        public long Limit { get; set; }

        [JsonProperty("order")]
        public List<Order> Orders { get; set; }

        [JsonProperty("total_record")]
        public long TotalRecord { get; set; }

        [JsonProperty("total_amount")]
        public decimal TotalAmount { get; set; }

        [JsonProperty("vat_amount")]
        public decimal VatAmount { get; set; }

        [JsonProperty("cash_amount")]
        public decimal CashAmount { get; set; }

        [JsonProperty("bank_amount")]
        public decimal BankAmount { get; set; }

        [JsonProperty("discount_amount")]
        public decimal DiscountAmount { get; set; }


    }

}

