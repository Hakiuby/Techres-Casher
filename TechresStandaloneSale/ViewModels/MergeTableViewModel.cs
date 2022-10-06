using DevExpress.Mvvm.Native;
using Newtonsoft.Json;
using OfficeOpenXml.FormulaParsing.ExpressionGraph.FunctionCompilers;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using TechresStandaloneSale.Helpers;
using TechresStandaloneSale.Interface;
using TechresStandaloneSale.Models;
using TechresStandaloneSale.Models.Response;
using TechresStandaloneSale.Services;
using TechresStandaloneSale.UserControlView;
using TechresStandaloneSale.Views.Dialogs;
using WebSocket4Net.Command;

namespace TechresStandaloneSale.ViewModels
{
    public class MergeTableViewModel : BaseViewModel, ICacheService, IDeserializer, IErrorLogger
    {
        private ObservableCollection<Area> _AreaList = new ObservableCollection<Area>();
        public ObservableCollection<Area> AreaList
        {
            get
            {
                return _AreaList;
            }
            set
            {
                _AreaList = value;
                OnPropertyChanged("AreaList");
            }
        }

        private ObservableCollection<MergeTable> _TableList = new ObservableCollection<MergeTable>();
        public ObservableCollection<MergeTable> TableList
        {
            get
            {
                return _TableList;
            }
            set
            {
                _TableList = value;
                OnPropertyChanged("TableList");
            }
        }
        private string _Title;
        public string Title
        {
            get
            {
                return _Title;
            }
            set
            {
                _Title = value;
                OnPropertyChanged("Title");
            }
        }
        private string _TotalArea;
        public string TotalArea
        {
            get
            {
                return _TotalArea;
            }
            set
            {
                _TotalArea = value;
                OnPropertyChanged("TotalArea");
            }
        }
        private string _TotalTable;
        public string TotalTable
        {
            get
            {
                return _TotalTable;
            }
            set
            {
                _TotalTable = value;
                OnPropertyChanged("TotalTable");
            }
        }

        public ICommand BtnAreaCommand { get; set; }

        public ICommand AddTableCommand { get; set; }

        public ICommand AddCommand { get; set; }

        public ICommand CloseCommand { get; set; }

        public ICommand CancelTableCommand { get; set; }

        public List<MergeTable> mergeTables = new List<MergeTable>();
        public List<int> BookingTables = new List<int>();
        public int AreaId;
        public User currentUser = (User)Utils.Utils.GetCacheValue(Constants.CURRENT_USER);
        public void GetAreas(long branchId)
        {
            long sumTable = 0;
            if (AreaList != null)
            {
                AreaList.Clear();
            }
            else
            {
                AreaList = new ObservableCollection<Area>();
            }
            AreasClient client = new AreasClient(this, this, this);
            AreaResponse response = client.GetListAreaes(currentUser.RestaurantBrandId,branchId, (int)StatusEnum.YES, Constants.ALL);
            if (response != null && response.Status == (int)ResponseEnum.OK && response.Data != null)
            {
                response.Data.ForEach(AreaList.Add);
                TotalArea = string.Format(MessageValue.MESSAGE_FROM_AREA_FORMAT, AreaList.Count);
                foreach(var item in AreaList) // Dat
                {
                    sumTable = sumTable + item.TotalTable;
                }
                TotalTable = string.Format(MessageValue.MESSAGE_FROM_TABLE_FORMAT, sumTable);
            }
        }
        public void GetTable(int tableId,int areaId, long branchId)
        {
            if (TableList != null)
            {
                TableList.Clear();
            }
            else
            {
                TableList = new ObservableCollection<MergeTable>();
            }
            TablesClient tablesClient = new TablesClient(this, this, this);
            MergeTableResponse tableResponse = tablesClient.GetListMergeTableByAreaes(areaId,branchId, Constants.STATUS );
            if (tableResponse != null && tableResponse.Status == (int)ResponseEnum.OK && tableResponse.Data != null)
            {

                //tableResponse.Data.Select(x => new MergeTable()
                //{
                //    Id = x.Id,
                //    IsSelect = x.IsSelect,
                //    Name = x.Name,
                //    SlotNumber = x.SlotNumber,
                //    Status = x.Status,
                //    TableStatus = x.Status
                //}).ForEach(TableList.Add);
                foreach(MergeTable t in tableResponse.Data)
                {
                    t.TableStatus = t.Status;
                    if (t.Id != tableId && t.Status != (int)TableStatusEnum.MERGING)
                        TableList.Add(t);
                }

            }
        }

