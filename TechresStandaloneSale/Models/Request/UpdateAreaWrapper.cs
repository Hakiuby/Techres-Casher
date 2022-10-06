using Newtonsoft.Json;

namespace TechresStandaloneSale.Models.Request
{
    public class UpdateAreaWrapper
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("status")]
        public long Status { get; set; }
        [JsonProperty("branch_id")]
        public long BranchId { get; set; }
        public override string ToString()
        {
            return Name;
        }
        public UpdateAreaWrapper(long id, string name, long branchId, long status)
        {
            BranchId = branchId;
            this.Id = id;
            this.Name = name;
            this.Status = status;
        }
    }
}
