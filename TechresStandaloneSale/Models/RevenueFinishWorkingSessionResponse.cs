using Newtonsoft.Json;
using System.Collections.Generic;
using TechresStandaloneSale.Models.Response;

namespace TechresStandaloneSale.Models
{
    public class RevenueFinishWorkingSessionResponse : BaseResponse
    {
        [JsonProperty("data")]
        public RevenueFinishWorkingSession Data { get; set; }
    }

    public class RevenueFinishWorkingSession
    {
        [JsonProperty("total_cost_final")] 
        public string TotalCostFinal { get; set; }
        [JsonProperty("branch_working_session_name")]
        public string BranchWorkingSessionName { get; set; }
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("before_cash")]
        public decimal BeforeCash { get; set; }

        [JsonProperty("after_cash")]
        public decimal AfterCash { get; set; }

        [JsonProperty("cash_amount")]
        public decimal CashAmount { get; set; }

        [JsonProperty("bank_amount")]
        public decimal BankAmount { get; set; }
        [JsonProperty("transfer_amount")]
        public decimal TransferAmount { get; set; }

        [JsonProperty("total_amount")]
        public decimal TotalAmount { get; set; }

        [JsonProperty("debt_amount")]
        public decimal DebtAmount { get; set; }

        [JsonProperty("real_amount")]
        public decimal RealAmount { get; set; }

        [JsonProperty("number_orders")]
        public long NumberOrders { get; set; }

        [JsonProperty("cash_value")]
        public List<Money> CashValue { get; set; }

        [JsonProperty("addition_fee_ids")]
        public List<long> AdditionFeeIds { get; set; }

        [JsonProperty("out_total_amount_by_addition_fee")]
        public decimal OutTotalAmountByAdditionFee { get; set; }

        [JsonProperty("in_total_amount_by_addition_fee")]
        public decimal InTotalAmountByAdditionFee { get; set; }

        [JsonProperty("in_cash_amount_by_addition_fee")]
        public decimal InCashAmountByAdditionFee { get; set; }

        [JsonProperty("out_cash_amount_by_addition_fee")]
        public decimal OutCashAmountByAdditionFee { get; set; }
        [JsonProperty("tip_amount")]
        public decimal TipAmount { get; set; }

        [JsonProperty("deposit_amount")]
        public decimal DepositAmount { get; set; }

        [JsonProperty("return_deposit_amount")]
        public decimal ReturnDepositAmount { get; set; }

        [JsonProperty("open_time")]
        public string OpenTime { get; set; }
        [JsonProperty("close_time")]
        public string CloseTime { get; set; }

        [JsonProperty("take_away_orders")]
        public long TakeAwayOrders { get; set; }

        [JsonProperty("online_delivery_orders")]
        public long OnlineDeliveryOrders { get; set; }

        [JsonProperty("membership_point_used_amount")]
        public decimal MembershipPointUsedAmount { get; set; }

        [JsonProperty("membership_accumulate_point_used_amount")]
        public decimal MembershipAccumulatePointUsedAmount { get; set; }

        [JsonProperty("membership_promotion_point_used_amount")]
        public decimal MembershipPromotionPointUsedAmount { get; set; }

        [JsonProperty("membership_alo_point_used_amount")]
        public decimal MembershipAloPointUsedAmount { get; set; }

        [JsonProperty("in_bank_amount_by_addition_fee")]
        public decimal InBankAmountByAdditionFee { get; set; }
        [JsonProperty("in_transfer_amount_by_addition_fee")]
        public decimal InTransferAmountByAdditionFee { get; set; }
        [JsonProperty("out_bank_amount_by_addition_fee")]
        public decimal OutBankAmountByAdditionFee { get; set; }

        [JsonProperty("out_transfer_amount_by_addition_fee")]
        public decimal OutTransferAmountByAdditionFee { get; set; }
        [JsonProperty("deposit_cash_amount")]
        public decimal DepositCashAmount { get; set; }

        [JsonProperty("deposit_bank_amount")]
        public decimal DepositBankAmount { get; set; }

