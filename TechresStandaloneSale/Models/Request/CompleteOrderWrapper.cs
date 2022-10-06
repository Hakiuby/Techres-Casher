using Newtonsoft.Json;

namespace TechresStandaloneSale.Models.Request
{
    public class CompleteOrderWrapper
    {
        [JsonProperty("bank_amount")]
        public decimal BankAmount { get; set; }

        [JsonProperty("cash_amount")]
        public decimal CashAmount { get; set; }

        [JsonProperty("tip_amount")]
        public decimal TipAmount { get; set; }
        [JsonProperty("transfer_amount")]
        public decimal TransferAmount { get; set; }
        public CompleteOrderWrapper( decimal bankAmount,decimal cashAmount, decimal tipAmount, decimal transferAmount)
        {
            BankAmount = bankAmount;
            CashAmount = cashAmount;
            TipAmount = tipAmount;
            TransferAmount = transferAmount;
        }
    }
}
