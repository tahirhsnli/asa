using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NestStart.DAL;
using NestStart.Models;

namespace NestStart.Areas.NestAdmin.Controllers
{
    [Area("NestAdmin")]
    public class CategoriesController : Controller
    {
        private readonly AppDbContext _context;
        public CategoriesController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categories.Where(x => x.IsDeleted == false).ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Category category) 
        {
            //if(!ModelState.IsValid)
            //{
            //    return View(category);
            //}
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Detail(int id)
        {
            return View(await _context.Categories.FirstOrDefaultAsync(x => x.Id == id));
        }
        public async Task<IActionResult> Edit(int id)
        {
            return View(await _context.Categories.FirstOrDefaultAsync(x => x.Id == id));
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Category category)
        {
            Category? exists = await _context.Categories.FirstOrDefaultAsync(x => x.Id == category.Id);
            if(exists == null)
            {
                ModelState.AddModelError("", "Category is null");
                return View(); ;
            }
            exists.Name = category.Name;
            exists.Logo = category.Logo;
            exists.Image = category.Image;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            Category? exists = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if(exists == null)
            {
                ModelState.AddModelError("", "Category is null");
                return RedirectToAction("Index");
            }
            _context.Categories.Remove(exists);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }

}
