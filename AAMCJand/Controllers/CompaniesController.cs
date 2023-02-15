using AAMCJand.Data.Services;
using AAMCJand.Models;
using Microsoft.AspNetCore.Mvc;

namespace AAMCJand.Controllers
{
    public class CompaniesController : Controller
    {
        private readonly ICompaniesService _service;
        public CompaniesController(ICompaniesService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var allCompanies = await _service.getAllAsync();
            return View(allCompanies);
        }

        //Get Companies Details
        public async Task<IActionResult> Details(int id)
        {
            var companyDetail = await _service.GetByIdAsync(id);
            if (companyDetail == null) return View("NotFound");
            return View(companyDetail);
        }
        //Get Companies Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("ProfilePictureURL,FullName,Description")] Company company)
        {
            await _service.AddAsync(company);
            return RedirectToAction(nameof(Index));
        }
        //Get Producer/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var companyDetail = await _service.GetByIdAsync(id);
            if (companyDetail == null) return View("NotFound");
            return View(companyDetail);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProfilePictureURL,FullName,Description")] Company company)
        {
            if (id == company.Id)
            {
                await _service.UpdateAsync(id, company);
                return RedirectToAction(nameof(Index));
            }
            return View(company);
        }

        //Get Companies/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var companyDetail = await _service.GetByIdAsync(id);
            if (companyDetail == null) return View("NotFound");
            return View(companyDetail);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var companyDetail = await _service.GetByIdAsync(id);
            if (companyDetail == null) return View("NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
