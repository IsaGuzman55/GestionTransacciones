using GestionTransacciones.App.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using GestionTransacciones.Models;
using GestionTransacciones.App.interfaces;

namespace GestionTransacciones.Controllers{
    public class ClienteController : Controller{

        private readonly IClienteRepository _clienteRepository;
        public ClienteController(IClienteRepository clienteRepository){
            _clienteRepository = clienteRepository;
        }

        // Registrar cliente
        // GET  
        [HttpGet]
        public IActionResult SignUp(){
             if(User.Identity!.IsAuthenticated){
                return RedirectToAction("Index", "Home");
            }
            return View();
            
        }

        // POST
        [HttpPost]
        public async Task<IActionResult> SignUp(Cliente cliente){
            // VERIFICAR SI YA EXISTE UN CLIENTE CON EL MISMO CORREO
            var foundClient = await _clienteRepository.ObtenerClientePorCorreo(cliente.Correo);
            if(foundClient!= null){
               ViewData["Mensaje"] = "El correo ingresado ya esta registrado";
                return View();
            }

            // Crear el usuario con sus datos
            var clienteNuevo = new Cliente()
            {
                Nombre = cliente.Nombre,
                Identificacion = cliente.Identificacion,
                Direccion = cliente.Direccion,
                Telefono = cliente.Telefono,
                Correo = cliente.Correo,
                Contrasena = BCrypt.Net.BCrypt.HashPassword(cliente.Contrasena),
                Rol = "User"
            };

            // Llamar la funcion para crear un nuevo cliente
            var usuarioCreado = await _clienteRepository.CrearCliente(clienteNuevo);

            if(usuarioCreado){
                // Si el usuario se creo mandarlo a la página del login.
                return RedirectToAction("Login", "Cliente");
            }

            // Si no se creó, notificar el error
            ViewData["Mensaje"] = "No se pudo crear el usuario";
            return View();
        }

        // Iniciar sesion
        [HttpGet]
        public IActionResult Login(){
            if(User.Identity!.IsAuthenticated){
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // POST
        [HttpPost]
        public async Task<IActionResult> Login(Cliente cliente){
            // Obtener al cliente registrado
            var clienteEncontrado = await _clienteRepository.ObtenerCliente(cliente.Correo, cliente.Contrasena);

            if(clienteEncontrado == null){
                ViewData["ErrorMessage"] = "Correo o contraseña incorrectos";
                return View();
            }

            if(clienteEncontrado.Correo == "robinson.cortes@riwi.io" && clienteEncontrado.Rol == "Admin"){
                return RedirectToAction("Admin", "Cliente");
            }

            // Iniciar sesion con los datos del usuario
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, clienteEncontrado.Id.ToString()),
                new Claim(ClaimTypes.Name, clienteEncontrado.Nombre),
                new Claim(ClaimTypes.Email, clienteEncontrado.Correo),
                new Claim(ClaimTypes.Role, clienteEncontrado.Rol)
            };


            /* ClaimsIdentity: Es una colección de Claims que describen la identidad del usuario */
            /* Crear un nuevo ClaimsIdentity en el que a traves de "claims" le pasaremos los datos que guardamos anteriormente(Name) */
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            AuthenticationProperties properties = new AuthenticationProperties(){
                /* Refercar la sesion del usuario */
                AllowRefresh = true,
            };

            await HttpContext.SignInAsync(
                /* Utilizar el esquema de autenticaion por Cookie */
                CookieAuthenticationDefaults.AuthenticationScheme,
                /* Pasarle el ClaimsIdentity que creamos anteriormente */
                new ClaimsPrincipal(claimsIdentity),
                properties
            );
            return RedirectToAction("Index", "Home");
        }

        // Funcionlidad para cerrar sesion
        public async Task<IActionResult> LogOut(){
            /* SignOutAsync: Eliminar Cookies de autenticación */
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Cliente");
        }
        


        // ADMIN
        [HttpGet]
        public IActionResult Admin(){
            return View();
            
        }
    }
}