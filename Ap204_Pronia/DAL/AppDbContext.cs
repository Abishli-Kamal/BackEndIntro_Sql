using Ap204_Pronia.Models;
using Microsoft.EntityFrameworkCore;

namespace Ap204_Pronia.DAL
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options ):base(options)
        {

        }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }
}
