using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechresStandaloneSale.Models.Response
{
    public class FormServiceResponse:BaseResponse
    {
        [JsonProperty("data")]
        public FormServiceData Data { get; set; }
    }
    public class FormServiceData
    {
        [JsonProperty("sum_amount")]
        public long SumAmount { get; set; }

        [JsonProperty("total_number_order")]
        public long TotalNumberOrder { get; set; }

        [JsonProperty("details")]
        public List<FormService> Details { get; set; }
    }

    public class FormService
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("total_amount")]
        public long TotalAmount { get; set; }

        [JsonProperty("number_order")]
        public long NumberOrder { get; set; }

        [JsonProperty("revenue_rate")]
        public double RevenueRate { get; set; }

        [JsonProperty("order_rate")]
        public double OrderRate { get; set; }
        public string TotalAmountFormat {
            get
            {
                return Utils.Utils.FormatMoney(this.TotalAmount);
            }
            set
            {
                TotalAmountFormat = value;
            }
        }
    }
}
