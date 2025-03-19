using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Borrower;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class AppDbContext : DbContext 
    {
        public DbSet<BorrowerEntity> Borrowers { get; set; } = null!;
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<BorrowerEntity> Borrowers { get; set; } = null!;
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BorrowerEntity>().HasKey(b => b.Sin);

            modelBuilder.Entity<BorrowerEntity>(b => b.HasData(
                new BorrowerEntity { Sin = "157489632", FirstName = "Zakaria", LastName = "Morjani", Phone = "4182571159", Email = "zakaria@gmail.com", Address = "le lac fortain" }
            ));

            base.OnModelCreating(modelBuilder);
        }


    }
}
