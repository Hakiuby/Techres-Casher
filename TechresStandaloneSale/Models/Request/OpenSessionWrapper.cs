using Newtonsoft.Json;

namespace TechresStandaloneSale.Models.Request
{
    public class OpenSessionWrapper
    {
        [JsonProperty("before_cash")]
        public decimal BeforeCash { get; set; }
        [JsonProperty("branch_working_session_id")]
        public long BranchWorkingSessionId { get; set; }

        public OpenSessionWrapper(decimal before, long branchWorkingSessionId)
        {
            BeforeCash = before;
            BranchWorkingSessionId = branchWorkingSessionId;
        }
    }
}
