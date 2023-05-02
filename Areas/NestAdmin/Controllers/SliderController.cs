using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NestStart.DAL;
using NestStart.Models;

namespace NestStart.Areas.NestAdmin.Controllers
{
    [Area("NestAdmin")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _context;
        public SliderController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Sliders.ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Slider slider)
        {
            //if(!ModelState.IsValid)
            //{
            //    return View(category);
            //}
            await _context.Sliders.AddAsync(slider);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Detail(int id)
        {
            return View(await _context.Sliders.FirstOrDefaultAsync(x => x.Id == id));
        }
        public async Task<IActionResult> Edit(int id)
        {
            return View(await _context.Sliders.FirstOrDefaultAsync(x => x.Id == id));
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Slider slider)
        {
            Slider? exists = await _context.Sliders.FirstOrDefaultAsync(x => x.Id == slider.Id);
            if (exists == null)
            {
                ModelState.AddModelError("", "Slider is null");
                return View(); ;
            }
            exists.Title1 = slider.Title1;
            exists.Title2 = slider.Title2;
            exists.Image = slider.Image;
            exists.Description = slider.Description;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            Slider? exists = await _context.Sliders.FirstOrDefaultAsync(x => x.Id == id);
            if (exists == null)
            {
                ModelState.AddModelError("", "Category is null");
                return RedirectToAction("Index");
            }
            _context.Sliders.Remove(exists);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
