using Newtonsoft.Json;

namespace TechresStandaloneSale.Models.Request
{
   public class UpdateTableWrapper
    {
        [JsonProperty("table_id")]
        public int Id { get; set; }
        [JsonProperty("table_name")]
        public string Name { get; set; }
        [JsonProperty("area_id")]
        public long AreaId { get; set; }
        [JsonProperty("total_slot")]
        public int TotalSlot { get; set; }
        [JsonProperty("status")]
        public long Status { get; set; }

        [JsonProperty("branch_id")]
        public long BranchId { get; set; }
        public override string ToString()
        {
            return Name;
        }

        public UpdateTableWrapper(int id, string name, long areaId, int totalSlot, long status, long branchId)
        {
            this.Id = id;
            this.Name = name;
            this.AreaId = areaId;
            this.TotalSlot = totalSlot;
            this.Status = status;
            this.BranchId = branchId;
        }
    }
}
