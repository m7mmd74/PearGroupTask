using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PearTask.Models;

namespace PearTask.Controllers
{
    public class ProductsController : Controller
    {
        PearTaskContext contxt;
        public ProductsController()
        {
            contxt = new PearTaskContext();
        }

        // GET: Products
        public IActionResult Index()
        {
            List<Product> productlist = contxt.Products.Include(p=>p.Supplier).ToList();
            return View(productlist);
        }
        // GET: Products/Add
        public IActionResult Add()
        {
            return View();
        }

        // POST: Products/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }

            contxt.Products.Add(product);
            await contxt.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Products/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var product = await contxt.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            if (id != product.ProductID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                contxt.Update(product);
                await contxt.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var product = await contxt.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/1
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await contxt.Products.SingleOrDefaultAsync(p => p.ProductID == id);
            contxt.Products.Remove(product);
            await contxt.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
