using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechresStandaloneSale.Models
{
   public class Supplier
    {
       [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("prefix")]
        public string Prefix { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("status")]
        public long Status { get; set; }

        [JsonProperty("website")]
        public string Website { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("normalize_name")]
        public string NormalizeName { get; set; }

        [JsonProperty("tax_code")]
        public string TaxCode { get; set; }


        //[JsonProperty("materials_branch")]
        //public List<Material> MaterialsBranch { get; set; }

        [JsonProperty("is_selected")]
        public long IsSelected { get; set; }

        [JsonProperty("restaurant_supplier_template_status")]
        public long RestaurantSupplierTemplateStatus { get; set; }
        [JsonIgnore]
        public bool isCheck { get; set; }

        public string Code
        {
            get
            {

                return string.Format("#{0}", this.Id);
            }
            set
            {
                Code = value;
            }
        }

        public override string ToString()
        {
                return Name;
          }
    }
}
