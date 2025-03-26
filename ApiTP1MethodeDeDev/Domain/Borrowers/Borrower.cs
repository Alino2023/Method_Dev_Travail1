using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Borrowers
{
    public class Borrower
    {
        [Key]
        [Required]
        [MinLength(9)]
        [MaxLength(11)]
        [Description("User's Social insurance Number")]
        //[Example("123-456-789")]
        public string Sin { get; set; }

        [Required]
        [MaxLength(255)]
        [Description("User's FirstName")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(255)]
        [Description("User's LastName")]
        public string LastName { get; set; }

        [Required]
        [MaxLength(11)]
        [Description("User's Phone")]
        public string Phone { get; set; }

        [Required]
        [MaxLength(255)]
        [Description("User's Email")]
        public string Email { get; set; }

        [Required]
        [MaxLength(255)]
        [Description("User's Address")]
        public string Address { get; set; }

        [Required]
        public decimal MonthlyIncome { get; set; } 

        public List<decimal> MonthlyLoanPayments { get; set; } = new List<decimal>();
        public List<decimal> ActiveLoanPayments { get; set; } = new List<decimal>(); 


        public Borrower()
        {
        }

        public Borrower(string sin, string firstName, string lastName, string phone, string email, string address, decimal monthlyIncome)
        {
            Sin = sin;
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            Email = email;
            Address = address;
            MonthlyIncome = monthlyIncome;  
        }

        public Borrower(string sin, string firstName, string lastName, string phone, string email, string address)
        {
            Sin = sin;
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            Email = email;
            Address = address;
        }
    }
}
