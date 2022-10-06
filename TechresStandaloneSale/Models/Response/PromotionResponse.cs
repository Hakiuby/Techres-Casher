using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechresStandaloneSale.Models.Response
{
    public class PromotionResponse : BaseResponse
    {
        [JsonProperty("data")]
        public List<Promotion> Data { get; set; }

    }
    public class PromotionItemResponse : BaseResponse
    {
        [JsonProperty("data")]
        public Promotion Data { get; set; }

    }
    public class Promotion
    {
        [JsonIgnore]
        public bool IsApplyPromotion { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("restaurant_promotion_voucher_id")]
        public long RestaurantPromotionVoucherId { get; set; }

        [JsonProperty("restaurant_promotion_voucher_code")]
        public string RestaurantPromotionVoucherCode { get; set; }

        [JsonProperty("is_allow_use_with_other_promotion")]
        public byte IsAllowUseWithOtherPromotion { get; set; }
    }
}
