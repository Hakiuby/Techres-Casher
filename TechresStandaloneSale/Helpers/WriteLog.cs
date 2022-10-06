using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechresStandaloneSale.Helpers
{
    public static class WriteLog
    {

        public static void logs(string message)
        {

            if (System.Diagnostics.Debugger.IsAttached)
            {
                Console.WriteLine(message);
            }
        }

    }
}
