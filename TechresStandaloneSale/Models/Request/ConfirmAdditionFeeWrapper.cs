using Newtonsoft.Json;

namespace TechresStandaloneSale.Models.Request
{
    public class ConfirmAdditionFeeWrapper
    {
        [JsonProperty("branch_id")]
        public long BranchId { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        public ConfirmAdditionFeeWrapper(long branchId, long id)
        {
            BranchId = branchId;
            Id = id;
        }
    }
}
