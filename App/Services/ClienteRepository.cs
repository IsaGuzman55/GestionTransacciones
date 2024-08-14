using GestionTransacciones.App.interfaces;
using GestionTransacciones.Data;
using GestionTransacciones.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionTransacciones.App.Services{
    public class ClienteRepository : IClienteRepository{
        private readonly PruebaContext _context;
        public ClienteRepository(PruebaContext context){
            _context = context;
        }

        // SignUp: POST
        // Comprobar la creacion del cliente
        public async Task<bool> CrearCliente(Cliente clienteRegistrado){
            // Agregar y guardar los datos   
            await _context.Clientes.AddAsync(clienteRegistrado);
            await _context.SaveChangesAsync();

            // Comprobar si el usuario se creo
            return clienteRegistrado.Id > 0;
        }

        // Verificar la existencia del cleinte
        public async Task<Cliente> ObtenerClientePorCorreo(string correobuscado){
            return await _context.Clientes.FirstOrDefaultAsync(c => c.Correo == correobuscado);
        }

        // LogIn: POST
        // Encontrar al usuario que inicio sesion
        public async Task<Cliente> ObtenerCliente(string clienteEmail, string password){
            var clienteRegistrado = await _context.Clientes.Where(c => c.Correo == clienteEmail).FirstOrDefaultAsync();

            if(clienteRegistrado == null){
                return null;
            }

            bool contrasenaValida = BCrypt.Net.BCrypt.Verify(password, clienteRegistrado.Contrasena);
        
            if(contrasenaValida){
                return clienteRegistrado;
            }else{
                return null;
            }
        }

    }
}