        [JsonProperty("deposit_transfer_amount")]
        public decimal DepositTransferAmount { get; set; }

        [JsonProperty("return_deposit_cash_amount")]
        public decimal ReturnDepositCashAmount { get; set; }

        [JsonProperty("return_deposit_bank_amount")]
        public decimal ReturnDepositBankAmount { get; set; }

        [JsonProperty("return_deposit_transfer_amount")]
        public decimal ReturnDepositTransferAmount { get; set; }
        [JsonProperty("total_in_amount")]
        public decimal TotalInAmount { get; set; }

        [JsonProperty("total_in_cash_amount")]
        public decimal TotalInCashAmount { get; set; }

        [JsonProperty("total_in_bank_amount")]
        public decimal TotalInBankAmount { get; set; }

        [JsonProperty("total_in_transfer_amount")]
        public decimal TotalInTransferAmount { get; set; }

        [JsonProperty("total_cash")]
        public decimal TotalCash { get; set; }

        [JsonProperty("total_out_amount")]
        public decimal TotalOutAmount { get; set; }

        [JsonProperty("total_out_cash_amount")]
        public decimal TotalOutCashAmount { get; set; }

        [JsonProperty("total_out_bank_amount")]
        public decimal TotalOutBankAmount { get; set; }

        [JsonProperty("total_out_transfer_amount")]
        public decimal TotalOutTransferAmount { get; set; }

        [JsonProperty("open_employee_id")]
        public long OpenEmployeeId { get; set; }

        [JsonProperty("close_employee_id")]
        public long CloseEmployeeId { get; set; }

        [JsonProperty("branch_id")]
        public long BranchId { get; set; }

        [JsonProperty("branch_name")]
        public string BranchName { get; set; }

        [JsonProperty("open_employee_name")]
        public string OpenEmployeeName { get; set; }

        [JsonProperty("open_employee_prefix")]
        public string OpenEmployeePrefix { get; set; }

        [JsonProperty("open_employee_normalize_name")]
        public string OpenEmployeeNormalizeName { get; set; }

        [JsonProperty("close_employee_name")]
        public string CloseEmployeeName { get; set; }

        [JsonProperty("close_employee_prefix")]
        public string CloseEmployeePrefix { get; set; }

        [JsonProperty("close_employee_normalize_name")]
        public string CloseEmployeeNormalizeName { get; set; }

        [JsonProperty("total_amount_final")] 
        public decimal TotalAmountFinal { get; set; }

        [JsonProperty("total_receipt_amount_final")]
        public decimal TotalReceiptAmountFinal { get; set; }

        public string DifferenceAmountString
        {
            get
            {

                return Utils.Utils.FormatMoney(this.TotalReceiptAmountFinal - RealAmount);
            }
            set
            {
                DifferenceAmountString = value;
            }
        }

        public string Code
        {
            get
            {

                return string.Format("#{0}", this.Id);
            }
            set
            {
                Code = value;
            }
        }
        public string BeforeCashString
        {
            get
            {

                return Utils.Utils.FormatMoney(this.BeforeCash);
            }
            set
            {
                BeforeCashString = value;
            }
        }
        public string InAmountString
        {
            get
            {

                return Utils.Utils.FormatMoney(this.InTotalAmountByAdditionFee);
            }
            set
            {
                InAmountString = value;
            }
        }
        public string OutAmountString
        {
            get
            {

                return Utils.Utils.FormatMoney(this.OutTotalAmountByAdditionFee);
            }
            set
            {
                OutAmountString = value;
            }
        }
        public string CashAmountString
        {
            get
            {

                return Utils.Utils.FormatMoney( this.AfterCash);
            }
            set
            {
                CashAmountString = value;
            }
        }

        public string CashAmountInShiftString
        {
            get
            {

                return Utils.Utils.FormatMoney( this.TotalCash);
            }
            set
            {
                CashAmountInShiftString = value;
            }
        }
        public string RealAmountString
        {
            get
            {

                return Utils.Utils.FormatMoney(this.RealAmount);
            }
            set
            {
                RealAmountString = value;
            }
        }

    }
}
