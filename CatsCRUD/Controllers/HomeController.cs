using Microsoft.AspNetCore.Mvc;

namespace CatsCRUD.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View("~/wwwroot/index.cshtml");
        }
    }
}
