using Microsoft.AspNetCore.Mvc;

namespace CrescentCoffee.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
