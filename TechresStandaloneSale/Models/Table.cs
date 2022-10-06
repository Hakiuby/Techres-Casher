using Newtonsoft.Json;
using System.Collections.Generic;

namespace TechresStandaloneSale.Models
{
    public class Table
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("area_id")]
        public int AreaId { get; set; }
        [JsonProperty("area_name")]
        public string AreaName { get; set; }
        [JsonProperty("area")]
        public string AreaNameBooking { get; set; }
        [JsonProperty("status")]
        public int TableStatus { get; set; }
        [JsonProperty("is_active")]
        public int IsActive { get; set; }

        [JsonProperty("slot_number")]
        public int SlotNumber { get; set; }

        [JsonProperty("table_merged_name")]
        public List<string> TableMergedName { get; set; }
        [JsonProperty("merge_table_name")]
        public string MergerTableName { get; set; }
        [JsonProperty("status_text")]
        public string StatusText { get; set; }
        [JsonProperty("booking_time")]
        public string BookingTime { get; set; }

        [JsonProperty("is_not_alow_open")]
        public byte IsNotAlowOpen { get; set; }
        [JsonProperty("is_take_away")]
        public long IsTakeAway { get; set; }
        [JsonProperty("order_status")]
        public long OrderStatus { get; set; }
       

        public override string ToString()
        {
            return Name;
        }

    }
}
