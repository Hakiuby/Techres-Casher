using Newtonsoft.Json;
using System.Windows;

namespace TechresStandaloneSale.Models.Request
{
    public class DeviceConfigWrapper
    {
        [JsonProperty(PropertyName = "is_qrcode")]
        public bool IsQRCode { get; set; }
        [JsonProperty(PropertyName = "is_branch_qrcode")]
        public bool IsBranchQRCode { get; set; }
        [JsonProperty(PropertyName = "is_employee_qrcode")]
        public bool IsEmployeeQRCode { get; set; }
        [JsonProperty(PropertyName = "is_casher")]
        public bool IsCasher { get; set; }
        [JsonProperty(PropertyName = "qrcode_printer")]
        public string QrCodePrinter { get; set; }
        [JsonProperty(PropertyName = "branch_qrcode_printer")]
        public string BranchQrCodePrinter { get; set; }
        [JsonProperty(PropertyName = "employee_qrcode_printer")]
        public string EmployeeQrCodePrinter { get; set; }
        [JsonProperty(PropertyName = "casher_printer")]
        public string CasherPrinter { get; set; }
        [JsonProperty(PropertyName = "qrcode_size")]
        public int QrCodeSize { get; set; }
        [JsonProperty(PropertyName = "branch_qrcode_size")]
        public int BranchQrCodeSize { get; set; }
        [JsonProperty(PropertyName = "employee_qrcode_size")]
        public int EmployeeQrCodeSize { get; set; }
        [JsonProperty(PropertyName = "casher_size")]
        public int CasherSize { get; set; }
        [JsonProperty(PropertyName = "is_fish_tank")]
        public bool IsFishtank { get; set; }
        [JsonProperty(PropertyName = "fish_tank_printer")]
        public string FishtankPrinter { get; set; }
        [JsonProperty(PropertyName = "fish_tank_size")]
        public long FishtankSize { get; set; }
        [JsonProperty("is_stamp")]
        public bool IsStamp { get; set; }
        [JsonProperty("stamp_printer")]
        public string StampPrinter { get; set; }
        [JsonProperty("stamp_size")]
        public long StampSize { get; set; }
    }
}
