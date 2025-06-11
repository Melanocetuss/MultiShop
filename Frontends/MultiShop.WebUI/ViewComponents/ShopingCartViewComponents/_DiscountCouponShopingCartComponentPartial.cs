using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.ViewComponents.ShopingCartViewComponents
{
    public class _DiscountCouponShopingCartComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}