using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TechresStandaloneSale.Helpers;

namespace TechresStandaloneSale.UserControlView
{
    /// <summary>
    /// Interaction logic for DomainRestaurantUC.xaml
    /// </summary>
    public partial class DomainRestaurantUC : UserControl
    {

        public DomainRestaurantUC()
        {
            InitializeComponent();
        }
       
        private async void DomainRestaurant_TextChanged(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(DomainRestaurant.Text)) MessageBox.Show("Bạn chưa nhập tên nhà hàng!");
            string txt;
            // this inner method checks if user is still typing
            async Task<bool> UserKeepsTyping()
            {
                txt = DomainRestaurant.Text;   // remember text
                await Task.Delay(500);        // wait some
                return txt != DomainRestaurant.Text;  // return that text chaged or not
            }
            if (await UserKeepsTyping() || DomainRestaurant.Text == Constants.Domain) return;
            // save the text you process, and do your stuff
            Constants.Domain = DomainRestaurant.Text;
            Properties.Settings.Default.RestaurantDomainAPI = DomainRestaurant.Text;



        }

    }
}
