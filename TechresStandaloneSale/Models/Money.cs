using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechresStandaloneSale.Models
{
    public class Money
    {
        [JsonProperty("value")]
        public decimal Denominations { get; set; }
        [JsonProperty("quantity")]
        public long Quantity { get; set; }
      [JsonIgnore]
        public decimal Amount
        {
            get
            {
                return Denominations*Quantity;
            }
            set
            {
                Amount = value;
            }
        }
        public Money(decimal denominations, long quantity)
        {
            Denominations = denominations;
            Quantity = quantity;
            Console.WriteLine(Denominations); 
        }
        [JsonIgnore]
        public string DenominationsFormat
        {
            get
            {
                return Utils.Utils.FormatMoney(Denominations);
            }
            set
            {
                DenominationsFormat = value;
            }
        }
        [JsonIgnore]
        public string AmountFormat
        {
            get
            {
                return Utils.Utils.FormatMoney(this.Amount);
            }
            set
            {
                AmountFormat = value;
            }
        }

    }
}
