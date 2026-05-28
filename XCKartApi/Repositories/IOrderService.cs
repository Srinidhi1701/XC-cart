using XCKartApi.Models;

namespace XCKartApi.Repositories
{
    public interface IOrderService
    {
        public void OrderDetails(OrderModel order);
        public void AddOrderToDatabase(OrderModel order);

        public List<OrderModel> GetOrder();
        public void UpdateCartDetails(ModifyOrderModel modify);
        public void UpdateOrderDatabase(ModifyOrderModel modify);
    }
}
