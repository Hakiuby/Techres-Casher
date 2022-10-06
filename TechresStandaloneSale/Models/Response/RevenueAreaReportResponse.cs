using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechresStandaloneSale.Models.Response
{
    public class RevenueAreaReportResponse : BaseResponse
    {
        [JsonProperty("data")]
        public RevenueAreaDataResponse Data { get; set; }

    }
    public class RevenueAreaDataResponse
    {

        [JsonProperty("total_revenue")]
        public decimal TotalRevenue { get; set; }

        [JsonProperty("details")]
        public List<ReportRevenueAreas> ReportData { get; set; }
    }

    
}
