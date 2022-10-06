using Newtonsoft.Json;

namespace TechresStandaloneSale.Models.Request
{
    public class CreateOrderWrapper
    {
        [JsonProperty(PropertyName = "note")]
        public string Note { get; set; }

        [JsonProperty(PropertyName = "table_id")]
        public long TableId { get; set; }

        [JsonProperty(PropertyName = "employee_id")]
        public long EmployeeId { get; set; }

        [JsonProperty(PropertyName = "customer_slot_number")]
        public long CustomerSlotNumber { get; set; }
        [JsonProperty(PropertyName = "order_method")]
        public long OrderMethod { get; set; }

        public CreateOrderWrapper(string note, long tableId, long employeeId, long customerSlotNumber, long orderMethod)
        {
            Note = string.IsNullOrEmpty(note) ? "": note;
            TableId = tableId;
            EmployeeId = employeeId;
            CustomerSlotNumber = customerSlotNumber;
            OrderMethod = orderMethod;
        }

    }
}
