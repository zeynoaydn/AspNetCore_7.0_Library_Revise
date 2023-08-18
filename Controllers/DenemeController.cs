using Microsoft.AspNetCore.Mvc;

namespace Mvc_Project.Controllers
{
    public class DenemeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
