using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class LatePaymentBorrowerEntity
    {
        [Key]
        public int LatePaymentId { get; set; }

        public DateTime LatePaymentDate { get; set; }
    }
}
