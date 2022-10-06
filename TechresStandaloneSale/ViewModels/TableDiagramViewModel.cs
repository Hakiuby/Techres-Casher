using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using TechresStandaloneSale.Helpers;
using TechresStandaloneSale.Interface;
using TechresStandaloneSale.Models;
using TechresStandaloneSale.Models.Response;
using TechresStandaloneSale.Services;
using DevExpress.Mvvm.Native;

namespace TechresStandaloneSale.ViewModels
{
    public class TableDiagramViewModel : BaseViewModel, ICacheService, IDeserializer, IErrorLogger
    {
        private ObservableCollection<Area> listAreas;
        public ObservableCollection<Area> ListAreas
        {
            get
            {
                return listAreas;
            }
            set
            {
                listAreas = value;
                OnPropertyChanged("ListAreas");
            }
        }

        private ObservableCollection<Models.TableItem> tableList;
        public ObservableCollection<Models.TableItem> TableList
        {
            get
            {
                return tableList;
            }
            set
            {
                tableList = value;
                OnPropertyChanged("TableList");
            }
        }
        public bool _DialogHostOpen;
        public bool DialogHostOpen
        {
            get
            {
                return _DialogHostOpen;
            }
            set
            {
                _DialogHostOpen = value;

                OnPropertyChanged("DialogHostOpen");
            }
        }
        private string _ContentTitle { get; set; }
        public string ContentTitle { get => _ContentTitle; set { _ContentTitle = value; OnPropertyChanged("ContentTitle"); } }

        private string _TotalArea { get; set; }
        public string TotalArea { get => _TotalArea; set { _TotalArea = value; OnPropertyChanged("TotalArea"); } }

        private string _TotalTable { get; set; }
        public string TotalTable { get => _TotalTable; set { _TotalTable = value; OnPropertyChanged("TotalTable"); } }

        public User currentUser = (User)Utils.Utils.GetCacheValue(Constants.CURRENT_USER);
        public ICommand BtnAreaes { get; set; }
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand MouseCommand { get; set; }
        public ICommand AddCommand { get; set; }

        public Table table;
        public long areaId;
        public void GetAreas(long branchId)
        {
            long sumtable = 0;
            if (ListAreas == null)
            {
                ListAreas = new ObservableCollection<Area>();
            }
            else
            {
                ListAreas.Clear();
            }
            AreasClient client = new AreasClient(this, this, this);
            //AreaResponse response = client.GetListAreaes(currentUser.RestaurantBrandId,branchId, Constants.STATUS, Constants.ALL);
            AreaResponse response = client.GetListAreaes(currentUser.RestaurantBrandId, branchId, Constants.STATUS, 0);
            if (response != null && response.Data != null && response.Data.Count > 0)
            {
                response.Data.Where(x => x.Name != "MV").ForEach(ListAreas.Add);
                //response.Data.Lists.ForEach(ListAreas.Add);
                foreach(var item in listAreas)
                {
                    sumtable = sumtable + item.TotalTable;
                }    
                TotalArea = string.Format(MessageValue.MESSAGE_FROM_AREA_FORMAT, ListAreas.Count);
                TotalTable = string.Format(MessageValue.MESSAGE_FROM_TABLE_FORMAT, sumtable);
            }
        }
        public Window window;
        public TableDiagramViewModel(string contentTitle)
        {
            LoadedWindowCommand = new RelayCommand<Window>((p) => { return true; }, p =>
            {
                ContentTitle = contentTitle;
                GetAreas(currentUser.BranchId);
                if (ListAreas != null && ListAreas.Count > 0)
                {
                    Area area = ListAreas[0];
                    GetTable(area.Id, false, currentUser.BranchId);
                    area.isChoose = true;
                    int index = ListAreas.IndexOf(area);
                    ListAreas.RemoveAt(index);
                    ListAreas.Insert(index, area);
                    areaId = area.Id;
                }
                window = p;
            });
            CloseCommand = new RelayCommand<Window>((p) => { return true; }, p =>
            {
                p.Close();
            });
            BtnAreaes = new RelayCommand<Area>((p) => { return true; }, p =>
            {
                if (p != null)
                {
                    GetTable(p.Id, false, p.BranchId);
                    p.isChoose = true;
                    int index = ListAreas.IndexOf(p);
                    ListAreas.RemoveAt(index);
                    ListAreas.Insert(index, p);
                    Area area = ListAreas.Where(x => x.Id == areaId).FirstOrDefault();
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
                        int indexarea = ListAreas.IndexOf(area);
                        ListAreas.RemoveAt(indexarea);
                        ListAreas.Insert(indexarea, area);
                        areaId = p.Id;
                    }
                    else
                    {
                        area.isChoose = true;
                        int indexarea = ListAreas.IndexOf(area);
                        ListAreas.RemoveAt(indexarea);
                        ListAreas.Insert(indexarea, area);
                        areaId = p.Id;
                    }
                    #endregion
                }
            });
            MouseCommand = new RelayCommand<TableItem>((p) => { return true; }, p =>
            {
                table = new Table();
                table.Id = (int)p.Id; ;
                table.Name = p.TableName;
                table.TableStatus = p.TableStatus;
                window.Close();
            });
            //MouseCommand = new RelayCommand<TableItem>((p) => { return true; }, (p) => { MessageBox.Show("helo"); });

        }

