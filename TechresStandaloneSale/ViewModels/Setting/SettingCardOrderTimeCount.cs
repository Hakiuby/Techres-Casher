using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechresStandaloneSale.Utils;

namespace TechresStandaloneSale.ViewModels.SettingS
{
    public class SettingCardOrderTimeCount
    {
        public string SystemTime;
        private int h;
        private int m;
        private int s;
        private string hst;
        private string mst;
        private string sst;
        public void GetTimeFormat(long time)
        {
            h = (int) time/60;
            m = (int) time%60;
            s = 0;
            hst = h.ToString();
            mst = m.ToString();
            sst = s.ToString();
            SystemTime = string.Format("{0}:{1}:{2}", hst, mst, sst);
        }
        public SettingCardOrderTimeCount(long time)
        {
            if (time>0)
            {
                GetTimeFormat(time);
            }
            aTimer = new System.Windows.Forms.Timer();
            aTimer.Tick += new EventHandler(Timer_Tick);
            aTimer.Interval = 1000; // 1 second
            aTimer.Start();
        }
        private System.Windows.Forms.Timer aTimer;
        private void ReSetTime()
        {
            aTimer = new System.Windows.Forms.Timer();
            aTimer.Tick += new EventHandler(Timer_Tick);
            aTimer.Interval = 1000; // 1 second
            aTimer.Start();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            s++;
            if (s > 60)
            {
                m++;
                s = 0;
                if (m > 60)
                {
                    h++;
                    m = 0;
                    if (h > 9)
                        hst = h.ToString();
                    else
                        hst = "0" + h.ToString();
                }
                if (m > 9)
                    mst = m.ToString();
                else
                    mst = "0" + m.ToString();
            }
            if (s > 9)
                sst = s.ToString();
            else
                sst = "0" + s.ToString();
            SystemTime = string.Format("{0}:{1}:{2}", hst, mst, sst);
            aTimer.Stop();
            ReSetTime();
        }
    }
}
