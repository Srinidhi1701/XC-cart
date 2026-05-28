namespace XCKartApi.Models
{
    public class ModifyShipmentModel
    {
        public int ship_id { get; set; }
        public int shipment_type { get; set; }
        public string shipment_name { get; set; }
        public string shipment_address { get; set; }
    }
}
