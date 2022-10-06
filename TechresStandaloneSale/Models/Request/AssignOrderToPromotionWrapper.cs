using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechresStandaloneSale.Models.Request
{
  public  class AssignOrderToPromotionWrapper
    {
        [JsonProperty("branch_id")]
        public long BranchId { get; set; }

        [JsonProperty("promotion_voucher_id")]
        public long PromotionVoucherId { get; set; }

        public AssignOrderToPromotionWrapper(long branchId, long promotionVoucherId)
        {
            BranchId = branchId;
            PromotionVoucherId = promotionVoucherId;
        }
    }
}
