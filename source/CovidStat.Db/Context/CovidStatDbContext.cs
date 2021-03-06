using CovidStat.Db.Entities;
using Microsoft.EntityFrameworkCore;

namespace CovidStat.Db.Context
{
    public class CovidStatDbContext : DbContext
    {
        public CovidStatDbContext(DbContextOptions<CovidStatDbContext> options) : base(options)
        {
            
        }
        
        public DbSet<IpLocation> IpLocations { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<IpLocation>().HasKey(table => new {
                table.IpFrom, table.IpTo
            });
        }
    }
}