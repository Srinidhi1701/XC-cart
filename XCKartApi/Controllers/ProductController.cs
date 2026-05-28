using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XCKartApi.Models;
using XCKartApi.Repositories;


namespace XCKartApi.Controllers
{
    [ApiController]
    [Route("product")]
    
    public class ProductController : ControllerBase
    {
        private readonly IProductOrderService _productService;

        public ProductController(IProductOrderService order)
        {
            _productService = order;

        }

        [HttpGet(Name ="GetProduct")]
        public IEnumerable<ProductModel> GetProduct()
        {
            try
            {
                return _productService.GetProduct();
            }
            catch (Exception e)
            {
                return Enumerable.Empty<ProductModel>();
            }
        }
        
    }

}
