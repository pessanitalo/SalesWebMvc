using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.App.Services;
using SalesWebMvc.Business.Models;
using SalesWebMvc.Data.Context;

namespace SalesWebMvc.App.Controllers
{
    public class SellersController : Controller
    {
        private readonly DataContext _context;
        private readonly SellerService _service;

        public SellersController(DataContext context, SellerService service)
        {
            _context = context;
            _service = service;
        }

        public IActionResult Index()
        {
            var list = _service.FindAll();
            return View(list);
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Seller == null)
            {
                return NotFound();
            }

            var seller = await _context.Seller
                .FirstOrDefaultAsync(m => m.id == id);
            if (seller == null)
            {
                return NotFound();
            }

            return View(seller);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Name,Email,BirthDate,BaseSalary")] Seller seller)
        {
            if (ModelState.IsValid)
            {
                _context.Add(seller);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(seller);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Seller == null)
            {
                return NotFound();
            }

            var seller = await _context.Seller.FindAsync(id);
            if (seller == null)
            {
                return NotFound();
            }
            return View(seller);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Name,Email,BirthDate,BaseSalary")] Seller seller)
        {
            if (id != seller.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(seller);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SellerExists(seller.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(seller);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Seller == null)
            {
                return NotFound();
            }

            var seller = await _context.Seller
                .FirstOrDefaultAsync(m => m.id == id);
            if (seller == null)
            {
                return NotFound();
            }

            return View(seller);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Seller == null)
            {
                return Problem("Entity set 'SalesWebMvcAppContext.Seller'  is null.");
            }
            var seller = await _context.Seller.FindAsync(id);
            if (seller != null)
            {
                _context.Seller.Remove(seller);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SellerExists(int id)
        {
          return _context.Seller.Any(e => e.id == id);
        }
    }
}
