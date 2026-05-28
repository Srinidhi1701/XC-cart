using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.Common;
using XCKartApi;
using XCKartApi.Models;

namespace XCKartApi.Database
{
    public class DataDbContext : DbContext
    {

        private readonly IConfiguration Configuration;
        private IDbConnection connection { get; }
        public DataDbContext( IConfiguration configuration)
        {

            Configuration = configuration;
            connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnectionString"));
        }
        public string ConnectionString()
        {
            return connection.ConnectionString;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionString"));

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connection.ToString());
            }
        }
        public virtual DbSet<ProductModel> product { get; set; }
        public virtual DbSet<CartModel> cart { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductModel>().HasKey(c => c.product_id);
            modelBuilder.Entity<CartModel>().HasKey(d => d.c_id);

        }



    }
}
