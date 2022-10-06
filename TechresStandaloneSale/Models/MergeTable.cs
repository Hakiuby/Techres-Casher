using Newtonsoft.Json;
using System;
using System.Windows;
using System.Windows.Media.Imaging;
using TechresStandaloneSale.Helpers;

namespace TechresStandaloneSale.Models
{
    public class MergeTable
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("table_status")]
        public int TableStatus { get; set; }
        [JsonProperty("status")]
        public int Status { get; set; }
        [JsonProperty("slot_number")]
        public int SlotNumber { get; set; }

        [JsonProperty("isSelect")]
        public bool IsSelect { get; set; }
        public string TableNameString
        {
            get
            {
                if (!string.IsNullOrEmpty(Name))
                {
                    return Name.Length > 3 ? string.Format("{0}...", Name.Substring(0, 3).ToUpper()) : Name.ToUpper();
                }
                else
                {
                    return "";
                }

            }
            set
            {
                TableNameString = value;
            }
        }
        public BitmapImage LinkImage
        {
            get
            {
                if (Status == (int)TableStatusEnum.EMPTY || TableStatus == (int)TableStatusEnum.EMPTY)
                {
                    return new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/table_ic_closed.png", UriKind.RelativeOrAbsolute));
                }
                else if (Status == (int)(TableStatusEnum.USING) || Status == (int)(TableStatusEnum.MERGING)|| TableStatus == (int)(TableStatusEnum.USING) || TableStatus == (int)(TableStatusEnum.MERGING))
                {
                    return new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/table_ic_opening.png", UriKind.RelativeOrAbsolute));
                }
                else if (Status == (int)(TableStatusEnum.BOOKED)|| TableStatus == (int)(TableStatusEnum.BOOKED))
                {
                    return new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/table_ic_booking.png", UriKind.RelativeOrAbsolute));
                }
                else
                {
                    return new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/table_ic_closed.png"));
                }
            }
            set
            {
                LinkImage = value;
            }
        }
        public Visibility CheckVisibility
        {
            get
            {
                if (IsSelect)
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
                CheckVisibility = value;
            }
        }

    }
}
