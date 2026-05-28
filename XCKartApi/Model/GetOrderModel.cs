namespace XCKartApi.Models
{
    public class GetOrderModel
    {
        public string product_name { get; set; }
        public int product_quantity { get; set; }
        public int payment_type { get; set; }
        public int total_price { get; set; }
    }
}
