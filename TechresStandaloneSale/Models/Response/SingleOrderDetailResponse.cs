using Newtonsoft.Json;
using TechresStandaloneSale.Helpers;

namespace TechresStandaloneSale.Models.Response
{
    public class SingleOrderDetailResponse : BaseResponse
    {
        [JsonProperty("data")]
        public SingleOrderDetailData Data { get; set; }
    }
    public class SingleOrderDetailData
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("quantity")]
        public decimal Quantity { get; set; }

        [JsonProperty("note")]
        public string Note { get; set; }

        [JsonProperty("order_id")]
        public long OrderId { get; set; }

        [JsonProperty("table_id")]
        public long TableId { get; set; }

        [JsonProperty("table_name")]
        public string TableName { get; set; }

        [JsonProperty("food_id")]
        public long FoodId { get; set; }

        [JsonProperty("food_name")]
        public string FoodName { get; set; }

        [JsonProperty("unit_price")]
        public decimal UnitPrice { get; set; }

        [JsonProperty("total_quantity_for_drink")]
        public decimal TotalQuantityForDrink { get; set; }

        [JsonProperty("total_price")]
        public decimal TotalPrice { get; set; }

        [JsonProperty("order_detail_status")]
        public long OrderDetailStatus { get; set; }

        [JsonProperty("is_cook_on_table")]
        public long IsCookOnTable { get; set; }

        [JsonProperty("is_approved_drink")]
        public long IsApprovedDrink { get; set; }

        [JsonProperty("category_type")]
        public long CategoryType { get; set; }

        [JsonProperty("food_type")]
        public long FoodType { get; set; }

        [JsonProperty("cancel_reason")]
        public string CancelReason { get; set; }

        [JsonProperty("employee_id")]
        public long EmployeeId { get; set; }

        [JsonProperty("employee_name")]
        public string EmployeeName { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }
        [JsonProperty("normalize_name")]
        public string NormalizeName { get; set; }

        [JsonProperty("prefix")]
        public string Prefix { get; set; }
        [JsonProperty("history_log")]
        public string HistoryLog { get; set; }
        [JsonProperty("order_detail_status_name")]
        public string OrderDetailStatusName { get; set; }
        public string CreatedAtDay
        {
            get
            {
                //05/06/2019 09:06
                // DateTime date = DateTime.ParseExact(CreatedAt, "yyyy-MM-dd HH:mm", null);
                if (!string.IsNullOrEmpty(CreatedAt) && CreatedAt.Length > 6)
                {
                    string hour = CreatedAt.Substring(0, CreatedAt.Length - 6);
                    return hour;
                }
                else return "";
            }
            set
            {
                CreatedAtDay = value;
            }
        }
        public string CreatedAtHour
        {
            get
            {
                //05/06/2019 09:06
                if (!string.IsNullOrEmpty(CreatedAt) && CreatedAt.Length > 6)
                {
                    string hour = CreatedAt.Substring(CreatedAt.Length - 6, 6);
                    return hour;
                }
                else return "";

            }
            set
            {
                CreatedAtHour = value;
            }
        }
        public string UpdatedAtHour
        {
            get
            {
                if (!string.IsNullOrEmpty(UpdatedAt) && UpdatedAt.Length > 6)
                {
                    string hour = UpdatedAt.Substring(UpdatedAt.Length - 6, 6);
                    return hour;
                }
                else return "";
            }
            set
            {
                UpdatedAtHour = value;
            }
        }
        public string RefundQuantityDrink
        {
            get
            {
                return (TotalQuantityForDrink - Quantity).ToString();
            }
            set
            {
                RefundQuantityDrink = value;
            }
        }
        public string CurrentQuantity
        {
            get
            {
                return Quantity.ToString() + "/" + TotalQuantityForDrink.ToString();
            }
            set
            {
                CurrentQuantity = value;
            }
        }
        public string UnitPriceFormat
        {
            get
            {
                return string.Format("{0:0,0}", this.UnitPrice);
            }
            set
            {
                UnitPriceFormat = value;
            }
        }
        public string TotalPriceFormat
        {
            get
            {
                return string.Format("{0:0,0}", this.TotalPrice);
            }
            set
            {
                TotalPriceFormat = value;
            }
        }
        public string OrderDetailStatusString
        {
            get
            {
                if(CategoryType == (int)CategoryTypeEnum.DRINK || CategoryType == (int)CategoryTypeEnum.OTHER)
                {
                    if(OrderDetailStatus == (int)OrderDetailStatusEnum.PENDING)
                    {
                        return Helpers.MessageValue.MESSAGE_ORDER_DETAILS_DRINK_OPENING; 
                    }
                    else if (OrderDetailStatus == (int)OrderDetailStatusEnum.DONE && IsApprovedDrink == 0)
                    {
                        return Helpers.MessageValue.MESSAGE_ORDER_DETAILS_DRINK_COOKING;
                    }
                    else if (OrderDetailStatus == (int)OrderDetailStatusEnum.DONE && IsApprovedDrink == 1)
                    {
                        return Helpers.MessageValue.MESSAGE_ORDER_DETAILS_DONE;
                    }
                    else if (OrderDetailStatus == (int)OrderDetailStatusEnum.OUTSTOCK)
                    {
                        return Helpers.MessageValue.MESSAGE_ORDER_DETAILS_OUTSTOCK;
                    }

                    else if (OrderDetailStatus == (int)OrderDetailStatusEnum.CANCEL)
                    {
                        return Helpers.MessageValue.MESSAGE_ORDER_DETAILS_CANCEL;
                    }
                    else
                        return Helpers.MessageValue.MESSAGE_ORDER_DETAILS_OTHER;
                }
                else
                {
                    switch (this.OrderDetailStatus)
                    {
                        case (int)OrderDetailStatusEnum.PENDING:
                            return Helpers.MessageValue.MESSAGE_ORDER_DETAILS_OPENING;
                        case (int)OrderDetailStatusEnum.COOKING:
                            return Helpers.MessageValue.MESSAGE_ORDER_DETAILS_COOKING;
                        case (int)OrderDetailStatusEnum.DONE:
                            return Helpers.MessageValue.MESSAGE_ORDER_DETAILS_DONE;
                        case (int)OrderDetailStatusEnum.CANCEL:
                            return Helpers.MessageValue.MESSAGE_ORDER_DETAILS_CANCEL;
                        case (int)OrderDetailStatusEnum.OUTSTOCK:
                            return Helpers.MessageValue.MESSAGE_ORDER_DETAILS_OUTSTOCK;
                        default:
                            return Helpers.MessageValue.MESSAGE_ORDER_DETAILS_OTHER;
                    }
                }
                #region Phan Ha Command 
                //switch (this.OrderDetailStatus)
                //{
                //    case 0:
                //        return "Đang chờ";
                //    case 1:
                //        return "Đang nấu";
                //    case 2:
                //        return "Hoàn tất";
                //    case 3:
                //        return "Hết món";
                //    case 4:
                //        return "Đã huỷ";
                //    default:
                //        return "Unknow";
                //}
                #endregion 
            }
            set
            {
                OrderDetailStatusString = value;
            }
        }

    }
}
