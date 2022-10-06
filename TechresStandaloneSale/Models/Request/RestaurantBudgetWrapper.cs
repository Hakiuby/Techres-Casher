using Newtonsoft.Json;

namespace TechresStandaloneSale.Models.Request
{
    public class RestaurantBudgetWrapper
    {
        [JsonProperty("branch_id")]
        public long BranchId { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("note")]
        public string Note { get; set; }

        public RestaurantBudgetWrapper(long branchId, long id, string note)
        {
            BranchId = branchId;
            Id = id;
            Note = string.IsNullOrEmpty(note) ? "" : note;
        }
    }
}
