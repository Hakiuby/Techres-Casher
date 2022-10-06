using Newtonsoft.Json;
using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TechresStandaloneSale.Helpers;
using TechresStandaloneSale.Models.Response;

namespace TechresStandaloneSale.Models
{
    public class Area
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("branch_id")]
        public long BranchId { get; set; }

        [JsonProperty("is_take_away")]
        public long IsTakeAway { get; set; }

        [JsonProperty("active_count")]
        public long ActiveCount { get; set; }

        [JsonProperty("deactive_count")]
        public long DeactiveCount { get; set; }

        [JsonProperty("table_count")]
        public long TotalCount { get; set; }
        [JsonProperty("total_table")]
        public long TotalTable { get; set; }

        [JsonProperty("employee_manager_full_name")]
        public string EmployeeManagerFullName { get; set; }
        public bool isSelected { get; set; }

        public bool isChoose { get; set; }
        public string AreaString
        {
            get
            {
                return string.Format("{0} ({1})", Name, TotalTable);
            }
            set
            {
                AreaString = value;
            }
        }
        public BitmapImage ImageCheck
        {
            get
            {
                if (isChoose)
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
        public Brush ForegroundItem
        {
            get
            {
                if (isChoose)
                {
                    return new SolidColorBrush(Colors.White);
                }
                else
                {
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.MAIN_COLOR_SYSTEM));
                }

            }
            set
            {
                ForegroundItem = value;
            }
        }

        public Area(int id=0, string name="", long branchid=0,int status=0,bool isSelected = false)
        {
            this.Id = id;
            this.Name = name;
            this.Status = status;
            this.BranchId = branchid;
            this.isSelected = isSelected;
        }

        public override string ToString()
        {
            return Name;
        }
    }

}
