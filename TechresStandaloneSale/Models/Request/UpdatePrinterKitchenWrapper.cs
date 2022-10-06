using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechresStandaloneSale.Models.Request
{
   public class UpdatePrinterKitchenWrapper
    {
        [JsonProperty("list-print")]
        public List<UpdatePrinterKitchen> ListPrint { get; set; }
        public UpdatePrinterKitchenWrapper(List<UpdatePrinterKitchen> listPrint)
        {
            ListPrint = listPrint != null ? listPrint: new List<UpdatePrinterKitchen>();
        }
    }
    public class UpdatePrinterKitchen
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("printer_name")]
        public string PrinterName { get; set; }

        [JsonProperty("printer_ip_address")]
        public string PrinterIpAddress { get; set; }

        [JsonProperty("printer_port")]
        public string PrinterPort { get; set; }

        [JsonProperty("printer_paper_size")]
        public long PrinterPaperSize { get; set; }

        [JsonProperty("is_have_printer")]
        public long IsHavePrinter { get; set; }

        public UpdatePrinterKitchen(long id, string printerName, string printerIpAddress, string printerPort, long printerPapaerSize, bool isHavePrinter)
        {
            Id = id;
            PrinterName = printerName;
            PrinterIpAddress = printerIpAddress;
            PrinterPort = printerPort;
            PrinterPaperSize = printerPapaerSize;
            IsHavePrinter = isHavePrinter ? 1 : 0;
        }
    }

}
