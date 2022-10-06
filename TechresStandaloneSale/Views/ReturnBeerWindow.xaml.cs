using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TechresStandaloneSale.Views
{
    /// <summary>
    /// Interaction logic for ReturnBeerWindow.xaml
    /// </summary>
    public partial class ReturnBeerWindow : Window
    {
        public ReturnBeerWindow()
        {
            InitializeComponent();
        }

        private void tbQuantity_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void tbQuantity_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void tbQuantity_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
