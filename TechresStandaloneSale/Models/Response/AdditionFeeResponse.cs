using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Windows;
using TechresStandaloneSale.Helpers;
using TechresStandaloneSale.Models;
using TechresStandaloneSale.Models.Response;
using TechresStandaloneSale.Utils;

namespace TechresStandaloneSale.Models.Response
{
    public class AdditionFeeResponse : BaseResponse
    {
        [JsonProperty("data")]
        public AdditionFeeData Data { get; set; }
    }
    public class AdditionFeeData
    {
        [JsonProperty("limit")]
        public long Limit { get; set; }

        [JsonProperty("list")]
        public List<AdditionFee> List { get; set; }

        [JsonProperty("total_record")]
        public long TotalRecord { get; set; }

        [JsonProperty("in_amount")]
        public decimal InAmount { get; set; }

        [JsonProperty("out_amount")]
        public decimal OutAmount { get; set; }
    }

}
public class AdditionFeeOneResponse : BaseResponse
{
    [JsonProperty("data")]
    public AdditionFee Data { get; set; }
}
public class AdditionFee
{
    [JsonProperty ("addition_fee_status")]
    public long AdditionFeeStatus { get; set; }
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("code")]
    public string Code { get; set; }

    [JsonProperty("type")]
    public long Type { get; set; }

    [JsonProperty("amount")]
    public long Amount { get; set; }

    [JsonProperty("note")]
    public string Note { get; set; }

    [JsonProperty("date")]
    public string Date { get; set; }

    [JsonProperty("status")]
    public long Status { get; set; }

    [JsonProperty("created_at")]
    public string CreatedAt { get; set; }

    [JsonProperty("updated_at")]
    public string UpdatedAt { get; set; }

    [JsonProperty("branch")]
    public Branch Branch { get; set; }

    [JsonProperty("employee")]
    public Employee Employee { get; set; }

    [JsonProperty("employee_edit")]
    public Employee EmployeeEdit { get; set; }

    [JsonProperty("employee_confirm")]
    public Employee EmployeeConfirm { get; set; }

    [JsonProperty("type_name")]
    public string TypeName { get; set; }

    [JsonProperty("payment_method")]
    public string PaymentMethod { get; set; }

    [JsonProperty("addition_fee_reason_id")]
    public long ResonId { get; set; }

    [JsonProperty("addition_fee_reason_name")]
    public string AdditionFeeReasonName { get; set; }

    [JsonProperty("object_type")]
    public string ObjectType { get; set; }

    [JsonProperty("object_name")]
    public string ObjectName { get; set; }

    [JsonProperty("object_name_prefix")]
    public string ObjectNamePrefix { get; set; }

    [JsonProperty("object_name_normalize_name")]
    public string ObjectNameNormalizeName { get; set; }

    [JsonProperty("object_id")]
    public long ObjectId { get; set; }

    [JsonProperty("object_type_id")]
    public long ObjectTypeId { get; set; }

    [JsonProperty("supplier_order_ids")]
    public List<long> Supplier_order_ids { get; set; }

    [JsonProperty("status_text")]
    public string StatusText { get; set; }

    [JsonProperty("is_count_to_revenue")]
    public long IsCountToRevenue { get; set; }

    [JsonProperty("is_automatically_generated")]
    public long IsAutomaticallyGenerated { get; set; }

    [JsonProperty("is_automatically_generated_name")]
    public string IsAutomaticallyGeneratedName { get; set; }

    [JsonProperty("automatically_generated_type")]
    public long AutomaticallyGeneratedType { get; set; }

    [JsonProperty("automatically_generated_type_name")]
    public string AutomaticallyGeneratedTypeName { get; set; }


    [JsonProperty("payment_method_id")]
    public long PaymentMethodId { get; set; }

    [JsonProperty("supplier_orders")]
    public List<WarehouseSession> List { get; set; }

    private User currentUser = (User)Utils.GetCacheValue(Constants.CURRENT_USER);
    public Visibility ReturnVisibility
    {
        get
        {
            if (this.Status == (int)AdditionFeeStatusEnum.CANCEL_PAYMENT&& this.Type == (int)AdditionFeeEnum.OUT)
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Collapsed;
            }
        }
    }
    public Visibility ApproveVisibility1
    {
        get
        {
            if (this.Status == (int)AdditionFeeStatusEnum.WAITTING_APPROVE_PAYMENT)
            {
                return Visibility.Visible;
            }
            else if (this.Status == (int)AdditionFeeStatusEnum.WAITING_PAYMENT)
            {
                return Visibility.Visible;
            }
            else
                return Visibility.Collapsed; 
        }
    }


    public Visibility ApproveVisibility
    {
        get
        {
            switch (this.AdditionFeeStatus)
            {
                case (int)AdditionFeeStatusEnum.WAITTING_APPROVE_PAYMENT:
                    {
                        if (Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.APPROVE_ADDITION_FEE), currentUser.Permissions))
                        {
                            return Visibility.Visible;
                        }
                        else
                        {
                            return Visibility.Collapsed;
                        }
                    }
                case (int)AdditionFeeStatusEnum.WAITING_PAYMENT:
                    {
                        if (Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.PAY_ADDITION_FEE), currentUser.Permissions))
                        {
                            return Visibility.Visible;
                        }
                        else
                        {
                            return Visibility.Collapsed;
                        }
                    }
                case (int)AdditionFeeStatusEnum.PAID:
                    {
                        if (Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.ADDITION_FEE_APPROVEMENT), currentUser.Permissions))
                        {
                            return Visibility.Visible;
                        }
                        else
                        {
                            return Visibility.Collapsed;
                        }
                    }
                default:
                    return Visibility.Collapsed;
            }
        }
    }
    public Visibility EditVisibility
    {
        get
        {
            // Đạt thêm AdditionFeeStatus != 2
            if (this.AdditionFeeStatus == (int)AdditionFeeStatusEnum.WAITTING_APPROVE_PAYMENT || this.Status == (int)AdditionFeeStatusEnum.WAITING_PAYMENT && this.AdditionFeeStatus !=2)
            { 
                return Visibility.Visible;
            }
            else
            { 
                return Visibility.Hidden; // Đạt sửa Collapsed thành Hidden
            }
        }
    }
   
    
    public string AmountString
    {
        get
        {
            return Utils.FormatMoney(this.Amount);
        }
        set
        {
            AmountString = value;
        }
    }
    public string StatusAdditionString
    {
        get
        {
            if (this.AdditionFeeStatus == (int)AdditionFeeStatusEnum.WAITING_PAYMENT && this.Type == (int)AdditionFeeEnum.OUT)
            {
                return "CHỜ CHI";
            }
            else if (this.AdditionFeeStatus == (int)AdditionFeeStatusEnum.CONFIRMED)
            {
                return "ĐÃ XÁC NHẬN";
            }
            else if (this.AdditionFeeStatus == (int)AdditionFeeStatusEnum.CANCEL)
            {
                return "ĐÃ HỦY";
            }
            else if (this.AdditionFeeStatus == (int)AdditionFeeStatusEnum.PAID && this.Type == (int)AdditionFeeEnum.OUT)
            {
                return "ĐÃ CHI";
            }
            else if (this.AdditionFeeStatus == (int)AdditionFeeStatusEnum.CANCEL_PAYMENT &&  this.Type == (int)AdditionFeeEnum.OUT)
            {
                return "HỦY DO CHI SAI";
            }
            else if (this.AdditionFeeStatus == (int)AdditionFeeStatusEnum.CANCEL_PAYMENT_REFUNDED && this.Type == (int)AdditionFeeEnum.OUT)
            {
                return "ĐÃ TRUY THU CHI SAI";
            }
            else if (this.AdditionFeeStatus == (int)AdditionFeeStatusEnum.WAITTING_APPROVE_PAYMENT && this.Type == (int)AdditionFeeEnum.OUT)
            {
                return "CHỜ DUYỆT CHI";
            }
            else if (this.AdditionFeeStatus == (int)AdditionFeeStatusEnum.WAITING_PAYMENT && this.Type == (int)AdditionFeeEnum.IN)
            {
                return "CHỜ THU";
            }
           
            else if (this.AdditionFeeStatus == (int)AdditionFeeStatusEnum.PAID && this.Type == (int)AdditionFeeEnum.IN)
            {
                return "ĐÃ THU";
            }
            else if (this.AdditionFeeStatus == (int)AdditionFeeStatusEnum.CANCEL_PAYMENT && this.Type == (int)AdditionFeeEnum.IN)
            {
                return "HỦY DO THU SAI";
            }
            else if (this.AdditionFeeStatus == (int)AdditionFeeStatusEnum.CANCEL_PAYMENT_REFUNDED && this.Type == (int)AdditionFeeEnum.IN)
            {
                return "ĐÃ TRUY THU CHI SAI";
            }
            else if (this.AdditionFeeStatus == (int)AdditionFeeStatusEnum.WAITTING_APPROVE_PAYMENT && this.Type == (int)AdditionFeeEnum.IN)
            {
                return "CHỜ DUYỆT THU";
            }
            else
                return "UNKNOW"; 
        }
    }

    public string TypeString
    {
        get
        {
            if (this.Type == (int)AdditionFeeEnum.IN)
            {
                return "PHIẾU THU";
            }
            else if (this.Type == (int)AdditionFeeEnum.OUT)
            {
                return "PHIẾU CHI";
            }
            else
                return "UNKNOW"; 
        }
    }
    public string AutomaticGenaralString
    {
        get
        {
            if(this.IsAutomaticallyGenerated == 1 && this.Type == (int)AdditionFeeEnum.IN)
            {
                return "PHIẾU THU TỰ ĐỘNG"; 
            }
            else if(this.IsAutomaticallyGenerated == 0 && this.Type == (int)AdditionFeeEnum.IN)
            {
                return "PHIẾU THU TAY"; 
            }
            if (this.IsAutomaticallyGenerated == 1 && this.Type == (int)AdditionFeeEnum.OUT)
            {
                return "PHIẾU CHI TỰ ĐỘNG";
            }
            else if (this.IsAutomaticallyGenerated == 0 && this.Type == (int)AdditionFeeEnum.OUT)
            {
                return "PHIẾU CHI TAY";
            }
            else
                return "UNKNOW"; 
        }
    }
    public string PaymentIdString
    {
        get
        {
            if (this.PaymentMethodId == 1)
            {
                return "TIỀN MẶT";
            }
            else if (this.PaymentMethodId == 2)
            {
                return "THẺ NGÂN HÀNG"; 
            }
            else if (this.PaymentMethodId == 6)
            {
                return "CHUYỂN KHOẢN"; 
            }
            else if (this.PaymentMethodId == 8)
            {
                return "ĐIỂM NẠP"; 
            }
            else
                return "UNKNOW"; 

        }
    }
}
