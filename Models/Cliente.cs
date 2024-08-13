using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace GestionTransacciones.Models
{
    public class Cliente
    {
        /* ------- */
        public int Id {get; set;}
        
        /* ------- */
        [Required(ErrorMessage = "El nombre del cliente es necesario.")]
        [MinLength(1, ErrorMessage = "El nombre debe tener al menos {1} caracteres.")]
        [MaxLength(100, ErrorMessage = "El nombre debe tener como maximo {1} caracteres.")]
        public string Nombre {get; set;}
        
        /* ------- */
        [Required(ErrorMessage = "La identificacion del cliente es necesario.")]
        [MinLength(1, ErrorMessage = "La identificacion debe tener al menos {1} caracteres.")]
        [MaxLength(50, ErrorMessage = "La identificacion debe tener como maximo {1} caracteres.")]
        public string Identificacion {get; set;}

        /* ------- */
        [Required(ErrorMessage = "La direccion del cliente es necesaria.")]
        [MinLength(1, ErrorMessage = "La direccion debe tener al menos {1} caracteres.")]
        [MaxLength(255, ErrorMessage = "La direccion debe tener como maximo {1} caracteres.")]
        public string Direccion {get; set;}
        
        /* ------- */
        [Required(ErrorMessage = "El telefono del cliente es necesaria.")]
        [MinLength(1, ErrorMessage = "El telefono debe tener al menos {1} caracteres.")]
        [MaxLength(50, ErrorMessage = "El telefono debe tener como maximo {1} caracteres.")]
        public string Telefono {get; set;}
        
        /* ------- */
        [Required(ErrorMessage = "El correo del cliente es necesaria.")]
        [MinLength(1, ErrorMessage = "El correo debe tener al menos {1} caracteres.")]
        [MaxLength(100, ErrorMessage = "El correo debe tener como maximo {1} caracteres.")]
        public string Correo {get; set;}
        
        /* ------- */
        [Required(ErrorMessage = "La contraseña del cliente es necesaria.")]
        [MinLength(1, ErrorMessage = "La contraseña debe tener al menos {1} caracteres.")]
        [MaxLength(100, ErrorMessage = "La contraseña debe tener como maximo {1} caracteres.")]
        public string Contrasena {get; set;}

        /* ------- */
        public string Rol {get; set;}

        
    }
}