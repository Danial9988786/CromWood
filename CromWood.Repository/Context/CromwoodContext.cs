using CromWood.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CromWood.Data.Context
{
    public class CromwoodContext : DbContext
    {
        public CromwoodContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Test> Tests { get; set; }
    }
}
