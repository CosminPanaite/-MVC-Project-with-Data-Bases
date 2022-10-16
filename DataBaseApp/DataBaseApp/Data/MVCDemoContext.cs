using DataBaseApp.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace DataBaseApp.Data
{
    public class MVCDemoContext : DbContext
    {
        public MVCDemoContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }

    }
}
