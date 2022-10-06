using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using TechresStandaloneSale.Helpers;

namespace TechresStandaloneSale.BroadCast
{
    public static class BroadCastClient
    {
        private const int listenPort = 11000;
        private static UdpClient listener;
        public static void StartListener()
        {
            IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, listenPort);
            listener = new UdpClient(listenPort);
            byte[] bytes = new byte[1024];
            try
            {
                while (true)
                {
                    bytes = listener.Receive(ref groupEP);
                    string BroadCastKey = Encoding.ASCII.GetString(bytes, 0, bytes.Length);
                    System.Diagnostics.Debug.WriteLine(BroadCastKey);
                    if (!string.IsNullOrEmpty(BroadCastKey))
                    {
                        Utils.Utils.AddCacheValue(Constants.CACHE_BROAD_CAST_CLIENT, BroadCastKey, DateTimeOffset.Now.AddDays(1));
                        return;
                    }
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                listener.Close();
            }
        }
        public static void StopListener()
        {
            listener.Close();
        }
    }
}
