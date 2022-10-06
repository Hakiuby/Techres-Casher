using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechresStandaloneSale.Helpers
{
    public enum OrderDetailStatusEnum
    {
        ALL = -1,
        PENDING = 0,
        COOKING = 1,
        DONE = 2,
        OUTSTOCK = 3,
        CANCEL = 4,
    }
}