        public Models.TableItem AddTableItem(Table t)
        {
            if (t.TableStatus == (int)TableStatusEnum.EMPTY)
            {
                return new Models.TableItem(t.Id, t.Name, new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/table_ic_closed.png", UriKind.RelativeOrAbsolute)), t.TableStatus);
            }
            else if (t.TableStatus == (int)TableStatusEnum.USING || t.TableStatus == (int)TableStatusEnum.MERGING)
            {
                return new Models.TableItem(t.Id, t.Name, new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/table_ic_closed.png", UriKind.RelativeOrAbsolute)), t.TableStatus);
            }
            else if (t.TableStatus == (int)TableStatusEnum.BOOKED)
            {
                return new Models.TableItem(t.Id, t.Name, new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/table_ic_booking.png", UriKind.RelativeOrAbsolute)), t.TableStatus);
            }
            else
            {
                return new Models.TableItem(t.Id, t.Name, new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/table_ic_closed.png", UriKind.RelativeOrAbsolute)), t.TableStatus);
            }
        }
        public async void GetTable(int areaId, bool isFull, long branchId)
        {
            //if (TableList != null)
            //{
            //    TableList.Clear();
            //}
            //else
            //{
            //    TableList = new ObservableCollection<Models.TableItem>();
            //}
            TablesClient tablesClient = new TablesClient(this, this, this);
            DialogHostOpen = true;
            TableResponse tableResponse = await System.Threading.Tasks.Task.Run(() => tablesClient.GetListTableByAreaId(areaId, branchId, Constants.ALL));
            if (tableResponse != null && tableResponse.Status == (int)ResponseEnum.OK)
            {
                #region Dat
                if (TableList != null)
                {
                    TableList.Clear();
                }
                else
                {
                    TableList = new ObservableCollection<Models.TableItem>();
                }
                #endregion
                if (isFull)
                {
                    foreach (Table t in tableResponse.Data)
                    {
                        TableList.Add(AddTableItem(t));
                    }
                }
                else
                {
                    foreach (Table t in tableResponse.Data)
                    {
                        if (t.TableStatus == (int)TableStatusEnum.EMPTY)
                        {
                            TableList.Add(AddTableItem(t));

                        }
                        else
                        {
                            TableList.Add(AddTableItem(t));

                        }
                    }
                }
                DialogHostOpen = false;
            }
            else
                DialogHostOpen = false;
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
