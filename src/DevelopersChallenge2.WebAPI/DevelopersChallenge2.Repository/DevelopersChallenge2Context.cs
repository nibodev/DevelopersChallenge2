﻿using DevelopersChallenge2.Domain;
using Microsoft.EntityFrameworkCore;

namespace DevelopersChallenge2.Repository
{
    public class DevelopersChallenge2Context : DbContext
    {
        public DbSet<BankList> BankList { get; set; }
        public DbSet<Transaction> Transaction { get; set; }

        public DevelopersChallenge2Context(DbContextOptions<DevelopersChallenge2Context> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }

}
