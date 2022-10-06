using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechresStandaloneSale.Models.Request
{
    public class UpdatePaymentSlipWrapper
    {
        [JsonProperty("branch_id")]
        public long BranchId { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("is_count_to_revenue")]
        public long IsCountToRevenue { get; set; }

        [JsonProperty("payment_method_id")]
        public long PaymentMethodId { get; set; }

        [JsonProperty("addition_fee_reason_id")]
        public long AdditionFeeReasonId { get; set; }

        [JsonProperty("note")]
        public string Note { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }
        [JsonProperty("warehouse_session_ids")]
        public List<long> WarehouseSessionIds { get; set; }
        [JsonProperty("object_name")]
        public string ObjectName { get; set; }

        public UpdatePaymentSlipWrapper(long branchId, long id, long isCountToRevenue, long paymentMethodId, long additionFeeReasonId, string note, string date, decimal amount,string objectname)
        {
            
            BranchId = branchId;
            Id = id;
            IsCountToRevenue = isCountToRevenue;
            PaymentMethodId = paymentMethodId;
            AdditionFeeReasonId = additionFeeReasonId;
            Note = note;
            Date = date;
            Amount = amount;
            WarehouseSessionIds = new List<long>();
            ObjectName = objectname; 
        }
    }
}
