using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechresStandaloneSale.Models.Request
{
    public class EditBookingWrapper
    {
        [JsonProperty("customer_id")]
        public long CustomerId { get; set; }
        [JsonProperty("customer_last_name")]
        public string CustomerLastName { get; set; }
        [JsonProperty("customer_first_name")]
        public string CustomerFristName { get; set; }

        [JsonProperty("customer_name")]
        public string CustomerName { get; set; }

        [JsonProperty("customer_phone")]
        public string CustomerPhone { get; set; }
        [JsonProperty("booking_id")]
        public long BookingId { get; set; }

        [JsonProperty("branch_id")]
        public long BranchId { get; set; }

        [JsonProperty("orther_requirements")]
        public string OrtherRequirements { get; set; }

        [JsonProperty("note")]
        public string Note { get; set; }

        [JsonProperty("number_slot")]
        public long NumberSlot { get; set; }

        [JsonProperty("food_request")]
        public List<FoodUsing> FoodRequest { get; set; }

        [JsonProperty("booking_time")]
        public string BookingTime { get; set; }


        [JsonProperty("deposit_amount")]
        public decimal DepositAmount { get; set; }
       

        public EditBookingWrapper(long BookingId, long BranchId, string OrtherRequirements, string Note,
               long NumberSlot, List<FoodUsing> FoodRequest, string BookingTime, long customerId, string customerName, string customerPhone, string customerFristName, string customerLastName,decimal depositamount)
        {
            this.BookingId = BookingId;
            this.BranchId = BranchId;
            this.OrtherRequirements = OrtherRequirements;
            this.Note = Note;
            this.NumberSlot = NumberSlot;
            this.FoodRequest = FoodRequest;
            this.BookingTime = BookingTime;
            CustomerId = customerId;
            CustomerName = customerName;
            CustomerPhone = customerPhone;
            CustomerFristName = customerFristName;
            CustomerLastName = customerLastName;
            this.DepositAmount = depositamount;
        }
    }
}
