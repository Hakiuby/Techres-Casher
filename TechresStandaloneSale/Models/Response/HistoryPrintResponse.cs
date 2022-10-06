using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TechresStandaloneSale.Models.Response
{
    public class HistoryPrintResponse: BaseResponse
    {
        [JsonProperty("data")]
        public List<HistoryPrintData> Data { get; set; }


    }
    public class HistoryPrintData
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("table_id")]
        public long TableId { get; set; }
        [JsonProperty("table_name")]
        public string TableName { get; set; }
        [JsonProperty("last_update_at")]
        public string LastUpdateAt { get; set; }

        [JsonProperty("order_details")]
        public List<HistoryOrderDetails> OrderDetails { get; set;  }

        [JsonIgnore]
        public List<string> LstNameFood
        {
            get
            {
                return this.OrderDetails.Select(x => x.FoodName).ToList();
            }
            set
            {
                LstNameFood = value;
            }

        }
        int i;
        [JsonIgnore]
        public Visibility ZoomInVisibilityy { get; set; }
        [JsonIgnore]
        public Visibility ZoomOutVisibility { get; set; }
        [JsonIgnore]
        public string NameFood { get; set; }
        //public string NameFood
        //{
        //    get
        //    {
        //        if(LstNameFood.Count > 6)
        //        {
        //            var list = LstNameFood.Take(6).ToList();
        //            return list.Aggregate((current, next) => current + ", " + next) ;
        //        }
        //        else
        //        {
        //            return LstNameFood.Aggregate((current, next) => current + ", " + next);
        //        }
        //        //return LstNameFood.Aggregate((current, next) => current + ", " + next);

        //    }
        //    set
        //    {
        //        NameFood = value;
        //    }
        //}

    }
    public class HistoryOrderDetails
    {
        [JsonProperty("name")]
        public string FoodName { get; set; }
        [JsonProperty("table_name")]
        public string TableName { get; set; }
        [JsonProperty("note")]
        public string Note { get; set; }
        [JsonProperty("created_at")]
        public string CreateAt { get; set; }
        [JsonProperty("employee_name")]
        public string EmployeeName { get; set; }
        [JsonProperty("status_text")]
        public string StatusText { get; set; }
    }
}
