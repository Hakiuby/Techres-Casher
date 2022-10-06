namespace TechresStandaloneSale.Helpers
{
    public enum TechresEnum
    {
        TOPUP_CUSTOMER_CARD,//Nạp thẻ cho khách hàng trên ứng dụng thu ngân
        BRANCH_MANAGER,                 //Hưởng doanh số kinh doanh trong chi nhánh
        AREA_TABLE_MANAGER,             //Tạo, chỉnh sửa bàn và khu vực
        FOOD_MANAGER,                   //Tạo, chỉnh sửa thực đơn và món ăn
        EMPLOYEE_MANAGER,               //Tạo, chỉnh sửa nhân viên
        SETTING_MANAGER,                //Cài đặt hệ thống
        ORDERS_MANAGER,                 //Thanh toán hoá đơn
        CHECKIN_MANAGER,                //Tắt bật checkin
        EMPLOYEE_RANKING_MANAGER,       //Quản lý hạng nhân viên
        WAREHOUSE_MANAGER,              //Order hàng qua App TECHRES-TMS
        BOOKING_MANAGER,                //Quản lý thông tin đặt bàn
        CUSTOMER_MANAGER,               //Quản lý khách hàng
        COOKING_MANAGER,                //Tả hổ bếp nấu
        DRINK_MANAGER,                  //Xác nhận đồ uống và các món khác
        GIFT_FOOD,                      //Tặng món
        CANCEL_DRINK,                   //Huỷ đồ uống & món khác
        CANCEL_COMPLETED_FOOD,          //Huỷ món đã hoàn tất và đã xác nhận
        ADD_CUSTOM_FOOD,                //Order món ngoài menu
        DISCOUNT_FOOD,                  //Giảm giá món
        DISCOUNT_ORDER,                 //Giảm giá hoá đơn
        BBQ_KITCHEN_MANAGER,            //Tả hổ bếp nướng
        EMPLOYEE_RANK_MANAGER,          //Tạo, chỉnh sửa hạng nhân viên
        TASK_MANAGER,                   //Giao việc, kiểm tra công việc nhân viên
        JOB_MANAGER,                    //Tạo, chỉnh sửa công việc hệ thống
        PERMISSION_ROLE_MANAGER,        //Phân quyền bộ phân
        SALARY_TARGET_MANAGER,          //Tạo, chỉnh sửa thang điểm
        SALARY_LEVEL_MANAGER,           //Tạo, chỉnh sửa bậc lương
        SALARY_MANAGER,                 //Chỉnh sửa bảng lương nhân viên
        OWNER,                          //Chủ nhà hàng
        ORDER_SESSION_MANAGER,          //Quản lý chốt ca thu ngân
        ORDER_FOOD,                     //Order món ăn trên ứng dụng
        EMPLOYEE_ROLE_MANAGER,          //Tạo, chỉnh sửa bộ phận
        EMPLOYEE_KAIZEN_MANAGER,        //Kiểm duyệt kaizen
        EMPLOYEE_LEAVE_FORM_MANAGER,    //Kiểm duyệt đơn nghỉ phép
        WORKING_SESSION_MANAGER,        //Tạo, chỉnh sửa ca làm việc
        BRANCH_DATA_MANAGER,            //Chỉnh sửa thông tin chi nhánh
        ORDER_SESSION,                  //Thực hiện việc chốt ca của thu ngân
        PERMISSION_EMPLOYEE_MANAGER,    //Phân quyền nhân viên
        REPORT_REVENUE,                 //Báo cáo doanh thu
        REPORT_FOOD,                    //Báo cáo món ăn
        REPORT_DRINK,                   //Báo cáo đồ uống
        REPORT_GIFT_FOOD,               //Báo cáo món tặng
        REPORT_DISCOUNT,                //Báo cáo giảm giá
        REPORT_THREE_MONTHS,            //Báo cáo trong 3 tháng
        REPORT_ONE_YEARS,               //Báo cáo trong 1 năm
        REPORT_THREE_YEARS,             //Báo cáo trong 3 năm
        REPORT_MANY_YEARS,              //Báo cáo không giới hạn thời gian
        REPORT_VAT,                     //Báo cáo VAT
        REPORT_CUSTOMER_SLOT,           //Báo cáo số lượng khách
        REPORT_WAREHOUSE,               //Báo cáo kho
        REPORT_EMPLOYEE_POINT,          //Báo cáo doanh số, điểm, tiền thưởng nhân viên
        CASHIER_ACCESS,                 //Quyền truy cập thu ngân
        ACCOUNTANT_ACCESS,              //Quyền truy cập kế toán
        CHEF_COOK_ACCESS,               //Quyền truy cập tả hổ bếp nấu
        CHEF_BBQ_ACCESS,                //Quyền truy cập tả hổ bếp nướng
        BAR_ACCESS,                     //Quyền truy cập kho bia
        ADDITION_FEE_MANAGER,           //Tạo, chỉnh sửa phiếu thu chi
        BYPASS_CHECKIN,                 //Không cần checkin vẫn làm việc được
        REPORT_MATERIAL,                //Báo cáo nguyên liệu
        TMS_SALARY_TARGET_AND_RANKING_BOARD,//Xem doanh số và bảng xếp hạng trên App TECHRES-TMS
        MASTER_CHEF_BONUS_FOOD_REVIEW,  //Hưởng doanh số đánh giá món dành cho bếp trưởng
        CHEF_BONUS_FOOD_REVIEW,         //Hưởng doanh số đánh giá món dành cho bếp viên
        CHECK_KPI_TASK_MANAGER,         //Chấm điểm KPI công việc nhân viên cấp dưới
        APPROVE_EMPLOYEE_ADVANCE_SALARY,//Kiểm duyệt ứng lương cho nhân viên cấp dưới
        TMS_CHAT_GROUP_ADMIN,           //Kiểm soát mọi group chat trong App TECHRES-TMS
        TMS_VIEW_EMPLOYEE_SALARY,       //Xem bảng lương toàn bộ nhân viên trên App TECHRES-TMS
        ADDITION_FEE_APPROVEMENT,       //Kiểm duyệt, sửa phiếu thu chi đã duyệt
        EMPLOYEE_REPORT,                //Báo cáo nhân sự
        EMPLOYEE_REVENUE_REPORT,        //Báo cáo doanh thu theo nhân viên
        AREA_REVENUE_REPORT,            //Báo cáo doanh thu theo khu vực
        DASHBOARD_WEB_ACCESS,           //Quyền truy cập web dashboard
        PAID_EMPLOYEE_ADVANCE_SALARY,   //Chi ứng lương cho nhân viên
        APPROVE_SALARY_TABLE,           //Duyệt bảng lương chờ chi
        PAID_SALARY_TABLE,              //Chi bảng lương và tạo phiếu chi lương cho nhân viên
        REPORT_PROFIT_BY_FOOD,          //Báo cáo lợi nhuận theo món ăn
        REPORT_BUSINESS_RESULT,          //Báo cáo kết quả kinh doanh
        ACTION_ON_FOOD_AND_TABLE,       //Cho phép chuyển món giữa các bếp
        PAY_ADDITION_FEE,              //Chi phiếu chi
        APPROVE_ADDITION_FEE,          //Duyệt phiếu chi
        BRAND_MANAGER,//Truy cập các chi nhánh bên thuộc thương hiệu
        RESTAURANT_MANAGER,             //Truy cập các thương hiệu
        ADD_EXTRA_CHARGE_TO_ORDER, // Thêm phụ thu

        UPDATE_BRAND_INFO, //Sửa thông tin nhãn hiệu
        RESTAURANT_BRAND_FOOD_MANAGER,
        REMOVE_EXTRA_CHARGE_FROM_ORDER

    }
}
