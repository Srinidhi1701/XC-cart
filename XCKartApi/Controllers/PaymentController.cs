using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using XCKartApi.Models;
using XCKartApi.Repositories;

namespace XCKartApi.Controllers
{
    [Route("payment")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("cashPay")]
        public string cashPayDetails([FromBody] CashModel cashModel)
        {
            try
            {
                _paymentService.CashDetails(cashModel);
                return "Cash Payment Successful!!!";
            }
            catch (Exception ex)
            {
                return  ex.Message;
            }

        }
        [HttpPost("upiPay")]
        public string UpiPayDetails([FromBody] UpiModel upiModel)
        {
            try
            {
                _paymentService.UpiDetails(upiModel);
                return "Upi Payment Successful!!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        [HttpPost("creditDebitPay")]
        public string CreditDebitDetails([FromBody] CreditModel creditDebitModel)
        {
            try
            {
                _paymentService.creditDetail(creditDebitModel);
                return "Credit Debit Payment Successfull!!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
    }
}
