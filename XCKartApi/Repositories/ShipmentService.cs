using Microsoft.Data.SqlClient;
using System.Data;
using XCKartApi.Database;
using XCKartApi.Models;
using XCKartApi.Repositories;

namespace XCKart.Repositories
{
    public class ShipmentService : IShipmentService
    {
        private readonly DataDbContext _dbContext;
        public ShipmentService(DataDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public int shipmentChoice;
        public List<ShipmentModel> shipmentList = new();
        public void AddShipment(ShipmentModel ship)
        {
            shipmentChoice = ship.shipment_type;
            var shipmentModel = new ShipmentModel()
            {
                shipment_type = ship.shipment_type,
                shipment_name = ship.shipment_name,
                shipment_address = ship.shipment_address,

            };
            AddShipmentToDatabase(ship);
        }
        public void AddShipmentToDatabase(ShipmentModel ship)
        {
            var connectionstring = _dbContext.ConnectionString();
            using (SqlConnection sqlConnec = new SqlConnection(connectionstring))
            {
                sqlConnec.Open();

                SqlCommand cmd = new SqlCommand("AddShipmentDetails", sqlConnec);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@shipment_type", ship.shipment_type);
                cmd.Parameters.AddWithValue("@shipment_name", ship.shipment_name);
                cmd.Parameters.AddWithValue("@shipment_address", ship.shipment_address);
                cmd.ExecuteNonQuery();
            }

        }
        public string GetShipType(string name)
        {
            var connectionstring = _dbContext.ConnectionString();
            using (SqlConnection sqlConnec = new SqlConnection(connectionstring))
            {
                sqlConnec.Open();

                SqlCommand cmd = new SqlCommand("SP_Track_shipment", sqlConnec);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@shipment_name", name);
                SqlDataReader reader = cmd.ExecuteReader();


                while (reader.Read())
                {
                    
                    shipmentChoice = int.Parse(reader[0].ToString());

                }
                return TrackShipDetail(shipmentChoice);

            }
        }
        public string TrackShipDetail(int shipChoice)
        {
            var result = shipChoice switch
            {
                1 => @$"The order is placed on: {DateTime.Now}
The order will be shipped on: {TrackShipment(shipmentChoice)}
The order will be delivered on: {DateTime.Now.AddDays(2)}
                ",
                2 => @$"The order is placed on: {DateTime.Now}
The order will be shipped on: {TrackShipment(shipmentChoice)}
The order will be delivered on: {DateTime.Now.AddDays(10)}
                ",
                3 => @$"The order is placed on: {DateTime.Now}
The order will be shipped on: {TrackShipment(shipmentChoice)}
The order will be delivered on: {DateTime.Now.AddDays(5)}
                ",
                _ => @"Product will be delivered soon..",
            };
            return result;
        }
        public DateTime TrackShipment(int shipmentType)
        {
            var result = shipmentType switch
            {
                1 => DateTime.Now.AddDays(1),
                2 => DateTime.Now.AddDays(7),
                3 => DateTime.Now.AddDays(2),
                _ => DateTime.Now,
            };
            return result;

        }
        public List<ShipmentModel> GetShipment()
        {
            var connectionstring = _dbContext.ConnectionString();
            using (SqlConnection sqlConnec = new SqlConnection(connectionstring))
            {
                sqlConnec.Open();
                SqlCommand cmd = new SqlCommand("SP_Get_Shipment_Details", sqlConnec);

                cmd.CommandType = CommandType.StoredProcedure;


                SqlDataReader reader = cmd.ExecuteReader();


                while (reader.Read())
                {
                    ShipmentModel shipmentDetail = new();
                    shipmentDetail.shipment_type = int.Parse(reader[1].ToString());
                    shipmentDetail.shipment_name = reader[2].ToString();
                    shipmentDetail.shipment_address = reader[3].ToString();
                    shipmentList.Add(shipmentDetail);

                }
                return shipmentList;
            }
        }
        public void UpdateShipDetail(ModifyShipmentModel shipModify)
        {
            var shipDetail = new ModifyShipmentModel()
            {
                ship_id = shipModify.ship_id,
                shipment_type = shipModify.shipment_type,
                shipment_name = shipModify.shipment_name,
                shipment_address = shipModify.shipment_address
            };
            UpdateShipmentDatabse(shipDetail);

        }
        public void UpdateShipmentDatabse(ModifyShipmentModel shipInfo)
        {
            var connectionstring = _dbContext.ConnectionString();
            using (SqlConnection sqlConnec = new SqlConnection(connectionstring))
            {
                sqlConnec.Open();

                SqlCommand cmd = new SqlCommand("UpdateShipment_procedure", sqlConnec);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ship_id", shipInfo.ship_id);
                cmd.Parameters.AddWithValue("@shipment_type", shipInfo.shipment_type);
                cmd.Parameters.AddWithValue("@shipment_name", shipInfo.shipment_name);
                cmd.Parameters.AddWithValue("@shipment_address", shipInfo.shipment_address);
                cmd.ExecuteNonQuery();


            }
        }

    }
}
