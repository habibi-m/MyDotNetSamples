using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParentChildComponent.Data;

namespace ParentChildComponent.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _context.Categories
                .Where(c => !c.ParentId.HasValue)
                .Include(c => c.SubCategories)
                    .ThenInclude(c => c.SubCategories)
                        .ThenInclude(c => c.SubCategories)
                .AsNoTracking()
                .ToListAsync();

            return View(categories);
        }
    }
}
