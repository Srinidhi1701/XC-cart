using Microsoft.Data.SqlClient;
using System.Data;
using XCKartApi.Database;
using XCKartApi.Models;
using XCKartApi.Repositories;

namespace XCKart.Repositories
{
    public class CartService : ICartService
    {
        private readonly DataDbContext _dbContext;
        public CartService(DataDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<CartModel> cartList = new();
        public List<CartModel> GetCart()
        {
            var connectionstring = _dbContext.ConnectionString();
            using (SqlConnection sqlConnec = new SqlConnection(connectionstring))
            {
                sqlConnec.Open();

                SqlCommand cmd = new SqlCommand("[SP_GetCartPrice]", sqlConnec);

                cmd.CommandType = CommandType.StoredProcedure;


                SqlDataReader reader = cmd.ExecuteReader();


                while (reader.Read())
                {
                    CartModel cartDetails = new CartModel();
                    cartDetails.c_id = int.Parse(reader[0].ToString());
                    cartDetails.customer_id= int.Parse(reader[1].ToString());
                    cartDetails.product_name = reader[2].ToString();
                    cartDetails.product_id = int.Parse(reader[3].ToString());
                    cartDetails.product_description = reader[4].ToString();
                    cartDetails.product_count = int.Parse(reader[5].ToString());
                    cartDetails.total_price = int.Parse(reader[6].ToString());
                    cartList.Add(cartDetails);

                }
                return cartList;


            }


        }
        public void AddCartDetails(CartModel amodel)
        {
            var CartModel = new CartModel()
            {
                customer_id = amodel.customer_id,
                product_name = amodel.product_name,
                product_id = amodel.product_id,
                product_description = amodel.product_description,
                product_count = amodel.product_count,
                total_price= amodel.total_price,
            };
            AddCartToDatabase(amodel);


        }
        public void AddCartToDatabase(CartModel cartModel)
        {
            var connectionstring = _dbContext.ConnectionString();
            using (SqlConnection sqlConnec = new SqlConnection(connectionstring))
            {
                sqlConnec.Open();

                SqlCommand cmd = new SqlCommand("AddCartDetails", sqlConnec);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@customer_id", cartModel.customer_id);
                cmd.Parameters.AddWithValue("@product_name", cartModel.product_name);
                cmd.Parameters.AddWithValue("@product_id", cartModel.product_id);
                cmd.Parameters.AddWithValue("@product_description", cartModel.product_description);
                cmd.Parameters.AddWithValue("@product_count", cartModel.product_count);
                
                cmd.ExecuteNonQuery();

            }
        }
        public void DeleteCart(int id)
        {
            var connectionstring = _dbContext.ConnectionString();
            using (SqlConnection sqlConnec = new SqlConnection(connectionstring))
            {
                sqlConnec.Open();
                SqlCommand cmd = new SqlCommand("SP_delete_cart", sqlConnec);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@c_id", id);
                cmd.ExecuteNonQuery();
            }
        }
        public void UpdateCart(CartModel cart)
        {
            var cartItemModify = new CartModel()
            {
                c_id = cart.c_id,
                customer_id = cart.customer_id,
                product_name = cart.product_name,
                product_id = cart.product_id,
                product_description = cart.product_description,
                product_count = cart.product_count

            };
            UpdateCartDatabase(cartItemModify);

        }
        public void UpdateCartDatabase(CartModel cart)
        {
            var connectionstring = _dbContext.ConnectionString();
            using (SqlConnection sqlConnec = new SqlConnection(connectionstring))
            {
                sqlConnec.Open();

                SqlCommand cmd = new SqlCommand("UpdateCustomer_procedure", sqlConnec);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@c_id", cart.c_id);
                cmd.Parameters.AddWithValue("@customer_id", cart.customer_id);
                cmd.Parameters.AddWithValue("@product_name", cart.product_name);
                cmd.Parameters.AddWithValue("@product_id", cart.product_id);
                cmd.Parameters.AddWithValue("@product_description", cart.product_description);
                cmd.Parameters.AddWithValue("@product_count", cart.product_count);
                
                cmd.ExecuteNonQuery();


            }

        }


    }
}
