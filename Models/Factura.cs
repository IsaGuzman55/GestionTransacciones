using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace GestionTransacciones.Models
{
    public class Factura
    {
        /* ------- */
        public int Id {get; set;}
        
        /* ------- */
        [Required(ErrorMessage = "El periodo de la factura es necesaria.")]
        [MinLength(1, ErrorMessage = "El periodo de la factura debe tener al menos {1} caracteres.")]
        [MaxLength(100, ErrorMessage = "El periodo de la factura  debe tener como maximo {1} caracteres.")]
        public string PeriodoFactura {get; set;}
        
        /* ------- */
        public int MontoFacturado {get; set;}

        /* ------- */
        [Required(ErrorMessage = "El número de la factura es necesaria.")]
        [MinLength(1, ErrorMessage = "El número de la factura debe tener al menos {1} caracteres.")]
        [MaxLength(100, ErrorMessage = "El número de la factura  debe tener como maximo {1} caracteres.")]
        public string NumeroFactura {get; set;}
        
        /* ------- */
        public int ClienteId {get; set;}


        
    }
}