using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.BasketServices;
using MultiShop.WebUI.Services.DiscountServices;
using System.Threading.Tasks;

namespace MultiShop.WebUI.Controllers
{
    public class DiscountController : Controller
    {
        private readonly IBasketService _basketService;
        private readonly IDiscountService _discountService;
        public DiscountController(IBasketService basketService, IDiscountService discountService)
        {
            _basketService = basketService;
            _discountService = discountService;
        }

        [HttpGet]
        public PartialViewResult ConfirmDiscountCoupon()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmDiscountCoupon(string code)
        {
            var basket = await _basketService.GetBasket();
            
            var discount = await _discountService.GetDiscountCode(code);
            decimal PriceWithTax = basket.TotalPrice + (basket.TotalPrice / 100 * 18);      // Vergi dahil toplam fiyat
            decimal DiscountedPrice = basket.TotalPrice / 100 * discount.Rate;             // İndirim tutarı
            decimal DiscountedTotalPrice = PriceWithTax - DiscountedPrice;                // İndirimli Toplam fiyat
                                                                                      
            return RedirectToAction("Index", "ShopingCart",new 
                {
                    code = code,
                    discountedPrice = DiscountedPrice,
                    discountedTotalPrice = DiscountedTotalPrice
                });           
        }
    }
}