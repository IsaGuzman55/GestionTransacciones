using GestionTransacciones.Models;

namespace GestionTransacciones.App.interfaces{
    public interface IClienteRepository{
        Task<bool> CrearCliente(Cliente clienteRegistrado);
        Task<Cliente> ObtenerClientePorCorreo(string correo);
        Task<Cliente> ObtenerCliente(string clienteEmail, string password);
    }
}