using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.ProductDtos;
using MultiShop.WebUI.Services.CatalogServices.ProductServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.ViewComponents.ProductDetailViewComponents
{
    public class _FeatureInfoProductDetailComponentPartial : ViewComponent
    {
        private readonly IProductService _productService;
        public _FeatureInfoProductDetailComponentPartial(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string ProductID)
        {
            var values = await _productService.GetByIdProductAsync(ProductID);
            return View(values);
        }
    }
}