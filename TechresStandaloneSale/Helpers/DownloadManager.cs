using System;
using System.Net;

namespace TechresStandaloneSale.Helpers
{
    public class DownloadManager
    {
        public void DownloadFile(string address, string location)
        {
            WebClient client = new WebClient();
            Uri Uri = new Uri(address);
            client.DownloadFileAsync(Uri, location);

        }
    }
}
