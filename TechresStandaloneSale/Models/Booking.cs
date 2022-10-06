using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using TechresStandaloneSale.Helpers;

namespace TechresStandaloneSale.Models
{
    public class Booking
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("branch")]
        public Branch Branch { get; set; }

        [JsonProperty("order_id")]
        public long OrderId { get; set; }

        [JsonProperty("customer_id")]
        public long CustomerId { get; set; }

        [JsonProperty("customer_first_name")]
        public string CustomerFirstName { get; set; }

        [JsonProperty("customer_last_name")]
        public string CustomerLastName { get; set; }

        [JsonProperty("customer_name")]
        public string CustomerName { get; set; }

        [JsonProperty("customer_phone")]
        public string CustomerPhone { get; set; }

        [JsonProperty("booking_status")]
        public long BookingStatus { get; set; }

        [JsonProperty("booking_status_name")]
        public string BookingStatusName { get; set; }

        [JsonProperty("orther_requirements")]
        public string OrtherRequirements { get; set; }

        [JsonProperty("note")]
        public string Note { get; set; }

        [JsonProperty("deposit_amount")]
        public decimal DepositAmount { get; set; }

        [JsonProperty("deposit_employee_id")]
        public long DepositEmployeeId { get; set; }
        [JsonProperty("is_deposit_confirmed")]
        public int IsDepositConfirmed { get; set; }

        [JsonProperty("deposit_employee_name")]
        public string DepositEmployeeName { get; set; }

        [JsonProperty("receive_deposit_time")]
        public string ReceiveDepositTime { get; set; }

        [JsonProperty("return_deposit_amount")]
        public decimal ReturnDepositAmount { get; set; }

        [JsonProperty("return_deposit_time")]
        public string ReturnDepositTime { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("number_slot")]
        public long NumberSlot { get; set; }

        [JsonProperty("foods")]
        public List<FoodRequest> Foods { get; set; }

        [JsonProperty("tables")]
        public List<Table> Tables { get; set; }

        [JsonProperty("booking_time")]
        public string BookingTime { get; set; }

        [JsonProperty("booking_type")]
        public long BookingType { get; set; }

        [JsonProperty("booking_type_name")]
        public string BookingTypeName { get; set; }

        [JsonProperty("employee")]
        public Employee Employee { get; set; }

        [JsonProperty("employee_create")]
        public Employee EmployeeCreate { get; set; }

        [JsonProperty("total_amount")]
        public decimal TotalAmount { get; set; }

        [JsonProperty("deposit_payment_method")]
        public long DepositPaymentMethod { get; set; }

        [JsonProperty("return_deposit_payment_method")]
        public long ReturnDepositPaymentMethod { get; set; }

        [JsonProperty("area_id")]
        public long AreaId { get; set; }

        [JsonProperty("area_name")]
        public string AreaName { get; set; }

        [JsonProperty("cancel_reason")]
        public string CancelReason { get; set; }
        public string BookingCode
        {
            get
            {
                return string.Format("#{0}", this.Id);
            }
            set
            {
                BookingCode = value;
            }
        }
      
        public string TableString
        {
            get
            {
                string tableString = "";
                if (this.Tables != null && this.Tables.Count > 0)
                {
                    foreach (Table t in this.Tables)
                    {
                        tableString += string.Format("- {0} - {1} \n ", t.Name, t.AreaName);
                    }
                }
                return tableString;
            }
            set
            {
                TableString = value;
            }
        }
        public string TableFormatString
        {
            get
            {
                string tableString = "";

                if (this.Tables != null && this.Tables.Count > 0)
                {
                    tableString += string.Join(", ", this.Tables);                                      
                }
                return tableString;
            }
            set
            {
                TableFormatString = value;
            }
        }

        public string FoodString
        {
            get
            {
                string foodString = "";
                if (this.Foods != null && this.Foods.Count > 0)
                {
                    foreach (FoodRequest t in this.Foods)
                    {
                        foodString += string.Format("- {0}({1}) \n ", t.Name, t.Quantity);
                    }
                }
                return foodString;
            }
            set
            {
                FoodString = value;
            }
        }
        public string DepositString
        {
            get
            {
                return Utils.Utils.FormatMoney(this.DepositAmount);
            }
            set
            {
                DepositString = value;
            }
        }
        public string ReturnDepositString
        {
            get
            {
                return Utils.Utils.FormatMoney(this.ReturnDepositAmount);
            }
            set
            {
                ReturnDepositString = value;
            }
        }

        public string TotalAmountString
        {
            get
            {
                return Utils.Utils.FormatMoney(this.TotalAmount);
            }
            set
            {
                TotalAmountString = value;
            }
        }

        public string BookingString
        {
            get
            {
                return string.Format("#{0} - {1} -{2}", this.Id, this.TableFormatString, this.CustomerName);
            }
            set
            {
                BookingString = value;
            }
        }
        public Visibility BookingVisibility
        {
            get
            {
                if(BookingStatus == 1 || BookingStatus == 7 || BookingStatus == 2)
                {
                    return Visibility.Visible; 
                }
                else
                {
                    return Visibility.Hidden; 
                }
            //    if(BookingStatus == 5 || BookingStatus == 8 || BookingStatus == 3)
            //    {
            //        return Visibility.Hidden;
            //    }
            //    else 
            //    {
            //        return Visibility.Visible;

            //    }

            }
            set
            {
                BookingVisibility = value;
            }
        }

        public Visibility EditBookingVisibility
        {
            get
            {
                if(BookingStatus == 1 || BookingStatus == 7 || BookingStatus == 2 || BookingStatus == 9)
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
                EditBookingVisibility = value;
            }
        }
        public Visibility CancelBookinngVisibility
        {
            get
            {
                if(BookingStatus == 1)
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
                CancelBookinngVisibility = value; 
            }
            
        }
    
        public Visibility ConfirmVisibility
        {
            get
            {
                if (BookingStatus == (int)BookingStatusEnum.WAITING_COMPLETE || BookingStatus == (int)BookingStatusEnum.COMPLETED || BookingStatus == (int)BookingStatusEnum.CANCEL)
                {
                    return Visibility.Hidden;
                }
                else
                {
                    return Visibility.Visible;
                }
            }
            set
            {
                ConfirmVisibility = value;
            }
        }
        public string IsDepositConfirmedString 
        {
            get
            {
                if (IsDepositConfirmed == 1) return MessageValue.MESSAGE_CASHIER_MAIN_HOME_CONFIRMED_COST;
                else return MessageValue.MESSAGE_CASHIER_MAIN_HOME_NONCONFIRMED_COST;
            }
            set
            {
                IsDepositConfirmedString = value;
            }
        }
        public Visibility EditDepositBookingVisibility
        {
            get
            {
                //if (IsDepositConfirmed == (int)Constants.NOT_STATUS )
                //{
                //    return Visibility.Visible;
                //}
                //else
                //{
                //    return Visibility.Collapsed;
                //}
                #region Dat
                if(BookingStatus == 1 && IsDepositConfirmed == (int)Constants.NOT_STATUS || BookingStatus == 7 && IsDepositConfirmed == (int)Constants.NOT_STATUS)
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Collapsed;
                }
                #endregion
            }
            set
            {
                EditDepositBookingVisibility = value;
            }
        }

        public Visibility ConfirmDeposit
        {
            get
            {
                if(BookingStatus == 8 || BookingStatus == 5 || BookingStatus == 3 || BookingStatus == 4)
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
                ConfirmDeposit = value; 
            }
        }
        public Visibility ConfirmDepositVisibility
        {
            get
            {
                //if (IsDepositConfirmed == (int)Constants.STATUS)
                //{
                //    return Visibility.Collapsed;
                //}
                //else
                //{
                //    return Visibility.Visible;
                //}
                #region Dat
                if (BookingStatus == 1 && IsDepositConfirmed == (int)Constants.NOT_STATUS || BookingStatus == 7 && IsDepositConfirmed == (int)Constants.NOT_STATUS)
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Collapsed;
                }
                #endregion
            }
            set
            {
                ConfirmDepositVisibility = value;
            }
        }
        public string StatusName
        {
            get
            {
                if (BookingStatus == 1)
                {
                    return "Chờ xác nhận"; 
                }
                else if(BookingStatus == 2)
                {
                    return"Chờ setup";
                }
                else if (BookingStatus == 3)
                {
                    return "Đang phục vụ";
                }
                else if (BookingStatus == 4)
                {
                    return "Đã hoàn tất";
                }
                else if (BookingStatus == 5)
                {
                    return "Đã hủy"; 
                }
                else if (BookingStatus == 6)
                {
                    return  "Unknow"; 
                }
                else if (BookingStatus == 7)
                {
                    return "Chờ xếp bàn"; 
                }
                else if (BookingStatus == 8)
                {
                    return "Đã hết hạn "; 
                }
                else if(BookingStatus == 9)
                {
                    return "Chờ nhận khách "; 
                }
                return "Hoàn tất"; 

            }
            
          
        }
        public override string ToString()
        {
            return string.Format("#{0} - {1} -{2}", this.Id, this.TableFormatString, this.CustomerName);
        }
    }
}
