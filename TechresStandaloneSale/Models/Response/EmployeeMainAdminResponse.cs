using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechresStandaloneSale.Models.Response
{
   public class EmployeeMainAdminResponse:BaseResponse
    {
        [JsonProperty("data")]
        public Data Data { get; set; }
    }
    public partial class Data
    {
        [JsonProperty("total_revenue")]
        public long TotalRevenue { get; set; }

        [JsonProperty("list")]
        public List<EmployeeMainAdmin> List { get; set; }

        public string TotalRevenueString
        {
            get
            {
                return Utils.Utils.FormatMoney(this.TotalRevenue);
            }
            set
            {
                TotalRevenueString = value;
            }
        }

    }
    public class EmployeeMainAdmin
    {
        [JsonIgnore]
        public long No { get; set; }
        [JsonProperty("revenue")]
        public decimal Revenue { get; set; }

        [JsonProperty("branch")]
        public Branch Branch { get; set; }

        [JsonProperty("employee")]
        public Employee Employee { get; set; }
        public string RevenueString
        {
            get
            {
                return Utils.Utils.FormatMoney(this.Revenue);
            }
            set
            {
                RevenueString = value;
            }
        }
    }
}
