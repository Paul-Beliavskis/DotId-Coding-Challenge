using DotId.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotId.Persistence
{
    public class DotIdContext : DbContext
    {
        public DotIdContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<State> States { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<Score> Scores { get; set; }

        public DbSet<ScoreDetail> ScoreDetails { get; set; }
    }
}
