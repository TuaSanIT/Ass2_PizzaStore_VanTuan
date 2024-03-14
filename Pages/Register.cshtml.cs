using Ass2_PizzaStore_VanTuan.Data;
using Ass2_PizzaStore_VanTuan.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Ass2_PizzaStore_VanTuan.Pages
{
	public class UserRegister
	{
		[Required(ErrorMessage ="Username must be input value!")]
		public string username { get; set; }
		[Required(ErrorMessage = "Password must be input value!")]
		public string password { get; set; }
		[Required(ErrorMessage = "Fullname must be input value!")]
		public string fullName { get; set; }
		[Required(ErrorMessage = "Address must be input value!")]
		public string address { get; set; }
		[Required(ErrorMessage = "Phone must be input value!")]
		[StringLength(10, MinimumLength = 10)]
		public string phone { get; set; }

	}
	public class RegisterModel : PageModel
	{

		private readonly AppDBContext _context;
		public RegisterModel(AppDBContext context)
		{
			_context = context;
		}
		[BindProperty]
		public UserRegister UserRegister { get; set; }

		public Customers Customer { get; set; }

		public Account Account {  get; set; }

		public void OnGet()
		{
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (ModelState.IsValid)
			{
				Account = new Account()
				{
					UserName = UserRegister.username,
					Password = UserRegister.password,
					FullName = UserRegister.fullName,
				};
				_context.Accounts.Add(Account);
				try
				{
					await _context.SaveChangesAsync();
					return RedirectToPage("./Index");
				}
				catch (DbUpdateException ex)
				{
					ModelState.AddModelError("UserRegister.username", "Username already exists");
					return Page();
				}
			}
			return Page();
		}
	}
}
