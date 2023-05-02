using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NestStart.DAL;
using NestStart.Models;
using NestStart.ViewModel;

namespace NestStart.Controllers
{
	public class HomeController : Controller
	{
		public readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
			_context = context;
        }
        public async Task<IActionResult> Index()
		{

			HomeVM vm = new HomeVM()
			{
                Sliders = await _context.Sliders.ToListAsync(),
				PopularCategories = await _context.Categories.Where(x => x.IsDeleted == false).OrderByDescending(x => x.Products.Count).ToListAsync(),
				Products = await _context.Products.Include(x => x.Category)
				.Include(x => x.ProductImages).Where(x => x.IsDeleted == false)
				.OrderByDescending(x => x.Id).Take(10).ToListAsync(),
                RandomCategories = await _context.Categories.Where(x => x.IsDeleted == false).OrderBy(x => Guid.NewGuid()).ToListAsync(),
                TopRatedProducts = await _context.Products.Where(x => x.IsDeleted == false).OrderByDescending(p => p.Rating).Take(3).ToListAsync(),
                RecentProducts = await _context.Products.Where(x => x.IsDeleted == false).OrderByDescending(x => x.Id).ToListAsync()
            };
			return View(vm);
		}
	}
}
