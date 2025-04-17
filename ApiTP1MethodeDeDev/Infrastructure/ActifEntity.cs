using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class ActifEntity
    {
        public int Id { get; set; }
        public decimal Valeur { get; set; }
        public string Description { get; set; }
        public long BorroweId { get; set; }
    }
}
