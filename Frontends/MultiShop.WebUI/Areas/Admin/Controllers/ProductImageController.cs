using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.ProductDtos;
using MultiShop.DtoLayer.CatalogDtos.ProductImageDtos;
using MultiShop.WebUI.Services.CatalogServices.ProductImageServices;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductImageController : Controller
    {
        private readonly IProductImageService _productImageService;
        public ProductImageController(IProductImageService productImageService)
        {
            _productImageService = productImageService;
        }

        public async Task<IActionResult> Index(string ProductID)
        {
            ProductImageViewBag();
            var values = await _productImageService.GetProductImageByProductIdAsync(ProductID);
            return View(values);
        }

        [Route("/Admin/ProductImage/ProductImageList/{ProductID}")]
        public async Task<IActionResult> ProductImageList(string ProductID) 
        {
            ProductImageViewBag();
            var values = await _productImageService.GetProductImageByProductIdAsync(ProductID);

            if (values == null)
            {
                return Content("Model null geldi");
            }

            return View("ProductImageList", values);
        }

        [HttpGet("Admin/ProductImage/UpdateProductImage/{ProductImageID}")]
        public async Task<IActionResult> UpdateProductImage(string ProductImageID)
        {
            ProductImageViewBag();
            var values = await _productImageService.GetByIdProductImageAsync(ProductImageID);
            return View(values);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProductImage(UpdateProductImageDto updateProductImageDto)
        {
            await _productImageService.UpdateProductImageAsync(updateProductImageDto);
            return RedirectToAction("Index", "ProductImage", new { ProductID = updateProductImageDto.ProductID });
        }
        void ProductImageViewBag()
        {
            ViewBag.PageTitle = "Ürün Görseli İşlemleri";
            ViewBag.index1 = "Ana Sayfa";
            ViewBag.index2 = "Ürün Görseli";
            ViewBag.index3 = "Ürün Görseli İşlemleri";
        }
    }
}