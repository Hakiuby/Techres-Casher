using System.Collections.Generic;
using System.Net.WebSockets;
using System.Windows;
using TechresStandaloneSale.Models;

namespace TechresStandaloneSale.Helpers
{   
    /// <summary>
    /// The valid encoding options able to be passed to the QR object
    /// </summary>      
    public static class Constants
    {

        #region Server Live     
        public static string SERVER_OAUTH_DOMAIN = "https://api.gateway.techres.vn/api/queues";
        // Live RealTime
        public static string SERVER_REALTIME = "http://api.realtime.techres.vn:1483";
        //live
        public static long IS_MODE_SERVER = 1;
        #endregion

        #region Server Beta 
        ////public static string SERVER_OAUTH_DOMAIN = "https://beta.api.gateway.techres.vn/api/queues";
        ////public static string SERVER_OAUTH_DOMAIN = "http://techres.ddns.net:8092/api/queues";
        //public static string SERVER_OAUTH_DOMAIN = "http://172.16.2.243:8092/api/queues";
        //// beta realtime
        //public static string SERVER_REALTIME = "http://beta.api.realtime.techres.vn:1483";
        ////beta
        //public static long IS_MODE_SERVER = 0;

        #endregion


        public static string ADS_DOMAIN = "https://api.upload.techres.vn";
        public static string SERVER_OFFLINE_DOMAIN = "https://api.upload.techres.vn";
        public static string KEY_CALL_API = "api/large";
      

       


        public static string BackupServerOuathDomain;
        public static string DateFormat = "dd/MM/yyyy";
        public static readonly string MAIN_COLOR_SYSTEM = "#ffa233";
        public static readonly string BLUE_GG_COLOR = "#4285F4";
        public static readonly string BLUE_TABLE_OPENING_COLOR = "#418bca";
        public static readonly string YELLOW_GG_COLOR = "#FBBC05";
        public static readonly string GREEN_GG_COLOR = "#34A853";
        public static readonly string RED_GG_COLOR = "#EA4335"; 
        public static readonly string GRAY_COLOR = "#F0F0F0";
        public static readonly string LINE_COLOR = "#B4B1B0";
        //   public static readonly string WAVE_COLOR = "#66A5AD";
        public static readonly string WAVE_COLOR = "#418bca";
        public static readonly string PINK_TULIP_COLOR = "#F18D9E";
        public static readonly string BLUE_BACKGROUND = "#0072BC";
        public static readonly string GRAY_DARK_BACKGROUND = "#6D7278";
        public static readonly string GRAY_LIGTH_BACKGROUND = "#EEEAEA";
        public static readonly string CURRENT_USER = "CURRENT_USER";
        public static readonly string CURRENT_USER_CHART = "CURRENT_USER_CHART";
        public static readonly string CURRENT_LIST_CATEGORY = "CURRENT_LIST_CATEGORY";
        public static readonly string CURRENT_LIST_FOOD = "CURRENT_LIST_FOOD";
        public static readonly string CURRENT_RESTAURANT = "CURRENT_RESTAURANT";
        public static readonly string CURRENT_BRANCH = "CURRENT_BRANCH";
        public static readonly string CURRENT_LIST_BRANCH = "CURRENT_LIST_BRANCH";  
        //    public static readonly string CURRENT_LIST_KITCHEN = "CURRENT_LIST_KITCHEN";
        //   public static readonly string CURRENT_KITCHEN = "CURRENT_KITCHEN";
        public static readonly string LINK_IMAGE_LOGO = "LINK_IMAGE_LOGO";
        public static readonly string SAVE_USERNAME = "SAVE_USERNAME";
        public static readonly string SAVE_PASSWORD = "SAVE_PASSWORD";
        public static readonly string CURRENT_DEVICE = "CURRENT_DEVICE";
        public static readonly string CURRENT_SETTING = "CURRENT_SETTING";
        public static readonly string CURRENT_LIST_BRAND = "CURRENT_LIST_BRAND";
        public static readonly string CURRENT_LIST_COUPON_EDIT_PRICE = "CURRENT_LIST_COUPON_EDIT_PRICE";
        public static readonly string CURRENT_CONFIG = "CURRENT_CONFIG";
        public static readonly double MAX_EXPIRE_CACHE = 1;
        public static string BROAD_CAST_KEY = "BROAD_CAST_KEY";
        public static string CACHE_BROAD_CAST_CLIENT = "CACHE_BROAD_CAST_CLIENT";
        public static string CACHE_DOMAIN_RESTAURANT = "CACHE_DOMAIN_RESTAURANT";
        public static string CACHE_PRINT_SEAFOOD = "CACHE_PRINT_SEAFOOD";
        public static string CACHE_PRINT_COOK = "CACHE_PRINT_COOK";
        public static string CACHE_PRINT_DRINK = "CACHE_PRINT_DRINK";
        public static string CACHE_PRINT_BILL = "CACHE_PRINT_BILL";
        public static string SETTING_PRINT = "SETTING_PRINT";
        public static string SETTING_LAYOUT = "SETTING_LAYOUT";
        public static string SETTING_MIND_USERNAME = "SETTING_MID_USERNAME";
        public static string SETTING_MIND_PASSWORD = "SETTING_MID_USERNAME";
        public static string SETTING_MIND = "SETTING_MID";
        public static string CHOOSE_ALL = " Chọn tất cả";
        public static string MESSAGE_EXCEL_AUTHOR = "ALOAPP COMPANY";
        public static string MESSAGE_EXCEL_FONT_NAME = "Times New Roman";
        public static int MESSAGE_EXCEL_FONT_SIZE_HEARDER = 17;
        public static int MESSAGE_EXCEL_FONT_SIZE = 12;
        public static int CURRENT_SESSION = 0;
        public static int ALL = -1;
        public static int CANCEL_REASON_OTHER = 0;
        public static int DEBT_DATE = 1;
        public static int DEBT_MONTH = 2;
        public static int ROUND_DEFAULT = 3;
        public static int ROUND_TWO = 2;
        public static int ROUND_ZERO = 0;

