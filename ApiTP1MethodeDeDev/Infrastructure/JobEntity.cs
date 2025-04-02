using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class JobEntity
    {
        public int JobId { get; set; }
        public string InstitutionName { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime EndingDate { get; set; }
        public Decimal MentualSalary { get; set; }
    }
}
