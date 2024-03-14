using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Ass2_PizzaStore_VanTuan.Data;
using Ass2_PizzaStore_VanTuan.Models;

namespace Ass2_PizzaStore_VanTuan.Pages.ProductPages
{
    public class IndexModel : PageModel
    {
        private readonly Ass2_PizzaStore_VanTuan.Data.AppDBContext _context;

        public IndexModel(Ass2_PizzaStore_VanTuan.Data.AppDBContext context)
        {
            _context = context;
        }

        public IList<Product> Product { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Suppliers).ToListAsync();
        }
    }
}
