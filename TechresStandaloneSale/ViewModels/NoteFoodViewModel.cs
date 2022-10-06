using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using TechresStandaloneSale.Helpers;
using TechresStandaloneSale.Interface;
using TechresStandaloneSale.Models;
using TechresStandaloneSale.Models.Response;
using TechresStandaloneSale.Services;
using TechresStandaloneSale.Views.Dialogs;

namespace TechresStandaloneSale.ViewModels
{
    public class NoteFoodViewModel : BaseViewModel, ICacheService, IDeserializer, IErrorLogger
    {
        private ObservableCollection<OrderNoteDetailResponseData> _TagSuggestList = new ObservableCollection<OrderNoteDetailResponseData>();
        public ObservableCollection<OrderNoteDetailResponseData> TagSuggestList { get => _TagSuggestList; set { _TagSuggestList = value; OnPropertyChanged("TagSuggestList"); } }

        public string _Note;
        public string Note
        {
            get
            {
                return _Note;
            }
            set
            {
                _Note = value;
                OnPropertyChanged("Note");
            }
        }

        public ICommand AddCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand ChooseNoteSuggestCommand { get; set; }
        public BillResponse OrderFoodNote;
        public bool isCheck;
        public void GetListNote(int brandId, long branchId)
        {
            if (TagSuggestList != null)
                TagSuggestList.Clear();
            else
                TagSuggestList = new ObservableCollection<OrderNoteDetailResponseData>();
            OrderDetailsClient client = new OrderDetailsClient(this, this, this);
            OrderNoteDetailResponse response = client.GetOrderDetailNote(brandId,branchId);
            if (response != null && response.Data != null && response.Data != null)
            {
                response.Data.ForEach(TagSuggestList.Add);
            }
        }
        public NoteFoodViewModel(BillResponse order,int brandId,  long branchId)
        {
            OrderFoodNote = order;
            Note = order.Note;
            isCheck = false;
            GetListNote(brandId,branchId);
            ChooseNoteSuggestCommand = new RelayCommand<OrderNoteDetailResponseData>((p) => { return true; }, p =>
            {
                if (p!= null)
                {
                    if (string.IsNullOrEmpty(Note))
                    {
                        Note = p.Content;
                    }
                    else
                    {
                        Note = string.Format("{0}, {1}", Note, p.Content);
                    }
                }
            });
                AddCommand = new RelayCommand<Window>((p) => { return true; }, p =>
            {
                order.Note = Note;
                OrderFoodNote = order;
                isCheck = true;
                p.Close();
            });
            CloseCommand = new RelayCommand<Window>((p) => { return true; }, p =>
            {
                if (!string.IsNullOrEmpty(Note))
                {
                    ConfirmDeleteWindow confirmDeleteWindow = new ConfirmDeleteWindow();
                    string contentConfirm = MessageValue.MESSAGE_CONFIRM_EDIT_EXIT;
                    string Title = MessageValue.MESSAGE_CONFIRM_EDIT_EXIT_TITLE;
                    string YesContent = MessageValue.MESSAGE_EXIT_WINDOW_CONTENT;
                    string NoContent = MessageValue.MESSAGE_NOT_EXIT_WINDOW_CONTENT;
                    confirmDeleteWindow.DataContext = new ConfirmViewModel(contentConfirm, Title, NoContent, YesContent);
                    confirmDeleteWindow.ShowDialog();
                    var confirm = confirmDeleteWindow.DataContext as ConfirmViewModel;
                    if (confirm.isConfirm)
                    {
                        isCheck = false;
                        p.Close();
                    }
                }
                else
                {
                    isCheck = false;
                    p.Close();
                }
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
