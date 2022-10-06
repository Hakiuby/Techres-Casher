using Newtonsoft.Json;
using System.Collections.Generic;

namespace TechresStandaloneSale.Models.Request
{
    public class RevenueFinishWorkingSessionWrapper
    {
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

        [JsonProperty("deposit_amount")]
        public decimal DepositAmount { get; set; }

        [JsonProperty("return_deposit_amount")]
        public decimal ReturnDepositAmount { get; set; }
        [JsonProperty("tip_amount")]
        public decimal TipAmount { get; set; }
       

        [JsonProperty("out_bank_amount_by_addition_fee")]
        public decimal OutBankAmountByAdditionFee { get; set; }

        [JsonProperty("out_transfer_amount_by_addition_fee")]
        public decimal OutTransferAmountByAdditionFee { get; set; }

        [JsonProperty("in_bank_amount_by_addition_fee")]
        public decimal InBankAmountByAdditionFee { get; set; }

        [JsonProperty("in_transfer_amount_by_addition_fee")]
        public decimal InTransferAmountByAdditionFee { get; set; }

        [JsonProperty("deposit_bank_amount")]
        public decimal DepositBankAmount { get; set; }

        [JsonProperty("deposit_transfer_amount")]
        public decimal DepositTransferAmount { get; set; }

        [JsonProperty("deposit_cash_amount")]
        public decimal DepositCashAmount { get; set; }
        [JsonProperty("membership_point_used_amount")]
        public decimal MembershipPointUsedAmount { get; set; }

        [JsonProperty("return_deposit_cash_amount")]
        public decimal ReturnDepositCashAmount { get; set; }

        [JsonProperty("return_deposit_bank_amount")]
        public decimal ReturnDepositBankAmount { get; set; }

        [JsonProperty("return_deposit_transfer_amount")]
        public decimal ReturnDepositTransferAmount { get; set; }

        [JsonProperty("membership_accumulate_point_used_amount")]
        public decimal MembershipAccumulatePointUsedAmount { get; set; }

        [JsonProperty("membership_promotion_point_used_amount")]
        public decimal MembershipPromotionPointUsedAmount { get; set; }

        [JsonProperty("membership_alo_point_used_amount")]
        public decimal MembershipAloPointUsedAmount { get; set; }

      
        public RevenueFinishWorkingSessionWrapper(decimal beforeCash,
            decimal afterCash,
            decimal TotalAmount,
            decimal CashAmount,
            decimal BankAmount,
            decimal TransferAmount,
            decimal debtAmount,
            decimal realAmount,
            long numberOrder,
            List<Money> money,
            List<long> addtionFeeIds,
            decimal outTotalAmountByAdditionFee,
            decimal inTotalAmountByAdditionFee,
            decimal inCashAmountByAdditionFee,
            decimal outCashAmountByAdditionFee,
            decimal depositAmount,
            decimal returnDepositAmount,
            decimal outBankAmountByAdditionFee,
            decimal outTransferAmountByAdditionFee,
            decimal inBankAmountByAdditionFee,
            decimal inTransferAmountByAdditionFee,
             decimal depositBankAmount,
            decimal depositTransferAmount,
           decimal depositCashAmount,
           decimal returnDepositCashAmount,
           decimal returnDepositBankAmount,
          decimal returnDepositTransferAmount,
          decimal membershipPointUsedAmount,
          decimal membershipAloPointUsedAmount,
          decimal tipamount,
          decimal membershipAccumulatePointUsedAmount,
          decimal membershipPromotionPointUsedAmount)
        {

            this.BeforeCash = beforeCash;
            this.AfterCash = afterCash;
            this.TotalAmount = TotalAmount;
            this.CashAmount = CashAmount;
            this.BankAmount = BankAmount;
            this.TransferAmount = TransferAmount;
            this.DebtAmount = debtAmount;
            this.RealAmount = realAmount;
            this.NumberOrders = numberOrder;
            this.CashValue = money;
            this.AdditionFeeIds = addtionFeeIds;
            this.OutTotalAmountByAdditionFee = outTotalAmountByAdditionFee;
            this.InTotalAmountByAdditionFee = inTotalAmountByAdditionFee;
            this.InCashAmountByAdditionFee = inCashAmountByAdditionFee;
            this.OutCashAmountByAdditionFee = outCashAmountByAdditionFee;
            this.DepositAmount = depositAmount;
            this.ReturnDepositAmount = returnDepositAmount;
            this.OutBankAmountByAdditionFee = outBankAmountByAdditionFee;
            this.TipAmount = tipamount; 
            //this.Here = here; 
            OutTransferAmountByAdditionFee = outTransferAmountByAdditionFee;
            InBankAmountByAdditionFee = inBankAmountByAdditionFee;
            InTransferAmountByAdditionFee = inTransferAmountByAdditionFee;
            DepositBankAmount = depositBankAmount;
            DepositTransferAmount = depositTransferAmount;
            DepositCashAmount = depositCashAmount;
            ReturnDepositCashAmount = returnDepositCashAmount;
            ReturnDepositBankAmount = returnDepositBankAmount;
            ReturnDepositTransferAmount = returnDepositTransferAmount;
            MembershipPointUsedAmount = membershipPointUsedAmount;
            MembershipAccumulatePointUsedAmount = membershipAccumulatePointUsedAmount;
            MembershipAloPointUsedAmount = membershipAloPointUsedAmount;
            MembershipPromotionPointUsedAmount = membershipPromotionPointUsedAmount;

        }
    }
}
