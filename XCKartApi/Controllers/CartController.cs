using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XCKartApi.Repositories;
using XCKartApi.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace XCKartApi.Controllers
{
    [Route("cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("getCart")]
        public IEnumerable<CartModel> GetCart()
        {
            try
            {
                return _cartService.GetCart();
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<CartModel>();
            }

        }
        [HttpPost("addCartDetails")]
        public string AddCartDetails([FromBody] CartModel cart)
        {
            try
            {
                _cartService.AddCartDetails(cart);
                return "Added Successfully";
            }
            catch (Exception ex)
            {
                return  ex.Message;
            }

        }
        [HttpDelete]
        [Route("deleteCart")]
        public string DeleteCartDetails(int id)
        {
            try
            {

                _cartService.DeleteCart(id);
                return "Deleted Successfully";
            }
            catch (Exception ex)
            {
                return  ex.Message;
            }
        }
        [HttpPut]
        [Route("updatecart")]
        public string UpdateSpecificItem([FromBody] CartModel cart)
        {
            try
            {
                _cartService.UpdateCart(cart);
                return "Updated Succesfully";
            }
            catch (Exception ex)
            {
                return  ex.Message;
            }
        }

    }
}
