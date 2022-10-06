using System.Net;
using System.Windows;
using System;
using System.ComponentModel;
using System.IO;
using System.Diagnostics;
using System.IO.Compression;

namespace TechresStandaloneSale.Views
{
    /// <summary>
    /// Interaction logic for DownloadWindow.xaml
    /// </summary>
    public partial class DownloadWindow : Window
    {
        string Update = "TechresStandaloneSale.exe";
        //string Update = System.Windows.Forms.Application.StartupPath + @"\..\update\update.exe";
        string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\techres_order.zip";
        public DownloadWindow(string address, string location)
        {
            InitializeComponent();
            WebClient client = new WebClient();
            Uri Uri = new Uri(address);

            client.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);

            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgress);
            client.DownloadFileAsync(Uri, location);
        }
        private void DownloadProgress(object sender, DownloadProgressChangedEventArgs e)
        {
            this.progressBar.Value = e.ProgressPercentage;
            TitleDowload.Text = string.Format("Downloading.......... ");
            precent.Text = string.Format("{0}%", e.ProgressPercentage);
            decimal BytesReceived = e.BytesReceived / 1024 / 1024;
            decimal TotalBytesToReceive = e.TotalBytesToReceive / 1024 / 1024;
            this.percentDownload.Text= string.Format("{0}MB/{1}MB", Math.Round(BytesReceived, 2),  Math.Round(TotalBytesToReceive, 2));
        }
        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            try
            {
                if (e.Cancelled == true)
                {
                    Console.WriteLine("Download has been canceled.");
                }
                else
                {
                    
                    if (File.Exists(path))
                    {

                        // 
                      
                        //String extractPath = System.Windows.Forms.Application.StartupPath;
                        //ZipFile.ExtractToDirectory(path, extractPath);


                        this.Close();
                        System.Windows.Application.Current.Shutdown();
                        if (Utils.Utils.IsAdministrator() == false)
                        {
                            // Restart program and run as admin
                            string[] filePaths = Directory.GetFiles(System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.StartupPath), "*.exe",
                                         SearchOption.AllDirectories);

                            foreach (string exe in filePaths)
                            {
                                string name = System.IO.Path.GetFileName(exe);
                                if (string.Compare(name, Update, true) == 0)
                                {
                                    ProcessStartInfo startInfo = new ProcessStartInfo(exe);
                                    startInfo.Verb = "runas";
                                    System.Diagnostics.Process.Start(startInfo);
                                    break;
                                }
                            }
                        }
                        else
                        {
                            string[] filePaths = Directory.GetFiles(System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.StartupPath), "*.exe",
                                         SearchOption.AllDirectories);
                            foreach (string exe in filePaths)
                            {
                                string name = System.IO.Path.GetFileName(exe);
                                if (String.Compare(name, Update, true) == 0)
                                {
                                    Process.Start(exe);
                                    break;
                                }
                            }
                        }
                    }

                }
            }
            catch (InvalidOperationException esx)
            {
                MessageBox.Show(esx.Message);
                Console.WriteLine(esx.Message);
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Console.WriteLine(ex.Message);
            }


        }

        private void progressBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }
    }
}
