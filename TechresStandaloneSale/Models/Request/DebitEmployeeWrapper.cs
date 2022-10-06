using Newtonsoft.Json;

namespace TechresStandaloneSale.Models.Request
{
    public class DebitEmployeeWrapper
    {
        [JsonProperty("employee_id")]
        public long EmployeeId { get; set; }

        [JsonProperty("order_id")]
        public long OrderId { get; set; }
        [JsonProperty("note")]
        public string Note { get; set; }
        [JsonProperty("branch_id")]
        public long BranchId { get; set; }

        public DebitEmployeeWrapper(long EmployeeId, long OrderId, string Note, long brachId)
        {
            this.EmployeeId = EmployeeId;
            this.OrderId = OrderId;
            this.Note = Note;
            this.BranchId = brachId; 
        }
    }
}
