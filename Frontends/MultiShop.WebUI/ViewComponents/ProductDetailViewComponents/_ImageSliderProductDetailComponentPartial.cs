using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.ProductImageDtos;
using MultiShop.WebUI.Services.CatalogServices.ProductImageServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.ViewComponents.ProductDetailViewComponents
{
    public class _ImageSliderProductDetailComponentPartial :ViewComponent
    {
        private readonly IProductImageService _productImageService;
        public _ImageSliderProductDetailComponentPartial(IProductImageService productImageService)
        {
            _productImageService = productImageService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string ProductID)
        {
            var values = await _productImageService.GetProductImageByProductIdAsync(ProductID);
            return View(values);
        }
    }
}