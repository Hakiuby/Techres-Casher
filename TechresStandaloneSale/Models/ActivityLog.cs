using Newtonsoft.Json;
using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TechresStandaloneSale.Helpers;

namespace TechresStandaloneSale.Models
{
    public class ActivityLog
    {

        [JsonProperty("restaurant_id")]
        public long RestaurantId { get; set; }

        [JsonProperty("branch_id")]
        public long BranchId { get; set; }

        [JsonProperty("employee_id")]
        public long EmployeeId { get; set; }
        [JsonProperty("employee_name")]
        public string EmployeeName { get; set; }

        [JsonProperty("object_id")]
        public long ObjectId { get; set; }

        [JsonProperty("log_type")]
        public string LogType { get; set; }

        [JsonProperty("log_type_number")]
        public int LogTypeNumber { get; set; }

        [JsonProperty("action_type")]
        public string ActionType { get; set; }

        [JsonProperty("action_detail")]
        public string ActionDetail { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }
        [JsonProperty("is_viewed")]
        public int IsViewed { get; set; }

        public BitmapImage Avatar
        {
            get
            {
                 switch (LogTypeNumber)
                    {
                     
                    case 1:
                        return new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/icon-create-order.png", UriKind.RelativeOrAbsolute));
                    case 2:
                        return new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/icon_gitf.png", UriKind.RelativeOrAbsolute));
                    case 3:
                        return new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/request-payment.png", UriKind.RelativeOrAbsolute));
                    case 4:
                        return new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/table_ic_closed.png", UriKind.RelativeOrAbsolute));
                    case 5:
                        return new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/table_ic_closed.png", UriKind.RelativeOrAbsolute));
                    case 6:
                        return new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/table_ic_closed.png", UriKind.RelativeOrAbsolute));
                    case 7:
                        return new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/table_ic_closed.png", UriKind.RelativeOrAbsolute));
                    case 8:
                        return new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/ic-vat.png", UriKind.RelativeOrAbsolute));
                    default:
                            return new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/user_name.png", UriKind.RelativeOrAbsolute));
                    }
             
            }
            set
            {
                Avatar = value;
            }
        }
        public Brush ItemBackground
        {
            get
            {
                BrushConverter bc = new BrushConverter();
                if (IsViewed == 1)
                {
                    return new SolidColorBrush(Colors.White);
                }
                else 
                {
                    return (Brush)bc.ConvertFrom(Constants.GRAY_COLOR);
                }

            }
            set
            {
                ItemBackground = value;
            }
        }

        }
}
