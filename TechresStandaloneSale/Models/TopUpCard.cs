using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TechresStandaloneSale.Models
{
   public class TopUpCard
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("status")]
        public long Status { get; set; }

        [JsonProperty("bonus_amount")]
        public decimal BonusAmount { get; set; }

        [JsonIgnore]
        public bool IsChoose { get; set; }
        public string AmountString
        {
            get
            {
                return Utils.Utils.FormatMoney(this.Amount);
            }
            set
            {
                AmountString = value;
            }
        }
        public string BonusAmountString
        {
            get
            {
                return string.Format("KM: {0}", Utils.Utils.FormatMoney(this.BonusAmount));
            }
            set
            {
                BonusAmountString = value;
            }
        }
        public Visibility CheckVisibility 
        {
            get
            {
                if (IsChoose)
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Collapsed;
                }
            }
            set
            {
                CheckVisibility = value;
            }
        }
    }
}
