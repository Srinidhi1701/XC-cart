using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XCKartApi.Models;
using XCKartApi.Repositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace XCKartApi.Controllers
{
    [Route("order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpPost("addOrder")]
        public string OrderDetails([FromBody] OrderModel orderDetails)
        {
            try
            {
                _orderService.OrderDetails(orderDetails);
                return "Added Successfully";
            }
            catch (Exception ex)
            {
                return  ex.Message;
            }
        }
        [HttpGet("showOrder")]
        public IEnumerable<OrderModel> GetOrderDetails()
        {
            try
            {
               return _orderService.GetOrder();
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<OrderModel>();
            }

        }
        [HttpPut("modifyOrder")]
        public string Put([FromBody] ModifyOrderModel modelDetails)
        {
            try
            {
                _orderService.UpdateCartDetails(modelDetails);
                return "Updated Successfully";
            }
            catch (Exception ex)
            {
                return  ex.Message;
            }

        }
    }
}