        public void GetTableBooking(int areaId, int branchId, long bookingId)
        {
            if (TableList != null)
            {
                TableList.Clear();
            }
            else
            {
                TableList = new ObservableCollection<MergeTable>();
            }
            BookingClient bookingClient = new BookingClient(this, this, this);
            TableBookingResponse tableResponse = bookingClient.GetTableBookingResponse(areaId, branchId, bookingId);
            if (tableResponse != null && tableResponse.Status == (int)ResponseEnum.OK && tableResponse.Data != null)
            {
                tableResponse.Data.ForEach(TableList.Add);
            }
        }
        public long CurrentAreaId;
        public MergeTableViewModel(int tableId)
        {
            Title = MessageValue.MESSAGE_FROM_MERGE_TABLE_HEADER;
            GetAreas(currentUser.BranchId);
            if (AreaList != null && AreaList.Count > 0)
            {
                Area area = AreaList[0];
                GetTable(tableId,area.Id, currentUser.BranchId);
                area.isChoose = true;
                int index = AreaList.IndexOf(area);
                AreaList.RemoveAt(index);
                AreaList.Insert(index, area);
                AreaId = area.Id;
            }
            BtnAreaCommand = new RelayCommand<Area>((p) => { return true; }, p =>
            {
                if (p != null)
                {
                    GetTable(tableId,p.Id, p.BranchId);
                    p.isChoose = true;
                    int index = AreaList.IndexOf(p);
                    AreaList.RemoveAt(index);
                    AreaList.Insert(index, p);
                    Area area = AreaList.Where(x => x.Id == AreaId).FirstOrDefault();
                    if (area != null)
                    {
                        area.isChoose = false;
                        int indexarea = AreaList.IndexOf(area);
                        AreaList.RemoveAt(indexarea);
                        AreaList.Insert(indexarea, area);
                        AreaId = p.Id;
                    }

                }
            });

            AddTableCommand = new RelayCommand<MergeTable>((p) => { return true; }, p =>
           {
               if (p != null)
               {
                   int index = TableList.IndexOf(p);
                   if (!mergeTables.Contains(p))
                   {
                       TableList.Remove(p);
                       p.IsSelect = true;
                       TableList.Insert(index, p);
                       mergeTables.Add(p);
                   }
                   else
                   {
                       TableList.Remove(p);
                       p.IsSelect = false;
                       TableList.Insert(index, p);
                       mergeTables.Remove(p);
                   }
               }
           });
            CancelTableCommand = new RelayCommand<MergeTable>((p) => { return true; }, p =>
            {
                if (p != null)
                {
                    int index = TableList.IndexOf(p);
                    TableList.Remove(p);
                    p.IsSelect = false;
                    TableList.Insert(index, p);
                    mergeTables.Remove(p);
                }
            });
            AddCommand = new RelayCommand<Window>((p) => { return true; }, p =>
            {
                if (mergeTables != null && mergeTables.Count > 0)
                {
                    p.Close();
                }
                else
                {
                    NotificationMessage.Error(MessageValue.MESSAGE_FROM_MERGE_TABLE_EMPTY);
                }
            });
            CloseCommand = new RelayCommand<Window>((p) => { return true; }, p =>
            {
                mergeTables.Clear();
                p.Close();
            });
        }

