using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Borrower;

namespace Infrastructure
{
    public class BorrowerEntity
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

        //public BorrowerEntity(Borrower borrower)
        //{
        //    Sin = borrower.Sin;
        //    FirstName = borrower.FirstName;
        //    LastName = borrower.LastName;
        //    Phone = borrower.Phone;
        //    Email = borrower.Email;
        //    Address = borrower.Address;
        //}


    }
}
