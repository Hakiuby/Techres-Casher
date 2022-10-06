using Newtonsoft.Json;
using TechresStandaloneSale.Models.Response;

namespace TechresStandaloneSale.Models
{
    public class Debit : BaseResponse
    {
        [JsonProperty("order_id")]
        public long OrderId { get; set; }

        [JsonProperty("employee")]
        public Employee Employee { get; set; }

        [JsonProperty("debt_time")]
        public string DebtTime { get; set; }

        [JsonProperty("debt_amount")]
        public long DebtAmount { get; set; }

        [JsonProperty("table_name")]
        public string TableName { get; set; }

        [JsonProperty("table_id")]
        public long TableId { get; set; }
        [JsonProperty("note")]
        public string Note { get; set; }
        

        public string DebitAmountString
        {
            get
            {

                return string.Format("{0:0,0}", this.DebtAmount);
            }
            set
            {
                DebitAmountString = value;
            }
        }
        public string OrderCode
        {
            get
            {

                return string.Format("#{0}", this.OrderId);
            }
            set
            {
                OrderCode = value;
            }
        }
    }
}
