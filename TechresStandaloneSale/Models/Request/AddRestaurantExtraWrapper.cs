using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechresStandaloneSale.Models.Request
{
   public class AddRestaurantExtraWrapper
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("note")]
        public string Note { get; set; }

        public AddRestaurantExtraWrapper(long id, string name, decimal price, int quantity, string note)
        {
            Id = id;
            Name = string.IsNullOrEmpty(name) ? "KHÁC" : name;
            Price = price;
            Quantity = quantity;
            Note = string.IsNullOrEmpty(note) ? "" : note;
        }
    }
}
