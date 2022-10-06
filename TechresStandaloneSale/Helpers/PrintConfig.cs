using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Printing;
using TechresStandaloneSale.Models;

namespace TechresStandaloneSale.Helpers
{
    public class PrintConfig
    {

        public static List<Printer> FindAllPrint()
        {

            List<Printer> PrintList = new List<Printer>();
            var scope = new ManagementScope(@"\root\cimv2");
            scope.Connect();

            var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Printer");
            var results = searcher.Get();
            foreach (var printer in results)
            {
                var portName = printer.Properties["PortName"].Value;

                var searcher2 = new ManagementObjectSearcher("SELECT * FROM Win32_TCPIPPrinterPort where Name LIKE '" + portName + "'");
                var results2 = searcher2.Get();
                //if (results2 != null)
                    if (results2!= null && results2.Count > 0)
                {
                    foreach (var printer2 in results2)
                    {
                        PrintList.Add(new Printer((string)printer.Properties["Name"].Value, (string)printer2.Properties["HostAddress"].Value, printer2.Properties["PortNumber"].Value.ToString()));
                    }
                }
                else
                {
                    PrintList.Add(new Printer((string)printer.Properties["Name"].Value, string.Empty, string.Empty));
                }
              
            }
            return PrintList;
        }

        public string CheckPrinterConfiguration(string printerName)
        {
            var server = new PrintServer();
            var queues = server.GetPrintQueues(new[]
            { EnumeratedPrintQueueTypes.Local, EnumeratedPrintQueueTypes.Connections });
            string fulllName = queues.Where(q => q.Name == printerName).Select(q => q.FullName).FirstOrDefault();
            return fulllName;
        }
    }
}
