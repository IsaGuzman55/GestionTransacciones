using Microsoft.EntityFrameworkCore;
using GestionTransacciones.Models;

namespace GestionTransacciones.Data{
    public class PruebaContext : DbContext{
        public PruebaContext(DbContextOptions<PruebaContext> options) : base(options) { }
        public DbSet<Transaccion> Transacciones{get;set;}
        public DbSet<Cliente> Clientes{get;set;}
        public DbSet<Factura> Facturas{get;set;}
        public DbSet<Pago> Pagos{get;set;}
    
    }
}