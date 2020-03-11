using Microsoft.EntityFrameworkCore;

namespace DevelopersChallenge2.Repository
{
    public class DevelopersChallenge2Context : DbContext
    {
        // public DbSet<Processo> Processo { get; set; }

        public DevelopersChallenge2Context(DbContextOptions<DevelopersChallenge2Context> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }

}
