using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class categoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
