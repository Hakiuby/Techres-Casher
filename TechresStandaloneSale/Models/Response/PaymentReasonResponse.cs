using Newtonsoft.Json;
using System.Collections.Generic;

namespace TechresStandaloneSale.Models.Response
{
    public class PaymentReasonResponse : BaseResponse
    {
        [JsonProperty("data")]
        public List<PaymentReason> Data { get; set; }
    }
    public class PaymentReasonData:BaseResponse
    {
        [JsonProperty("data")]
        public PaymentReason Data { get; set; }
    }
    public class PaymentReason
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("status")]
        public long Status { get; set; }
        [JsonProperty("addition_fee_reason_type_id")]
        public long AdditionFeeReasonTypeId { get; set; }

        [JsonProperty("addition_fee_reason_type_name")]
        public string AdditionFeeReasonTypeName { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}
