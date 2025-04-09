using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Emploi
{
    public class Job
    {
        [Key]
        public int JobId {  get; set; } 
        public string InstitutionName {  get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime EndingDate { get; set; }
        public Decimal MentualSalary {  get; set; }

        
    }
}
