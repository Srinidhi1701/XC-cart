using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XCKartApi.Database;
using XCKartApi.Models;
using Microsoft.Data.Sql;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.Common;
using Microsoft.EntityFrameworkCore.Scaffolding;
using XCKartApi.Repositories;

namespace XCKart.Repositories
{
    public class ProductOrderService: IProductOrderService
    {       
        private readonly DataDbContext _dbContext;      
        public ProductOrderService(DataDbContext dbContext) 
        {
            _dbContext = dbContext;
        }
        public List<ProductModel> productList = new();
        public List<ProductModel>  GetProduct()
        {
            var connectionstring = _dbContext.ConnectionString();
            using (SqlConnection sqlConnec = new SqlConnection(connectionstring))
            {
                sqlConnec.Open();
                SqlCommand cmd = new SqlCommand("SP_Get_Product_Details", sqlConnec);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ProductModel prodItem = new ProductModel();
                    prodItem.product_id = int.Parse(reader[0].ToString());
                    prodItem.product_name = reader[1].ToString();
                    prodItem.product_description = reader[2].ToString();
                    prodItem.product_price = int.Parse(reader[3].ToString());
                    prodItem.product_manufacturer = reader[4].ToString();
                    productList.Add(prodItem);
                }
                return productList;
            }           
        }        
    }
}
