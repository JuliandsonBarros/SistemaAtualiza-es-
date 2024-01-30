using Microsoft.AspNetCore.Mvc;

namespace Sisat.Views.Home
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Projetos");
        }
    }
}
