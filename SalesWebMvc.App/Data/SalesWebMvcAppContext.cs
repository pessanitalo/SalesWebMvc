using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Business.Models;

namespace SalesWebMvc.App.Data
{
    public class SalesWebMvcAppContext : DbContext
    {
        public SalesWebMvcAppContext (DbContextOptions<SalesWebMvcAppContext> options)
            : base(options)
        {
        }

        public DbSet<SalesWebMvc.Business.Models.Seller> Seller { get; set; } = default!;
    }
}
