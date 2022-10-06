using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;

namespace TechresStandaloneSale.Models
{
    public class OrderDetailAddition
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("prefix")]
        public string Prefix { get; set; }

        [JsonProperty("quantity")]
        public decimal Quantity { get; set; }

        [JsonProperty("note")]
        public string Note { get; set; }

        [JsonProperty("food_id")]
        public long FoodId { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("food_name")]
        public string FoodName { get; set; }

        [JsonProperty("normalize_name")]
        public string NormalizeName { get; set; }

        [JsonProperty("unit")]
        public string Unit { get; set; }
        [JsonProperty("unit_price")]
        public decimal UnitPrice { get; set; }
        [JsonIgnore]
        public string FoodNameAdditionPrintBill
        {
            get
            {
                if (!string.IsNullOrEmpty(FoodName))
                {
                    //return string.Format("  + {0} {1} ({2}/{3})", Quantity, FoodName, Utils.Utils.FormatMoney(UnitPrice), Unit);
                    return string.Format("  + {0} {1}", Quantity, FoodName);
                }
                else
                {
                    //return string.Format(" + {0} {1} ({2}/{3})", Quantity, Name, Utils.Utils.FormatMoney(UnitPrice), Unit);
                    return string.Format(" + {0} {1}", Quantity, Name);
                }
            }
            set
            {
                FoodNameAdditionPrintBill = value;
            }
        }

        [JsonIgnore]
        public string FoodNameValue
        {
            get
            {
                return string.Format(" + {0}  {1}", Quantity, FoodName); ;
            }
            set
            {
                FoodNameValue = value;
            }
        }
    }
}
