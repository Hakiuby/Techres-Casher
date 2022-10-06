using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TechresStandaloneSale.Helpers;

namespace TechresStandaloneSale.Models.Response
{
      public class WarehouseSession
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("type")]
        public long Type { get; set; }

        [JsonProperty("status")]
        public long Status { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }

        [JsonProperty("branch")]
        public Branch Branch { get; set; }

        [JsonProperty("restaurant_supplier")]
        public Supplier RestaurantSupplier { get; set; }

        [JsonProperty("employee")]
        public Employee Employee { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("total_amount")]
        public decimal TotalAmount { get; set; }

        [JsonProperty("paid_status")]
        public long PaidStatus { get; set; }

        [JsonProperty("paid_status_name")]
        public string PaidStatusName { get; set; }

        [JsonProperty("type_name")]
        public string TypeName { get; set; }

        [JsonProperty("session_status_name")]
        public string SessionStatusName { get; set; }

        [JsonProperty("delivery_date")]
        public string DeliveryDate { get; set; }

        [JsonProperty("session_status")]
        public long SessionStatus { get; set; }

        [JsonProperty("history_log")]
        public string HistoryLog { get; set; }

        [JsonProperty("goods_type")]
        public long GoodsType { get; set; }

        [JsonProperty("goods_type_name")]
        public string GoodsTypeName { get; set; }

        public bool isCheck { get; set; }
        public string NameString
        {
            get
            {

                return string.Format("{0} - TT:{1} - NN:{2}", this.Code,this.TotalAmountString, DeliveryDate);
            }
            set
            {
                NameString = value;
            }
        }
        public string TotalAmountString
        {
            get
            {

                return Utils.Utils.FormatMoney(Math.Abs(this.TotalAmount));
            }
            set
            {
                TotalAmountString = value;
            }
        }
        
        [JsonIgnore]
        public Visibility BtnEditVisibility
        {
            get
            {
                if (SessionStatus == (int)AdditionFeeStatusEnum.PAID || SessionStatus == (int)AdditionFeeStatusEnum.CANCEL)
                {
                    return Visibility.Collapsed;
                }
                else
                {
                    return Visibility.Visible;
                }
            }
            set
            {
                BtnEditVisibility = value;
            }
        }

        [JsonIgnore]
        public Visibility BtnCopyVisibility
        {
            get
            {
                if ((Type == (int)WarehouseTypeEnum.OUT || Type == (int)WarehouseTypeEnum.IN) && SessionStatus != (int)AdditionFeeStatusEnum.CANCEL)
                {
                    return Visibility.Visible;
                }
                else 
                {
                    return Visibility.Collapsed;
                }
            }
            set
            {
                BtnCopyVisibility = value;
            }
        }

        [JsonIgnore]
        public Visibility BtnReturnVisibility
        {
            get
            {
                if (Type == (int)WarehouseTypeEnum.OUT || Type == (int)WarehouseTypeEnum.IN)
                {
                    
                        return Visibility.Visible;
                }
                else
                {
                    return Visibility.Collapsed;
                }
            }
            set
            {
                BtnReturnVisibility = value;
            }
        }

        [JsonIgnore]
        public Visibility BtnCancelVisibility
        {
            get
            {
                if (SessionStatus == (int)AdditionFeeStatusEnum.PAID || SessionStatus == (int)AdditionFeeStatusEnum.CANCEL)
                {
                    return Visibility.Collapsed;
                }
                else
                {
                    return Visibility.Visible;
                }
            }
            set
            {
                BtnCancelVisibility = value;
            }
        }
    }
}
