using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GestionTransacciones.Models
{
    public class Pago
    {
        /* ------- */
        public int Id {get; set;}
        
        /* ------- */
        public string TransaccionId {get; set;}
        
        /* ------- */
        public int FacturaId {get; set;}

        /* ------- */
        public int MontoPagado {get; set;}
        
    }
}