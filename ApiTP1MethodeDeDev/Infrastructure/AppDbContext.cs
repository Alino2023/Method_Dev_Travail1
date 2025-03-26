using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Borrowers;
using Domain.Loans;
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

            modelBuilder.Entity<LoanEntity>().HasKey(l => l.IdLoan);

            modelBuilder.Entity<LoanEntity>(l => l.HasData(new LoanEntity 
            { IdLoan = 1, 
                Amount = 20000m, 
                InterestRate = 9.20m,
                 DurationInMonths = 24, 
                StartDate = DateTime.Now,
                RemainingAmount = 20000m,
                BorrowerSin = "987654321",
                //TheBorrower = null!
            }));

            //modelBuilder.Entity<LoanEntity>()
            //    .Property(l => l.Status)
            //    .HasConversion<int>(); // Stocker l'enum sous forme d'entier

            base.OnModelCreating(modelBuilder);
        }

       

    }
}
