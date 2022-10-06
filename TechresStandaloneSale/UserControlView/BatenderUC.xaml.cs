using System;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using TechresStandaloneSale.Models;

namespace TechresStandaloneSale.UserControlView
{
    /// <summary>
    /// Interaction logic for BatenderUC.xaml
    /// </summary>
    public partial class BatenderUC : UserControl
    {
        public BatenderListUC batenderList;
        public HistoryOrderDetailUserControl batenderHistoryUC;
        public DrinkOutStockUC drinkOutOfStock; 


        public bool isButtonHistory = false;
        public bool isButtonOutOfStock = false;
        public bool isButtonAll = false;
        public bool isButtonWorking = false;
        public bool isButtonTakeAway = false; 

        public BatenderUC()
        {
            InitializeComponent();
            //PART_ContentHost_Supplier.Focus();
            //Keyboard.Focus(PART_ContentHost_Supplier);
            //PART_ContentHost_Supplier.Select(0, 0);

            //if (PART_ContentHost_Supplier.Text.Length > 0)
            //{
            //    PART_ContentHost_Supplier.SelectAll();
            //}
        }
        private void PART_ContentHost_Supplier_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.PART_Popup_Supplier.IsOpen = true;
            if (PART_ItemList_Supplier.ItemsSource != null)
            {
                this.PART_ItemList_Supplier.SelectedIndex = 0;
            }
        }
        private void PART_ContentHost_Supplier_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!isButtonOutOfStock)
            {
                this.PART_Popup_Supplier.IsOpen = false;
                if (PART_ItemList_Supplier.ItemsSource != null)
                {
                   CollectionViewSource.GetDefaultView(PART_ItemList_Supplier.ItemsSource).Refresh();
                   CollectionViewSource.GetDefaultView(PART_ItemList_Supplier.ItemsSource).Filter = OrderDetailFilter;
                     //CollectionViewSource.GetDefaultView(PART_ItemList_Supplier.ItemsSource).Filter = TableFilter;
                    this.PART_ItemList_Supplier.SelectedIndex = 0;
                }
                if (string.IsNullOrEmpty(txtFilter.Text))
                {
                    TextInputSearch = "";
                    txtFilter_TextChanged();
                }
            }
            else
            {
                drinkOutOfStock = (DrinkOutStockUC)this.listData.Content;
            }
            if(drinkOutOfStock != null)
            {
                if (drinkOutOfStock.drinkWorking.ItemsSource != null)
                {
                    CollectionViewSource.GetDefaultView(drinkOutOfStock.drinkWorking.ItemsSource).Refresh();
                    CollectionViewSource.GetDefaultView(drinkOutOfStock.drinkWorking.ItemsSource).Filter = DrinkOutOfStockFilter; 
                }
            }
        }
        //private bool TableFilter(object item)
        //{
        //    if (string.IsNullOrEmpty(PART_ContentHost_Supplier.Text))
        //        return true;
        //    var od = (Table)item;
        //    return (od.Name.ToLower().IndexOf(PART_ContentHost_Supplier.Text.ToLower(), StringComparison.OrdinalIgnoreCase) >= 0);
        //}

        public string TextInputSearch = "";
        private bool OrderDetailFilter(object item)
        {
            if (String.IsNullOrEmpty(txtFilter.Text))
                return true;
            var orderDetail = (OrderDetail)item;
            return (orderDetail.FoodName.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0 ||
               orderDetail.TableName.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0
               || orderDetail.EmployeeName.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0
               || orderDetail.Note.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0
               || orderDetail.Prefix.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0
               || orderDetail.NormalizeName.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }
        private void batenderUC_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (!isButtonHistory)
            {
                batenderList = (BatenderListUC)this.listData.Content;
            }
            else
            {
                batenderHistoryUC = (HistoryOrderDetailUserControl)this.listData.Content;
            }

            if (batenderList != null)
            {
                if (batenderList.lvFoodPending.ItemsSource != null)
                {
                    CollectionViewSource.GetDefaultView(batenderList.lvFoodPending.ItemsSource).Refresh();
                    CollectionViewSource.GetDefaultView(batenderList.lvFoodPending.ItemsSource).Filter = OrderDetailFilter;
                }
                if (batenderList.lvFoodWaitingConfirm.ItemsSource != null)
                {
                    CollectionViewSource.GetDefaultView(batenderList.lvFoodWaitingConfirm.ItemsSource).Refresh();
                    CollectionViewSource.GetDefaultView(batenderList.lvFoodWaitingConfirm.ItemsSource).Filter = OrderDetailFilter;
                }
            }
            if (batenderHistoryUC != null)
            {
                if (batenderHistoryUC.lvFoodPending.ItemsSource != null)
                {
                    CollectionViewSource.GetDefaultView(batenderHistoryUC.lvFoodPending.ItemsSource).Refresh();
                    CollectionViewSource.GetDefaultView(batenderHistoryUC.lvFoodPending.ItemsSource).Filter = OrderDetailFilter;
                }
                //if (batenderHistoryUC.lvFoodWaitingConfirm.ItemsSource != null)
                //{
                //    CollectionViewSource.GetDefaultView(batenderHistoryUC.lvFoodWaitingConfirm.ItemsSource).Refresh();
                //    CollectionViewSource.GetDefaultView(batenderHistoryUC.lvFoodWaitingConfirm.ItemsSource).Filter = OrderDetailFilter;
                //}
            }

        }

        private void txtFilter_TextChanged()
        {
      if (!isButtonHistory)
            {
                if (isButtonOutOfStock)
                {
                    drinkOutOfStock = (DrinkOutStockUC)this.listData.Content; 
                }
                else
                {
                   batenderList = (BatenderListUC)this.listData.Content;
                }
                
            }
            else
            {
                batenderHistoryUC = (HistoryOrderDetailUserControl)this.listData.Content;
            }
         
            if (batenderList != null)
            {
                if (batenderList.lvFoodPending.ItemsSource != null)
                {
                    CollectionViewSource.GetDefaultView(batenderList.lvFoodPending.ItemsSource).Refresh();
                    CollectionViewSource.GetDefaultView(batenderList.lvFoodPending.ItemsSource).Filter = OrderDetailFilter;
                }
                if (batenderList.lvFoodWaitingConfirm.ItemsSource != null)
                {
                    CollectionViewSource.GetDefaultView(batenderList.lvFoodWaitingConfirm.ItemsSource).Refresh();
                    CollectionViewSource.GetDefaultView(batenderList.lvFoodWaitingConfirm.ItemsSource).Filter = OrderDetailFilter;
                }
            }
            if (batenderHistoryUC != null)
            {
                if (batenderHistoryUC.lvFoodPending.ItemsSource != null)
                {
                    CollectionViewSource.GetDefaultView(batenderHistoryUC.lvFoodPending.ItemsSource).Refresh();
                    CollectionViewSource.GetDefaultView(batenderHistoryUC.lvFoodPending.ItemsSource).Filter = OrderDetailFilter;
                }
                //if (batenderHistoryUC.lvFoodWaitingConfirm.ItemsSource != null)
                //{
                //    CollectionViewSource.GetDefaultView(batenderHistoryUC.lvFoodWaitingConfirm.ItemsSource).Refresh();
                //    CollectionViewSource.GetDefaultView(batenderHistoryUC.lvFoodWaitingConfirm.ItemsSource).Filter = OrderDetailFilter;
                //}
            }
            if(drinkOutOfStock != null)
            {
                if(drinkOutOfStock.drinkWorking.ItemsSource != null)
                {
                    CollectionViewSource.GetDefaultView(drinkOutOfStock.drinkWorking.ItemsSource).Refresh();
                    CollectionViewSource.GetDefaultView(drinkOutOfStock.drinkWorking.ItemsSource).Filter = DrinkOutOfStockFilter;
                }
            }
        }

        private void PART_ContentHost_Supplier_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (!isButtonHistory)
            {
                if (isButtonOutOfStock)
                {
                    drinkOutOfStock = (DrinkOutStockUC)this.listData.Content;
                }
                else
                {
                    batenderList = (BatenderListUC)this.listData.Content;
                }

            }
            else
            {
                batenderHistoryUC = (HistoryOrderDetailUserControl)this.listData.Content;
            }

            if (batenderList != null)
            {
                if (batenderList.lvFoodPending.ItemsSource != null)
                {
                    CollectionViewSource.GetDefaultView(batenderList.lvFoodPending.ItemsSource).Refresh();
                    CollectionViewSource.GetDefaultView(batenderList.lvFoodPending.ItemsSource).Filter = OrderDetailFilter;
                }
                if (batenderList.lvFoodWaitingConfirm.ItemsSource != null)
                {
                    CollectionViewSource.GetDefaultView(batenderList.lvFoodWaitingConfirm.ItemsSource).Refresh();
                    CollectionViewSource.GetDefaultView(batenderList.lvFoodWaitingConfirm.ItemsSource).Filter = OrderDetailFilter;
                }
            }
            if (batenderHistoryUC != null)
            {
                if (batenderHistoryUC.lvFoodPending.ItemsSource != null)
                {
                    CollectionViewSource.GetDefaultView(batenderHistoryUC.lvFoodPending.ItemsSource).Refresh();
                    CollectionViewSource.GetDefaultView(batenderHistoryUC.lvFoodPending.ItemsSource).Filter = OrderDetailFilter;
                }
                //if (batenderHistoryUC.lvFoodWaitingConfirm.ItemsSource != null)
                //{
                //    CollectionViewSource.GetDefaultView(batenderHistoryUC.lvFoodWaitingConfirm.ItemsSource).Refresh();
                //    CollectionViewSource.GetDefaultView(batenderHistoryUC.lvFoodWaitingConfirm.ItemsSource).Filter = OrderDetailFilter;
                //}
            }
            if (drinkOutOfStock != null)
            {
                if (drinkOutOfStock.drinkWorking.ItemsSource != null)
                {
                    CollectionViewSource.GetDefaultView(drinkOutOfStock.drinkWorking.ItemsSource).Refresh();
                    CollectionViewSource.GetDefaultView(drinkOutOfStock.drinkWorking.ItemsSource).Filter = DrinkOutOfStockFilter;
                }
            }

        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (sender != null)
            {
               string str = sender.ToString();
                int index = str.IndexOf(":");
                string st = str.Substring(index+1);
                st = st.TrimStart(' ').TrimEnd(' ');
                TextInputSearch = st;
                txtFilter.Text = st;
                txtFilter.SelectAll();
                txtFilter_TextChanged();
                PART_Popup_Supplier.IsOpen = false;
            }
        }

        private void Button_Click_1(object sender, System.Windows.RoutedEventArgs e)
        {
            isButtonOutOfStock = false; 
            isButtonHistory = true;
        }

        private void Button_Click_2(object sender, System.Windows.RoutedEventArgs e)
        {
            isButtonAll = true; 
            isButtonHistory = false;
            isButtonOutOfStock = false;
            isButtonTakeAway = false;
            isButtonWorking = false; 
        }

        private void Button_Click_3(object sender, System.Windows.RoutedEventArgs e)
        {
            isButtonHistory = false;
            isButtonOutOfStock = false; 
        }
        private bool DrinkOutOfStockFilter(object name)
        {
            if (String.IsNullOrEmpty(txtFilter.Text))
                return true;
            var food = (OutOfFood)name;
            if (food.Prefix != null && food.Name != null)
            {
                return (food.Prefix.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    food.Name.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            }
            return true;
        }

        private void Button_Click_4(object sender, System.Windows.RoutedEventArgs e)
        {
            isButtonOutOfStock = true;
            isButtonAll = false;
            isButtonHistory = false;
            isButtonTakeAway = false;
            isButtonWorking = false; 
        }
    }
}
