namespace XCKartApi.Models
{
    public class ModifyOrderModel
    {
        public int order_id { get; set; }
        public string product_name { get; set; }
        public int product_quantity { get; set; }
        public int payment_type { get; set; }
    }
}
