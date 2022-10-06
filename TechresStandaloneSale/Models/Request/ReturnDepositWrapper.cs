using Newtonsoft.Json;

namespace TechresStandaloneSale.Models.Request
{
    public class ReturnDepositWrapper
    {
        [JsonProperty("branch_id")]
        public long BranchId { get; set; }

        [JsonProperty("payment_method")]
        public int PaymentMethod { get; set; }

        public ReturnDepositWrapper(long branchId, int paymentMethod)
        {
            this.BranchId = branchId;
            this.PaymentMethod = paymentMethod;
        }
    }
}
