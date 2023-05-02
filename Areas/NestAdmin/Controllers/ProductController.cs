using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NestStart.DAL;
using NestStart.Models;

namespace NestStart.Areas.NestAdmin.Controllers
{
    [Area("NestAdmin")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        public ProductController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Products.Where(x => x.IsDeleted == false).ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Detail(int id)
        {
            return View(await _context.Products.FirstOrDefaultAsync(x => x.Id == id)); 
        }
        public async Task<IActionResult> Edit(int id)
        {
            return View(await _context.Products.FirstOrDefaultAsync(x => x.Id == id));
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Product product)
        {
            Product? exists = await _context.Products.FirstOrDefaultAsync(x => x.Id == product.Id);
            if (exists == null)
            {
                ModelState.AddModelError("", "Product is null");
            }
            exists.Name = product.Name;
            exists.SellPrice = product.SellPrice;
            exists.CostPrice = product.CostPrice;
            exists.Rating = product.Rating;
            exists.Discount = product.Discount;
            exists.StockCount = product.StockCount;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            Category? exists = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (exists == null)
            {
                ModelState.AddModelError("", "Product is null");
                return RedirectToAction("Index");
            }
            _context.Categories.Remove(exists);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
