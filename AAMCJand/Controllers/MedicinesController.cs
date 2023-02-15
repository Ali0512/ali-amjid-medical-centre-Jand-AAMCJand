using AAMCJand.Data.Services;
using AAMCJand.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AAMCJand.Controllers
{
    public class MedicinesController : Controller
    {
        private readonly IMedicinesService _service;
        public MedicinesController(IMedicinesService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var allMedicines = await _service.getAllAsync(n => n.Store);
            return View(allMedicines);
        }
        public async Task<IActionResult> Filter(string searchString)
        {
            var allMovies = await _service.getAllAsync(n => n.Store);

            if (!string.IsNullOrEmpty(searchString))
            {
                var filteredResult = allMovies.Where(n => n.Name.Contains(searchString) || n.Description.Contains(searchString)).ToList();
                return View("Index", filteredResult);
            }

            return View("Index", allMovies);
        }
        public async Task<IActionResult> Details(int id)
        {
            var mediDetails = await _service.GetMedicineByIdAsync(id);
            return View(mediDetails);
        }
        public async Task<IActionResult> Create()
        {
            var medicineDropDownData = await _service.GetNewMedicineDropDownVM();
            ViewBag.Stores = new SelectList(medicineDropDownData.Stores, "Id", "Name");
            ViewBag.Companies = new SelectList(medicineDropDownData.Companies, "Id", "FullName");
            ViewBag.Formulas = new SelectList(medicineDropDownData.Formulas, "Id", "FullName");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(NewMedicineVM movie)
        {
            if (!ModelState.IsValid)
            {
                var medicineDropDownData = await _service.GetNewMedicineDropDownVM();
                ViewBag.Stores = new SelectList(medicineDropDownData.Stores, "Id", "Name");
                ViewBag.Companies = new SelectList(medicineDropDownData.Companies, "Id", "FullName");
                ViewBag.Formulas = new SelectList(medicineDropDownData.Formulas, "Id", "FullName");
                return View(movie);
            }
            await _service.AddNewMedicineAsync(movie);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int id)
        {
            var movieDetails = await _service.GetMedicineByIdAsync(id);
            if (movieDetails == null) return View("NotFound");

            var response = new NewMedicineVM()
            {
                Id = movieDetails.Id,
                Name = movieDetails.Name,
                Description = movieDetails.Description,
                Price = movieDetails.Price,
                MenuDate = movieDetails.MenuDate,
                ExpiryDate = movieDetails.ExpiryDate,
                ImageURL = movieDetails.ImageURL,
                MediCategory = movieDetails.MediCategory,
                storeId = movieDetails.storeId,
                companyId = movieDetails.companyId,
                FormulaIds = movieDetails.Formula_Medicines.Select(n => n.formulaId).ToList(),
            };


            var movieDropDownData = await _service.GetNewMedicineDropDownVM();
            ViewBag.Stores = new SelectList(movieDropDownData.Stores, "Id", "Name");
            ViewBag.Companies = new SelectList(movieDropDownData.Companies, "Id", "FullName");
            ViewBag.Formulas = new SelectList(movieDropDownData.Formulas, "Id", "FullName");
            return View(response);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewMedicineVM movie)
        {
            if (id != movie.Id) return View("NotFound");
            if (!ModelState.IsValid)
            {
                var movieDropDownData = await _service.GetNewMedicineDropDownVM();
                ViewBag.Stores = new SelectList(movieDropDownData.Stores, "Id", "Name");
                ViewBag.Companies = new SelectList(movieDropDownData.Companies, "Id", "FullName");
                ViewBag.Formulas = new SelectList(movieDropDownData.Formulas, "Id", "FullName");
                return View(movie);
            }
            await _service.UpdateMedicineAsync(movie);
            return RedirectToAction(nameof(Index));
        }
    }
}
