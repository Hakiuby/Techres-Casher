using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Interaction logic for ShiftActivityReport.xaml
    /// </summary>
    public partial class ShiftActivityReport : Window
    {
        public ShiftActivityReport()
        {
            InitializeComponent();
        }

        //private void TextBoxKeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.Key == Key.Enter & (sender as TextBox).AcceptsReturn == false) MoveToNextUIElement(e);
        //}
        //void MoveToNextUIElement(KeyEventArgs e)
        //{
        //    // Creating a FocusNavigationDirection object and setting it to a
        //    // local field that contains the direction selected.
        //    FocusNavigationDirection focusDirection = FocusNavigationDirection.Next;

        //    // MoveFocus takes a TraveralReqest as its argument.
        //    TraversalRequest request = new TraversalRequest(focusDirection);

        //    // Gets the element with keyboard focus.
        //    UIElement elementWithFocus = Keyboard.FocusedElement as UIElement;

        //    // Change keyboard focus.
        //    if (elementWithFocus != null)
        //    {
        //        if (elementWithFocus.MoveFocus(request)) e.Handled = true;
        //    }
        
    }
}
