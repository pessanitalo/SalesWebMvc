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
    }
}
