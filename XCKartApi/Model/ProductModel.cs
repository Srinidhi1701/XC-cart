using System.ComponentModel.DataAnnotations.Schema;

namespace XCKartApi.Models
{
    //Table("product_details_table")]
    public class ProductModel
    {
        
        public int product_id { get; set; }
        public string product_name { get; set; }
        public string product_description { get; set; }
        public int product_price { get; set; }
        public string product_manufacturer { get; set; }
    }
}
