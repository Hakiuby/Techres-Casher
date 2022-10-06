using Newtonsoft.Json;

namespace TechresStandaloneSale.Models.Request
{
    public class LoginWrapper
    {
        [JsonProperty(PropertyName = "username")]
        public string Username { get; set; }

        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }



        public LoginWrapper(string username, string password)
        {
            this.Username = username;
            this.Password = Utils.Utils.Base64Encode(password);

        }

    }
}
