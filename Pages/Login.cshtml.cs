using Ass2_PizzaStore_VanTuan.Data;
using Ass2_PizzaStore_VanTuan.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Ass2_PizzaStore_VanTuan.Models.Enum;

namespace Ass2_PizzaStore_VanTuan.Pages
{
    public class User
    {
        [Required(ErrorMessage ="Username must be input value!")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password must be input value!")]
        public string Password { get; set; }

    }
    public class LoginModel : PageModel
    {
        private readonly AppDBContext _context;


		public LoginModel( AppDBContext context)
		{
			_context = context;
		}

		[BindProperty]
        public User User { get; set; } 
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                Account? account = _context.Accounts.SingleOrDefault(
                    x => x.UserName == User.Username && x.Password == User.Password
                    );
                if (account == null)
                {
                    ModelState.AddModelError("Error", "Wrong UserName or password.");
                    return Page();
                }
                else
                {
                    var accountType = (account.Type == 1) ? AccountType.Staff : AccountType.Normal;

                    var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, account.UserName),
                new Claim("Role", accountType.ToString())
            };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        AllowRefresh = true,
                        IsPersistent = true
                    };

                    await HttpContext.SignInAsync(
                            CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(claimsIdentity),
                            authProperties
                            );

                    return RedirectToPage("./Index");
                }
            }
            return Page();
        }


    }
}

