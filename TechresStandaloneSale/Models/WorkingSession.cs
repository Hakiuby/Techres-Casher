using Newtonsoft.Json;
using TechresStandaloneSale.Helpers;

namespace TechresStandaloneSale.Models
{
    public class WorkingSession
    {

        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("from_hour")]
        public string FromHour { get; set; }
        [JsonProperty("to_hour")]
        public string ToHour { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("time_interval_string")]
        public string Interval { get; set; }
        [JsonProperty("status")]
        public int Status { get; set; }

        public string Code
        {
            get
            {

                return string.Format("#{0}", this.Id);
            }
            set
            {
                Code = value;
            }
        }
        public string StatusString
        {
            get
            {
                return Status == Constants.STATUS ? "Đang hoạt động" : "Không hoạt động";
            }
            set
            {
                StatusString = value;
            }
        }
        public string NameTime
        {
            get
            {
                return string.Format("{0}:{1}", this.Name, this.Interval);
            }
            set
            {
                NameTime = value;
            }
        }
        public override string ToString()
        {
            return Name + ":" + Interval;
        }

    }
}
