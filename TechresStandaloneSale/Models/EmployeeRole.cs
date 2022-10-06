using MaterialDesignThemes.Wpf;
using Newtonsoft.Json;
using System.Windows.Media;
using TechresStandaloneSale.Helpers;

namespace TechresStandaloneSale.Models
{
    public class EmployeeRole
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("role")]
        public string Role { get; set; }

        [JsonProperty("descripttion")]
        public string Descripttion { get; set; }

        [JsonProperty("status")]
        public long Status { get; set; }

        [JsonProperty("number_employees")]
        public long NumberEmployees { get; set; }

        [JsonProperty("role_leader_id")]
        public long RoleLeaderId { get; set; }

        [JsonProperty("role_leader_name")]
        public string RoleLeaderName { get; set; }
        public bool IsSelectAll { get; set; }

        public Brush CheckAllColor
        {
            get
            {
                if (IsSelectAll)
                {
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.GREEN_GG_COLOR));
                }
                else
                {
                    return new SolidColorBrush(Colors.White);

                }

            }
            set
            {
                CheckAllColor = value;
            }
        }
        public EmployeeRole() { }
        public EmployeeRole(long id,string name )
        {
            Id = id;
            Name = name;
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
