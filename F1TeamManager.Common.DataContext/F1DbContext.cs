using F1.Common.DataContext.DataModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1.Common.DataContext
{
    public class F1DbContext : DbContext
    {
        public DbSet<Team> Teams { get; set; }
        public DbSet<LicenseFee> LicenseFees { get; set; }
        public DbSet<Champion> Champions { get; set; }

        public F1DbContext() : base()
        {
        }

        public F1DbContext(DbContextOptions<F1DbContext> options)
        : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var path = Path.Combine(Environment.CurrentDirectory, "f1tm.db");
            options.UseSqlite($"Filename={path}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LicenseFee>()
                .Property(l => l.Year)
                .HasConversion<string>();

            modelBuilder.Entity<Team>()
                .Property(t => t.EstabilishmentDate)
                .HasConversion<string>();
        }
    }
}
