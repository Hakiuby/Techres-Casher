using Microsoft.Office.Interop.Excel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using TechresStandaloneSale.Helpers;

namespace TechresStandaloneSale.Models
{
    public class BillResponse
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("quantity")]
        public decimal Quantity { get; set; }

        [JsonProperty("note")]
        public string Note { get; set; }

        [JsonProperty("food_id")]
        public long FoodId { get; set; }
        [JsonProperty("food_prefix")]
        public string FoodPrefix { get; set; }
        [JsonProperty("food_normalize_name")]
        public string FoodNormalizeName { get; set; }
        [JsonProperty("prefix")]
        public string Prefix { get; set; }
        [JsonProperty("normalize_name")]
        public string NormalizeName { get; set; }

        [JsonProperty("food_name")]
        public string FoodName { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("food_unit")]
        public string FoodUnit { get; set; }

        [JsonProperty("unit_price")]
        public decimal UnitPrice { get; set; }

        [JsonProperty("unit_type")]
        public string UnitType { get; set; }

        [JsonProperty("total_price")]
        public decimal TotalPrice { get; set; }

        [JsonProperty("enable_return_beer")]
        public decimal EnableReturnBeer { get; set; }

        [JsonProperty("is_gift")]
        public int IsGift { get; set; }

        [JsonProperty("status")]
        public long Status { get; set; }

        [JsonProperty("order_detail_status")]
        public int OrderDetailStatus { get; set; }
        [JsonProperty("category_type")]
        public int CategoryType { get; set; }
        [JsonProperty("category_id")]
        public long CategoryId { get; set; }
        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("is_extra_charge")]
        public int IsExtraCharge { get; set; }

        [JsonProperty("is_allow_employee_gift")]
        public int IsAllowEmployeeGift { get; set; }

        [JsonProperty("isExtracCharge")]//toan
        public bool IsExtracCharge { get; set; }

        [JsonProperty("total_price_include_addition_foods")]
        public decimal TotalPriceInlcudeAdditionFoods { get; set; }

        [JsonProperty("order_detail_additions")]
        public List<BillResponse> OrderDetailAdditions { get; set; }
        [JsonProperty("order_detail_combo")]
        public List<BillResponse> OrderDetailCombo { get; set; }
        [JsonProperty("order_detail_restaurant_pc_foods")]
        public List<BillResponse> OrderDetailPromotion { get; set; }
        [JsonProperty("addition_foods")]
        public List<    BillResponse> OrderAdditions { get; set; }
        [JsonProperty("food_in_combo")]
        public List<BillResponse> FoodInCombo { get; set; }
        [JsonProperty("buy_one_get_one_foods")]
        public List<BillResponse> FoodInPrmotion { get; set; }

        [JsonProperty("is_cook_on_table")]
        public int IsCookOnTable { get; set; }

        [JsonProperty("food_type")]
        public long FoodType { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("is_selected")]
        public int IsSelectedAdditionFood { get; set; }
        [JsonIgnore]
        public decimal RealPrint { get; set; }
        [JsonIgnore]
        public bool IsPrint { get; set; }
        [JsonIgnore]
        public bool IsAlowPrint { get; set; }
        [JsonIgnore]
        public decimal OldQuantity { get; set; }
        [JsonIgnore]
        public decimal MoveQuantity { get; set; }

        [JsonIgnore]
        public decimal OnlyViewQuantity { get; set; }
        [JsonIgnore]
        public long OrderFoodId { get; set; }
        [JsonIgnore]
        public decimal AmountAddition { get; set; }

        [JsonProperty("is_approved_drink")]
        public long IsApprovedDrink { get; set; }

        [JsonProperty("restaurant_kitchen_place_id")]
        public long RestaurantKitchenPlaceId { get; set; }
        [JsonProperty("restaurant_extra_charge_id")]
        public long RestaurantExtraChargeId { get; set; }
        [JsonProperty("unit_purchase_point")]
        public long UnitPurchasePoint { get; set; }
        [JsonProperty("purchase_point")]
        public long? PurchasePoint { get; set; }
        [JsonProperty("is_purchase_by_point")]
        public long IsPurchaseByPoint { get; set; }

        [JsonProperty("vat_amount")]
        public decimal VatAmout { get; set; }

        [JsonProperty("is_sell_by_weight")] // Dat
        public long IsSellByWeight { get; set; }

        [JsonProperty("printed_quantity")] // Dat
        public decimal PrintedQuantity { get; set; }
        [JsonIgnore] // Dat
        public Visibility visibilityQuantity
        {
            get
            {
                if (IsSellByWeight == 1)
                    return Visibility.Visible;
                if (Status == 4)
                    return Visibility.Collapsed;
                else
                    return Visibility.Collapsed;
            }
        }

        [JsonIgnore] // Dat
        public Visibility visibilityQuantity1
        {
            get
            {
                if (IsSellByWeight == 0)
                    return Visibility.Visible;
                if (Status == 4)
                    return Visibility.Collapsed;
                else
                    return Visibility.Collapsed;
            }
        }

        [JsonIgnore] // Dat
        public Visibility VisibilityCancel
        {
            get
            {
                if (Status != 4)
                    return Visibility.Visible;
                else
                    return Visibility.Collapsed;
            }
        }

        [JsonIgnore] // Dat
        public Visibility VisibilityCancel1
        {
            get
            {
                if (Status == 4)
                    return Visibility.Visible;
                else
                    return Visibility.Collapsed;
            }
        }

        [JsonIgnore]
        public decimal TotalAmount
        {
            get
            {
                return this.AmountAddition + this.Amount;
            }
            set
            {
                TotalAmount = value;
            }
        }
        [JsonIgnore]
        public string TotalAmountString
        {
            get
            {
                if (this.IsGift != 1)
                {
                    return Utils.Utils.FormatMoney(this.AmountAddition + this.Amount);
                }
                else
                {
                    return "🎁";
                }
            }
            set
            {
                TotalAmountString = value;
            }
        }
        [JsonIgnore]
        public string TotalPriceInlcudeAdditionFoodsString
        {
            get
            {
                if (this.IsGift != 1)
                {
                    #region toan
                    if (this.IsPrint)
                    {
                        if (IsExtracCharge)
                        {
                            return Utils.Utils.FormatMoney(this.TotalPrice);
                        }
                        else
                        {
                            return Utils.Utils.FormatMoney(this.TotalPriceInlcudeAdditionFoods);
                        }
                    }
                    else
                    {
                        return Utils.Utils.FormatMoney(this.TotalPrice);
                    }
                    #endregion

                    //return Utils.Utils.FormatMoney(this.TotalPriceInlcudeAdditionFoods);
                    //return Utils.Utils.FormatMoney(this.Price);
                }
                else
                {
                    return "🎁";
                }
            }
            set
            {
                TotalPriceInlcudeAdditionFoodsString = value;
            }
        }
        [JsonIgnore]
        public Brush ForeGroundFoodName
        {
            get
            {
                return this.IsPrint ? new SolidColorBrush(Colors.Black) : new SolidColorBrush((Color)ColorConverter.ConvertFromString(Helpers.Constants.MAIN_COLOR_SYSTEM));
            }
            set
            {
                ForeGroundFoodName = value;
            }
        }
        [JsonIgnore]
        public decimal Amount
        {
            get
            {
                return this.Quantity * this.Price;
            }
            set
            {
                Amount = value;
            }
        }

        [JsonIgnore]
        public string AmountString
        {
            get
            {
                if (this.IsGift != 1)
                {
                    return Utils.Utils.FormatMoney(this.Amount);
                }
                else
                {
                    return "🎁";
                }
            }
            set
            {
                AmountString = value;
            }
        }

        [JsonIgnore]
        public Visibility IsCheckVisibility
        {
            get
            {
                if (IsPrint)
                    return Visibility.Hidden;
                else
                    return Visibility.Visible;
            }
            set
            {
                IsCheckVisibility = value;
            }
        }
        [JsonIgnore]
        public bool IsNote
        {
            get
            {
                if (Note != "" && Note != null)
                    return true;
                else
                    return false; 
            }
            set
            {
                //IsNote = value; 
            }
        }

        [JsonIgnore]
        public bool IsEnabledQuantity
        {
            get
            {
                if (IsPrint)
                    return false;
                else
                    return true;
            }
            set
            {
                IsEnabledQuantity = value;
            }
        }
        [JsonIgnore]
        public Visibility FoodQuantityVisibility
        {
            get
            {
                if (IsPrint)
                    return Visibility.Hidden;
                else
                    return Visibility.Visible;
            }
            set
            {
                FoodQuantityVisibility = value;
            }
        }
        [JsonIgnore]
        public Visibility FoodQuantityVisibilityOnlyView
        {
            get
            {
                if (IsPrint)
                    return Visibility.Visible;
                else
                    return Visibility.Hidden;
            }
            set
            {
                FoodQuantityVisibilityOnlyView = value;
            }
        }
        [JsonIgnore]
        public Visibility QuantityVisibilityOnlyView
        {
            get
            {
                if (IsPrint)
                    return Visibility.Visible;
                else
                    return Visibility.Hidden;
            }
            set
            {
                QuantityVisibilityOnlyView = value;
            }
        }
        #region Dat
        [JsonIgnore]
        public Visibility visibilityShowDetail
        {
            get
            {
                if (OrderDetailCombo != null && OrderDetailCombo.Count != 0 || OrderAdditions != null && OrderAdditions.Count != 0 || OrderDetailAdditions !=null && OrderDetailAdditions.Count != 0
                    || OrderDetailPromotion != null && OrderDetailPromotion.Count != 0)
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
                visibilityShowDetail = value;
            }
        }
        [JsonIgnore]
        public Visibility visibilityReturnBeer // Dat
        {
            get
            {
                if ((IsPrint == true && CategoryType == 2 && EnableReturnBeer == 1 && Status !=4)
                    || (IsPrint == true && CategoryType == 3 && EnableReturnBeer == 1 && Status != 4))
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
                visibilityShowDetail = value;
            }
        }
        [JsonIgnore]
        public Visibility visibilityDoneReturnBeer // Dat
        {
            get
            {
                if ((IsPrint == true && CategoryType == 2 && EnableReturnBeer == 0)
                    || (IsPrint == true && CategoryType == 3 && EnableReturnBeer == 0))
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
                visibilityShowDetail = value;
            }
        }
        #endregion
        [JsonIgnore]
        public Visibility GiftFoodVisibility
        {
            get
            {
               
                if (visibilityReturnBeer == Visibility.Visible) // Dat
                {
                    return Visibility.Collapsed;
                }
                if(IsAllowEmployeeGift == 0)
                {
                    return Visibility.Hidden;
                }    
                else if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.GIFT_FOOD), currentUser.Permissions)
                    || Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.OWNER), currentUser.Permissions))
                    return IsPrint ? Visibility.Hidden : Visibility.Visible;
                else
                    return Visibility.Hidden;
            }
            set
            {
                GiftFoodVisibility = value;
            }
        }
        [JsonIgnore]
        public User currentUser = (User)Utils.Utils.GetCacheValue(Helpers.Constants.CURRENT_USER);
        [JsonIgnore]
        public Visibility CancelFoodVisibility
        {
            get
            {
                if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.OWNER), currentUser.Permissions) && Status !=  4
                    || (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.CASHIER_ACCESS), currentUser.Permissions) && Status != 4)
                    ||  (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.CANCEL_COMPLETED_FOOD), currentUser.Permissions) && IsExtraCharge == 0 && Status != 4)
                    || (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.REMOVE_EXTRA_CHARGE_FROM_ORDER), currentUser.Permissions) && IsExtraCharge == 1 && Status != 4))
                {
                    return Visibility.Visible;
                }
                else
                {
                    if (!this.IsPrint && Status != 4)
                        return Visibility.Visible;
                    else
                        return Visibility.Hidden;
                }
            }
            set
            {
                CancelFoodVisibility = value;
            }
        }
        [JsonIgnore]
        public Visibility QuantityVisibility
        {
            get
            {
                if (IsSelectedAdditionFood == 1 && !IsPrint)
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Hidden;
                }
            }
            set
            {
                QuantityVisibility = value;
            }
        }
        [JsonIgnore]
        public string FoodNamePromotion
        {
            get
            {
                if (!string.IsNullOrEmpty(FoodName))
                    return string.Format("+{0} {1} (🎁)", Quantity, FoodName);
                else
                {
                    if(this.IsGift == 1)
                    {
                        return string.Format("+{0} x {1} (🎁)", Name, Quantity);
                    }
                    else
                    {
                        return string.Format("{0} (🎁)", Name);
                    }
                }
                    //return string.Format("{0}", Name);
            }
        }
        [JsonIgnore]
        public string FoodNameAddition
        {
            get
            {
                if (!string.IsNullOrEmpty(FoodName))
                    return string.Format("{0}\n({1})", FoodName, UnitPriceFormat);
                else
                    return string.Format("{0}\n({1})", Name, UnitPriceFormat);
            }
            set
            {
                FoodNameAddition = value;
            }
        }
        [JsonIgnore]
        public string FoodNameCombo
        {
            get
            {
                if (!string.IsNullOrEmpty(FoodName))
                {
                    return string.Format("+ {0} x {1}", FoodName, Quantity);
                }
                else
                {
                    if(Quantity != 0)
                    {
                        return string.Format("+ {0} x {1}", Name, Quantity);
                    }
                    else
                    {
                        return string.Format("+ {0}", Name);
                    }
                    //return string.Format("+ {0}", Name);
                }
            }
            set
            {
                FoodNameCombo = value;
            }
        }
        [JsonIgnore]
        public string FoodNameValue
        {
            get
            {
                return string.Format(" + {0}  {1}", Quantity, Name); ;
            }
            set
            {
                FoodNameValue = value;
            }
        }

        [JsonIgnore]
        public string FoodNameCustom
        {
            get
            {
                if (string.IsNullOrEmpty(FoodUnit))
                    return Name;
                if (!string.IsNullOrEmpty(Name))
                    return string.Format("{0} \n ({1}/{2})", FoodName, Utils.Utils.FormatMoney(this.UnitPrice), FoodUnit);
                else
                    return string.Format("{0} \n ({1}/{2})", Name, Utils.Utils.FormatMoney(this.UnitPrice), FoodUnit);
            }
            set
            {
                FoodNameCustom = value;
            }
        }
        [JsonIgnore]
        public string FoodNameAdditionPrintBill
        {
            get
            {
                if (!string.IsNullOrEmpty(FoodName))
                    return string.Format("  + {0} {1} ({2}/{3})", Quantity, FoodName, UnitPriceFormat, UnitType);
                else
                    return string.Format(" + {0} {1} ({2}/{3})", Quantity, Name, UnitPriceFormat, UnitType);
            }
            set
            {
                FoodNameAdditionPrintBill = value;
            }
        }
        [JsonIgnore]
        public string FoodNameAdditionPrintBill1
        {
            get
            {
                if (!string.IsNullOrEmpty(FoodName))
                    return string.Format("  + {0} {1} ({2})", Quantity, FoodName, UnitPriceFormat);
                else
                    return string.Format(" + {0} {1} ({2})", Quantity, Name, UnitPriceFormat);
            }
            set
            {
                FoodNameAdditionPrintBill = value;
            }
        }
        [JsonIgnore]
        public string UnitPriceFormat
        {
            get
            {
                if (this.IsPurchaseByPoint == 1)
                {
                    return string.Format("{0} điểm", this.PurchasePoint.GetValueOrDefault());
                }
                else
                {
                    return Utils.Utils.FormatMoney(this.Price);
                }
            }
            set
            {
                UnitPriceFormat = value;
            }
        }
        [JsonIgnore]
        public string TotalPriceFormat
        {
            get
            {
                if (this.IsGift == 1) return string.Format(" 🎁 {0}", Utils.Utils.FormatMoney(this.TotalPrice));
                else return Utils.Utils.FormatMoney(this.TotalPrice);
            }
            set
            {
                TotalPriceFormat = value;
            }
        }


        [JsonIgnore]
        public string OrderDetailStatusString
        {
            get
            {
                if (CategoryType == (int)CategoryTypeEnum.DRINK || CategoryType == (int)CategoryTypeEnum.OTHER)
                {
                    if (Status == (int)OrderDetailStatusEnum.PENDING)
                    {
                        return Helpers.MessageValue.MESSAGE_ORDER_DETAILS_DRINK_OPENING;
                    }
                    else if (Status == (int)OrderDetailStatusEnum.DONE && EnableReturnBeer == 1)
                    {
                        return Helpers.MessageValue.MESSAGE_ORDER_DETAILS_DRINK_COOKING;
                    }
                    else if (Status == (int)OrderDetailStatusEnum.DONE && EnableReturnBeer == 0)
                    {
                        return Helpers.MessageValue.MESSAGE_ORDER_DETAILS_DONE;
                    }
                    else if (Status == (int)OrderDetailStatusEnum.OUTSTOCK)
                    {
                        return Helpers.MessageValue.MESSAGE_ORDER_DETAILS_OUTSTOCK;
                    }

                    else if (Status == (int)OrderDetailStatusEnum.CANCEL)
                    {
                        return Helpers.MessageValue.MESSAGE_ORDER_DETAILS_CANCEL;
                    }
                    else
                        return Helpers.MessageValue.MESSAGE_ORDER_DETAILS_OTHER;
                }
                else
                {
                    switch (this.Status)
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
            }
            set
            {
                OrderDetailStatusString = value;
            }
        }
        [JsonIgnore]
        public string ColorStatus
        {
            get
            {
                if (CategoryType == (int)CategoryTypeEnum.DRINK || CategoryType == (int)CategoryTypeEnum.OTHER)
                {
                    if (Status == (int)OrderDetailStatusEnum.PENDING)
                    {
                        return Helpers.Constants.MAIN_COLOR_SYSTEM;
                    }
                    else if (Status == (int)OrderDetailStatusEnum.DONE && EnableReturnBeer == 1)
                    {
                        return Helpers.Constants.BLUE_GG_COLOR;
                    }
                    else if (Status == (int)OrderDetailStatusEnum.DONE && EnableReturnBeer == 0)
                    {
                        return Helpers.Constants.GREEN_GG_COLOR;
                    }
                    else if (Status == (int)OrderDetailStatusEnum.OUTSTOCK || Status == (int)OrderDetailStatusEnum.CANCEL)
                    {
                        return Helpers.Constants.RED_GG_COLOR;
                    }
                    else
                        return Helpers.Constants.MAIN_COLOR_SYSTEM;
                }
                else
                {
                    switch (this.Status)
                    {
                        case (int)OrderDetailStatusEnum.PENDING:
                            return Helpers.Constants.MAIN_COLOR_SYSTEM;
                        case (int)OrderDetailStatusEnum.COOKING:
                            return Helpers.Constants.BLUE_GG_COLOR;
                        case (int)OrderDetailStatusEnum.DONE:
                            return Helpers.Constants.GREEN_GG_COLOR;
                        case (int)OrderDetailStatusEnum.CANCEL:
                            return Helpers.Constants.RED_GG_COLOR;
                        case (int)OrderDetailStatusEnum.OUTSTOCK:
                            return Helpers.Constants.RED_GG_COLOR;
                        default:
                            return Helpers.Constants.MAIN_COLOR_SYSTEM;
                    }
                }
            }
            set
            {
                ColorStatus = value;
            }
        }

        [JsonIgnore]
        public string ColorStatusLv2
        {
            get
            {
                if (CategoryType == (int)CategoryTypeEnum.DRINK || CategoryType == (int)CategoryTypeEnum.OTHER)
                {
                    if (Status == (int)OrderDetailStatusEnum.PENDING)
                    {
                        return Helpers.Constants.MAIN_COLOR_SYSTEM;
                    }
                    else if (Status == (int)OrderDetailStatusEnum.DONE)
                    {
                        return Helpers.Constants.GREEN_GG_COLOR;
                    }
                    else if (Status == (int)OrderDetailStatusEnum.OUTSTOCK || Status == (int)OrderDetailStatusEnum.CANCEL)
                    {
                        return Helpers.Constants.RED_GG_COLOR;
                    }
                    else
                        return Helpers.Constants.MAIN_COLOR_SYSTEM;
                }
                else
                {
                    switch (this.Status)
                    {
                        case (int)OrderDetailStatusEnum.PENDING:
                            return Helpers.Constants.MAIN_COLOR_SYSTEM;
                        case (int)OrderDetailStatusEnum.DONE:
                            return Helpers.Constants.GREEN_GG_COLOR;
                        case (int)OrderDetailStatusEnum.CANCEL:
                            return Helpers.Constants.RED_GG_COLOR;
                        case (int)OrderDetailStatusEnum.OUTSTOCK:
                            return Helpers.Constants.RED_GG_COLOR;
                        default:
                            return Helpers.Constants.MAIN_COLOR_SYSTEM;
                    }
                }
            }
            set
            {
                ColorStatus = value;
            }
        }

        [JsonIgnore]
        public string PurchasePointString
        {
            get
            {
                return string.Format("{0} điểm", this.PurchasePoint.GetValueOrDefault());
            }
            set
            {
                PurchasePointString = value;
            }
        }
        public Visibility PurchasePointVisibility
        {
            get
            {
                if(this.PurchasePoint.GetValueOrDefault() > 0)
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
                PurchasePointVisibility = value;
            }
        }
    }
    public class FoodAddition
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("price")]
        public decimal Price { get; set; }
        [JsonProperty("vat_percent")]
        public long VatPercent { get; set; }
        [JsonProperty("quantity")]
        public long Quantity { get; set; }
        [JsonProperty("is_selected")]
        public int IsSelectedAdditionFood { get; set; }
    }
}
