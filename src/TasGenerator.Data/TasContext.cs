using Microsoft.EntityFrameworkCore;
using TasGenerator.Model;

namespace TasGenerator.Data
{
    public class TasContext : DbContext
    {
        public TasContext(DbContextOptions<TasContext> options) : base(options)
        {

        }

        public DbSet<Season> Seasons { get; set; }
        public DbSet<Competition> Competitions { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Team> Teams { get; set; }
    
    }
}