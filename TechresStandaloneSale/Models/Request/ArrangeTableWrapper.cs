using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechresStandaloneSale.Models.Request
{
    public class ArrangeTableWrapper
    {
        [JsonProperty("branch_id")]
        public long BranchId { get; set; }

        [JsonProperty("area_id")]
        public int AreaId { get; set; }
        [JsonProperty("tables_ids")]
        public List<int> Tables { get; set; }

        public ArrangeTableWrapper(long BranchId, int AreaId,List<int> Tables)
        {
            this.BranchId = BranchId;
            this.AreaId = AreaId;
            this.Tables = Tables;
        }
    }
}
