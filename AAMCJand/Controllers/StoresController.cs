using AAMCJand.Data.Services;
using AAMCJand.Models;
using Microsoft.AspNetCore.Mvc;

namespace AAMCJand.Controllers
{
    public class StoresController : Controller
    {
        private readonly IStoresService _service;
        public StoresController(IStoresService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var allStores = await _service.getAllAsync();
            return View(allStores);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Logo,Name,Description")] Store store)
        {
            //if(!ModelState.IsValid)
            //{
            //    return View(actor);
            //}
            await _service.AddAsync(store);
            return RedirectToAction(nameof(Index));
        }
        //Cinema/Details
        public async Task<IActionResult> Details(int id)
        {
            var storesDetails = await _service.GetByIdAsync(id);
            if (storesDetails == null) return View("NotFound");
            return View(storesDetails);
        }
        //Cinema/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var storesDetails = await _service.GetByIdAsync(id);
            if (storesDetails == null) return View("NotFound");
            return View(storesDetails);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Logo,Description")] Store store)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View(actor);
            //}
            await _service.UpdateAsync(id, store);
            return RedirectToAction(nameof(Index));
        }
        //Actors/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var storesDetails = await _service.GetByIdAsync(id);
            if (storesDetails == null) return View("NotFound");
            return View(storesDetails);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var storesDetails = await _service.GetByIdAsync(id);
            if (storesDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
