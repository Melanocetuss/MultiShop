using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.ViewComponents.ShopingCartViewComponents
{
    public class _ShopingCartComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
