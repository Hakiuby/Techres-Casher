using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechresStandaloneSale.Helpers
{
    public enum BookingStatusEnum
    {
        WAITING_CONFIRM = 1, //đang chờ nhà hàng xác nhận
        ARRANGED_TABLE = 2, //nhà hàng đã xác nhận, đang chờ khách tới
        WAITING_COMPLETE = 3, //đơn hàng đã bắt đầu, chờ hoàn tất hóa đơn
        COMPLETED = 4, // hoàn tất
        CANCEL = 5, //hủy
        UNKONW = 6,
        CONFIMED=7,
        EXPIRED=8, //
        SET_UP=9
    }
}
