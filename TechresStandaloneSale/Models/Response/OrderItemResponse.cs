using Newtonsoft.Json;
using System.Collections.Generic;

namespace TechresStandaloneSale.Models.Response
{
    public class OrderItemResponse : BaseResponse
    {
        [JsonProperty("data")]
        public OrderBillData Data { get; set; }
    }

    public class OrderBillData
    {
        [JsonProperty("employee_give_qrcode_fullname")]
        public string EmployeeGiveQrCodeName { get; set; }
        [JsonProperty("employee_give_qrcode_id")]
        public long EmployeeGiveQrCodeId { get; set; }
        //public long EmployeeAssign { get; set; }
        [JsonProperty("customer_name")]
        public string CustomerName { get; set; }
        [JsonProperty("customer_phone")]
        public string CustomerPhone { get; set; }
        [JsonProperty("customer_id")] 
        public long CustomerId { get; set; }
        [JsonProperty("customer_address")]
        public string CustomerAddress { get; set; }

        [JsonProperty("service_charge_amount")]
        public decimal ServiceChargeAmount { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("vat")]
        public long Vat { get; set; }

        [JsonProperty("branch_id")]
        public long BranchId { get; set; }

        [JsonProperty("branch_name")]
        public string BranchName { get; set; }

        [JsonProperty("branch_phone")]
        public string BranchPhone { get; set; }

        [JsonProperty("branch_address")]
        public string BranchAddress { get; set; }

        [JsonProperty("table_id")]
        public long TableId { get; set; }

        [JsonProperty("table_name")]
        public string TableName { get; set; }
        [JsonProperty("is_return_deposit")]
        public int IsReturnDeposit { get; set; }
        [JsonProperty("booking_deposit_amount")]
        public decimal BookingDepositAmount { get; set; }

        [JsonProperty("vat_amount")]
        public decimal VatAmount { get; set; }

        [JsonProperty("discount_percent")]
        public float DiscountPercent { get; set; }

        [JsonProperty("discount_type")]
        public long DiscountType { get; set; }

        [JsonProperty("discount_amount")]
        public decimal DiscountAmount { get; set; }

        [JsonProperty("discount_promotion_amount")]
        public decimal DiscountPromotionAmount { get; set; }

        [JsonProperty("restaurant_promotion_id")]
        public string RestaurantPromotionId { get; set; }

        [JsonProperty("restaurant_promotion_voucher_id")]
        public long RestaurantPromotionVoucherId { get; set; }

        [JsonProperty("total_gift_food_amount")]
        public decimal TotalGiftFoodAmount { get; set; }

        [JsonProperty("total_amount")]
        public decimal TotalAmount { get; set; }

        [JsonProperty("cash_amount")]
        public decimal CashAmount { get; set; }

        [JsonProperty("bank_amount")]
        public decimal BankAmount { get; set; }

        [JsonProperty("membership_total_point_used")]
        public long MembershipTotalPointUsed { get; set; }

        [JsonProperty("membership_point_used_amount")]
        public long MembershipPointUsedAmount { get; set; }

        [JsonProperty("membership_point_used")]
        public long MembershipPointUsed { get; set; }

        [JsonProperty("membership_promotion_point_used")]
        public long MembershipPromotionPointUsed { get; set; }

        [JsonProperty("membership_alo_point_used")]
        public long MembershipAloPointUsed { get; set; }
          
        [JsonProperty("membership_point_added")]
        public long MembershipPointAdded { get; set; }

        [JsonProperty("membership_accumulate_point_used")]
        public long MembershipAccumulatePointUsed { get; set; }

        [JsonProperty("status")]
        public long OrderStatus { get; set; }

        [JsonProperty("move_from_tables")]
        public List<string> MoveFromTables { get; set; }

        [JsonProperty("table_merge_list_name")]
        public List<string> TableMergeListName { get; set; }

        [JsonProperty("employee_id")]
        public long EmployeeId { get; set; }

        [JsonProperty("employee_name")]
        public string EmployeeName { get; set; }
      

        [JsonProperty("customer_slot_number")]
        public long CustomerSlotNumber { get; set; }

        [JsonProperty("payment_date")]
        public string PaymentDate { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }

        [JsonProperty("cashier_id")]
        public long CashierId { get; set; }

        [JsonProperty("cashier_name")]
        public string CashierName { get; set; }

        //[JsonProperty("customer")]
        //public Customer Customer { get; set; }
        [JsonProperty("employee_assign")]
        public Employee EmployeeAssign { get; set; }

        [JsonProperty("shipping_receiver_name")]
        public string ShippingReceiverName { get; set; }

        [JsonProperty("shipping_address")]
        public string ShippingAddress { get; set; }

        [JsonProperty("shipping_phone")]
        public string ShippingPhone { get; set; }

        [JsonProperty("shipping_fee")]
        public decimal ShippingFee { get; set; }


        [JsonProperty("employee_debt_id")]
        public long EmployeeDebtId { get; set; }

        [JsonProperty("employee_debt_name")]
        public string EmployeeDebtName { get; set; }

        [JsonProperty("customer_debt_id")]
        public long CustomerDebtId { get; set; }

        [JsonProperty("customer_debt_name")]
        public string CustomerDebtName { get; set; }

        [JsonProperty("note")]
        public string Note { get; set; }

        [JsonProperty("is_online")]
        public long IsOnline { get; set; }

        [JsonProperty("is_take_away")]
        public long IsTakeAway { get; set; }
        [JsonProperty("booking_deposit_payment_method")]
        public int BookingDepositPaymentMethod { get; set; }
        [JsonProperty("order_details")]
        public List<BillResponse> Foods { get; set; }
    }

}
