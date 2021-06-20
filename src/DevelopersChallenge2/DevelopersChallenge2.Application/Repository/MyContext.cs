using DevelopersChallenge2.Application.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace DevelopersChallenge2.Application.Repository
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options)
        : base(options)
        { }

        public DbSet<Transaction> Transactions { get; set; }
    }
}
