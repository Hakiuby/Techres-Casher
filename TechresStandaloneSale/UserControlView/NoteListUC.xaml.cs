using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using TechresStandaloneSale.Models.Response;

namespace TechresStandaloneSale.UserControlView
{
    /// <summary>
    /// Interaction logic for NoteListUC.xaml
    /// </summary>
    public partial class NoteListUC : UserControl
    {
        public NoteListUC()
        {
            InitializeComponent();
        }
        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ListNote.ItemsSource != null)
            {
                CollectionViewSource.GetDefaultView(ListNote.ItemsSource).Refresh();
                CollectionViewSource.GetDefaultView(ListNote.ItemsSource).Filter = EmployeeFilter;
            }

        }
        private bool EmployeeFilter(object item)
        {
            if (String.IsNullOrEmpty(txtFilter.Text))
                return true;
            var employee = (OrderNoteDetailResponseData)item;
            return (employee.Content.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0
                       || employee.Id.ToString().IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }
        public void NoteListLoaded(object sender, RoutedEventArgs e)
        {
            if (ListNote.ItemsSource != null)
            {
                CollectionViewSource.GetDefaultView(ListNote.ItemsSource).Filter = EmployeeFilter;
            }
        }
    }
}
