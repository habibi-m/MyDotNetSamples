using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParentChildComponent.Data;

namespace ParentChildComponent.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _context.Products
                .Include(s => s.Category)
                .AsNoTracking()
                .ToListAsync();

            return View(products);
        }
    }
}
