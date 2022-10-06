namespace TechresStandaloneSale.Models.Request
{
  public  class CustomerRegisterWrapper
    {
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public string birthday { get; set; }

        public CustomerRegisterWrapper(string frist_name, string last_name, string address, string phone, string birthday)
        {
            this.first_name =  frist_name;
            this.last_name = last_name;
            this.phone = phone;
            this.address = address;
            this.birthday = birthday;
        }
    }
}
