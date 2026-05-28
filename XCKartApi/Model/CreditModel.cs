namespace XCKartApi.Models
{
    public class CreditModel
    {
        public string cname { get; set; }
        public string cnumber { get; set; }
        public string cservice { get; set; }
        public int expmonth { get; set; }
        public int expyear { get; set; }
        public int cvv { get; set; }
        public int amount { get; set; }
        public string product_name { get; set; }
        public int product_count { get; set; }
    }
}
