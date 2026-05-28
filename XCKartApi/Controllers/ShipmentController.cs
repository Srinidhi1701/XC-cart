using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using XCKartApi.Repositories;
using XCKartApi.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace XCKartApi.Controllers
{
    [Route("shipment")]
    [ApiController]
    public class ShipmentController : ControllerBase
    {
        private readonly IShipmentService _shipmentService;
        public ShipmentController(IShipmentService shipmentService)
        {
            _shipmentService = shipmentService;
        }
        [HttpGet("{shipname}/trackShipment")]
        public string ShipDetail([FromRoute]string shipname)
        {
            try
            {
                return _shipmentService.GetShipType(shipname);
            }
            catch (Exception ex)
            {
                return  ex.Message ;
            }

        }
        [HttpPost("addShipDetails")]
        public string AddShipDetail([FromBody] ShipmentModel shipDetail)
        {
            try
            {
                _shipmentService.AddShipment(shipDetail);
                return "Added Successfully";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        [HttpGet("getShipmentDetails")]
        public IEnumerable<ShipmentModel> GetShipment()
        {
            try
            {
                return _shipmentService.GetShipment();
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<ShipmentModel>();   
            }
        }
        [HttpPut("modifyShipment")]
        public string ModifyShipment([FromBody] ModifyShipmentModel shipInfo)
        {
            try
            {
                _shipmentService.UpdateShipDetail(shipInfo);
                return "Modified Successfully";
            }
            catch (Exception ex)
            {
                return  ex.Message;
            }
        }

    }
}
