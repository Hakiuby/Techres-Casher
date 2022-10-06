using Newtonsoft.Json;
using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TechresStandaloneSale.Helpers;

namespace TechresStandaloneSale.Models
{
    public class Category
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("status")]
        public long Status { get; set; }
        [JsonProperty("category_type")]
        public int CategoryType { get; set; }
        public bool IsChoose { get; set; }
        public BitmapImage ImageCheck
        {
            get
            {
                if (IsChoose)
                {
                    return new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/dot-active.png"));
                }

                else
                {
                    return new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/dot-inactive.png"));
                }
            }
            set
            {
                ImageCheck = value;
            }
        }
        
        public string TypeString
        {
            get
            {
                return Type(this.CategoryType);
            }
            set
            {
                TypeString = value;
            }
        }
        public string Type(int type)
        {
            switch (type)
            {
                case 1:
                    return ("Món ăn");
                case 2:
                    return ("Món uống");
                case 3:
                    return ("Món khác");
                case 4:
                    return ("Hải sản");
                default:
                    return ("Không biết");
            }
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
