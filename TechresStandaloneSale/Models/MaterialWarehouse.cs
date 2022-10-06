using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace TechresStandaloneSale.Models
{
    public class MaterialWarehouse
    {
        public long Id { get; set; }
        public long MaterialId { get; set; }
        public long ActionType { get; set; }
        public decimal Quantity { get; set; }
        public decimal SalePrice { get; set; }
        public string Note { get; set; }
        public decimal Price { get; set; }
        public string MaterialCode { get; set; }
        public string MaterialName { get; set; }
        public decimal InventoryQuantity { get; set; }
        public decimal InventoryQuantityUnitValue { get; set; }
        public decimal InventoryQuantityValue { get; set; }
        public decimal QuantityUnitValue { get; set; }
        public decimal QuantityValue { get; set; }
        public decimal DeficiencyQuantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal UnitPriceValue { get; set; }
        public string Prefix { get; set; }
        public string NormalizeName { get; set; }
        public string UnitName { get; set; }
        public long Status { get; set; }
        public string MaterialUnitValueName { get; set; }
        public decimal MaterialUnitValue { get; set; }
        public string InventoryQuantityString { get; set; }
        public BasicModel SelectedUnit { get; set; }
        public ObservableCollection<BasicModel> Units { get; set; }
        public long UnitIndex
        {
            get
            {
                if (SelectedUnit == null)
                {
                    return 0;
                }
                else
                {
                    BasicModel u = Units.Where(x => x.Value == SelectedUnit.Value).FirstOrDefault();
                    if (u != null)
                    {
                        return Units.IndexOf(u);
                    }
                    else
                    {
                        return 0;
                    }
                }

            }
            set
            {
                UnitIndex = value;
            }
        }
        public decimal AmountDifference
        {
            get
            {

                return Quantity - InventoryQuantity;

            }
            set
            {
                AmountDifference = value;
            }
        }

        public string QuantityString
        {
            get
            {
                return string.Format("{0} {1}", Quantity / MaterialUnitValue, UnitName);
            }
            set
            {
                QuantityString = value;
            }
        }
        public string UnitQuantity
        {
            get
            {
                return string.Format("{0} {1}", (long)(Quantity * MaterialUnitValue), MaterialUnitValueName);
            }
            set
            {
                UnitQuantity = value;
            }
        }
        public string InventoryUnitQuantity
        {
            get
            {
                return string.Format("{0} {1}", (InventoryQuantity * MaterialUnitValue), MaterialUnitValueName);
            }
            set
            {
                InventoryUnitQuantity = value;
            }
        }
        public string MaterialUnitPriceString
        {
            get
            {
                return Utils.Utils.FormatMoney( (long)(UnitPrice / MaterialUnitValue));
            }
            set
            {
                MaterialUnitPriceString = value;
            }
        }
        
       
        public decimal TotalPrice
        {
            get
            {
                if (Quantity > 0 && UnitPrice > 0)
                {
                    return this.Price * this.Quantity;
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                TotalPrice = value;
            }
        }
        public string TotalPriceString
        {
            get
            {
                if (Quantity > 0 && Price > 0)
                {
                    return Utils.Utils.FormatMoney( this.Price * this.Quantity);
                }
                else
                {
                    return "0";
                }

            }
            set
            {
                TotalPriceString = value;
            }
        }
        public string UnitPriceString
        {
            get
            {
                if (UnitPrice > 0)
                {
                    return Utils.Utils.FormatMoney(this.UnitPrice);
                }
                else
                {
                    return "0";
                }
            }
            set
            {
                UnitPriceString = value;
            }
        }
        public string SalePriceString
        {
            get
            {
                if (SalePrice > 0)
                {
                    return Utils.Utils.FormatMoney(this.SalePrice);
                }
                else
                {
                    return "0";
                }
            }
            set
            {
                SalePriceString = value;
            }
        }

    }
}
