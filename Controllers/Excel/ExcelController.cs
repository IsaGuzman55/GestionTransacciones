using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ClosedXML.Excel;
using GestionTransacciones.Models;
using GestionTransacciones.Data;
using GestionTransacciones.App.interfaces;
using GestionTransacciones.App.Services;

namespace GestionTransacciones.Controllers{

    public class ExcelController : Controller
    {
        private readonly IExcelRepository _excelRepository;

        public ExcelController(IExcelRepository excelRepository)
        {
            _excelRepository = excelRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ImportDocExcel(IFormFile excel)
        {
            await _excelRepository.ImportDataFromExcelAsync(excel);
            return RedirectToAction("Index");
        }


    }


}
