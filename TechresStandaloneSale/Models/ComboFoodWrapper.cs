using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechresStandaloneSale.Models
{
    public class ComboFoodWrapper
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("quantity")]
        public decimal Quantity { get; set; }
        public ComboFoodWrapper(long Id, decimal Quantity)
        {
            this.Id = Id;
            this.Quantity = Quantity;
        }
    }
}
