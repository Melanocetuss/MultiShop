using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.FeaturedDtos;
using MultiShop.WebUI.Services.CatalogServices.FeaturedServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FeaturedController : Controller
    {
        private readonly IFeaturedService _featuredService;
        public FeaturedController(IFeaturedService featuredService)
        {
            _featuredService = featuredService;
        }

        public async Task<IActionResult> Index()
        {
            FeaturedViewBag();
            var values = await _featuredService.GetAllFeaturedAsync();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateFeatured()
        {
            FeaturedViewBag();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateFeatured(CreateFeaturedDto createFeaturedDto)
        {
            await _featuredService.CreateFeaturedAsync(createFeaturedDto);
            return RedirectToAction("Index", "Featured", new { Area = "Admin" });
        }

        public async Task<IActionResult> DeleteFeatured(string id)
        {
            await _featuredService.DeleteFeaturedAsync(id);
            return RedirectToAction("Index", "Featured", new { Area = "Admin" });
        }

        [HttpGet]
        public async Task<IActionResult> UpdateFeatured(string id)
        {
            FeaturedViewBag();
            var values = await _featuredService.GetByIdFeaturedAsync(id);
            return View(values);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateFeatured(UpdateFeaturedDto updateFeaturedDto)
        {
            await _featuredService.UpdateFeaturedAsync(updateFeaturedDto);
            return RedirectToAction("Index", "Featured", new { Area = "Admin" });
        }

        void FeaturedViewBag()
        {
            ViewBag.PageTitle = "Özellik İşlemleri";
            ViewBag.index1 = "Ana Sayfa";
            ViewBag.index2 = "Özellikler";
            ViewBag.index3 = "Özellik İşlemleri";
        }
    }
}