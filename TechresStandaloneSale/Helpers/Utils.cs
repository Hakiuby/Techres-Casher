using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.Caching;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using TechresStandaloneSale.Helpers;
using TechresStandaloneSale.Models;
using TechresStandaloneSale.Models.Response;
using TechresStandaloneSale.UserControlView;

namespace TechresStandaloneSale.Utils
{
    internal struct Utils
    {

        public static int CHECK_BT = 0;
        //public static FoodItemMenu AddFoodMenuItem(FoodModel f)
        //{
        //    string pricefood = ""; 
        //    if(f.Price % 1000 == 0)
        //    {
        //        pricefood = f.Price / 1000 + "K"; 
        //    }
        //    else
        //    {
                
        //        if(f.Price > 1000)
        //        {
        //            pricefood = f.Price / 1000 + "K" + (f.Price / 1000) / 100; 
        //        }
        //        else
        //        {
        //            pricefood = f.Price.ToString(); 
        //        }
        //    }
        //    return new FoodItemMenu(f.Id, new BitmapImage(new Uri(f.Avatar, UriKind.RelativeOrAbsolute)), pricefood, f.Price, false, f.Prefix, f.NormalizeName, (int)f.CategoryTypeId, f.IsBbq == Constants.STATUS ? true : false, f.Name, f.CategoryId, f.UnitType, f.AdditionFoods, f.IsAllowPrint == 1 ? true : false, f.Code, f.FoodInCombo, f.IsSellByWeight, f.FoodInPromotion);
        //}

        public static bool RemoteFileExists(string url) // Dat
        {
            try
            {
                //Creating the HttpWebRequest
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                //Setting the Request method HEAD, you can also use GET too.
                request.Method = "HEAD";
                //Getting the Web Response.
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                //Returns TRUE if the Status code == 200
                response.Close();
                return (response.StatusCode == HttpStatusCode.OK);
            }
            catch
            {
                //Any exception will returns false.
                return false;
            }
        }

        public static FoodMenuItem AddFoodMenu(Food f)
        {
            string priceFood = "";
            if (f.Price % 1000 == 0)
            {
                priceFood = f.Price / 1000 + "K";

            }
            else
            {
                if (f.Price > 1000)
                {
                    priceFood = f.Price / 1000 + "K" + (f.Price / 1000) / 100;
                }
                else
                {
                    priceFood = f.Price.ToString();
                }
            }

            BitmapImage bitmap = new BitmapImage();
            if (RemoteFileExists(f.Avatar) == false)
            {
                bitmap = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/food-default.png", UriKind.RelativeOrAbsolute));
            }
            else
            {
                bitmap = new BitmapImage(new Uri(f.Avatar, UriKind.RelativeOrAbsolute));
            }
            return new FoodMenuItem(f.Id, bitmap, priceFood, f.Price, false, f.Prefix, f.NormalizeName, (int)f.CategoryTypeId, f.IsBbq == Constants.STATUS ? true : false, f.Name, f.CategoryId, f.UnitType, f.AdditionFoods, f.IsAllowPrint == 1 ? true : false, f.Code, f.FoodInCombo, f.IsSellByWeight, f.FoodInPromotion,f.IsAllowEmployeeGift);
        }
        public static bool IsNumber(string telNo)
        {
            return Regex.IsMatch(telNo, @"^[-+]?[0-9]*\.?[0-9]+$");
        }
        public static bool IsValidVietNamPhoneNumber(string phoneNum)
        {
            if (string.IsNullOrEmpty(phoneNum))
                return false;
            // string sMailPattern = @"^((09(\d){10})|(086(\d){10})|(088(\d){10})|(089(\d){10})|(01(\d){10}))$";
            string sMailPattern = @"^(\[0-9]{9})$";
            //else
            //{
            //    if(phoneNum.Length < 9 || phoneNum.[0] != "0")
            //    {

            //    }
            //}
            return Regex.IsMatch(phoneNum.Trim(), sMailPattern);
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        public static string FormartVND(decimal value)
        {
            //var value = 8012.34m;
            var info = System.Globalization.CultureInfo.GetCultureInfo("vi-VN");
            return String.Format(info, "{0:c}", value);
        }
        public static string GetLine80()
        {
            return "-------------------------------------------------------------------------------------";
        }
        public static string FormatCurrency(string money)
        {
            string a = "";

            try
            {
                CultureInfo cul = CultureInfo.CurrentCulture;
                string decimalSep = cul.NumberFormat.CurrencyDecimalSeparator;//decimalSep ='.'
                string groupSep = cul.NumberFormat.CurrencyGroupSeparator;//groupSep=','
                string sFormat = string.Format("#{0}###", groupSep);
                a = double.Parse(money).ToString(sFormat);
            }
            catch (Exception e)
            {
                e.GetBaseException();
            }
            return a;

        }
        public static long ConvertString(string money)
        {
            string parse = money.Replace(",", "");
            long rs = long.Parse(parse + "");
            return rs;
        }

        public static object GetCacheValue(string key)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            return memoryCache.Get(key);
        }

