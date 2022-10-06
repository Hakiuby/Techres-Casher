using System;
using System.Windows;
using TechresStandaloneSale.Helpers;

namespace TechresStandaloneSale.Views
{
    /// <summary>
    /// Interaction logic for UpdateVersion.xaml
    /// </summary>
    public partial class UpdateVersion : Window
    {
        
        public UpdateVersion()
        {
            InitializeComponent();
        }
        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            string url = this.DownloadUrl.Text;
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\techres_order.zip";
            this.Hide();
            DownloadWindow download = new DownloadWindow(url, path);
            download.ShowDialog();
            this.Close();


        }

    }
}
