using Newtonsoft.Json;
using System.Collections.Generic;

namespace TechresStandaloneSale.Models.Request
{
    public class WorkingSessionWrapper
    {

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("branch_ids")]
        public List<long> BranchId { get; set; }

        [JsonProperty("from_hour")]
        public string FromHour { get; set; }

        [JsonProperty("to_hour")]
        public string ToHour { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        public WorkingSessionWrapper(long id, List<long> branchId, string FromHour, string ToHour, int Status, string name)
        {
            this.Id = id;
            if (branchId != null && branchId.Count > 0)
            {
                this.BranchId = branchId;
            }
            else
            {
                this.BranchId = new List<long>();
            }
            this.FromHour = FromHour;
            this.ToHour = ToHour;
            this.Status = Status;
            this.Name = name;
        }
    }
}
