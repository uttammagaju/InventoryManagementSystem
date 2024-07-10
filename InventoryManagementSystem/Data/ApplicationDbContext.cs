using InventoryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
namespace InventoryManagementSystem.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<ItemModel>   Items { get; set; }
        public DbSet<CustomerModel> Customers { get; set; }
        public DbSet<SalesMasterModel> SalesMaster { get; set; }
        public DbSet<SalesDetailsModel> SalesDetails { get; set; }
    }
}
