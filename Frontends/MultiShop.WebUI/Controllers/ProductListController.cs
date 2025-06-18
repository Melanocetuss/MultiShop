using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.Controllers
{
    public class ProductListController : Controller
    {
        public IActionResult Index(string CategoryID)
        {
            ViewBag.FirstBreadcrump = "Ana Sayfa";
            ViewBag.SecondBreadcrump = "Ürün Listesi";
            ViewBag.CategoryID = CategoryID;
            return View();
        }

        public IActionResult ProductDetail()
        {
            ViewBag.FirstBreadcrump = "Ana Sayfa";
            ViewBag.SecondBreadcrump = "Ürün Detayları";
            return View();
        }
    }
}