using Microsoft.EntityFrameworkCore;
using SalesWebMvc.App.Services.Exceptions;
using SalesWebMvc.Business.Models;
using SalesWebMvc.Data.Context;

namespace SalesWebMvc.App.Services
{
    public class SellerService
    {
        private readonly DataContext _context;

        public SellerService(DataContext context)
        {
            _context = context;
        }

        public List<Seller> FindAll()
        {
            return _context.Seller.ToList();
        }

        public Seller FindById(int id)
        {
            return _context.Seller.Include(obj => obj.Department).FirstOrDefault(obj => obj.id == id);
        }
        public void Remove(int id)
        {
            var seller = _context.Seller.Find(id);
            _context.Seller.Remove(seller);
            _context.SaveChanges();
        }

        public void Update(Seller obj)
        {
            if (!_context.Seller.Any(x => x.id == obj.id))
            {
                throw new NotFoundException("Id não encontrado");
            }
            try
            {
                _context.Seller.Update(obj);
                _context.SaveChanges();
            }
            catch (DbConcurrencyException e)
            {

                throw new DbConcurrencyException(e.Message);
            }

        }
    }
}