        //chọn booking table

        public MergeTableViewModel(string _Title, long branchId, long bookingId)
        {
            Title = _Title;
            GetAreas(branchId);
            if (AreaList != null && AreaList.Count > 0)
            {
                Area area = AreaList[0];
                GetTableBooking(area.Id,(int) branchId, bookingId);
                CurrentAreaId = area.Id;
            }
            BtnAreaCommand = new RelayCommand<Area>((p) => { return true; }, p =>
            {
                if (p != null && p.Id != CurrentAreaId)
                {
                    GetTableBooking(p.Id, (int)p.BranchId, bookingId);
                    BookingTables.Clear();
                    p.isChoose = true;
                    int index = AreaList.IndexOf(p);
                    AreaList.RemoveAt(index);
                    AreaList.Insert(index, p);
                    Area area = AreaList.Where(x => x.Id == CurrentAreaId).FirstOrDefault();
                    //if (area != null)
                    //{
                    //    area.isChoose = false;
                    //    int indexarea = ListAreas.IndexOf(area);
                    //    ListAreas.RemoveAt(indexarea);
                    //    ListAreas.Insert(indexarea, area);
                    //    areaId = p.Id;
                    //}
                    #region Dat
                    if (area != p)
                    {
                        area.isChoose = false;
                        int indexarea = AreaList.IndexOf(area);
                        AreaList.RemoveAt(indexarea);
                        AreaList.Insert(indexarea, area);
                        CurrentAreaId = p.Id;
                    }
                    else
                    {
                        area.isChoose = true;
                        int indexarea = AreaList.IndexOf(area);
                        AreaList.RemoveAt(indexarea);
                        AreaList.Insert(indexarea, area);
                        CurrentAreaId = p.Id;
                    }
                    #endregion
                }
            });
            AddTableCommand = new RelayCommand<MergeTable>((p) => { return true; }, p =>
            {
                if (p != null)
                {
                    int index = TableList.IndexOf(p);
                    if(!BookingTables.Contains(p.Id))
                    {
                        TableList.Remove(p);
                        p.IsSelect = true;
                        TableList.Insert(index, p);
                        BookingTables.Add(p.Id);
                    }
                    else
                    {
                        TableList.Remove(p);
                        p.IsSelect = false;
                        TableList.Insert(index, p);
                        BookingTables.Remove(p.Id);
                    }                   
                }
            });
            AddCommand = new RelayCommand<Window>((p) => { return true; }, p =>
            {
                if (BookingTables != null && BookingTables.Count > 0)
                {
                    p.Close();
                }
                else
                {
                    NotificationMessage.Error(MessageValue.MESSAGE_FROM_MERGE_TABLE_EMPTY);
                }
            });
            CloseCommand = new RelayCommand<Window>((p) => { return true; }, p =>
            {
                BookingTables = new List<int>();
                p.Close();
            });
        }
   
        public void LogError(Exception ex, string infoMessage)
        {
            throw new NotImplementedException();
        }

        public T Deserialize<T>(IRestResponse response)
        {
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                dynamic jsonResponse = JsonConvert.DeserializeObject(response.Content);
                if (jsonResponse.status == 200)
                {
                    T check = jsonResponse.ToObject<T>();
                    if (check != null)
                    {
                        return check;
                    }
                }
                else
                {
                    NotificationMessage.Error(jsonResponse.message);
                }
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                NotificationMessage.Warning(MessageValue.FORBIDDEN);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                NotificationMessage.Error(MessageValue.INTERNAL_SERVER_ERROR);
            }
            else
            {
                NotificationMessage.Error(response.ErrorMessage);
            }
            return default(T);
        }

        public T Get<T>(string cacheKey) where T : class
        {
            throw new NotImplementedException();
        }


        public void Set(string cacheKey, object item, int minutes)
        {
            throw new NotImplementedException();
        }
    }
}
