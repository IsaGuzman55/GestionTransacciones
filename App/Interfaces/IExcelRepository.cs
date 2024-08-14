using GestionTransacciones.Models;

namespace GestionTransacciones.App.interfaces{
    public interface IExcelRepository{
        Task ImportDataFromExcelAsync(IFormFile excel);
    }
}