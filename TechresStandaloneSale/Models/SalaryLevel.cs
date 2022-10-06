using Newtonsoft.Json;

namespace TechresStandaloneSale.Models
{
    public class SalaryLevel
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("level")]
        public string Level { get; set; }

        [JsonProperty("basic_salary")]
        public decimal BasicSalary { get; set; }
        public string BasicSalaryString
        {
            get
            {

                return Utils.Utils.FormatMoney(this.BasicSalary);
            }
            set
            {
                BasicSalaryString = value;
            }
        }
        public override string ToString()
        {
            return Level.ToString();
        }
    }
}
