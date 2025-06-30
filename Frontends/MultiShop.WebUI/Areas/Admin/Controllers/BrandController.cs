using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.BrandDtos;
using MultiShop.WebUI.Services.CatalogServices.BrandServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandController : Controller
    {
        private readonly IBrandService _brandService;
        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        public async Task<IActionResult> Index()
        {
            BrandViewBag();
            var values = await _brandService.GetAllBrandAsync();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateBrand()
        {
            BrandViewBag();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateBrand(CreateBrandDto createBrandDto)
        {
            await _brandService.CreateBrandAsync(createBrandDto);
            return RedirectToAction("Index", "Brand", new { Area = "Admin" });
        }

        public async Task<IActionResult> DeleteBrand(string id)
        {
            await _brandService.DeleteBrandAsync(id);
            return RedirectToAction("Index", "Brand", new { Area = "Admin" });
        }

        [HttpGet]
        public async Task<IActionResult> UpdateBrand(string id)
        {
            BrandViewBag();
            var values = await _brandService.GetByIdBrandAsync(id);
            return View(values);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBrand(UpdateBrandDto updateBrandDto)
        {
            await _brandService.UpdateBrandAsync(updateBrandDto);
            return RedirectToAction("Index", "Brand", new { Area = "Admin" });
        }

        void BrandViewBag()
        {
            ViewBag.PageTitle = "Marka İşlemleri";
            ViewBag.index1 = "Ana Sayfa";
            ViewBag.index2 = "Markalar";
            ViewBag.index3 = "Marka İşlemleri";
        }
    }
}