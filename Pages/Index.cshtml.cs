using Ass2_PizzaStore_VanTuan.Data;
using Ass2_PizzaStore_VanTuan.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Ass2_PizzaStore_VanTuan.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly AppDBContext _context;
        public IndexModel(ILogger<IndexModel> logger, AppDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        private List<string> searchBys = new List<string>() { "ID", "ProductName", "Unit Price" };
        [BindProperty]
        public string searchValue { get; set; }
        [BindProperty]
        public string searchBy { get; set; }
        public List<Product> Products { get; set; }

        [BindProperty]
        public string notFound { get; set; } = "";
        public void OnGet()
        {
            Products = _context.Products
                .Include(p => p.Category)
                .ToList();

            ViewData["searchBys"] = new SelectList(searchBys);
        }

        public void OnPost()
        {

            if (searchValue == null)
            {
                Products = _context.Products.ToList();

            }
            else
            {
                switch (searchBy)
                {
                    case "ID":
                        Products = _context.Products
                            .Include(p => p.Category)
                            .Where(p => p.ProductID.ToString().Equals(searchValue)).ToList();
                        if (Products.Count == 0)
                        {
                            notFound = "Not Found";
                        }
                        break;
                    case "ProductName":
                        Products = _context.Products
                            .Include(p => p.Category)
                            .Where(p => p.ProductName.Contains(searchValue)).ToList();
                        if (Products.Count == 0)
                        {
                            notFound = "Not Found";
                        }
                        break;
                    case "Unit Price":
                        Products = _context.Products
                            .Include(p => p.Category)
                            .Where(p => p.UnitPrice <= decimal.Parse(searchValue)).ToList();
                        if (Products.Count == 0)
                        {
                            notFound = "Not Found";
                        }
                        break;
                }
            }
            ViewData["searchBys"] = new SelectList(searchBys);
        }
    }
}