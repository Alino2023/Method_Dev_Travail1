using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.LatePayment
{
    public class LatePaymentBorrower
    {
        public int LatePaymentId { get; set; }

        public DateTime LatePaymentDate {  get; set; }
    }
}
