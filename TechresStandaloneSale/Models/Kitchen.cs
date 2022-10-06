using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using TechresStandaloneSale.Helpers;

namespace TechresStandaloneSale.Models
{
    public class Kitchen
    {
        [JsonIgnore]
        public bool IsChoose { get; set; }
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("status")]
        public long Status { get; set; }

        [JsonProperty("restaurant_id")]
        public long RestaurantId { get; set; }

        [JsonProperty("branch_id")]
        public long BranchId { get; set; }

        [JsonProperty("branch_name")]
        public string BranchName { get; set; }

        [JsonProperty("printer_name")]
        public string PrinterName { get; set; }

        [JsonProperty("printer_ip_address")]
        public string PrinterIpAddress { get; set; }

        [JsonProperty("printer_paper_size")]
        public long PrinterPaperSize { get; set; }

        [JsonProperty("is_have_printer")]
        public bool IsHavePrinter { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }
        [JsonIgnore]
        public BasicModel PrinterPaperSizeItem { get; set; }
        [JsonIgnore]
        public Printer PrinterNameItem { get; set; }
        [JsonIgnore]
        public BitmapImage ImageCheck
        {
            get
            {
                if (IsChoose)
                {
                    return new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/icon-checked.png"));
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
        [JsonIgnore]
        public Visibility IsHavePrinterVisibility
        {
            get
            {
                if (IsHavePrinter)
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
                IsHavePrinterVisibility = value;
            }
        }
    }
}
