using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechresStandaloneSale.Models.Response
{
    public class RevenuePaymentMethodResponse : BaseResponse
    {
        [JsonProperty("data")]
        public RevenuePaymentMethodData Data { get; set; }
    }
    public class RevenuePaymentMethodData
    {
        [JsonProperty("cash_amount")]
        public decimal CashAmount { get; set; }
        [JsonProperty("bank_amount")]
        public decimal BankAmount { get; set; }
        [JsonProperty("transfer_amount")]
        public decimal TransferAmount { get; set; }
        [JsonProperty("total_point_used")]
        public decimal TotalPointUsed { get; set; }
        [JsonProperty("total_point_used_amount")]
        public decimal TotalPointUsedAmount { get; set; }
    }
}