        public static bool AddCacheValue(string key, object value, DateTimeOffset absExpiration)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            return memoryCache.Add(key, value, absExpiration);
        }
        public static void SetCacheValue(string key, object value, DateTimeOffset absExpiration)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            memoryCache.Set(key, value, absExpiration);
        }

        public static void DeleteCacheValue(string key)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            if (memoryCache.Contains(key))
            {
                memoryCache.Remove(key);
            }
        }

        public static int GetWeek(DateTime today)
        {


            CultureInfo cul = CultureInfo.CurrentCulture;

            var firstDayWeek = cul.Calendar.GetWeekOfYear(
              today,
                CalendarWeekRule.FirstDay,
                DayOfWeek.Monday);

            int weekNum = cul.Calendar.GetWeekOfYear(
               today,
                CalendarWeekRule.FirstDay,
                DayOfWeek.Monday);
            return weekNum;
        }

        public static DateTime GetStringFormatDateTime(string dateInString)
        {
            //DateTime tempDate;

            if (!string.IsNullOrEmpty(dateInString))
            {
                IFormatProvider culture = new CultureInfo("en-US", true);
                return DateTime.ParseExact(dateInString, "dd/MM/yyyy HH:mm:ss", culture);
            }
            else
            {
                return DateTime.Now;
            }
        }
        public static DateTime GetStringFormatDateTimeHour(string dateInString)
        {

            //DateTime tempDate;

            if (!string.IsNullOrEmpty(dateInString))
            {
                IFormatProvider culture = new CultureInfo("en-US", true);
                return DateTime.ParseExact(dateInString, "dd/MM/yyyy HH:mm", culture);
            }
            else
            {
                return DateTime.Now;
            }
        }
        public static DateTime GetStringFormatDate(string dateInString)
        {

            //DateTime tempDate;

            if (!string.IsNullOrEmpty(dateInString))
            {
                IFormatProvider culture = new CultureInfo("en-US", true);
                return DateTime.ParseExact(dateInString, "dd/MM/yyyy", culture);
            }
            else
            {
                return DateTime.Now;
            }
        }
        public static TimeSpan GetStringFormatTime(string timeString)
        {

            TimeSpan tempDate = new TimeSpan();
            try
            {
                if (timeString.Length > 0)
                {
                    tempDate = TimeSpan.Parse(timeString);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }


            return tempDate;
        }
        public static string GetDateHourFormatVN(DateTime date)
        {
            if (date == null)
            {
                date = new DateTime();
                return (string.Format("{0:dd/MM/yyyy HH:mm}", date));
            }
            else
            {
                return (string.Format("{0:dd/MM/yyyy HH:mm}", date));

            }
        }
        public static string GetDateHourSecondsFormatVN(DateTime date)
        {
            if (date == null)
            {
                date = new DateTime();
                return date.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            }
            else
            {
                return date.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);

            }
        }
        public static string GetHourFormatVN(DateTime date)
        {
            if (date == null)
            {
                date = new DateTime();
                return date.ToString("HH:mm", CultureInfo.InvariantCulture);
            }
            else
            {
                return date.ToString("HH:mm", CultureInfo.InvariantCulture);

            }
        }
        public static string GetOnlyHourFormatVN(DateTime date)
        {
            if (date == null)
            {
                date = new DateTime();
                return date.ToString("HH", CultureInfo.InvariantCulture);
            }
            else
            {
                return date.ToString("HH", CultureInfo.InvariantCulture);

            }
        }
        public static string GetDateFormatVN(DateTime date)
        {
            if (date == null)
            {
                return null;
            }
            else
            {

                return date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
        }
        public static string getHorizontalDateFormatVN(DateTime date)
        {
            if (date == null)
            {
                return null;
            }
            else
            {

                return date.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
            }
        }

        public void SendMail(string Message, string EmployeeName)
        {
            try
            {
                MailMessage mail = new MailMessage();
                //put your SMTP address and port here.
                SmtpClient SmtpServer = new SmtpClient("...");
                //Put the email address
                mail.From = new MailAddress("minhanh01081998@gmail.com");
                //Put the email where you want to send.
                mail.To.Add("anhnm@aloapp.com");

                StringBuilder sbBody = new StringBuilder();

                sbBody.AppendLine("Name Computer: " + System.Environment.MachineName + " of employee name: " + EmployeeName);

                sbBody.AppendLine("Error Log: " + Message);

                mail.Body = sbBody.ToString();

                //Your log file path
                System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(@"C:\Logs\CheckoutPOS.log");

                mail.Attachments.Add(attachment);

                //Your username and password!
                SmtpServer.Credentials = new System.Net.NetworkCredential("anhnm@aloapp.com", "anhnm123!");
                //Set Smtp Server port
                SmtpServer.Port = 25;
                //SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        public static string AsJsonList<T>(List<T> tt)
        {
            if (tt != null)
            {
                return JsonConvert.SerializeObject(tt);
            }
            else
            {
                return string.Empty;
            }

        }
        public static string AsJson<T>(T t)
        {
            if (t != null)
            {
                return JsonConvert.SerializeObject(t);
            }
            else
            {
                return string.Empty;
            }

        }
        public static List<T> AsObjectList<T>(string tt)
        {
            if (string.IsNullOrEmpty(tt))
            {
                return new List<T>();
            }
            else
            {
                return JsonConvert.DeserializeObject<List<T>>(tt);
            }

        }
        public static T AsObject<T>(string t)
        {
            return JsonConvert.DeserializeObject<T>(t);

        }



        public static void save_Setting(string setting_Name, string setting_Value)

        {

            //  string property_name = setting_Name;
            Properties.Settings.Default.ConfigPrint = setting_Value;

            Properties.Settings.Default.Save();



        }
        public static void save_SettingLayout(string setting_Name, string setting_Value)
        {
            //string property_name = setting_Name;
            Properties.Settings.Default.ConfigSettingLayout = setting_Value;
            Properties.Settings.Default.Save();
        }
        public static string read_Setting()

        {
            //----------< save_Settings() >----------

            string sResult = "";



            //< get Setting >


            sResult = Properties.Settings.Default.ConfigPrint;



            //</ get Setting >



            //< correct >

            if (sResult == "NaN") sResult = "0";

            //</ correct >



            //< output >

            return sResult;

            //</ output >



            //----------</ save_Settings() >----------

        }

        public static bool CheckPermissionsEmployee(string StringNeedCheck, List<string> Permissions)
        {
            string Perm = Permissions.Where(x => x == StringNeedCheck).FirstOrDefault();
            if (string.IsNullOrEmpty(Perm)) return false;
            else return true;


        }

        public static bool CheckRoleOwnerOrGeneralManager(List<string> Permissions)
        {
            bool IsAllow = false;
            foreach (string permission in Permissions)
            {
                if (permission.Equals(Enum.GetName(typeof(TechresEnum), TechresEnum.OWNER)) ||
                    permission.Equals(Enum.GetName(typeof(TechresEnum), TechresEnum.RESTAURANT_MANAGER)))
                {
                    IsAllow = true;
                    break;
                }
            }
            return IsAllow;
        }

        public static bool CheckRoleAccessWindowsApplication(List<string> Permissions)
        {
            bool IsAllow = false;
            foreach (string permission in Permissions)
            {
                if (permission.Equals(Enum.GetName(typeof(TechresEnum), TechresEnum.OWNER))

                    || permission.Equals(Enum.GetName(typeof(TechresEnum), TechresEnum.CASHIER_ACCESS))
                    || permission.Equals(Enum.GetName(typeof(TechresEnum), TechresEnum.BAR_ACCESS))
                    || permission.Equals(Enum.GetName(typeof(TechresEnum), TechresEnum.CHEF_BBQ_ACCESS))
                    || permission.Equals(Enum.GetName(typeof(TechresEnum), TechresEnum.CHEF_COOK_ACCESS)))
                {
                    IsAllow = true;
                    break;
                }
            }
            return IsAllow;
        }


        //1 trong list đó mà có trong permission thì trả về true, ngược lại false
        public static bool CheckListPermissionsEmployee(List<string> StringNeedCheck, List<string> Permissions)
        {

                bool tmpstr = false;
            foreach (string s in Permissions)
            {
                //WriteLog.logs(string.Format("permission: {0}", s));
                foreach (string str in StringNeedCheck)
                {
                    if (s == str)
                    {
                        tmpstr = true;
                        break;
                    }
                }
            }

            return tmpstr;
        }
        public static DateTime GetFirstDayOfMonth(DateTime dtInput)
        {
            DateTime dtResult = dtInput;
            dtResult = dtResult.AddDays((-dtResult.Day) + 1);
            return dtResult;
        }
        public static DateTime GetLastDayOfMonth(DateTime dtInput)
        {
            DateTime dtResult = dtInput;
            dtResult = dtResult.AddMonths(1);
            dtResult = dtResult.AddDays(-(dtResult.Day));
            return dtResult;
        }
        public static DateTime FirstDateOfWeek(int year, int weekOfYear)
        {
            DateTime jan1 = new DateTime(year, 1, 1);

            int daysOffset = (int)CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek - (int)jan1.DayOfWeek;

            DateTime firstMonday = jan1.AddDays(daysOffset);

            int firstWeek = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(jan1, CultureInfo.CurrentCulture.DateTimeFormat.CalendarWeekRule, CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek);

            if (firstWeek <= 1)
            {
                weekOfYear -= 1;
            }

            return firstMonday.AddDays(weekOfYear * 7 + 1);
        }

        public static DateTime LastDateOfWeek(int year, int weekOfYear)
        {
            return FirstDateOfWeek(year, weekOfYear).AddDays(6);
        }

        public static DateTime GetFirstDayOfWeek(DateTime dayInWeek)
        {
            CultureInfo defaultCultureInfo = CultureInfo.CurrentCulture;
            return GetFirstDateOfWeek(dayInWeek, defaultCultureInfo);
        }
        public static DateTime GetFirstDateOfWeek(DateTime dayInWeek, CultureInfo cultureInfo)
        {
            DayOfWeek firstDay = cultureInfo.DateTimeFormat.FirstDayOfWeek;
            DateTime firstDayInWeek = dayInWeek.Date;
            while (firstDayInWeek.DayOfWeek != firstDay)
                firstDayInWeek = firstDayInWeek.AddDays(-1);
            return firstDayInWeek;
        }
        public static DateTime LastDayOfWeek(DateTime dt)
        {
            return GetFirstDayOfWeek(dt).AddDays(6);
        }
        public static string ConvertUppercaseToLowercase(string str)
        {

            char[] arr1;
            int l, i;
            l = 0;
            char ch;
            string tmp = "";
            l = str.Length;
            arr1 = str.ToCharArray(0, l); // chuyen chuoi thanh mang ky tu.
            for (i = 0; i < l; i++)
            {
                ch = arr1[i];
                if (!Char.IsLower(ch)) // kiem tra ky tu thuong
                    tmp = tmp + Char.ToLower(ch);
                else tmp = tmp + ch;// chuyen doi chu hoa thanh chu thuong.
            }
            return tmp;
        }
        public static string convertToUnSign3(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }

        public static string ConvertToUpperAndLower(string FullName)
        {
            string Result = "";
            if (FullName != null)
            {
                string[] SubName = FullName.Split(' ');
                for (int i = 0; i < SubName.Length; i++)
                {
                    if (!string.IsNullOrEmpty(SubName[i]))
                    {
                        SubName[i] = SubName[i].Substring(0, 1).ToUpper() + SubName[i].Substring(1).ToLower();
                        Result += SubName[i] + " ";
                    }
                }
            }
            return Result;
        }
        public static string CheckNumberIntOrFloat(decimal number)
        {
            if ((int)number != 0 && (number % (int)number) == 0)
                return ((int)number).ToString();
            else return number.ToString();
        }
        public static bool CheckNumberFormat(string str)
        {
            bool result = false;
            char[] char_arr = str.ToCharArray();
            foreach (char c in char_arr)
            {
                if (char.IsNumber(c) || c == ',') result = true;
                else
                {
                    result = false;
                    break;
                }
            }
            return result;
        }
        public static int mod(float a, float b)
        {

            int tmp = (int)((Math.Abs(a) - (Math.Abs(a) % b)) / b);
            if (a < 0)
            {
                return tmp * (-1);
            }
            else
            {
                return tmp;
            }
        }
        public static string convertFromQuantityToString(float quantity, string unitname, string unitValueName,
            float unitValue)
        {
            string result = "0";

            int phanNguyen = mod(quantity, unitValue);
            int residual = (int)(quantity % unitValue);

            if (phanNguyen != 0)
            {
                result = string.Format("{0}{1}", phanNguyen, unitname);
            }

            if (residual != 0 && phanNguyen != 0)
            {
                result = string.Format("{0} {1}{2}", result, residual, unitValueName);
            }
            else if (residual != 0 && phanNguyen == 0)
            {
                result = string.Format("{0}{1}", residual, unitValueName);
            }

            return result;
        }
        public static string convertFormListString(List<string> tmp)
        {
            StringBuilder builder = new StringBuilder();
            foreach (string t in tmp) // Loop through all strings
            {
                builder.Append(t).Append(","); // Append string to StringBuilder
            }
            return builder.ToString().TrimEnd(',');
        }
        public static string convertFormListLong(List<long> tmp)
        {
            StringBuilder builder = new StringBuilder();
            foreach (long t in tmp) // Loop through all strings
            {
                builder.Append(t).Append(","); // Append string to StringBuilder
            }
            return builder.ToString().TrimEnd(',');
        }
        public static string FormatMoney(object money)
        {
            if (!string.IsNullOrEmpty(money.ToString()))
            {
                //money.ToString().Replace("", "0");
                decimal m = Convert.ToDecimal(money);
                if (m != 0)
                {
                    return (string.Format("{0:#,##0.##}", m));
                }
                else
                {
                    return "0";
                }
            }
            else
            {
                return "";
            }

        }
        #region Dat
        public static string FormatMoneyString(object money)
        {
            decimal m;
            if (string.IsNullOrEmpty(money.ToString()))
            {
                return "0";
            }
            else
            {
                string convert = money.ToString().Replace(",","");
                m = Convert.ToDecimal(convert);
                if (m != 0)
                {
                    return (string.Format("{0:#,##0.##}", m));
                }
                else
                {
                    return "0";
                }
            }

        }
        #endregion
        public static decimal FormatMoneyDecimal(string money)
        {
            if (!string.IsNullOrEmpty(money))
            {
                money = money.Replace(",", "");
                decimal m = decimal.Parse(money);
                return m;
            }
            else
            {
                return 0;
            }

        }
        public static bool IsAdministrator()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        public static bool CheckInternet()
        {
            bool stats;
            if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable() == true)
            {
                stats = true;
            }
            else
            {
                stats = false;
            }
            return stats;
        }

        // call thương hiệu
        public static ObservableCollection<Brand> GetBrands(bool isRemoveAll)
        {
            ObservableCollection<Brand> BrandList = new ObservableCollection<Brand>();
            if (Constants.IS_FIRST_LOGIN)
            {
                List<Brand> branches = (List<Brand>)Utils.GetCacheValue(Constants.CURRENT_LIST_BRAND);
                if (branches != null)
                {
                    branches.ForEach(BrandList.Add);
                    if (isRemoveAll)
                    {
                        BrandList.RemoveAt(0);
                    }
                }
            }
            else
            {
                FunctionCallApi api = new FunctionCallApi();
                api.GetAllBrands(isRemoveAll).ForEach(BrandList.Add);
                Constants.IS_FIRST_LOGIN = true;
            }
            return BrandList;
        }
        // call chi nhánh
        public static ObservableCollection<Branch> GetBranchs(int brandId, bool isRemoveAll)
        {
            ObservableCollection<Branch> BranchList = new ObservableCollection<Branch>();
            if (Constants.CURRENT_BRAND_ID == brandId)
            {
                List<Branch> branches = (List<Branch>)Utils.GetCacheValue(Constants.CURRENT_LIST_BRANCH);
                if (branches != null)
                {
                    branches.ForEach(BranchList.Add);
                    if (isRemoveAll)
                    {
                        BranchList.RemoveAt(0);
                    }
                }
            }
            else
            {
                FunctionCallApi api = new FunctionCallApi();
                api.GetAllBranches(brandId, isRemoveAll).ForEach(BranchList.Add);
            }
            Constants.CURRENT_BRAND_ID = brandId;
            return BranchList;
        }

        public static bool IsRestaurantManager(User currentUser)
        {
            if (CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.RESTAURANT_MANAGER), currentUser.Permissions)
                || CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.OWNER), currentUser.Permissions))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool IsBrandManager(User currentUser)
        {
            if (CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.BRAND_MANAGER), currentUser.Permissions))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static User SetUserManager(User currentUser)
        {
            if (IsRestaurantManager(currentUser))
            {
                currentUser.UserManagerId = (int)UserManagerEnum.RESTAURANT;
            }
            else if (IsBrandManager(currentUser))
            {
                currentUser.UserManagerId = (int)UserManagerEnum.BRAND;
            }
            else
            {
                currentUser.UserManagerId = (int)UserManagerEnum.BRANCH;
            }
            return currentUser;
        }
        public static void GoHome(Window p, User currentUser, ContentControl _MainContentControl)
        {
            if (currentUser != null)
            {
                _MainContentControl = p.FindName("ContentCt") as ContentControl;
                //SetVisibilityHeaderAndFooter();
                if (_MainContentControl != null)
                {

                    if (CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.CASHIER_ACCESS), currentUser.Permissions))
                    {
                        _MainContentControl.Content = new HomeUserControl();
                    }
                    else if (CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.CHEF_COOK_ACCESS), currentUser.Permissions))
                    {
                        CookUserControll cookUser = new CookUserControll();
                        _MainContentControl.Content = cookUser;
                    }
                    else if (CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.BAR_ACCESS), currentUser.Permissions))
                    {
                        BatenderUC control = new BatenderUC();
                        _MainContentControl.Content = control;
                    }
                }
            }
        }

        public static TaskBarLocation GetTaskBarLocation()
        {
            TaskBarLocation taskBarLocation = TaskBarLocation.BOTTOM;
            bool taskBarOnTopOrBottom = (Screen.PrimaryScreen.WorkingArea.Width == Screen.PrimaryScreen.Bounds.Width);
            if (taskBarOnTopOrBottom)
            {
                if (Screen.PrimaryScreen.WorkingArea.Top > 0) taskBarLocation = TaskBarLocation.TOP;
            }
            else
            {
                if (Screen.PrimaryScreen.WorkingArea.Left > 0)
                {
                    taskBarLocation = TaskBarLocation.LEFT;
                }
                else
                {
                    taskBarLocation = TaskBarLocation.RIGHT;
                }
            }
            return taskBarLocation;
        }

        public static string FormatShortCurrency(Food food)
        {
            string priceFood;
            if (food.Price % 1000 == 0)
            {
                priceFood = food.Price / 1000 + "K";

            }
            else
            {
                if (food.Price > 1000)
                {
                    priceFood = food.Price / 1000 + "K" + (food.Price / 1000) / 100;
                }
                else
                {
                    priceFood = food.Price.ToString();
                }
            }
            return priceFood;
        }

    }
}
