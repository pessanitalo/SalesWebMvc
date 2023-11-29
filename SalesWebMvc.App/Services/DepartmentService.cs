using SalesWebMvc.Business.Models;
using SalesWebMvc.Data.Context;

namespace SalesWebMvc.App.Services
{
    public class DepartmentService
    {
        private readonly DataContext _context;

        public DepartmentService(DataContext context)
        {
            _context = context;
        }

        public List<Department> FindAll()
        {
            return _context.Department.OrderBy(x => x.Name).ToList();
        }
    }
}
