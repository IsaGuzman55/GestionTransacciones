using ClosedXML.Excel;
using GestionTransacciones.App.interfaces;
using GestionTransacciones.Data;
using GestionTransacciones.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionTransacciones.App.Services{
    public class ExcelRepository : IExcelRepository{
        private readonly PruebaContext _context;
        public ExcelRepository(PruebaContext context){
            _context = context;
        }

        public async Task ImportDataFromExcelAsync(IFormFile excel)
        {
            var workbook = new XLWorkbook(excel.OpenReadStream());
            var hoja = workbook.Worksheet(1);
            var primeraFilaUsada = hoja.FirstRowUsed().RangeAddress.FirstAddress.RowNumber;
            var ultimaFilaUsada = hoja.LastRowUsed().RangeAddress.FirstAddress.RowNumber;

            var clientes = new List<Cliente>();
            for (int i = primeraFilaUsada + 1; i <= ultimaFilaUsada; i++)
            {
                var fila = hoja.Row(i);

                // Verificar si el cliente ya existe
                var identificacion = fila.Cell(7).GetString();
                var cliente = _context.Clientes.FirstOrDefault(c => c.Identificacion == identificacion);
                if (cliente == null)
                {
                    cliente = new Cliente
                    {
                        Nombre = fila.Cell(6).GetString(),
                        Correo = fila.Cell(10).GetString(),
                        Identificacion = identificacion,
                        Direccion = fila.Cell(8).GetString(),
                        Telefono = fila.Cell(9).GetString(),
                        Contrasena = BCrypt.Net.BCrypt.HashPassword(fila.Cell(7).GetString()),
                        Rol = "User"
                    };
                    clientes.Add(cliente);
                }
            }

            await _context.AddRangeAsync(clientes);
            await _context.SaveChangesAsync();

            // TRANSACCIONES
            var transaccional = new List<Transaccion>();
            for (int i = primeraFilaUsada + 1; i <= ultimaFilaUsada; i++)
            {
                var fila = hoja.Row(i);

                    var cliente = clientes.FirstOrDefault(c => c.Identificacion == fila.Cell(7).GetString());

                    var transaccion = new Transaccion
                    {
                        Id = fila.Cell(1).GetString(),  // Extraer el Id como string
                        FechaHora = fila.Cell(2).GetDateTime(),
                        Monto = fila.Cell(3).GetValue<int>(),
                        Estado = fila.Cell(4).GetString(),
                        TipoTransaccion = fila.Cell(5).GetString(),
                        Plataforma = fila.Cell(11).GetString(),
                        ClienteId = cliente.Id
                    };
                    transaccional.Add(transaccion);
            
            }
            await _context.AddRangeAsync(transaccional);
            await _context.SaveChangesAsync();




            // FACTURAS
            var facturas = new List<Factura>();
            for (int i = primeraFilaUsada + 1; i <= ultimaFilaUsada; i++)
            {
                var fila = hoja.Row(i);

                    var cliente = clientes.FirstOrDefault(c => c.Identificacion == fila.Cell(7).GetString());

                    var factura = new Factura
                    {
                        PeriodoFactura = fila.Cell(13).GetString(),
                        MontoFacturado = fila.Cell(14).GetValue<int>(),
                        NumeroFactura = fila.Cell(12).GetString(),
                        ClienteId = cliente.Id
                    };
                    facturas.Add(factura);
            
            }
            await _context.AddRangeAsync(facturas);
            await _context.SaveChangesAsync();



            // PAGOS
            var Pagos = new List<Pago>();
            for (int i = primeraFilaUsada + 1; i <= ultimaFilaUsada; i++)
            {
                var fila = hoja.Row(i);

                    var sacarTransaccion = transaccional.FirstOrDefault(c => c.Id == fila.Cell(1).GetString());
                    var sacarFactura = facturas.FirstOrDefault(c => c.NumeroFactura == fila.Cell(12).GetString());

                    var Pago = new Pago
                    {
                        TransaccionId = sacarTransaccion.Id,
                        FacturaId = sacarFactura.Id,
                        MontoPagado = fila.Cell(15).GetValue<int>()
                    };
                    Pagos.Add(Pago);
            
            }
            await _context.AddRangeAsync(Pagos);
            await _context.SaveChangesAsync();

        }
    }
}