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

        public IActionResult ProductDetail(string ProductID)
        {
            ViewBag.FirstBreadcrump = "Ana Sayfa";
            ViewBag.SecondBreadcrump = "Ürün Detayları";
            ViewBag.ProductID = ProductID;
            return View();
        }
    }
}