using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ass2_PizzaStore_VanTuan.Data;
using Ass2_PizzaStore_VanTuan.Models;
using System.Security.Claims;

namespace Ass2_PizzaStore_VanTuan.Pages.Customers
{
	public class UpdateProfileModel : PageModel
	{
		private readonly Ass2_PizzaStore_VanTuan.Data.AppDBContext _context;

		public UpdateProfileModel(Ass2_PizzaStore_VanTuan.Data.AppDBContext context)
		{
			_context = context;
		}

		[BindProperty]
		public string FullName { get; set; }

		[BindProperty]
		public string Address { get; set; }

		[BindProperty]
		public string Phone { get; set; }

		[BindProperty]
		public string Password { get; set; }
		public string succesMessage { get; set; } = "";
		public async Task<IActionResult> OnGetAsync(int? id)
		{
			var user = HttpContext.User;
			var username = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			var customer = await _context.Customers.FirstOrDefaultAsync(m => m.UserName == username);

			if (customer != null)
			{
				FullName = customer.fullName;
				Address = customer.address;
				Phone = customer.phone;
				Password = customer.password;
			}

			return Page();
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			var user = HttpContext.User;
			var username = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			var customer = await _context.Customer.FirstOrDefaultAsync(m => m.username == username);

			if (customer != null)
			{
				customer.fullName = FullName;
				customer.address = Address;
				customer.phone = Phone;
				customer.password = Password;
				

				try
				{
					await _context.SaveChangesAsync();
					succesMessage = "Update profile succesfull!";
					//return RedirectToPage("./UpdateProfile");
				}
				catch (DbUpdateConcurrencyException)
				{
					throw;
				}
			}

			return Page();
		}


	}
}
