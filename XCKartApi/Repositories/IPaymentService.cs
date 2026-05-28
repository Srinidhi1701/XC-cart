using XCKartApi.Models;

namespace XCKartApi.Repositories
{
    public interface IPaymentService
    {
        public void CashDetails(CashModel cashModel);
        public void AddCashToDatabase(CashModel cashDetail);
        public void UpiDetails(UpiModel upi);
        public void AddUpiToDatabase(UpiModel upiDetail);
        public void creditDetail(CreditModel credit);
        public void AddCreditToDatabase(CreditModel creditDetail);
    }
}
