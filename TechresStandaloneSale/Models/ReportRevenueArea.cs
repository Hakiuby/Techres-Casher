using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechresStandaloneSale.Models
{
    public class ReportRevenueAreas
    {
        public long No { get; set; }

        [JsonProperty("revenue")]
        public decimal Revenue { get; set; }

        [JsonProperty("branch_id")]
        public long BranchId { get; set; }

        [JsonProperty("branch_name")]
        public string BranchName { get; set; }

        [JsonProperty("branch_phone")]
        public string BranchPhone { get; set; }

        [JsonProperty("branch_address")]
        public string BranchAddress { get; set; }

        [JsonProperty("area_id")]
        public long AreaId { get; set; }

        [JsonProperty("area_name")]
        public string AreaName { get; set; }
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
