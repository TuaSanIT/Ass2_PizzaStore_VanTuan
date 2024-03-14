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

namespace Ass2_PizzaStore_VanTuan.Pages.Orders
{
	[Authorize(Policy = "Admin")]
	public class AdminModel : PageModel
	{
		private readonly AppDBContext _context;

		public AdminModel(AppDBContext context)
		{
			_context = context;
		}

		// startDate and endDate are used to filter orders by date
		[BindProperty]
		public DateTime? startDate { get; set; }
		[BindProperty]
		public DateTime? endDate { get; set; }


		public IList<Order> Order { get; set; } = default!;

		public async Task OnGetAsync()
		{
			if (_context.Orders != null)
			{
				Order = await _context.Orders
				.Include(o => o.Customer).ToListAsync();
			}
		}
		public async Task OnPostAsync()
		{
			if (_context.Orders != null)
			{
				if(startDate.HasValue && endDate.HasValue && startDate <= endDate)
					Order = await _context.Orders
					.Include(o => o.Customer)
					.Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate).OrderByDescending(o => o.OrderDate)
					.ToListAsync();
				else if (startDate.HasValue && !endDate.HasValue)
					Order = await _context.Orders
					.Include(o => o.Customer)
					.Where(o => o.OrderDate >= startDate).OrderByDescending(o => o.OrderDate)
                    .ToListAsync();
				else if (!startDate.HasValue && endDate.HasValue)
					Order = await _context.Orders
					.Include(o => o.Customer)
					.Where(o => o.OrderDate <= endDate).OrderByDescending(o => o.OrderDate)
                    .ToListAsync();
				else
					Order = await _context.Orders
					.Include(o => o.Customer)
					.ToListAsync();			
			}
		}
	}
}
