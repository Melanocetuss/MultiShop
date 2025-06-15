using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.SpecialOfferDtos;
using MultiShop.Catalog.Services.SpecialOfferServices;

namespace MultiShop.Catalog.Controllers
{
    [AllowAnonymous]
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialOffersController : ControllerBase
    {
        private readonly ISpecialOfferService _specialOfferService;
        public SpecialOffersController(ISpecialOfferService specialOfferService)
        {
            _specialOfferService = specialOfferService;
        }

        [HttpGet]
        public async Task<IActionResult> SpecialOfferList() 
        {
            var values = await _specialOfferService.GetAllSpecialOfferAsync();
            return Ok(values);
        }

        [HttpGet("GetSpecialOfferById")]
        public async Task<IActionResult> GetSpecialOfferById(string SpecialOfferID) 
        {
            var values = await _specialOfferService.GetByIdSpecialOfferAsync(SpecialOfferID);
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSpecialOffer(CreateSpecialOfferDto createSpecialOfferDto) 
        {
            await _specialOfferService.CreateSpecialOfferAsync(createSpecialOfferDto);
            return Ok("Özel Teklif Ekleme İşlemi Başarıyla Gerçekleşti.");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSpecialOffer(string SpecialOfferID) 
        {
            await _specialOfferService.DeleteSpecialOfferAsync(SpecialOfferID);
            return Ok("Özel Teklif Silme İşlemi Başarıyla Gerçekleşti.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSpecialOffer(UpdateSpecialOfferDto updateSpecialOfferDto) 
        {
            await _specialOfferService.UpdateSpecialOfferAsync(updateSpecialOfferDto);
            return Ok("Özel Teklif Güncelleme İşlemi Başarıyla Gerçekleşti.");
        }

        [HttpPut("SpecialOfferChangeStatusAsync")]
        public async Task<IActionResult> SpecialOfferChangeStatusAsync(string SpecialOfferID) 
        {
            await _specialOfferService.SpecialOfferChangeStatusAsync($"{SpecialOfferID}");
            return Ok("Özel Teklif Durumu Güncelleme İşlemi Başarıyla Gerçekleşti.");
        }
    }
}