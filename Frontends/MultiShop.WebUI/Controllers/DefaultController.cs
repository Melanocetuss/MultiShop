using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.FirstBreadcrump = "Ana Sayfa";
            ViewBag.SecondBreadcrump = "Genel Bakış";
            return View();
        }
    }
}