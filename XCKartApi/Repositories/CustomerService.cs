using Microsoft.Data.SqlClient;
using System.Data;
using XCKartApi.Database;
using XCKartApi.Models;
using XCKartApi.Repositories;

namespace XCKart.Repositories
{
    public class CustomerService : ICustomerService
    {
        private readonly DataDbContext _dbContext;
        VerificationService verfication = new();
        public CustomerService(DataDbContext dbContext)
        {
            _dbContext = dbContext;
            
        }
        public List<CustomerModel> customerList = new();
        public List<CustomerModel> GetCustomer()
        {
            var connectionstring = _dbContext.ConnectionString();
            using (SqlConnection sqlConnec = new SqlConnection(connectionstring))
            {
                sqlConnec.Open();

                SqlCommand cmd = new SqlCommand("SP_Get_Customer_Details", sqlConnec);

                cmd.CommandType = CommandType.StoredProcedure;


                SqlDataReader reader = cmd.ExecuteReader();


                while (reader.Read())
                {
                    CustomerModel customerDetail = new();
                    customerDetail.customer_id = int.Parse(reader[0].ToString());
                    customerDetail.customer_name = reader[1].ToString();
                    customerDetail.phone_no = reader[2].ToString();
                    customerDetail.email = reader[3].ToString();
                    customerDetail.cust_address = reader[4].ToString();
                    customerList.Add(customerDetail);

                }
                return customerList;

            }
        }
        public void CustomerDetails(CustomerModel custDetails)
        {
            if (verfication.CustomerVerification(custDetails) == 3)
            {
                var CustomerDetailModel = new CustomerModel()
                {
                    customer_name = custDetails.customer_name,
                    phone_no = custDetails.phone_no,
                    email = custDetails.email,
                    cust_address = custDetails.cust_address,

                };
                AddCustomerToDatabase(CustomerDetailModel);

            }
        }
        public void AddCustomerToDatabase(CustomerModel customer)
        {
            var connectionstring = _dbContext.ConnectionString();
            using (SqlConnection sqlConnec = new SqlConnection(connectionstring))
            {
                sqlConnec.Open();

                SqlCommand cmd = new SqlCommand("SP_AddCustomerDetails", sqlConnec);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@customer_name", customer.customer_name);
                cmd.Parameters.AddWithValue("@phone_no", customer.phone_no);
                cmd.Parameters.AddWithValue("@email", customer.email);
                cmd.Parameters.AddWithValue("@cust_address", customer.cust_address);
                cmd.ExecuteNonQuery();

            }
        }
        public void UpdateCustomer(CustomerModel customer)
        {
            var CustomerUpdate = new CustomerModel()
            {
                customer_id = customer.customer_id,
                customer_name = customer.customer_name,
                phone_no = customer.phone_no,
                email = customer.email,
                cust_address = customer.cust_address

            };
            UpdateCustomerDatabase(CustomerUpdate);
        }
        public void UpdateCustomerDatabase(CustomerModel modify)
        {
            var connectionstring = _dbContext.ConnectionString();
            using (SqlConnection sqlConnec = new SqlConnection(connectionstring))
            {
                sqlConnec.Open();

                SqlCommand cmd = new SqlCommand("[UpdateCustomer_procedure]", sqlConnec);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@customer_id", modify.customer_id);
                cmd.Parameters.AddWithValue("@customer_name", modify.customer_name);
                cmd.Parameters.AddWithValue("@phone_no", modify.phone_no);
                cmd.Parameters.AddWithValue("@email", modify.email);
                cmd.Parameters.AddWithValue("@cust_address", modify.cust_address);
                cmd.ExecuteReader();
            }
        }
    }
}
