using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.BasketDtos;
using MultiShop.WebUI.Services.BasketServices;
using MultiShop.WebUI.Services.CatalogServices.ProductServices;
using MultiShop.WebUI.Services.DiscountServices;
using Newtonsoft.Json.Linq;

namespace MultiShop.WebUI.Controllers
{
    public class ShopingCartController : Controller
    {
        private readonly IProductService _productService;
        private readonly IBasketService _basketService;

        public ShopingCartController(IProductService productService, IBasketService basketService)
        {
            _productService = productService;
            _basketService = basketService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string code, decimal discountedPrice, decimal discountedTotalPrice)
        {
            ViewBag.FirstBreadcrump = "Ana Sayfa";
            ViewBag.SecondBreadcrump = "Sepetim";                                    
            
            var basket = await _basketService.GetBasket();
            
            var PriceWithTax = basket.TotalPrice + (basket.TotalPrice /100 *18);
            ViewBag.TotalPrice = basket.TotalPrice;                                   
            ViewBag.Tax = PriceWithTax - basket.TotalPrice;         // Vergi tutarı
            ViewBag.PriceWithTax = PriceWithTax;                   // Vergi dahil toplam fiyat
            
            ViewBag.DiscountedPrice = discountedPrice;            // İndirim tutarı
            ViewBag.DiscountedTotalPrice = discountedTotalPrice; // İndirimli Toplam fiyat
            ViewBag.Code = code;                                // İndirim kodu
            return View();
        }       

        [Route("/ShopingCart/AddBasketItem/{productId}")]
        public async Task<IActionResult> AddBasketItem(string productId)
        {
            var values = await _productService.GetByIdProductAsync(productId);
            var items = new BasketItemDto
            {
                ProductID = values.ProductID,
                ProductName = values.ProductName,
                ProductImageUrl = values.ProductImageUrl,
                Price = values.ProductPrice,
                Quantity = 1
            };
            await _basketService.AddBasketItem(items);
            return RedirectToAction("Index");
        }

        [Route("/ShopingCart/RemoveBasketItem/{productId}")]
        public async Task<IActionResult> RemoveBasketItem(string productId)
        {
            await _basketService.RemoveBasketItem(productId);
            return RedirectToAction("Index");
        }
    }
}