using Newtonsoft.Json;
using System.Collections.Generic;
using System.Windows;
using TechresStandaloneSale.Helpers;

namespace TechresStandaloneSale.Models.Response
{
    public class RestaurantBudgetResponse : BaseResponse
    {
        [JsonProperty("data")]
        public RestaurantBudgetData Data { get; set; }
    }
    public class RestaurantBudgetData
    {
        [JsonProperty("limit")]
        public double Limit { get; set; }

        [JsonProperty("list")]
        public List<RestaurantBudget> Lists { get; set; }

        [JsonProperty("total_record")]
        public double TotalRecord { get; set; }
    }
    public class RestaurantBudget
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("note")]
        public string Note { get; set; }
        [JsonProperty("from")]
        public string From { get; set; }
        [JsonProperty("to")]
        public string To { get; set; }
        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("branch_id")]
        public long BranchId { get; set; }

        [JsonProperty("employee_id")]
        public long EmployeeId { get; set; }

        [JsonProperty("employee_name")]
        public string EmployeeName { get; set; }

        [JsonProperty("employee_edit_id")]
        public long EmployeeEditId { get; set; }

        [JsonProperty("employee_edit_name")]
        public string EmployeeEditName { get; set; }
        [JsonProperty("employee_complete_id")]
        public long EmployeeCompleteId { get; set; }

        [JsonProperty("employee_complete_name")]
        public string EmployeeCompleteName { get; set; }
        [JsonProperty("openning_amount")]
        public decimal OpenningAmount { get; set; }
        [JsonProperty("closing_amount")]
        public decimal ClosingAmount { get; set; }
        [JsonProperty("in_amount")]
        public decimal InAmount { get; set; }
        [JsonProperty("out_amount")]
        public decimal OutAmount { get; set; }
        [JsonProperty("order_amount")]
        public decimal OrderAmount { get; set; }
        [JsonProperty("changing_amount")]
        public decimal ChangingAmount { get; set; }
        [JsonProperty("status_name")]
        public string StatusName { get; set; }
        public string DateString
        {
            get
            {
                return string.Format("{0} - {1}", string.IsNullOrEmpty(this.From) ? "" : this.From.Substring(0, 10), this.To.Substring(0, 10));
            }
            set
            {
                DateString = value;
            }
        }
        public Visibility AdditionFeesCompletedVisibility
        {
            get
            {
                if (Status == (int)AdditionFeeStatusEnum.PAID || Status == (int)AdditionFeeStatusEnum.CANCEL)
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
                AdditionFeesCompletedVisibility = value;
            }
        }
        public Visibility AdditionFeesCancelVisibility
        {
            get
            {
                if (Status == (int)AdditionFeeStatusEnum.PAID || Status == (int)AdditionFeeStatusEnum.CANCEL)
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
                AdditionFeesCancelVisibility = value;
            }
        }
        public string ChangingAmountString
        {
            get
            {
                return string.Format("{0:0,0}", this.ChangingAmount);
            }
            set
            {
                ChangingAmountString = value;
            }
        }
        public string OrderAmountString
        {
            get
            {

                return string.Format("{0:0,0}", this.OrderAmount);
            }
            set
            {
                OrderAmountString = value;
            }
        }
        public string OutAmountString
        {
            get
            {

                return string.Format("{0:0,0}", this.OutAmount);
            }
            set
            {
                OutAmountString = value;
            }
        }
        public string InAmountString
        {
            get
            {

                return string.Format("{0:0,0}", this.InAmount);
            }
            set
            {
                InAmountString = value;
            }
        }
        public string OpenningAmountString
        {
            get
            {

                return string.Format("{0:0,0}", this.OpenningAmount);
            }
            set
            {
                OpenningAmountString = value;
            }
        }
        public string ClosingAmountString
        {
            get
            {

                return string.Format("{0:0,0}", this.ClosingAmount);
            }
            set
            {
                ClosingAmountString = value;
            }
        }
    }


}
