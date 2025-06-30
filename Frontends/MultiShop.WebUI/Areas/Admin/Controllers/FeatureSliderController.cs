using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.FeatureSliderDtos;
using MultiShop.WebUI.Services.CatalogServices.FeatureSliderServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FeatureSliderController : Controller
    {
        private readonly IFeatureSliderService _featureSliderService;
        public FeatureSliderController(IFeatureSliderService featureSliderService)
        {
            _featureSliderService = featureSliderService;
        }

        public async Task<IActionResult> Index()
        {
            FeatureSliderViewBag();
            var values = await _featureSliderService.GetAllFeatureSliderAsync();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateFeatureSlider()
        {
            FeatureSliderViewBag();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateFeatureSlider(CreateFeatureSliderDto createFeatureSliderDto)
        {
            await _featureSliderService.CreateFeatureSliderAsync(createFeatureSliderDto);
            return RedirectToAction("Index", "FeatureSlider", new { Area = "Admin" });
        }

        public async Task<IActionResult> DeleteFeatureSlider(string id)
        {
            await _featureSliderService.DeleteFeatureSliderAsync(id);
            return RedirectToAction("Index", "FeatureSlider", new { Area = "Admin" });
        }

        [HttpGet]
        public async Task<IActionResult> UpdateFeatureSlider(string id)
        {
            FeatureSliderViewBag();
            var values = await _featureSliderService.GetByIdFeatureSliderAsync(id);
            return View(values);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateFeatureSlider(UpdateFeatureSliderDto updateFeatureSliderDto)
        {
            await _featureSliderService.UpdateFeatureSliderAsync(updateFeatureSliderDto);
            return RedirectToAction("Index", "FeatureSlider", new { Area = "Admin" });
        }

        public async Task<IActionResult> ChangeStatus(string id)
        {
            await _featureSliderService.FeatureSliderChangeStatusAsync(id);
            return RedirectToAction("Index", "FeatureSlider", new { Area = "Admin" });
        }

        void FeatureSliderViewBag()
        {
            ViewBag.PageTitle = "Öne Çıkanlar İşlemeleri";
            ViewBag.index1 = "Ana Sayfa";
            ViewBag.index2 = "Öne Çıkanlar";
            ViewBag.index3 = "Öne Çıkanlar İşlemeleri";
        }
    }
}