using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Emploi
{
    public class Job
    {
        public int JobId {  get; set; } 
        public string InstitutionName {  get; set; }
        DateTime StartingDate { get; set; }
        DateTime EndingDate { get; set; }
        Decimal MentualSalary {  get; set; }

        
    }
}
