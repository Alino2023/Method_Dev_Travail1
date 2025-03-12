using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Borrower
{
    public class Borrower
    {
        [Required]
        [MinLength(9)]
        [MaxLength(11)]
        [Description("User's Social insurance Number")]
        //[Example("123-456-789")]
        private string Sin { get; set; }

        [Required]
        [MaxLength(255)]
        [Description("User's FirstName")]
        private string FirstName { get; set; }

        [Required]
        [MaxLength(255)]
        [Description("User's LastName")]
        private string LastName { get; set; }

        [Required]
        [MaxLength(11)]
        [Description("User's Phone")]
        private string Phone { get; set; }

        [Required]
        [MaxLength(255)]
        [Description("User's Email")]
        private string Email { get; set; }

        [Required]
        [MaxLength(255)]
        [Description("User's Address")]
        private string Address { get; set; }

        public Borrower()
        {
        }

    }
}
