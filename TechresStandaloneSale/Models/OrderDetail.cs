using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Windows;
using TechresStandaloneSale.Helpers;

namespace TechresStandaloneSale.Models
{
    public class OrderDetail
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
        [JsonProperty("normalize_name")]
        public string NormalizeName { get; set; }
        [JsonProperty("prefix")]
        public string Prefix { get; set; }
        [JsonProperty("unit_price")]
        public decimal UnitPrice { get; set; }
        [JsonProperty("total_quantity_for_drink")]
        public decimal TotalQuantityForDrink { get; set; }
        [JsonProperty("total_price")]
        public decimal TotalPrice { get; set; }
        [JsonProperty("order_detail_status")]
        public long OrderDetailStatus { get; set; }
        [JsonProperty("order_detail_status_name")]
        public string OrderDetailStatusName { get; set; }
        [JsonProperty("is_approved_drink")]
        public long IsApprovedDrink { get; set; }
        [JsonProperty("category_type")]
        public long CategoryType { get; set; }
        [JsonProperty("category_id")]
        public long CategoryId { get; set; }
        [JsonProperty("food_type")]
        public long FoodType { get; set; }
        [JsonProperty("employee_name")]
        public string EmployeeName { get; set; }
        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }
        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }
        [JsonProperty("completed_at")]
        public string CompletedAt { get; set; }
        [JsonProperty("is_gift")]
        public bool IsGift { get; set; }
        [JsonProperty("is_bbq")]
        public bool isBBQ { get; set; }
        [JsonProperty("old_quantity")]
        public decimal OldQuantity { get; set; }
        [JsonProperty("printed_quantity")]
        public decimal PrintQuantity { get; set; }
        [JsonProperty("is_take_away")]
        public int  IsTakeAway { get; set; }
        [JsonProperty("is_online")]
        public int IsOnline { get; set; }
        [JsonProperty("order_detail_combo_parent_id")]
        public long OrderDetailComboParentId { get; set; }
        [JsonProperty("order_detail_additions")]
        public List<OrderDetailAddition> OrderDetailAdditions { get; set; }
        public bool IsLoad { get; set; }
        public Visibility BtnVisibility
        {
            get
            {
                if (IsLoad)
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
                BtnVisibility = value;
            }
        }
        public string FoodNameString
        {
            get
            {
                if (OrderDetailComboParentId>0)
                {
                    return string.Format("{0} - {1}", FoodName, MessageValue.MESSAGE_FROM_COMBO);
                }
                else
                {
                    return FoodName;
                }
            }
            set
            {
                FoodNameString = value;
            }
        }
        public Visibility BtnCancelVisibility
        {
            get
            {
                 if (OrderDetailComboParentId>0)
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
        public Visibility UpdateTimeVisibility
        {
            get
            {
                if (OrderDetailStatus == (int)OrderDetailStatusEnum.CANCEL || OrderDetailStatus == (int)OrderDetailStatusEnum.OUTSTOCK)
                {
                    return Visibility.Visible;
                }
                else if (OrderDetailStatus == (int)OrderDetailStatusEnum.DONE && IsApprovedDrink == Constants.STATUS_IS_APPROVED_LONG)
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
                UpdateTimeVisibility = value;
            }
        }
        public string CreateAtCheckOrderDetailStatus
        {
            get
            {
                if (OrderDetailStatus == (int)OrderDetailStatusEnum.CANCEL || OrderDetailStatus == (int)OrderDetailStatusEnum.OUTSTOCK)
                {
                    if (!string.IsNullOrEmpty(CreatedAt))
                    {
                        DateTime dateTime = Utils.Utils.GetStringFormatDateTimeHour(CreatedAt);
                        if (dateTime != null)
                        {
                            return Utils.Utils.GetHourFormatVN(dateTime);
                        }
                        else
                        {
                            return "";
                        }
                    }
                    else return "";

                }
                else
                {
                    if (!string.IsNullOrEmpty(CreatedAt))
                    {
                        DateTime dateTime = Utils.Utils.GetStringFormatDateTimeHour(CreatedAt);
                        if (dateTime != null)
                        {
                            return Utils.Utils.GetHourFormatVN(dateTime);
                        }
                        else
                        {
                            return "";
                        }
                    }
                    else return "";
                }

            }
            set
            {
                CreateAtCheckOrderDetailStatus = value;
            }
        }
        public string CreatedAtDay
        {
            get
            {
                //05/06/2019 09:06
                // DateTime date = DateTime.ParseExact(CreatedAt, "yyy-MM-dd HH:mm", null);
                if (!string.IsNullOrEmpty(CreatedAt))
                {
                    DateTime dateTime = Utils.Utils.GetStringFormatDateTimeHour(CreatedAt);
                    if (dateTime != null)
                    {
                        return Utils.Utils.GetDateFormatVN(dateTime);
                    }
                    else
                    {
                        return "";
                    }
                }
                else return "";
            }
            set
            {
                CreatedAtDay = value;
            }
        }
        public string CompletedAtHour
        {
            get
            {
                if (!string.IsNullOrEmpty(CompletedAt))
                {
                    DateTime dateTime = Utils.Utils.GetStringFormatDateTime(CompletedAt);
                    if (dateTime != null)
                    {
                        return Utils.Utils.GetHourFormatVN(dateTime);
                    }
                    else
                    {
                        return "";
                    }
                }
                else return "";
            }
            set
            {
                CompletedAtHour = value;
            }
        }
        public string CreatedAtHour
        {
            get
            {
                //05/06/2019 09:06
                if (!string.IsNullOrEmpty(CreatedAt))
                {
                    DateTime dateTime = Utils.Utils.GetStringFormatDateTimeHour(CreatedAt);
                    if (dateTime != null)
                    {
                        return Utils.Utils.GetHourFormatVN(dateTime);
                    }
                    else
                    {
                        return "";
                    }
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
                if (!string.IsNullOrEmpty(UpdatedAt))
                {
                    DateTime dateTime = Utils.Utils.GetStringFormatDateTimeHour(UpdatedAt);
                    if (dateTime != null)
                    {
                        return Utils.Utils.GetHourFormatVN(dateTime);
                    }
                    else
                    {
                        return "";
                    }
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
        public bool ShowCancelButton
        {
            get
            {
                return FoodType == 0 ? true : false;
            }
            set
            {
                ShowCancelButton = value;
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
                return Utils.Utils.FormatMoney( this.UnitPrice);
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
                if (IsGift) return "🎁";
                else return Utils.Utils.FormatMoney( this.TotalPrice);
            }
            set
            {
                TotalPriceFormat = value;
            }
        }
        #region By Phan Viet Ha 
        private long _fonsize; 
        public long fonsize
        {
            get => _fonsize; 
            set
            {
                _fonsize = value;
            }
        }
        
        public OrderDetail()
        {
            if(SystemParameters.MaximizedPrimaryScreenWidth >= 1920)
            {
                fonsize = 17; 
            }
            else if (SystemParameters.MaximizedPrimaryScreenWidth >= 1366)
            {
                fonsize = 13; 
            }
            else
            {
                fonsize = 11; 
            }
        }

        #endregion
    }
}
