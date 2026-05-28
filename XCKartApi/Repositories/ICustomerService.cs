using XCKartApi.Models;

namespace XCKartApi.Repositories
{
    public interface ICustomerService
    {
        public void CustomerDetails(CustomerModel custDetails);
        public void AddCustomerToDatabase(CustomerModel customer);
        public List<CustomerModel> GetCustomer();
        public void UpdateCustomer(CustomerModel customer);
        public void UpdateCustomerDatabase(CustomerModel modify);
    }
}
