using Nest;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace TechresStandaloneSale.Models.Request
{
    public class AdditionFeeWrapper
    {
        [JsonProperty("branch_id")]
        public long BranchId { get; set; }

        [JsonProperty("addition_fee_reason_id")]
        public long AdditionFeeReasonId { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("is_count_to_revenue")]
        public long IsCountToRevenue { get; set; }

        [JsonProperty("note")]
        public string Note { get; set; }

        [JsonProperty("object_id")]
        public long ObjectId { get; set; }

        [JsonProperty("object_name")]
        public string ObjectName { get; set; }

        [JsonProperty("object_type")]
        public long ObjectType { get; set; }

        [JsonProperty("payment_method_id")]
        public long PaymentMethodId { get; set; }

        [JsonProperty("type")]
        public long Type { get; set; }
        [JsonProperty("is_paid")]
        public int IsPaid { get; set; }

        [JsonProperty("warehouse_session_ids")]
        public List<long> WarehouseSessionIds { get; set; }
        public AdditionFeeWrapper(
            long id,
            long branchId, 
            long additionFeeReasonId,
            decimal amount, 
            string date,
            long isCountToRevenue,
            string note, 
            long objectId,
            string objectName,
            long objectType,
            long paymentMethodId, 
            long type, 
            List<long> warehouseSessionIds)
        {
            BranchId = branchId;
            AdditionFeeReasonId = additionFeeReasonId;
            Id = id;
            Amount = amount;
            Date = date;
            IsCountToRevenue = isCountToRevenue;
            Note = string.IsNullOrEmpty(note) ? "" : note;
            ObjectId = objectId;
            ObjectName = string.IsNullOrEmpty(objectName) ? "" : objectName;
            ObjectType = objectType;
            PaymentMethodId = paymentMethodId;
            Type = type;
            WarehouseSessionIds = warehouseSessionIds;
            IsPaid = 1;
        }
    }
  
}
