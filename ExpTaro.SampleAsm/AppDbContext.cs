using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpTaro.SampleAsm
{
    public class AppDbContext : DbContext
    {
        public virtual DbSet<OrderDetails> OrderDetails { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Products> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<OrderDetails>().HasNoKey();
            modelBuilder.Entity<Orders>().HasKey(x => x.Cd);
            modelBuilder.Entity<Users>().HasKey(x => x.Cd);
            modelBuilder.Entity<Products>().HasKey(x => x.Cd);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql($"").EnableSensitiveDataLogging().LogTo(Console.WriteLine);
        }
    }
}
