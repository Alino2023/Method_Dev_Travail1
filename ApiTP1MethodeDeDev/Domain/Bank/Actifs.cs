using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Bank
{
    public class Actifs
    {
        int id {  get; set; }
        decimal Valeur { get; set; }
        string Description {  get; set; }

        public Actifs(decimal valeur, string description)
        {
            Valeur = valeur;
            Description = description;
        }
    }
}
