using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Ass2_PizzaStore_VanTuan.Data;
using Ass2_PizzaStore_VanTuan.Models;
using Microsoft.AspNetCore.Authorization;

namespace Ass2_PizzaStore_VanTuan.Pages.CustomersPages
{
    [Authorize(Policy = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly Ass2_PizzaStore_VanTuan.Data.AppDBContext _context;

        public IndexModel(Ass2_PizzaStore_VanTuan.Data.AppDBContext context)
        {
            _context = context;
        }

        public IList<Customers> Customers { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Customers = await _context.Customers.ToListAsync();
        }
    }
}
