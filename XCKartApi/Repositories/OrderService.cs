using Microsoft.Data.SqlClient;
using System.Data;
using XCKartApi.Database;
using XCKartApi.Models;
using XCKartApi.Repositories;

namespace XCKart.Repositories
{
    public class OrderService : IOrderService
    {
        private readonly DataDbContext _dbContext;
        public OrderService(DataDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<OrderModel> orderList = new();
        public void OrderDetails(OrderModel order)
        {
            var oderInfo = new OrderModel()
            {

                product_name = order.product_name,
                product_quantity = order.product_quantity,
                payment_type = order.payment_type,
                total_price = order.total_price

            };
            AddOrderToDatabase(oderInfo);
        }
        public void AddOrderToDatabase(OrderModel order)
        {
            var connectionstring = _dbContext.ConnectionString();
            using (SqlConnection sqlConnec = new SqlConnection(connectionstring))
            {
                sqlConnec.Open();

                SqlCommand cmd = new SqlCommand("AddOrderDetails", sqlConnec);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@product_name", order.product_name);
                cmd.Parameters.AddWithValue("@product_quantity", order.product_quantity);
                cmd.Parameters.AddWithValue("@payment_type", order.payment_type);
                cmd.ExecuteNonQuery();
            }
        }
        public List<OrderModel> GetOrder()
        {
            var connectionstring = _dbContext.ConnectionString();
            using (SqlConnection sqlConnec = new SqlConnection(connectionstring))
            {
                sqlConnec.Open();
                SqlCommand cmd = new SqlCommand("SP_GetPrice", sqlConnec);

                cmd.CommandType = CommandType.StoredProcedure;


                SqlDataReader reader = cmd.ExecuteReader();


                while (reader.Read())
                {
                    OrderModel order = new();

                    order.product_name = reader[0].ToString();
                    order.product_quantity = int.Parse(reader[1].ToString());
                    order.payment_type = int.Parse((reader[2].ToString()));
                    order.total_price = int.Parse(reader[3].ToString());

                    orderList.Add(order);
                }
                return orderList;
            }
        }
        public void UpdateCartDetails(ModifyOrderModel modify)
        {
            ModifyOrderModel modifyCart = new()
            {
                order_id = modify.order_id,
                product_name = modify.product_name,
                product_quantity = modify.product_quantity,
                payment_type = modify.payment_type

            };
        }
        public void UpdateOrderDatabase(ModifyOrderModel modify)
        {
            var connectionstring = _dbContext.ConnectionString();
            using (SqlConnection sqlConnec = new SqlConnection(connectionstring))
            {
                sqlConnec.Open();
                SqlCommand cmd = new SqlCommand("UpdateOrder_procedure", sqlConnec);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@order_id", modify.order_id);
                cmd.Parameters.AddWithValue("@product_name", modify.product_name);
                cmd.Parameters.AddWithValue("@product_quantity", modify.product_quantity);
                cmd.Parameters.AddWithValue("@payment_type", modify.payment_type);
                cmd.ExecuteReader();


            }

        }

    }
}
