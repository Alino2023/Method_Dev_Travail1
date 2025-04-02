using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.LatePayment
{
    public class LatePaymentBorrower
    {
        [Key]
        public int LatePaymentId { get; set; }

        public DateTime LatePaymentDate {  get; set; }
    }
}
