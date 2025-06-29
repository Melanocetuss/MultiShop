using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.ProductDetailDtos;
using MultiShop.WebUI.Services.CatalogServices.ProductDetailServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductDetailController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IProductDetailService _productDetailService;
        public ProductDetailController(IHttpClientFactory httpClientFactory, IProductDetailService productDetailService)
        {
            _httpClientFactory = httpClientFactory;
            _productDetailService = productDetailService;
        }

        [Route("/Admin/ProductDetail/index/{ProductID}")]
        public async Task<IActionResult> Index(string ProductID)
        {
            ProductDetailViewBag();
            var values = await _productDetailService.GetProductDetailByProductIdAsync(ProductID);
            if (values == null)
            {
                return Content("Model null geldi");
            }

            return View("Index", values);
        }

        [HttpGet("Admin/ProductDetail/UpdateProductDetail/{ProductDetailID}")]
        public async Task<IActionResult> UpdateProductDetail(string ProductDetailID)
        {
            ProductDetailViewBag();
            var values = await _productDetailService.GetByIdProductDetailAsync(ProductDetailID);
            return View(values);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProductDetail(UpdateProductDetailDto updateProductDetailDto)
        {
            await _productDetailService.UpdateProductDetailAsync(updateProductDetailDto);
            return RedirectToAction("Index", "ProductDetail", new { Area = "Admin" });
        }

        void ProductDetailViewBag()
        {
            ViewBag.PageTitle = "Ürün Detayları İşlemleri";
            ViewBag.index1 = "Ana Sayfa";
            ViewBag.index2 = "Ürün Detayları";
            ViewBag.index3 = "Ürün Detayları İşlemleri";
        }
    }
}