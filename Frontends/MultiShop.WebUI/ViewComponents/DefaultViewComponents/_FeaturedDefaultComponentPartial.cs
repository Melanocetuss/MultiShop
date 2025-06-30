using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.FeaturedDtos;
using MultiShop.WebUI.Services.CatalogServices.FeaturedServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.ViewComponents.DefaultViewComponents
{
    public class _FeaturedDefaultComponentPartial : ViewComponent
    {
        private readonly IFeaturedService _featuredService;
        public _FeaturedDefaultComponentPartial(IFeaturedService featuredService)
        {
            _featuredService = featuredService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _featuredService.GetAllFeaturedAsync();
            return View(values);
        }
    }
}