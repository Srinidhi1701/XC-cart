namespace XCKartApi.Models
{
    public class CashModel
    {
        public int order_id { get; set; }
        public string customer_name { get; set; }
        public string shipment_name { get; set; }
        public int amount { get; set; }
        public string product_name { get; set; }
        public int product_count { get; set; }
    }
}
