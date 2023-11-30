using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.App.Models;
using SalesWebMvc.App.Services;
using SalesWebMvc.Business.Models;
using SalesWebMvc.Business.Models.ViewModels;
using SalesWebMvc.Data.Context;
using System.Diagnostics;

namespace SalesWebMvc.App.Controllers
{
    public class SellersController : Controller
    {
        private readonly DataContext _context;
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;

        public SellersController(DataContext context, SellerService service, DepartmentService departmentService)
        {
            _context = context;
            _sellerService = service;
            _departmentService = departmentService;
        }

        public IActionResult Index()
        {
            var list = _sellerService.FindAll();
            return View(list);
        }


        public async Task<IActionResult> Details(int id)
        {

            var seller = _sellerService.FindById(id);
            return View(seller);
        }

        public IActionResult Create()
        {
            var departments = _departmentService.FindAll();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Seller seller)
        {
            if (ModelState.IsValid)
            {
                _context.Add(seller);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(seller);
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Houve um erro" });
            }

            var seller = _sellerService.FindById(id);

            if (seller == null)
            {
                return RedirectToAction(nameof(Error), new { message = "vendedor não encontrado" });
            }
            List<Department> department = _departmentService.FindAll();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = seller, Departments = department };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Seller seller)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Houve um erro" });
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
                        return RedirectToAction(nameof(Error), new { message = "Houve um erro" });
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

        public IActionResult Delete(int id)
        {
            if (id == null) return BadRequest();

            var seller = _sellerService.FindById(id);
            return View(seller);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (id == null) return BadRequest();
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return View(viewModel);
        }
        private bool SellerExists(int id)
        {
            return _context.Seller.Any(e => e.id == id);
        }
    }
}
