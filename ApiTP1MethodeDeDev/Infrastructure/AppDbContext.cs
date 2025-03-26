using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Borrower;
using Domain.Loan;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class AppDbContext : DbContext 
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<BorrowerEntity> Borrowers { get; set; } = null!;
        
        public DbSet<LoanEntity> Loans { get; set; } 




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BorrowerEntity>().HasKey(b => b.Sin);

            modelBuilder.Entity<BorrowerEntity>(b => b.HasData(
                new BorrowerEntity { Sin = "157489632", FirstName = "Zakaria", LastName = "Morjani", Phone = "4182571159", Email = "zakaria@gmail.com", Address = "le lac fortain" }
            ));


            modelBuilder.Entity<LoanEntity>()
                .Property(l => l.Status)
                .HasConversion<int>(); // Stocker l'enum sous forme d'entier

            base.OnModelCreating(modelBuilder);
        }

       

    }
}
