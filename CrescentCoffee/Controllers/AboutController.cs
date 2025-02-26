using Microsoft.AspNetCore.Mvc;

namespace CrescentCoffee.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
