using Newtonsoft.Json;

namespace TechresStandaloneSale.Models.Request
{
    public class ReceiveDepositWrapper
    {
        public long BookingId { get; set; }
        [JsonProperty("branch_id")]
        public long BranchId { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }
        [JsonProperty("payment_method")]
        public int PaymentMethod { get; set; }

        public ReceiveDepositWrapper(long bookingId, long branchId, decimal amount,int paymentMethod)
        {
            this.BookingId = bookingId;
            this.BranchId = branchId;
            this.Amount = amount;
            this.PaymentMethod = paymentMethod;
        }
    }
}
