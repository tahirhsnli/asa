using Microsoft.AspNetCore.Mvc;

namespace NestStart.Areas.NestAdmin.Controllers
{
    public class DashBoardController : Controller
    {
        [Area("NestAdmin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
