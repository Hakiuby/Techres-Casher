using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechresStandaloneSale.Models
{
   public class Printer
    {
        public string PrintName { get; set; }
        public string IpAddress { get; set; }
        public string Port { get; set; }
        public Printer(string printName, string ipAddress, string port)
        {
            PrintName = printName;
            IpAddress = ipAddress;
            Port = port;
        }
    }
}
