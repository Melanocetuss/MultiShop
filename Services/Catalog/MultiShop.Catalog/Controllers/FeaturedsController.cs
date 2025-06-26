using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.FeaturedDtos;
using MultiShop.Catalog.Services.FeaturedServices;

namespace MultiShop.Catalog.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FeaturedsController : ControllerBase
    {
        private readonly IFeaturedService _FeaturedService;
        public FeaturedsController(IFeaturedService FeaturedService)
        {
            _FeaturedService = FeaturedService;
        }

        [HttpGet]
        public async Task<IActionResult> FeaturedList()
        {
            var values = await _FeaturedService.GetAllFeaturedAsync();
            return Ok(values);
        }

        [HttpGet("GetFeaturedById")]
        public async Task<IActionResult> GetFeaturedById(string FeaturedID)
        {
            var values = await _FeaturedService.GetByIdFeaturedAsync(FeaturedID);
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFeatured(CreateFeaturedDto createFeaturedDto)
        {
            await _FeaturedService.CreateFeaturedAsync(createFeaturedDto);
            return Ok("Özelik Ekleme İşlemi Başarıyla Gerçekleşti.");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFeatured(string FeaturedID)
        {
            await _FeaturedService.DeleteFeaturedAsync(FeaturedID);
            return Ok("Özelik Silme İşlemi Başarıyla Gerçekleşti.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFeatured(UpdateFeaturedDto updateFeaturedDto)
        {
            await _FeaturedService.UpdateFeaturedAsync(updateFeaturedDto);
            return Ok("Özelik Güncelleme İşlemi Başarıyla Gerçekleşti.");
        }
    }
}