using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XCKartApi.Models;
using XCKartApi.Repositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace XCKartApi.Controllers
{
    [Route("customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost("addCustomerDetails")]
        public string AddCustomerDetail([FromBody] CustomerModel custDetails)
        {
            try
            {
                _customerService.CustomerDetails(custDetails);
                return "Added Succesfully";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
        [HttpGet("getCustomerDetails")]
        public IEnumerable<CustomerModel> GetCustomerDetails()
        {
            try
            {
                return _customerService.GetCustomer();
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<CustomerModel>();
            }
        }
        [HttpPut("updateCustomerDetails")]
        public string Put([FromBody] CustomerModel customerDetails)
        {
            try
            {
                _customerService.UpdateCustomer(customerDetails);
                return "Updated Successfully";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }


        }
    }
}
