using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO.Compression;
using System.Diagnostics;
using System.Security.Principal;
using System.IO;

namespace Update
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += ExtraFile;
        }

        private async void ExtraFile(object sender, RoutedEventArgs e)
        {
            try
            {
                //địa chỉ file zip
                string zipPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\techres_order.zip";
                //địa chỉ giải nén
                string ExtractPath = Path.GetDirectoryName(System.Windows.Forms.Application.StartupPath) + @"\TechresOrder";
                //tên file app
                string techres = "TechresStandaloneSale.exe";

                //delete folder TechresOrder
                await Task.Run(()=> Directory.Delete(ExtractPath));
                //string[] files = Directory.GetFiles(ExtractPath);
                //foreach (string file in files)
                //{
                //    File.Delete(file);
                //    Console.WriteLine($"{file} is deleted.");
                //}        
                
                //giải nén
                await Task.Run(()=>  ZipFile.ExtractToDirectory(zipPath, ExtractPath));

                this.Content.Text = "Cập nhật thành công !";

                WindowsIdentity identity = WindowsIdentity.GetCurrent();
                WindowsPrincipal principal = new WindowsPrincipal(identity);

                //khởi động lại ứng dụng với quyền admin
                if (!principal.IsInRole(WindowsBuiltInRole.Administrator))
                {                    
                    string[] filePaths = Directory.GetFiles(System.Windows.Forms.Application.StartupPath + @"\..\", "*.exe",
                                         SearchOption.AllDirectories);
                    foreach (string exe in filePaths)
                    {
                        string name = System.IO.Path.GetFileName(exe);
                        if (String.Compare(name, techres, true) == 0)
                        {
                            ProcessStartInfo startInfo = new ProcessStartInfo();
                            startInfo.FileName = exe;
                            startInfo.UseShellExecute = true;
                            startInfo.Verb = "runas";
                            Process.Start(startInfo);
                            break;
                        }
                    }
                }
                else
                {
                    string[] filePaths = Directory.GetFiles(System.Windows.Forms.Application.StartupPath + @"\..\", "*.exe",
                                         SearchOption.AllDirectories);
                    foreach (string exe in filePaths)
                    {
                        string name = System.IO.Path.GetFileName(exe);
                        if (String.Compare(name, techres, true) == 0)
                        {
                            Process.Start(exe);
                            break;
                        }
                    }
                }
                await Task.Run(()=> File.Delete(zipPath));
                this.Close();
            }
            catch (Exception ex)
            {
                string dln = ex.Message;
                string dln2 = ex.ToString();
                FileStream expr_12A = new FileStream(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\FileError.txt", FileMode.Create);
                StreamWriter expr_135 = new StreamWriter(expr_12A, Encoding.UTF8);
                expr_135.WriteLine(dln);
                expr_135.WriteLine(dln2);
                expr_135.Flush();
                expr_12A.Close();
            }
        }
    }
}
