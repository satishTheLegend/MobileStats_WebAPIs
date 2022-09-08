using Microsoft.EntityFrameworkCore;
using MobileStats_WebAPIs.Models;

namespace MobileStats_WebAPIs.DBContext
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }


        public DbSet<MobileBrands> MobileBrands { get; set; }
        public DbSet<MobileDevices> MobileDevices { get; set; }
        public DbSet<MobileSellingRecords> MobileSellingRecords { get; set; }
    }
}
