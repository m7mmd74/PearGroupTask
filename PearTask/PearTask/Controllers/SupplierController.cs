using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PearTask.Models;

namespace PearTask.Controllers
{
    public class SupplierController : Controller
    {
        PearTaskContext contxt;
        public SupplierController()
        {
            contxt = new PearTaskContext();
        }
        public IActionResult Index()
        {
            List<Supplier> suppliers = contxt.Suppliers.ToList();
            return View(suppliers);
        }
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Supplier supplier)
        {
            if (!ModelState.IsValid)
            {
                return View(supplier);
            }

            contxt.Suppliers.Add(supplier);
            await contxt.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var supplier = await contxt.Suppliers.FindAsync(id);
            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Supplier supplier)
        {
            if (id != supplier.SupplierID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                contxt.Update(supplier);
                await contxt.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(supplier);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var supplier = await contxt.Suppliers.SingleOrDefaultAsync(p => p.SupplierID == id);
            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var supplier = await contxt.Suppliers.SingleOrDefaultAsync(p => p.SupplierID == id);
            contxt.Suppliers.Remove(supplier);
            await contxt.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
