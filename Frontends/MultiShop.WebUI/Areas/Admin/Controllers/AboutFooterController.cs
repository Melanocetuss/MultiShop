using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.AboutDtos;
using MultiShop.WebUI.Services.CatalogServices.AboutServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AboutFooterController : Controller
    {
        private readonly IAboutService _aboutService;
        public AboutFooterController(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }

        public async Task<IActionResult> Index()
        {
            AboutFooterViewBag();
            var values = await _aboutService.GetAllAboutAsync();
            return View(values);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateAbout(string id)
        {
            AboutFooterViewBag();
            var values = await _aboutService.GetByIdAboutAsync(id);
            return View(values);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAbout(UpdateAboutDto updateAboutDto)
        {
            await _aboutService.UpdateAboutAsync(updateAboutDto);
            return RedirectToAction("Index", "AboutFooter", new { Area = "Admin" });
        }

        void AboutFooterViewBag()
        {
            ViewBag.PageTitle = "Hakkımda Footer İşlemleri";
            ViewBag.index1 = "Ana Sayfa";
            ViewBag.index2 = "Hakkımda Footer";
            ViewBag.index3 = "Hakkımda Footer İşlemleri";
        }
    }
}