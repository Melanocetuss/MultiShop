using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.ProductDetailDtos;
using MultiShop.WebUI.Services.CatalogServices.ProductDetailServices;
using Newtonsoft.Json;
using System.Net.Http;

namespace MultiShop.WebUI.ViewComponents.ProductDetailViewComponents
{
    public class _AdditionalInformationProductDetailComponentPartial : ViewComponent
    {
        private readonly IProductDetailService _productDetailService;
        public _AdditionalInformationProductDetailComponentPartial(IProductDetailService productDetailService)
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