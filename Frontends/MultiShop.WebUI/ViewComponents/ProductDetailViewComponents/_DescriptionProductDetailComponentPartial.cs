using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.ProductDetailDtos;
using MultiShop.WebUI.Services.CatalogServices.ProductDetailServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.ViewComponents.ProductDetailViewComponents
{
    public class _DescriptionProductDetailComponentPartial : ViewComponent
    {
        private readonly IProductDetailService _productDetailService;
        public _DescriptionProductDetailComponentPartial(IProductDetailService productDetailService)
        {
            _productDetailService = productDetailService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string ProductID)
        {
            var values = await _productDetailService.GetProductDetailByProductIdAsync(ProductID);
            return View(values);
        }
    }
}