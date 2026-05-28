using Microsoft.Data.SqlClient;
using System.Data;
using XCKartApi.Database;
using XCKartApi.Models;
using XCKartApi.Repositories;

namespace XCKart.Repositories
{
    public class PaymentService : IPaymentService
    {
        private readonly DataDbContext _dbContext;
        VerificationService verification = new();

        public PaymentService(DataDbContext dbContext)
        {
            _dbContext = dbContext;
            
        }
        public void CashDetails(CashModel cashModel)
        {
            if (verification.CashAuthorize(cashModel.customer_name, cashModel.shipment_name) == 1)
            {
                var cashPayInfo = new CashModel()
                {
                    order_id = cashModel.order_id,
                    customer_name = cashModel.customer_name,
                    shipment_name = cashModel.shipment_name,
                    product_name = cashModel.product_name,
                    product_count = cashModel.product_count
                };
                AddCashToDatabase(cashPayInfo);
            }
        }
        public void AddCashToDatabase(CashModel cashDetail)
        {
            var connectionstring = _dbContext.ConnectionString();
            using (SqlConnection sqlConnec = new SqlConnection(connectionstring))
            {
                sqlConnec.Open();

                SqlCommand cmd = new SqlCommand("AddCashDetails", sqlConnec);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@customer_name", cashDetail.customer_name);
                cmd.Parameters.AddWithValue("@shipment_name", cashDetail.shipment_name);
                cmd.Parameters.AddWithValue("@product_name", cashDetail.product_name);
                cmd.Parameters.AddWithValue("@product_count", cashDetail.product_count);
                cmd.ExecuteNonQuery();

            }
        }
        public void UpiDetails(UpiModel upi)
        {
            if (verification.UpiAuthorize(upi.phoneNo) == 1)
            {
                var upiPayInfo = new UpiModel()
                {
                    phoneNo = upi.phoneNo,
                    product_name = upi.product_name,
                    product_count = upi.product_count,
                };
                AddUpiToDatabase(upiPayInfo);
            }

        }
        public void AddUpiToDatabase(UpiModel upiDetail)
        {
            var connectionstring = _dbContext.ConnectionString();
            using (SqlConnection sqlConnec = new SqlConnection(connectionstring))
            {
                sqlConnec.Open();

                SqlCommand cmd = new SqlCommand("AddUpiDetails", sqlConnec);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@phone_no", upiDetail.phoneNo);
                cmd.Parameters.AddWithValue("@product_name", upiDetail.product_name);
                cmd.Parameters.AddWithValue("@product_count", upiDetail.product_count);
                cmd.ExecuteNonQuery();
            }
        }

        public void creditDetail(CreditModel credit)
        {
            if (verification.CreditAuthorize(credit.cnumber, credit.cservice, credit.expmonth, credit.expyear, credit.cvv) == 1)
            {
                var creditPayInfo = new CreditModel()
                {
                    cname = credit.cname,
                    cnumber = credit.cnumber,
                    cservice = credit.cservice,
                    expmonth = credit.expmonth,
                    expyear = credit.expyear,
                    cvv = credit.cvv,
                    product_name = credit.product_name,
                    product_count = credit.product_count
                };
                AddCreditToDatabase(creditPayInfo);
            }
        }
        public void AddCreditToDatabase(CreditModel creditDetail)
        {
            var connectionstring = _dbContext.ConnectionString();
            using (SqlConnection sqlConnec = new SqlConnection(connectionstring))
            {
                sqlConnec.Open();

                SqlCommand cmd = new SqlCommand("Add_CreditDetails", sqlConnec);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cname", creditDetail.cname);
                cmd.Parameters.AddWithValue("@cnumber", creditDetail.cnumber);
                cmd.Parameters.AddWithValue("@cservice", creditDetail.cservice);
                cmd.Parameters.AddWithValue("@expmonth", creditDetail.expmonth);
                cmd.Parameters.AddWithValue("@expyear", creditDetail.expyear);
                cmd.Parameters.AddWithValue("@product_name", creditDetail.product_name);
                cmd.Parameters.AddWithValue("@product_count", creditDetail.product_count);
                cmd.ExecuteNonQuery();
            }
        }


    }
}
