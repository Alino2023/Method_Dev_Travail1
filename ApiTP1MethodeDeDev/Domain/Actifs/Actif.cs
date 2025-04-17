using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Actifs
{
    public class Actif
    {
        public Actif(int id, decimal valeur, string description, long borroweId)
        {
            Id = id;
            Valeur = valeur;
            Description = description;
            BorroweId = borroweId;
        }

        public int Id { get; set; }
        public decimal Valeur { get; set; }
        public string Description { get; set; }
        public long BorroweId {  get; set; }


    }
}
