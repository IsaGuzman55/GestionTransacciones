using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace GestionTransacciones.Models
{
    public class Transaccion
    {
        /* ------- */
        public int Id {get; set;}
        
        /* ------- */
        [Required(ErrorMessage = "La fecha de la transaccion es obligatoria.")]
        [DataType(DataType.DateTime)]
        public DateTime FechaHora {get; set;}
        
        /* ------- */
        public int Monto {get; set;}

        /* ------- */
        [Required(ErrorMessage = "El estado de la transaccion es necesaria.")]
        public string Estado {get; set;}
        
        /* ------- */
        [Required(ErrorMessage = "El tipo de transaccion es necesaria.")]
        [MinLength(1, ErrorMessage = "El tipo de transaccion debe tener al menos {1} caracteres.")]
        [MaxLength(100, ErrorMessage = "El tipo de transaccion debe tener como maximo {1} caracteres.")]
        public string TipoTransaccion {get; set;}
        

        /* ------- */
        [Required(ErrorMessage = "La plataforma de la transaccion es necesaria es necesaria.")]
        [MinLength(1, ErrorMessage = "El correo debe tener al menos {1} caracteres.")]
        [MaxLength(100, ErrorMessage = "El correo debe tener como maximo {1} caracteres.")]
        public string CorrPlataformaeo {get; set;}
        
        /* ------- */
        [Required(ErrorMessage = "La contraseña del cliente es necesaria.")]
        [MinLength(1, ErrorMessage = "La contraseña debe tener al menos {1} caracteres.")]
        [MaxLength(100, ErrorMessage = "La contraseña debe tener como maximo {1} caracteres.")]
        public string Contrasena {get; set;}

        /* ------- */
        public string Rol {get; set;}

        
    }
}