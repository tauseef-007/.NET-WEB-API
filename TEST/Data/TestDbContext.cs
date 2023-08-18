using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TEST.Models;

namespace TEST.Data
{
    public class TestDbContext : DbContext
    {
        public TestDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Records> Test { get; set; }
    }
}
