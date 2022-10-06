using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TechresStandaloneSale.Models
{
    public class Branch
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("status")]
        public long Status { get; set; }

        [JsonProperty("is_use_fingerprint")]
        public long IsUseFingerprint { get; set; }

        [JsonProperty("enable_checkin")]
        public long EnableCheckin { get; set; }

        [JsonProperty("qr_code_checkin")]
        public string QrCodeCheckin { get; set; }
        [JsonProperty("image_logo")]
        public string ImageLogo { get; set; }
        [JsonProperty("banner")]
        public string Banner { get; set; }

        [JsonProperty("is_enable_membership_card")]
        public int IsEnableMembershipCard { get; set; }

        [JsonProperty("is_enable_booking")]
        public int IsEnableBooking { get; set; }

        [JsonProperty("is_have_take_away")]
        public int IsHaveTakeAway { get; set; }

        [JsonProperty("maximum_promotion_point_allow_use_in_each_bill")]
        public long MaximumPromotionPointAllowUseInEachBill { get; set; }

        [JsonProperty("employee_manager_full_name")]
        public string EmployeeManagerFullName { get; set; }

        [JsonProperty("employee_id")]
        public long EmployeeId { get; set; }
        [JsonProperty("list_kitchen_place")]
        public List<Kitchen> RestaurantKitchenPlaces { get; set; }

        [JsonProperty("restaurant_kitchen_place")]
        public Kitchen KitchenPlace { get; set; }


        [JsonIgnore]
        public bool IsCheck { get; set; }
        public ObservableCollection<Kitchen> KitchenList
        {
            get
            {
                ObservableCollection<Kitchen> kitchens = new ObservableCollection<Kitchen>();
               if (this.RestaurantKitchenPlaces != null && this.RestaurantKitchenPlaces.Count > 0)
                {
                    this.RestaurantKitchenPlaces.ForEach(kitchens.Add);
                   // this.KitchenItem = KitchenList[0];
                }
                return kitchens;
            }
            set
            {
                KitchenList = value;
            }
        }
        public Kitchen kitchen = new Kitchen();
        public Kitchen KitchenItem {
            get
            {
                return kitchen;
            }
            set
            {
                kitchen = value;
                OnPropertyChanged("KitchenItem");
            }
        }
        public override string ToString()
        {
            return Name;
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
