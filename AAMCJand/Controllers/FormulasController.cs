using AAMCJand.Data.Services;
using AAMCJand.Models;
using Microsoft.AspNetCore.Mvc;

namespace AAMCJand.Controllers
{
    public class FormulasController : Controller
    {
        private readonly IFormulasService _service;
        public FormulasController(IFormulasService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var allFormula = await _service.getAllAsync();
            return View(allFormula);
        }
        //Formulas/Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("FullName,ProfilePictureURL,Description")] Formula formula)
        {
            //if(!ModelState.IsValid)
            //{
            //    return View(actor);
            //}
            await _service.AddAsync(formula);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int id)
        {
            var formulaDetails = await _service.GetByIdAsync(id);
            if (formulaDetails == null) return View("NotFound");
            return View(formulaDetails);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var formulaDetails = await _service.GetByIdAsync(id);
            if (formulaDetails == null) return View("NotFound");
            return View(formulaDetails);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,ProfilePictureURL,Description")] Formula formula)
        {
            //if(!ModelState.IsValid)
            //{
            //    return View(actor);
            //}
            await _service.UpdateAsync(id, formula);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            var formulaDetails = await _service.GetByIdAsync(id);
            if (formulaDetails == null) return View("NotFound");
            return View(formulaDetails);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var formulaDetails = await _service.GetByIdAsync(id);
            if (formulaDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
