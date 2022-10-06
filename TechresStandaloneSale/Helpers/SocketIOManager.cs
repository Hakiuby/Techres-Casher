using SocketIOClient;
using SocketIOClient.Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechresStandaloneSale.Models;

namespace TechresStandaloneSale.Helpers
{
    public sealed class SocketIOManager
    {
        
        public static SocketIOManager shareInstance = null;
        public SocketIO socket;
        private User currentUser;
        private Kitchen currentKitchen;
        private SettingData currentSetting;
        private SocketIOManager()
        {
            //  var options = new IO.Options() { IgnoreServerCertificateValidation = true, AutoConnect = true, ForceNew = true, ReconnectionDelay = 250 };

            var options = new SocketIOOptions() { Reconnection = true, ReconnectionDelay = 250};
            
            currentUser = (User)Utils.Utils.GetCacheValue(Constants.CURRENT_USER);
            currentKitchen = Utils.Utils.AsObject<Kitchen>(Properties.Settings.Default.CurrentKitchen);
            currentSetting = (SettingData)Utils.Utils.GetCacheValue(Constants.CURRENT_SETTING);
            
            var uri = new Uri(Constants.SERVER_REALTIME);
            socket = new SocketIO(uri, options);


            //var jsonserializer = socket.JsonSerializer

            //socket.JsonSerializer = new NewtonsoftJsonSerializer(socket.Options.EIO); // 
            //socket.JsonSerializer = new NewtonsoftJsonSerializer(); 


            try
            {
                if (!currentSetting.IsWorkingOffline)
                {

                    socket.ConnectAsync(); 
                    socket.OnConnected += (s, e) =>
                    {
                        WriteLog.logs("Connected " + socket.ServerUri);
                        if (currentUser != null)    
                        {
                            if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.BAR_ACCESS), currentUser.Permissions))
                            {
                                string LinkSocketWaiting = string.Format("restaurants/{0}/branches/{1}/kitchens/{2}/order_details", currentUser.RestaurantId, currentUser.BranchId, currentKitchen.Id);
                                SocketIOManager.Instance.socket.EmitAsync("join_room", LinkSocketWaiting);
                                string LinkSocketReturn = string.Format("restaurants/{0}/branches/{1}/order_detail_drink_and_other_only_for_return", currentUser.RestaurantId, currentUser.BranchId);
                                SocketIOManager.Instance.socket.EmitAsync("join_room", LinkSocketReturn);
                                string LinkSocketMoveFood = string.Format("restaurants/{0}/branches/{1}/order_detail_move_food_only", currentUser.RestaurantId, currentUser.BranchId);
                                SocketIOManager.Instance.socket.EmitAsync("join_room", LinkSocketMoveFood);
                                string LinkSocketOrder = string.Format("restaurants/{0}/branches/{1}/orders", currentUser.RestaurantId, currentUser.BranchId);
                                SocketIOManager.Instance.socket.EmitAsync("join_room", LinkSocketOrder);
                                string LinkSocketNotification = string.Format("restaurants/{0}/branches/{1}/kitchens/{2}/notify_winapp_kitchens", currentUser.RestaurantId, currentUser.BranchId,currentKitchen.Id);
                                SocketIOManager.Instance.socket.EmitAsync("join_room", LinkSocketNotification);
                            }

                            if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.CHEF_BBQ_ACCESS), currentUser.Permissions) || Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.CHEF_COOK_ACCESS), currentUser.Permissions))
                            {
                                // restaurants /{ restaurant_id}/ branches /{ branche_id}/ order_detail_food_only
                                // Properties.Settings.Default.SettingChanging += ChangeRealtime;
                                if(currentKitchen != null)
                                {
                                    
                                        string LinkSocketWaiting = string.Format("restaurants/{0}/branches/{1}/kitchens/{2}/order_details", currentUser.RestaurantId, currentUser.BranchId, currentKitchen.Id);

                                        SocketIOManager.Instance.socket.EmitAsync("join_room", LinkSocketWaiting);
                                        WriteLog.logs(LinkSocketWaiting);
                                        string LinkSocketMoveFood = string.Format("restaurants/{0}/branches/{1}/order_detail_move_food_only", currentUser.RestaurantId, currentUser.BranchId);
                                        SocketIOManager.Instance.socket.EmitAsync("join_room", LinkSocketMoveFood);
                                        string LinkSocketOrder = string.Format("restaurants/{0}/branches/{1}/orders", currentUser.RestaurantId, currentUser.BranchId);
                                        SocketIOManager.Instance.socket.EmitAsync("join_room", LinkSocketOrder);
                                        string LinkSocketNotification = string.Format("restaurants/{0}/branches/{1}/kitchens/{2}/notify_winapp_kitchens", currentUser.RestaurantId, currentUser.BranchId, currentKitchen.Id);
                                        SocketIOManager.Instance.socket.EmitAsync("join_room", LinkSocketNotification);
                                 
                                }
                                //string LinkSocketWaiting = string.Format("restaurants/{0}/branches/{1}/kitchens/{2}/order_details", currentUser.RestaurantId, currentUser.BranchId, currentKitchen.Id);
                     
                                //SocketIOManager.Instance.socket.EmitAsync("join_room", LinkSocketWaiting);
                                //WriteLog.logs(LinkSocketWaiting);
                                //string LinkSocketMoveFood = string.Format("restaurants/{0}/branches/{1}/order_detail_move_food_only", currentUser.RestaurantId, currentUser.BranchId);
                                //SocketIOManager.Instance.socket.EmitAsync("join_room", LinkSocketMoveFood);
                                //string LinkSocketOrder = string.Format("restaurants/{0}/branches/{1}/orders", currentUser.RestaurantId, currentUser.BranchId);
                                //SocketIOManager.Instance.socket.EmitAsync("join_room", LinkSocketOrder);
                                //string LinkSocketNotification = string.Format("restaurants/{0}/branches/{1}/kitchens/{2}/notify_winapp_kitchens", currentUser.RestaurantId, currentUser.BranchId, currentKitchen.Id);
                                //SocketIOManager.Instance.socket.EmitAsync("join_room", LinkSocketNotification);
                            }
                            if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.CASHIER_ACCESS), currentUser.Permissions))
                            {
                                string LinkSocket = string.Format("restaurants/{0}/branches/{1}/print", currentUser.RestaurantId, currentUser.BranchId);
                                SocketIOManager.Instance.socket.EmitAsync("join_room", LinkSocket);
                                string LinkSocketOrder = string.Format("restaurants/{0}/branches/{1}/orders", currentUser.RestaurantId, currentUser.BranchId);
                                SocketIOManager.Instance.socket.EmitAsync("join_room", LinkSocketOrder);
                                WriteLog.logs("Join room :" + LinkSocketOrder); 
                                string LinkSocketSeaFood = string.Format("restaurants/{0}/branches/{1}/order_detail_sea_food_only", currentUser.RestaurantId, currentUser.BranchId);
                                SocketIOManager.Instance.socket.EmitAsync("join_room", LinkSocketSeaFood);
                                string LinkSocketNotification = string.Format("restaurants/{0}/branches/{1}/notify_winapp_cashier", currentUser.RestaurantId, currentUser.BranchId);
                                SocketIOManager.Instance.socket.EmitAsync("join_room", LinkSocketNotification);
                            }
                        }
                        string LinkSocketUpdate = string.Format("update_app_windows");
                        SocketIOManager.Instance.socket.EmitAsync("join_room", LinkSocketUpdate);
                    };
                    socket.OnDisconnected += (s, e) =>
                    {
                        WriteLog.logs("Disconnected " + socket.ServerUri);

                    };
                    socket.OnError += (s, e) =>
                    {
                        WriteLog.logs("Error " + e);
                    };
                   
                    }
               
            }
            catch (Exception ex)
            {
                WriteLog.logs(ex.ToString());
                throw;
            }
        }
        public static SocketIOManager Instance
        {
            get
            {
                if (shareInstance == null)
                {
                    shareInstance = new SocketIOManager();
                }
                return shareInstance;
            }
        }
    }

}
