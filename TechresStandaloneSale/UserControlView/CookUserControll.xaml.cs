using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using TechresStandaloneSale.Models;
using TechresStandaloneSale.ViewModels;

namespace TechresStandaloneSale.UserControlView
{
    /// <summary>
    /// Interaction logic for CookUserControll.xaml
    /// </summary>
    public partial class CookUserControll : UserControl
    {
        public bool isCheckButtonDone = false;
        public bool isCheckButtonOutOfFood = false;
        public bool isCheckButtonAll = false;
        public bool isCheckButtonWorking = false;
        public bool isCheckButtonTakeAway = false; 
        public CookUserControll()
        {
            InitializeComponent();
            // this.DataContext = new CookViewModel();
        }

        private FoodingUserControl item;
        private HistoryOrderDetailUserControl listFood;
        private FoodOutStockUC outstock;
        private void FoodUserControk_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (!isCheckButtonDone)
            {
                item = (FoodingUserControl)this.ContentCook.Content;
            }
            else
            {
                listFood = (HistoryOrderDetailUserControl)this.ContentCook.Content;
            }
            if (item != null)
            {
                if (item.lvFoodWaitingConfirm.ItemsSource != null)
                {
                    CollectionViewSource.GetDefaultView(item.lvFoodWaitingConfirm.ItemsSource).Filter = OrderDetailFilter;
                }
                if (item.lvFoodPending.ItemsSource != null)
                {
                    CollectionViewSource.GetDefaultView(item.lvFoodPending.ItemsSource).Filter = OrderDetailFilter;
                }
            }
            if (listFood != null)
            {
               
                if (listFood.lvFoodPending.ItemsSource != null)
                {
                    CollectionViewSource.GetDefaultView(listFood.lvFoodPending.ItemsSource).Filter = OrderDetailFilter;
                }
            }

        }
    
        private bool OrderDetailFilter(object item)
        {
            if (String.IsNullOrEmpty(txtFilter.Text))
                return true;
            var orderDetail = (OrderDetail)item;
            return (orderDetail.FoodName.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0
                || orderDetail.TableName.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0
                || orderDetail.EmployeeName.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0
                || orderDetail.Note.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0
                || orderDetail.Prefix.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0
                || orderDetail.NormalizeName.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0
                || orderDetail.FoodNameString.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            isCheckButtonDone = false;
            isCheckButtonOutOfFood = false;
            txtFilter.Text = "";
            txtFilterOutStock.Text = "";
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) //    History 
        {
            isCheckButtonOutOfFood = false;
            isCheckButtonDone = true;
            txtFilter.Text = "";
            txtFilterOutStock.Text = "";
        }

        private void Button_Click_2(object sender, RoutedEventArgs e) // Out Of Food 
        {
            isCheckButtonDone = false;
            isCheckButtonOutOfFood = true;
            txtFilter.Text = "";
            txtFilterOutStock.Text = "";
        }
        private void Button_Click_3(object sender, RoutedEventArgs e) // All 
        {
            isCheckButtonAll = true;
            isCheckButtonDone = false;
            isCheckButtonOutOfFood = false;
            isCheckButtonWorking = false;
            isCheckButtonTakeAway = false; 
        }

        private void txtFilter_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (!isCheckButtonDone)
            {
                if (isCheckButtonOutOfFood)
                {
                   outstock = (FoodOutStockUC)this.ContentCook.Content;
                }
                else
                {
                    item = (FoodingUserControl)this.ContentCook.Content;
                }
            }
            
            else
            {
                listFood = (HistoryOrderDetailUserControl)this.ContentCook.Content;
            }
            if (item != null)
            {
                if (item.lvFoodWaitingConfirm.ItemsSource != null)
                {
                    CollectionViewSource.GetDefaultView(item.lvFoodWaitingConfirm.ItemsSource).Refresh();
                    CollectionViewSource.GetDefaultView(item.lvFoodWaitingConfirm.ItemsSource).Filter = OrderDetailFilter;

                }
                if (item.lvFoodPending.ItemsSource != null)
                {
                    CollectionViewSource.GetDefaultView(item.lvFoodPending.ItemsSource).Refresh();
                    CollectionViewSource.GetDefaultView(item.lvFoodPending.ItemsSource).Filter = OrderDetailFilter;
                }
               

            }
            if (listFood != null)
            {
               
                if (listFood.lvFoodPending.ItemsSource != null)
                {
                    CollectionViewSource.GetDefaultView(listFood.lvFoodPending.ItemsSource).Refresh();
                    CollectionViewSource.GetDefaultView(listFood.lvFoodPending.ItemsSource).Filter = OrderDetailFilter;
                }
            }
            if(outstock != null)
            {
                if (outstock.FoodWorking.ItemsSource != null)
                {
                    CollectionViewSource.GetDefaultView(outstock.FoodWorking.ItemsSource).Refresh();
                    CollectionViewSource.GetDefaultView(outstock.FoodWorking.ItemsSource).Filter = FoodOutOfStockFilter; 
                }
            }
        }

        private void txtFilterOutStock_TextChanged(object sender, TextChangedEventArgs e)
        {
                CollectionViewSource.GetDefaultView(outstock.FoodWorking.ItemsSource).Refresh();
                CollectionViewSource.GetDefaultView(outstock.FoodWorking.ItemsSource).Filter = FoodOutOfStockFilter;
        }
        private bool FoodOutOfStockFilter(object name)
        {
            if (String.IsNullOrEmpty(txtFilter.Text))
                return true;
            var food = (OutOfFood)name; 
            if(food.Prefix != null && food.Name != null)
            {
                return (food.Prefix.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    food.Name.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0); 
            }
            return true; 
        }
    }
}
