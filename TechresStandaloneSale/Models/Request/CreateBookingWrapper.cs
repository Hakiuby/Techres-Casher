using Newtonsoft.Json;
using System.Collections.Generic;

namespace TechresStandaloneSale.Models.Request
{
    public class CreateBookingWrapper
    {
        [JsonProperty("branch_id")]
        public long BranchId { get; set; }

        [JsonProperty("customer_id")]
        public long CustomerId { get; set; }
        [JsonProperty("customer_name")]
        public string CustomerName { get; set; }

        [JsonProperty("customer_phone")]
        public string CustomerPhone { get; set; }

        [JsonProperty("orther_requirements")]
        public string OrtherRequirements { get; set; }

        [JsonProperty("note")]
        public string Note { get; set; }

        [JsonProperty("deposit_amount")]
        public decimal Deposit { get; set; }

        [JsonProperty("number_slot")]
        public long NumberSlot { get; set; }

        [JsonProperty("food_request")]
        public List<FoodUsing> FoodRequest { get; set; }

        [JsonProperty("booking_time")]
        public string BookingTime { get; set; }

        [JsonProperty("booking_type")]
        public long BookingType { get; set; }

        [JsonProperty("employee_id")]
        public long EmployeeId { get; set; }
        [JsonProperty("last_name")]
        public string CustomerLastName { get; set; }
        [JsonProperty("first_name")]
        public string CustomerFristName { get; set; }
        public CreateBookingWrapper(long BranchId, long CustomerId, string CustomerName, string CustomerPhone, string OrtherRequirements, string Note,
            long NumberSlot, List<FoodUsing> FoodRequest, string BookingTime, long BookingType, long EmployeeId, string customerFristName, string customerLastName,decimal depositamount)
        {
            this.BranchId = BranchId;
            this.CustomerId = CustomerId;
            this.OrtherRequirements = OrtherRequirements;
            this.Note = Note;
            this.Deposit = Deposit;
            this.NumberSlot = NumberSlot;
            this.FoodRequest = FoodRequest;
            this.BookingTime = BookingTime;
            this.BookingType = BookingType;
            this.EmployeeId = EmployeeId;
            this.CustomerName = CustomerName;
            this.CustomerPhone = CustomerPhone;
            CustomerLastName = customerLastName; // Dat
            CustomerFristName = customerFristName; // Dat
            this.Deposit = depositamount;//toan
            //CustomerLastName = customerFristName;
            //CustomerFristName = customerLastName;
        }

    }
}