        //ORDER STATUS
        public static string STATUS_OPENING_WAITING_PAYMENT_AND_WAITING_COMPLETED = "1,0,4,7";

        // ORDER DETAIL STATUS
        public static string STATUS_ORDER_DETAIL_CANCEL_OUTSTOCK = "3,4";

        //CATEGORY TYPE
        public static string STATUS_CATEGORY_TYPE_COOK = "1,4,5";
        public static string STATUS_CATEGORY_TYPE_DRINK_OTHER = "2,3,5";

        // FOOD TYPE
        public static int STATUS_FOOD_TYPE_COOK = 0;
        public static int STATUS_FOOD_TYPE_BBQ = 1;

        // APPROVED DRINK
        public static string STATUS_IS_APPROVED = "1";
        public static string STATUS_IS_NOT_APPROVED = "0";
        public static long STATUS_IS_NOT_APPROVED_LONG = 0;
        public static long STATUS_IS_APPROVED_LONG = 1;
        public static int STATUS = 1;
        public static int NOT_STATUS = 0;
        public static int CURRENT_RESTAURANT_BUDGET_ID = 0;
        public static int MATERIAL_UNIT = 0;
        public static int MATERIAL_UNIT_VALUE = 1;

        // DEFINE PAGEGINATION 
        public static int OFFSET = 1;
        public static string IsBBQ = "-1";
        public static int REPORT_DAY = 1;
        public static int REPORT_WEEK = 2;
        public static int REPORT_MONTH = 3;
        public static int REPORT_QUARTER = 4;
        public static int REPORT_YEAR = 5;
        public static int TYPEDATE = 1;
        public static string STATUS_ALL_FOOD = "-1";
        public static string STATUS_FOOD = "0";
        public static string STATUS_BBQ = "1";

        //category
        public static string CATEGORY_TYPE_STRING;

        //Restaurant
        public static int RESTAURANT_SCALE_FULL = 1;
        public static int RESTAURANT_SCALE_SMALL = 2;

        // other
        public static int TYPE_IS_GIFT_FOOD_MENU = 1;
        public static int TYPE_IS_NOT_GIFT_FOOD_MENU = 0;
        public static long ORDER_ID_EDIT_ORDER;

        //flag branch to reload user control
        public static int BRANCH_FLAG = 1;
        public static int ACTION_BOOKING = 0;
        public static int ACTION_BOOKING_DEPOSIT = 1;
        public static int ACTION_BOOKING_RETURN_DEPOSIT = 2;
        public static int TYPE_ORDER_DONE_HISTORY = 1;
        public static int TYPE_ORDER_ALL_HISTORY = 0;
        public static int TYPE_BBQ = 1;
        public static int TYPE_NOT_BBQ = 0;
        public static string REPORT_DATE_TIME_NOW = "date_time_now";
        public static string REPORT_WEEK_TIME = "week";
        public static string REPORT_MONTH_TIME = "month";
        public static string REPORT_QUARTER_TIME = "quarter";
        public static string REPORT_YEAR_TIME = "year";
        public static string Domain;
        public static bool IS_FIRST_BRAND = false;
        public static bool IS_FIRST_COOK_CATEGORY = true;
        public static bool IS_FIRST_FOOD = true;
        public static bool IS_FIRST_CATEGORY = true;
        public static bool IS_FIRST_LOGIN = true;
        public static bool IS_FIRST_CONFIG = true;
        public static long CURRENT_BRANCH_ID = ALL;
        public static long CURRENT_BRAND_ID = 0;
        public static bool IS_MACHINE_POS = true;

        //takeaway
        public static int TAKE_AWAY = 1;
        public static int NOT_TAKE_AWAY = 0;

        // Payment & receipt type
        public static int RECEIPT = 0;
        public static int PAYMENT = 1;

        //second window
        public static Window WINDOW_SECOND;

        //link socket local
        public static string WEBSOCKET_DOMAIN;
        public static bool IS_NETWORK_ONLINE;
        public static ClientWebSocket CLIENT_WEB_SOCKET = null;

      //  public static List<Kitchen> CURRENT_LIST_KITCHEN_OBJECT;
      //  public static Kitchen CURRENT_KITCHEN_OBJECT;

    }

}
