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

namespace Ass2_PizzaStore_VanTuan.Pages.OrderPages
{
    [Authorize(Policy = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly Ass2_PizzaStore_VanTuan.Data.AppDBContext _context;

        public DeleteModel(Ass2_PizzaStore_VanTuan.Data.AppDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Order Order { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FirstOrDefaultAsync(m => m.OrderID == id);

            if (order == null)
            {
                return NotFound();
            }
            else
            {
                Order = order;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                Order = order;
                _context.Orders.Remove(Order);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Admin");
        }
    }
}
