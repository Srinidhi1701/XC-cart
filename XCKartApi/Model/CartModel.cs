namespace XCKartApi.Models;

public class CartModel
{
    public int c_id { get; set; }
    public int customer_id { get; set; }
    public string product_name { get; set; }
    public int product_id { get; set; }
    public string product_description { get; set; }
    public int product_count { get; set; }
    public int total_price { get; set; }
}
