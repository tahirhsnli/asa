using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NestStart.DAL;
using NestStart.Models;

namespace NestStart.Areas.NestAdmin.Controllers
{
    [Area("NestAdmin")]
    public class ProductImageController : Controller
    {
        private readonly AppDbContext _context;
        public ProductImageController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.ProductImages.ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductImage productImage)
        {
            //if(!ModelState.IsValid)
            //{
            //    return View(slider);
            //}
            await _context.ProductImages.AddAsync(productImage);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Detail(int id)
        {
            return View(await _context.ProductImages.FirstOrDefaultAsync(x => x.Id == id));
        }
        public async Task<IActionResult> Edit(int id)
        {
            return View(await _context.ProductImages.FirstOrDefaultAsync(x => x.Id == id));
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ProductImage productImage)
        {
            ProductImage? exists = await _context.ProductImages.FirstOrDefaultAsync(x => x.Id == productImage.Id);
            if (exists == null)
            {
                ModelState.AddModelError("", "ProductImage is null");
                return View(); ;
            }
            exists.Image = productImage.Image;
            exists.IsFront = productImage.IsFront;
            exists.IsBack = productImage.IsBack;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            ProductImage? exists = await _context.ProductImages.FirstOrDefaultAsync(x => x.Id == id);
            if (exists == null)
            {
                ModelState.AddModelError("", "Category is null");
                return RedirectToAction("Index");
            }
            _context.ProductImages.Remove(exists);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
