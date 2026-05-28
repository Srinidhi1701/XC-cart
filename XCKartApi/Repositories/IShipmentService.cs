using XCKartApi.Models;

namespace XCKartApi.Repositories
{
    public interface IShipmentService
    {
        public void AddShipment(ShipmentModel ship);
        public void AddShipmentToDatabase(ShipmentModel ship);
        public string GetShipType(string name);
        public string TrackShipDetail(int shipChoice);
        public DateTime TrackShipment(int shipmentType);
        public List<ShipmentModel> GetShipment();
        public void UpdateShipDetail(ModifyShipmentModel shipModify);
        public void UpdateShipmentDatabse(ModifyShipmentModel shipInfo);
    }
}
