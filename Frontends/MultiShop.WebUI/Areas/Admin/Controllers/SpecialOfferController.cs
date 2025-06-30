using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.SpecialOfferDtos;
using MultiShop.WebUI.Services.CatalogServices.SpecialOfferServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SpecialOfferController : Controller
    {
        private readonly ISpecialOfferService _specialOfferService;
        public SpecialOfferController(ISpecialOfferService specialOfferService)
        {
            _specialOfferService = specialOfferService;
        }

        public async Task<IActionResult> Index()
        {
            SpecialOfferViewBag();
            var values = await _specialOfferService.GetAllSpecialOfferAsync();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateSpecialOffer()
        {
            SpecialOfferViewBag();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateSpecialOffer(CreateSpecialOfferDto createSpecialOfferDto)
        {
            await _specialOfferService.CreateSpecialOfferAsync(createSpecialOfferDto);
            return RedirectToAction("Index", "SpecialOffer", new { Area = "Admin" });
        }

        public async Task<IActionResult> DeleteSpecialOffer(string id)
        {
            await _specialOfferService.DeleteSpecialOfferAsync(id);
            return RedirectToAction("Index", "SpecialOffer", new { Area = "Admin" });
        }

        [HttpGet]
        public async Task<IActionResult> UpdateSpecialOffer(string id)
        {
            SpecialOfferViewBag();
            var values = await _specialOfferService.GetByIdSpecialOfferAsync(id);
            return View(values);        
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSpecialOffer(UpdateSpecialOfferDto updateSpecialOfferDto)
        {
            await _specialOfferService.UpdateSpecialOfferAsync(updateSpecialOfferDto);
            return RedirectToAction("Index", "SpecialOffer", new { Area = "Admin" });
        }

        public async Task<IActionResult> ChangeStatus(string id)
        {
            await _specialOfferService.SpecialOfferChangeStatusAsync(id);
            return RedirectToAction("Index", "SpecialOffer", new { Area = "Admin" });
        }

        void SpecialOfferViewBag()
        {
            ViewBag.PageTitle = "Özel Teklifler";
            ViewBag.index1 = "Ana Sayfa";
            ViewBag.index2 = "Özel Teklifler";
            ViewBag.index3 = "Özel Teklifler İşelemleri";
        }
    }
}