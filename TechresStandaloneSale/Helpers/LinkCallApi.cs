namespace TechresStandaloneSale.Helpers
{
    public static class LinkCallApi
    {
        // employee
        public static readonly string API_GET_MATERIAL_SUPPLIER_BRANCH = "/application/branches/{0}/suppliers/{1}/materials";
        public static readonly string API_GET_MATERIAL_SYSTEM = "/general/materials";
        public static readonly string API_GET_MATERIAL_RESTAURANT = "/application/restaurants/{0}/supplier/{1}/materials";
        public static readonly string API_EMPLOYEE_LIST = "/application/employees";
        public static readonly string API_EMPLOYEE_LIST_DEBT = "/application/employees/can-debt-bill-employees";
        public static readonly string API_EMPLOYEE_SALARY_ADDITION_LIST = "/application/employee-salary-additions";
        public static readonly string API_EMPLOYEE_DEBT_UNIFORM_LIST = "/application/employee-salary-additions/debit-uniform-money";

        public static readonly string API_EMPLOYEE_DEBT_UNIFORM_CREATE = "/application/employee-salary-additions/debit-uniform-money/{0}";
        public static readonly string API_EMPLOYEE_SALARY_ADDITION_CREATE = "/application/employee-salary-additions/{0}";

        public static readonly string API_EMPLOYEE_ADVANCED_SALARY_LIST = "/application/employee-salary-additions/addvanced-salary";

        public static readonly string API_EMPLOYEE_ADVANCED_SALARY_PAY = "/application/employee-salary-additions/addvanced-salary/{0}/pay";

        public static readonly string API_EMPLOYEE_ADVANCED_SALARY_CANCEL = "/application/employee-salary-additions/addvanced-salary/{0}/cancel";
        public static readonly string API_EMPLOYEE_SALARY_ADDITION_CANCEL = "/application/employee-salary-additions/{0}/cancel";
        public static readonly string API_MANAGE_BONUS_EMPLOYEE_ADD = "/application/salary-targets/manage";

        public static readonly string API_FOOD_TAKE_AWAY = "/api/large/application/foods";
        
        public static readonly string API_CREATE_EMPLOYEE = "/application/employees/create";

        public static readonly string API_SALARY_TARGET_LIST = "/application/salary-targets";
        public static readonly string API_REVENUE_BY_PAYMENT_METHOD = "/application/reports/revenue-by-payment-method";

        public static readonly string API_ROLE_LIST = "/application/employee-roles";

        public static readonly string API_RANK_LIST = "/application/employee-ranks";

        public static readonly string API_SALARY_LEVEL = "/application/salary-levels";

        public static readonly string API_SALARY_TABLE = "/application/salary-table";

        public static readonly string API_SALARY_TABLE_CHECKIN_HISTORY = "/application/salary-table/checkin-history";

        public static readonly string API_SALARY_TABLE_POINT_HISTORY = "/application/salary-table/personal-point";

        public static readonly string API_SALARY_TABLE_PUNISH = "/application/salary-table/punish";

        public static readonly string API_WORKING_SESSION = "/application/employees/get-all-working-session";

        public static readonly string API_WORKING_SESSION_BY_EMPLOYEE = "/application/employees/{0}/branch-working-sessions";

        public static readonly string API_ALL_PRIVILEGES = "/application/employees/get-all-privileges";

        public static readonly string API_WORKING_SESSION_CREATE = "/application/working-sessions/create";

        public static readonly string API_WORKING_SESSION_UPDATE = "/application/working-sessions/update";

        public static readonly string API_UPDATE_VERSION = "/api/check-version?os_name=windows_sale";

        //    public static readonly string API_UPDATE_VERSION = "/api/check-version?os_name=windows";

        public static readonly string API_EMPLOYEES_PRIVILEGES = "/application/employees/{0}/privileges";
        public static readonly string API_EMPLOYEES_ROLE_PRIVILEGES = "/application/employee-roles/{0}/privileges";

        public static readonly string API_ROLE_PRIVILEGE = "/application/employee-privilege-groups";

        public static readonly string API_ALL_PRIVILEGE = "/application/privileges";

        public static readonly string API_EMPLOYEES_MANAGE_PRIVILEGES = "/application/employees/{0}/privileges";

        public static readonly string API_EMPLOYEE_ROLE_MANAGE_PRIVILEGES = "/application/employee-roles/{0}/change-privileges";

        public static readonly string API_EMPLOYEE_ROLE_CREATE = "/application/employee-roles/create";
        public static readonly string API_EMPLOYEE_ROLE_UPDATE = "/application/employee-roles/{0}/update";
        public static readonly string API_PRIVILEGES_UPDATE = "/application/employee-privilege-groups/{0}/update";

        public static readonly string API_EMPLOYEE_ROLE_CHANGE_STATUS = "/application/employee-roles/{0}/change-status";

        public static readonly string API_EMPLOYEES_UPDATE = "/application/employees/{0}/update";

        public static readonly string API_UPDATE_EMPLOYEE_RANK = "/application/employee-ranks/manage";

        public static readonly string API_UPDATE_EMPLOYEE_SALARY = "/application/salary-levels/manage";

        public static readonly string API_CONFIRM_SALARY_TABLE = "/application/salary-table/confirm";
        public static readonly string API_PAID_SALARY_TABLE = "/application/salary-table/paid";

        public static readonly string API_EMPLOYEES_CHANGE_STATUS = "/application/employees/{0}/change-status";

        public static readonly string API_EMPLOYEES_MANAGE = "/application/employees/{0}/manage";

        //order detail 
        public static readonly string API_GET_ORDERS_DETAIL = "/mobile/order-details/processing";
        public static readonly string API_GET_ORDERS_DETAIL_HISTORY = "/application/order-details/history";

        public static readonly string API_GET_ORDER = "/mobile/order-details/";
        public static readonly string API_GET_ORDER_EXTRA_CHARGE = "/application/order-extra-charges/";

        public static readonly string API_GET_ORDER_DETAIL_NOTE = "/application/order-detail-notes";

        public static readonly string API_MANAGE_ORDER_DETAIL_NOTE = "/application/order-detail-notes/manage";

        public static readonly string API_CHANGE_STATUS = "/mobile/orders/{0}/change-order-detail-status";

        public static readonly string API_APPROVED_DRINK_OTHER = "/mobile/orders/{0}/approve-drink";

        public static readonly string API_ORDER_DETAIL_IS_PRINT = "/mobile/order-details/is-print?order_id={0}";

        public static readonly string API_GET_ALL_REJECT_REASON = "/application/reject-reason";

        public static readonly string API_CREATE_REJECT_REASON = "/application/reject-reason/create";

        public static readonly string API_UPDATE_REJECT_REASON = "/application/reject-reason/{0}/update";

        public static readonly string API_REMOVE_REJECT_REASON = "/application/reject-reason/{0}/remove";
        public static readonly string API_TABLE_OPEN_BY_ID = "/mobile/tables/{0}/open"; 

        // order

        public static readonly string GET_ALL_ORDERS = "/application/orders";

        public static readonly string GET_ORDER_BY_ID = "/mobile/orders/{0}";
        public static readonly string GET_ORDER_DETAIL_CUSTOMER = "/application/orders/{0}/order-detail-customer";
        public static readonly string GET_ORDER_ADDTIONAL_VAT = "/mobile/orders/{0}/addtional-vat";
        public static readonly string GET_ORDER_BY_TABLE_ID = "/mobile/tables/{0}";

        public static readonly string API_MAKE_PAYMENNT = "/mobile/orders/{0}/complete";

        public static readonly string API_ORDER_CONFIRM = "/application/orders/{0}/confirm";
        public static readonly string API_ORDER_CANCEL = "/application/orders/{0}/cancel";

        public static readonly string API_MAKE_PAYMENT_WAITING = "/mobile/orders/{0}/waiting-complete";

        public static readonly string API_CREATE_ORDER = "/mobile/orders/create";
        public static readonly string API_UPDATE_SHIPPING_ORDER = "/mobile/orders/{0}/shipping-address";

        public static readonly string API_COMPLETE_WITHOUT_RP = "/mobile/orders/{0}/complete";

        public static readonly string API_ADD_FOOD_ORDER = "/mobile/orders/{0}/add-food";
        public static readonly string API_CREATE_SPECIAL_FOOD = "/mobile/orders/{0}/add-special-food?branch_id={1}";
        public static readonly string API_RETURN_BEER_MULTI = "/mobile/orders/{0}/return-multi-beer"; // prefix, id, branch_id


        public static readonly string API_ASSIGN_ORDER_TO_CUSTOMER = "/application/orders/{0}/assign-to-customer";

        public static readonly string API_ASSIGN_ORDER_TO_PROMOTION = "/application/orders/{0}/apply-promotion-voucher";

        public static readonly string API_UN_ASSIGN_ORDER_TO_PROMOTION = "/application/orders/{0}/un-apply-promotion-voucher";

        public static readonly string API_UN_ASSIGN_ORDER_TO_CUSTOMER = "/application/orders/{0}/un-assign-to-customer";

        public static readonly string API_APPLY_DISCOUNT = "/mobile/orders/{0}/apply-discount";//toan

        public static readonly string API_APPLY_VAT = "/mobile/orders/{0}/apply-vat";
        public static readonly string API_APPLY_RETURN_BOOKING = "/application/orders/{0}/return-deposit";

        public static readonly string API_UPDATE_CUSTOMER_SLOT_NUMBER = "/application/orders/{0}/update-customer-slot-number";

        public static readonly string API_WAITING_COMPLETE_NO_RP = "/mobile/orders/{0}/waiting-complete";//toan

        public static readonly string API_ORDER_HISTORY = "/application/orders/history";

        public static readonly string API_ORDER_DEBIT = "/mobile/orders/{0}/debt";
        public static readonly string API_ORDER_DEBIT_CUSTOMER = "/application/orders/{0}/customers/{1}/debt-order";

        public static readonly string API_ORDER_CANCEL_FOOD = "/mobile/orders/{0}/cancel-food";//toan

        public static readonly string API_ORDER_HISOTRY_DEBIT = "/application/orders/debt-history";

        public static readonly string API_ORDER_CANCEL_REASON = "/application/reject-reason";

        public static readonly string API_ORDER_ALL_PAYMENT_REASON = "/application/addition-fee-reason";

        public static readonly string API_ORDER_CREATE_PAYMENT_REASON = "/application/addition-fee-reason/manage";

        public static readonly string API_ORDER_CHANGE_STATUS_PAYMENT_REASON = "/application/addition-fee-reason/{0}/change-status";

        public static readonly string API_ORDER_REMOVE_PAYMENT_REASON = "/application/addition-fee-reason/{0}/remove";

        public static readonly string API_UPDATE_FOOD_IS_OUT_STOCK_BRACH_KITCHEN = "/mobile/foods/update-food-is-out-stock-branch-kitchen";

        public static readonly string API_ORDER_CANCEL_FOOD_V2 = "/mobile/orders/{0}/cancel-multi-order-detail";// prefix, id, branch_id



        public static readonly string API_ORDER_IS_PRINT = "/mobile/order-details/is-print?order_id={0}";
        public static readonly string API_HISTORY_PRINT = "/mobile/order-details/order-waiting-print"; 

        public static readonly string API_RETURN_BEER = "/api/mobile/orders/{0}/return-multi-beer?branch_id={1}";

        // user
        public static readonly string API_LOGIN = "/api/employees/login";
        public static readonly string API_LOGIN_CHART = "/api/login";
        public static readonly string API_LOGIN_CHAT = "/api/oauth-login-nodejs/login";
        public static readonly string API_GET_CONFIG_CHAT = "/api/oauth-configs-nodejs/get-configs";
        public static readonly string API_CONFIG = "api/configs";

        public static readonly string CURRENT_USER = "CURRENT_USER";

        public static readonly string API_RESTAURANT_INFO = "/application/restaurants/{0}";

        public static readonly string API_BRANCH_INFO = "/application/branches/{0}";

        //activity log
        public static readonly string API_ACTIVITY_LOG = "/api/activity-log/employee";
        public static readonly string API_ACTIVITY_LOG_ORDER = "/api/activity-log-object-id";

        //notification
        public static readonly string API_IS_VIEWD_EMPLOYEE_NOTIFICATION = "/api/notification/employees/viewed";
        public static readonly string API_EMPLOYEE_NOTIFICATION = "/api/notification/employees";

        // category

        public static readonly string API_CATEGORY_ALL = "/application/categories/";
        public static readonly string API_CATEGORY_BY_BRANCH_KITCHEN = "/application/categories/get_for_bar_or_kitchen";

        public static readonly string API_CATEGORY_CREATE = "/application/categories/create";

        public static readonly string API_CATEGORY_UPDATE = "/application/categories/{0}/update";

        // branch
        public static readonly string API_ALL_BRANCHES = "/application/branches";
        public static readonly string API_ALL_BRANCH_KITCHEN = "/application/branches/kitchen-places";
        //brand
        public static readonly string API_ALL_BRAND = "/application/restaurant-brands";
        public static readonly string API_ALL_BRAND_INFO = "/application/restaurant-brands/{0}";
        //areas
        public static readonly string API_AREAES = "/mobile/areas";

        public static readonly string API_TOP_UP_CARD = "/application/restaurant-top-up-cards";
        public static readonly string API_TOP_UP_CARD_CUSTOMER = "/application/customers/{0}/top-up";

        // working_day

        public static readonly string API_EMPOLYEE_WOKING_DAY = "/application/reports/employee-wroking-day";
        public static readonly string API_EMPOLYEE_CHECKIN_HISTORY = "/application/salary-table/checkin-history";

        public static readonly string API_EMPOLYEE_CHECKIN = "/application/employees/check-in";

        //forgot password
        public static readonly string API_FORGOT_PASSWORD = "/api/employees/forgot-password";

        public static readonly string API_VERIFY_CHANGE_PASSWORD = "/api/employees/verify-change-password";

        public static readonly string API_CHANGE_PASSWORD = "/application/employees/{0}/change-password";

        // reports data

        public static readonly string REPORT_TODOAY_BUSINESS_SITUATION = "/application/reports/today-business-sitiuation";

        public static readonly string REPORT_ESTIMATE_REVENUE_COST_PROFIT = "/application/reports/estimate-revenue-cost-profit";

        public static readonly string REVENUE_CASHIER = "/application/reports/synthesis-report";

        public static readonly string REVENUE_RANK_EMPLOYEE = "/application/reports/employees/revenue-rank";

        public static readonly string REPORT_DRINK = "/application/reports/drinks";

        public static readonly string REPORT_COOK = "/application/reports/foods";

        public static readonly string SYNTHESIS_REPORT_MATERIAL_QUANTITY = "/application/reports/systhesis-material-quantity-report";

        public static readonly string SYNTHESIS_REPORT_MATERIAL_FOOD = "/application/reports/food-material-synthesis-report";

        public static readonly string SYNTHESIS_REPORT_FOOD = "/application/reports/food-revenue";



        public static readonly string SYNTHESIS_REPORT_REVENUE = "/application/reports/synthesis-report-revenue";

        public static readonly string SYNTHESIS_REPORT_REVENUE_AREA = "/application/reports/revenue-rank-by-area";

        public static readonly string SYSTHESIS_REPORT_REVENUE = "/application/reports/synthesis-report";

        public static readonly string SYNTHESIS_REPORT_VAT = "/application/reports/synthesis-report-vat";
        public static readonly string SYNTHESIS_REPORT_EXTRA_CHARGE = "/application/reports/extra-charge-report";

        public static readonly string SYNTHESIS_REPORT_DISCOUNT = "/application/reports/synthesis-report-discount";

        public static readonly string SYNTHESIS_REPORT_ORDER = "/application/reports/synthesis-report-order";

        public static readonly string SYNTHESIS_REPORT_CUSTOMER = "/application/reports/synthesis-report-customer";

        public static readonly string SYNTHESIS_REPORT_GIFT_FOOD = "/application/reports/synthesis-report-gift-food";
        public static readonly string SYNTHESIS_REPORT_FOOD_COMBO = "/application/reports/food-combo-report";


        public static readonly string SYNTHESIS_REPORT_TODAY = "/application/reports/today_systhesis_revenue_report";

        public static readonly string TMP_REVENUE = "/application/orders/tmp-revenue";

        public static readonly string END_WORKING_SESSION = "/application/order-session/working-session-value";

        public static readonly string HISTORY_END_WORKING_SESSION = "/application/order-session/end-working-sessions";
        public static readonly string HISTORY_END_WORKING_SESSION_DETAIL = "/application/order-session/end-working-sessions/{0}";

        public static readonly string ORDER_SESSION_ORDER_HISTORY = "/application/order-session/order-history";

        public static readonly string ORDER_SESSION_BOOKING_HISTORY = "/application/order-session/deposit-history";

        public static readonly string REPORT_SYSTHESIS_MATERIAL_REPORT = "/application/reports/systhesis-material-report";

        public static readonly string MATERIAL_REMAIN_BY_DATE = "/general/materials/{0}/remain-by-date";

        public static readonly string MATERIAL_DAILY_INVENTORY_REPORT_CALCULATE = "/general/warehouse/daily-inventory-report/calculate";

        public static readonly string WARHOUSE_DAILY_INVENTORY_REPORT = "/general/warehouse/daily-inventory-report";
        public static readonly string API_REPORT_REVENUE_FOOD_ADMIN = "/application/reports/food-revenue";
        public static readonly string API_REPORT_FORM_SERVICE = "/application/reports/revenue-by-order-method";

        public static readonly string API_REPORT_REVENUE_RANK_BY_EMPLOYEE = "/application/reports/employees/revenue-rank";
        public static readonly string API_REPORT_BUSINESS_RESULT = "/application/reports/business-situation-detail";

        public static readonly string MATERIAL_DAILY_INVENTORY_REPORT_EXISTS_DAY = "/general/warehouse/daily-inventory-report/exists-today";

        public static readonly string CHECK_SESSION = "/application/order-session/check-working-session";

        public static readonly string OPEN_SESSION = "/application/order-session/open-session";
        public static readonly string CREATE_ORDER_SESSION_EMPLOYEE = "/application/order-session-employees/create"; 

        public static readonly string CLOSE_SESSION = "/application/order-session/close-session";

        public static readonly string STATISTIC_REPORT_AGE = "/application/reports/employees";

        public static readonly string STATISTIC_REPORT_BUSINESS_RESULT = "/application/reports/business-result";
        //table

        public static readonly string API_GET_TABLE_ALL = "/application/tables/manage";
        public static readonly string API_GET_TABLE_TAKE_AWAY = "/application/tables/take-away";

        public static readonly string API_ALL_TABLE_OPENING = "/mobile/tables";

        public static readonly string API_TABLE_MOVE = "/mobile/tables/{0}/move";

        public static readonly string API_TABLE_MERGE = "/mobile/tables/{0}/merge";

        public static readonly string API_TABLE_MOVE_FOOD = "/mobile/tables/{0}/move-food";

        public static readonly string API_TABLE_CLOSE = "/mobile/tables/{0}/close";

        public static readonly string API_SET_TABLE = "/application/orders/{0}/set-table";

        public static readonly string API_CREATE_TABLE = "/mobile/tables/manage";

        public static readonly string API_CREATE_AREAES = "/mobile/areas/manage";
        // setting

        public static readonly string API_SETTING = "/api/employees/settings";

        //customer
        public static readonly string API_CUSTOMER_SEARCH_BY_PHONE = "/application/customers/search";
        public static readonly string API_CUSTOMER_PROFILE = "/application/customers/{0}";
        public static readonly string API_CUSTOMER_DEBT_ALL = "/application/orders/customer-debts";
        public static readonly string API_CUSTOMER_DEBT_DETAIL = "/application/orders/customers/{0}/debt-order";
        public static readonly string API_CUSTOMER_DEBT_CONFIRM = "/application/orders/customers/debt-order/confirm";
        public static readonly string API_CUSTOMER_REGISTER = "/application/customers/create";

        //booking
        public static readonly string API_CREATE_BOOKING = "/application/bookings/create";
        public static readonly string API_UPDATE_BOOKING = "/application/bookings/{0}/update";
        public static readonly string API_ARRANGE_TABLE_BOOKING = "/application/bookings/{0}/tables";
        public static readonly string API_START_BOOKING = "/application/bookings/{0}/start";
        public static readonly string API_SETUP_BOOKING = "/application/bookings/{0}/set-up";
        public static readonly string API_CONFIRM_BOOKING = "/application/bookings/{0}/confirm";
        public static readonly string API_CONFIRM_DEPOSIT_BOOKING = "/application/bookings/{0}/confirm-deposit";
        public static readonly string API_CANCEL_BOOKING = "/application/bookings/{0}/cancel"; 
        public static readonly string API_BOOKING = "/application/bookings";
        public static readonly string API_BOOKING_TABLE = "/application/bookings/tables";
        public static readonly string API_RECEIVE_DEPOSIT_BOOKING = "/application/bookings/{0}/receive-deposit";
        public static readonly string API_RETURN_DEPOSIT_BOOKING = "/application/bookings/{0}/return-deposit";
        //food
        public static readonly string API_GET_FOOD_FOR_BRANCH_KITCHEN = "/mobile/foods/get-food-for-branch-kitchen";
        public static readonly string API_FOOD_LIST = "/application/foods";
        public static readonly string API_FOOD_IN_BRANCH_LIST = "/application/foods/branch";
        public static readonly string API_FOOD_UN_ASSIGN_LIST = "/application/foods/un-exist-branch";
        public static readonly string API_RESTAURANT_EXTRA_CHANGE = "/application/order-extra-charges";
        public static readonly string API_FOOD = "/mobile/foods";
        public static readonly string API_FOOD_DETAIL = "/application/foods/{0}";

        public static readonly string API_FOOD_CREATE = "/application/foods/create";

        public static readonly string API_FOOD_UPLOAD_IMAGE = "/application/foods/avatar";

        public static readonly string API_UNIT_CREATE = "/application/foods/unit/{0}";

        public static readonly string API_FOOD_UPDATE = "/application/foods/update";

        public static readonly string API_IMPORT_EXCEL_FOOD = "/application/foods/import-foods";

        public static readonly string API_FOOD_CHANGE_STATUS = "/application/foods/{0}/change-status";

        public static readonly string API_FOOD_UNIT = "/application/foods/unit";

        public static readonly string API_FOOD_UPDATE_KITCHEN = "/application/foods/kitchen-place";

        public static readonly string API_UPLOAD_AVATAR = "/application/foods/{0}/upload-avatar";

        public static readonly string API_UPLOAD_IMAGE = "/api-upload/upload-file-by-user/{0}/{1}";

        public static readonly string API_GET_LINK_IMAGE = "/api-upload/get-link-file-by-user";

        public static readonly string API_GET_FOOODS_KITCHEN = "/application/foods/kitchen"; 



        //warehouse
        public static readonly string API_MATERIAL_CATEGORY = "/general/materials/material-category";

        public static readonly string API_MATERIAL_ALL = "/application/branches/{0}/materials";

        public static readonly string API_ADDITION_FEES = "/application/addition-fees";
        public static readonly string API_ADDITION_FEES_DETAIL = "/application/addition-fees/{0}";

        public static readonly string API_RESTAURANT_BUDGET = "/application/restaurant-budgets";

        public static readonly string API_CALCULATE_RESTAURANT_BUDGETS = "/application/restaurant-budgets/calculate";

        public static readonly string API_MATERIAL_UNIT = "/general/materials/unit";

        public static readonly string API_MATERIAL_MANAGE = "/general/materials/manage";

        public static readonly string API_MATERIAL_CHANGE_STATUS = "/general/materials/{0}/change-status";

        public static readonly string API_FOOD_PRICE = "/application/foods/update-price-by-category";

        public static readonly string API_FOOD_LIST_COUPON_EDIT_PRICE = "/application/foods/price-adjustment";

        public static readonly string API_FOOD_LIST_COUPON_EDIT_PRICE_COMPLETED = "/application/foods/price-adjustment/{0}/apply";

        public static readonly string API_FOOD_LIST_TAKE_AWAY_ADD = "/application/foods/take-away";
        public static readonly string API_ADD_RESTAURANT_EXTRA = "/mobile/order-extra-charges/{0}/add-extra-charges";
        public static readonly string API_CANCEL_RESTAURANT_EXTRA = "/mobile/order-extra-charges/{0}/cancel-extra-charge";
        public static readonly string API_FOOD_ASSIGN_BRANCH = "/application/foods/assign-branch";
        public static readonly string API_FOOD_UN_ASSIGN_BRANCH = "/application/foods/un-assign-branch";

        public static readonly string API_FOOD_LIST_COUPON_EDIT_PRICE_CANCEL = "/application/foods/price-adjustment/{0}/cancel";

        public static readonly string API_CREATE_STOCK_TAKING = "general/warehouse/create-inventory-report";

        public static readonly string API_UPDATE_STOCK_TAKING = "general/warehouse/inventory-report/1/update";

        public static readonly string API_LIST_STOCK_TAKING = "general/warehouse/inventory-report";

        public static readonly string API_CONFIRM_STOCK_TAKING = "general/warehouse/inventory-report/{0}/confirm";

        public static readonly string API_CANCEL_STOCK_TAKING = "general/warehouse/inventory-report/{0}/cancel";

        public static readonly string API_DETAIl_STOCK_TAKING = "general/warehouse/inventory-report/{0}";

        public static readonly string API_DETAIl_ADDITION_FEE = "/application/addition-fees/{0}";

        public static readonly string API_CREATE_ADDITION_FEE = "/application/restaurant-budgets/create";


        public static readonly string API_RESTAURANT_KITCHEN_PLACE_PRINTER = "/application/restaurant-kitchen-places/printer";

        //suppliers
        public static readonly string API_MANAGE_CHECKIN_HISTORY = "/application/salary-table/checkin-history/{0}";
        public static readonly string API_RESTAURANT_SUPPLERS_RESTAURANT = "/application/restaurants/{0}/supplier";
        public static readonly string API_RESTAURANT_SUPPLERS_BY_BRANCH = "/application/branches/{0}/suppliers";
        public static readonly string API_RESTAURANT_SUPPLERS_DETAIL = "/application/restaurants/{0}/supplier/{1}";
        public static readonly string API_RESTAURANT_ALL_SUPPLERS_DETAIL = "/api/large/general/suppliers/{0}";

        public static readonly string API_SUPPLERS_DEBT = "/general/suppliers/debt";

        public static readonly string API_SUPPLERS_DEBT_HISTORY = "/general/suppliers/{0}/debt";

        public static readonly string API_MANAGE_SUPPLERS = "/application/restaurants/{0}/supplier/{0}";

        public static readonly string API_WAREHOUSE_CREATE_SESSION = "/general/warehouse/create-session";

        public static readonly string API_WAREHOUSE_RETURN_MATERIAL = "/general/warehouse/return-material";

        public static readonly string API_MANAGE_MATERIAL_FOOD = "/application/foods/manage-material";

        public static readonly string API_MANAGE_MATERIAL_CATEGORY = "/general/materials/material-category/manage";

        public static readonly string API_CREATE_DAILY_INVENTORY_REPORT = "/general/warehouse/daily-inventory-report/create";

        public static readonly string API_UPDATE_DAILY_INVENTORY_REPORT = "/general/warehouse/daily-inventory-report/{0}/update";

        public static readonly string API_WAREHOUSE_UPDATE_SESSION = "/general/warehouse/warehouse-session/{0}/update";

        public static readonly string API_WAREHOUSE_CONFIRM_SESSION = "/general/warehouse/warehouse-session/{0}/confirm";

        public static readonly string API_WAREHOUSE_CANCEL_SESSION = "/general/warehouse/warehouse-session/{0}/cancel";

        public static readonly string API_CONFIRM_ADDITION_FEE = "/application/addition-fees/{0}/confirm";
        public static readonly string API_PAID_ADDITION_FEE = "/application/addition-fees/{0}/paid";
        public static readonly string API_CANCEL_ADDITION_FEE = "/application/addition-fees/{0}/cancel";
        public static readonly string API_CANCEL_PAYMENT_ADDITION_FEE = "/application/addition-fees/{0}/cancel_payment";
        public static readonly string API_REFUND_ADDITION_FEE = "/application/addition-fees/{0}/refund";
        public static readonly string API_APPROVE_PAYMENT_ADDITION_FEE = "/application/addition-fees/{0}/approve-payment";

        public static readonly string API_CONFIRM_RESTAURANT_BUDGET = "/application/restaurant-budgets/{0}/confirm";

        public static readonly string API_CANCEL_RESTAURANT_BUDGET = "/application/restaurant-budgets/{0}/cancel";

        public static readonly string API_UPDATE_ADDITION_FEE = "/application/addition-fees/{0}/update";

        public static readonly string API_WAREHOUSE_SESSION = "/general/warehouse/warehouse-session";

        public static readonly string API_MATERIAL_MANAGE_UNIT = "/general/materials/manage-unit";
        public static readonly string API_DETAIL_WAREHOUSE_SESSION = "/general/warehouse/warehouse-session/{0}";

        public static readonly string API_GET_FOOD_MATERIAL = "/application/foods/{0}/material";

        public static readonly string API_ALL_PAYMENT_REASON = "/application/addition-fee-reason";
        public static readonly string API_ALL_PAYMENT_REASON_TYPE = "/application/addition-fee-reason/types";
        public static readonly string API_SYNC_DATA = "/application/sync";
        public static readonly string API_ALL_PAYMENT_REASON_MANAGE = "/application/addition-fee-reason/manage";

        public static readonly string API_ADD_ADDITION_FEE = "/application/addition-fees/create";
        public static readonly string API_RESTAURANT_SUPPLIER_DEBT_DETAIL = "/general/restaurant-suppliers/{0}/debt";


        //promotion
        //public static readonly string API_PROMOTION_APPLY = "/application/restaurant-promotions/apply";
        public static readonly string API_PROMOTION_DETAIL = "/application/restaurant-promotions/{0}";


        public static readonly string API_KITCHEN_LIST = "/application/restaurant-kitchen-places";


        // Node log
        public static readonly string API_NODE_CONFIG = "/api/oauth-configs-nodejs/get-configs";
        public static readonly string API_NODE_LOGIN = "/api/oauth-login-nodejs/login";
        public static readonly string REPORT_ORDER_PROFIT_FOOD = "/api/report/order/profit-food";

        // Node Report 
        public static readonly string API_REPORT_ORDER_GIFT_FOOD = "/api/report/order/gift-food?branch_id={0}&brand_id={1}&type={2}&time={3}";
        public static readonly string API_REPORT_ORDER_FOOD = "/api/report/order/food?branch_id={0}&brand_id={1}&type={2}&time={3}&is_goods={4}";
        public static readonly string API_REPORT_ORDER_CANCEL_FOOD = "/api/report/order/cancel-food?brand_id={0}&branch_id={1}&type={2}&time={3}";

        public static readonly string API_FOOD_V2 = "/api/report/order/profit-food";
        public static readonly string API_EXTRA_FOOD_V2 = "api/report/order/surcharge";
        public static readonly string API_GIFT_FOOD_ORDER = "/application/orders/{0}/gift-food";

        // Get Banner Ads Restauant 
        public static readonly string API_GET_LIST_RESTAURANT_PRIVATE_ADVERTS = " api/large/application/restaurant-private-adverts"; 
    }
}


