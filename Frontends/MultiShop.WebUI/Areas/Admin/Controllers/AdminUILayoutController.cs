using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminUILayoutController : Controller
    {
        public IActionResult AdminUILayout()
        {
            return View();
        }
    }
}