using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using TechresStandaloneSale.Helpers;

namespace TechresStandaloneSale.Models.Response
{
   public class EmployeeAdvancedSalaryResponse:BaseResponse
    {
        [JsonProperty("data")]
        public List<EmployeeAdvancedSalary> Data { get; set; }
    }
    public class EmployeeAdvancedSalaryData : BaseResponse
    {
        [JsonProperty("data")]
        public EmployeeAdvancedSalary Data { get; set; }
    }
    
    public class EmployeeAdvancedSalary 
    {

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("reason")]
        public string Reason { get; set; }

        [JsonProperty("status")]
        public long Status { get; set; }

        [JsonProperty("total_salary")]
        public decimal TotalSalary { get; set; }

        [JsonProperty("status_text")]
        public string StatusText { get; set; }

        [JsonProperty("branch_id")]
        public long BranchId { get; set; }

        [JsonProperty("branch_name")]
        public string BranchName { get; set; }

        [JsonProperty("employee_approved")]
        public Employee EmployeeApproved { get; set; }

        [JsonProperty("employee")]
        public Employee Employee { get; set; }
        [JsonProperty("time")]
        public string Time { get; set; }
        [JsonProperty("paid_at")]
        public string PaidAt { get; set; }
      
        public string AmountString
        {
            get
            {
                return Utils.Utils.FormatMoney(this.Amount);
            }
            set
            {
                AmountString = value;
            }
        }
        public string CurrentAmountString
        {
            get
            {
                return Utils.Utils.FormatMoney(this.TotalSalary);
            }
            set
            {
                CurrentAmountString = value;
            }
        }

        public Brush StatusForeground
        {
            get
            {
                if (Status == (int)EmployeeAdvancedSalaryEnum.APPROVED)
                {
                    return new SolidColorBrush(Colors.Black);
                }else if (Status == (int)EmployeeAdvancedSalaryEnum.PENDING)
                {
                    return new SolidColorBrush(Colors.Green);
                }
                else if (Status == (int)EmployeeAdvancedSalaryEnum.REJECTED)
                {
                    return new SolidColorBrush(Colors.Red);
                }
                else
                {
                    return new SolidColorBrush(Colors.Black);
                }
            }
            set
            {
                StatusForeground = value;
            }
        }

        public Visibility PendingVisibility
        {
            get
            {
                if (Status == (int)EmployeeAdvancedSalaryEnum.APPROVED)
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
                PendingVisibility = value;
            }
        }
    }
}
