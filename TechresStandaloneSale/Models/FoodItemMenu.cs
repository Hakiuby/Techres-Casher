using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace TechresStandaloneSale.Models
{
    public class FoodItemMenu
    {
        public long FoodId { get; set; }
        public BitmapImage ImageFood { get; set; }
        public long CategoryId { get; set; }
        public string PriceFood { get; set; }
        public decimal Price { get; set; }
        public bool IsGift { get; set; }
        public string Prefix { get; set; }
        public string NormalizeName { get; set; }
        public string Code { get; set; }
        public int CategoryTypeId { get; set; }
        public bool IsBbq { get; set; }
        public string ContentFoodName { get; set; }
        public string UnitType { get; set; }
        public bool IsAlowPrint { get; set; }

        public long IsSellByWeight { get; set; } 

        public string PriceFoodstring
        {
            get => Utils.Utils.FormatMoney(this.Price);
            set
            {
                PriceFoodstring = value;
            }
        }
        public bool isNote
        {
            get; set;
        }
        public List<AdditionFoodModel> AdditionFood { get; set; }
        public List<FoodInCombo> FoodInCombo { get; set; }
        public List<FoodInPromotionOneGetOne> FoodInPromotionBuyOneGetOne { get; set; }
        public FoodItemMenu(long foodId, BitmapImage imageFood, string priceFood, decimal foodPrice, bool isGift, string prefix, string normalizeName, int categoryTypeId, bool isBbq, string contentFoodName, long categoryId, string unitType, List<AdditionFoodModel> additionFood, bool isAlowPrint, string code, List<FoodInCombo> foodincombo, long isSellByWeight, List<FoodInPromotionOneGetOne> foodinpromotion)
        {
            FoodId = foodId;
            ImageFood = imageFood;
            PriceFood = priceFood;
            Price = foodPrice;
            IsGift = isGift;
            Prefix = prefix;
            NormalizeName = normalizeName;
            CategoryTypeId = categoryTypeId;
            IsBbq = isBbq;
            ContentFoodName = contentFoodName;
            CategoryId = categoryId;
            UnitType = unitType;
            AdditionFood = additionFood;
            IsAlowPrint = isAlowPrint;
            Code = code;
            FoodInCombo = foodincombo;
            IsSellByWeight = isSellByWeight;
            FoodInPromotionBuyOneGetOne = foodinpromotion;

        }
    }
}
