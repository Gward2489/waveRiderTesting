using Microsoft.EntityFrameworkCore;
using waveRiderTester.Models;

namespace waveRiderTester.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        public DbSet<Buoy> Buoy { get; set; }
        public DbSet<Beach> Beach { get; set; }

    }
}