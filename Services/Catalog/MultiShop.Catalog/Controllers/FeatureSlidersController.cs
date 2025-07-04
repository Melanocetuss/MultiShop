﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.FeatureSliderDtos;
using MultiShop.Catalog.Services.FeatureSliderServices;

namespace MultiShop.Catalog.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FeatureSlidersController : ControllerBase
    {
        private readonly IFeatureSliderService _featureSliderService;
        public FeatureSlidersController(IFeatureSliderService featureSliderService)
        {
            _featureSliderService = featureSliderService;
        }

        [HttpGet]
        public async Task<IActionResult> FeatureSliderList()
        {
            var values = await _featureSliderService.GetAllFeatureSliderAsync();
            return Ok(values);
        }

        [HttpGet("GetFeatureSliderById")]
        public async Task<IActionResult> GetFeatureSliderById(string FeatureSliderID)
        {
            var values = await _featureSliderService.GetByIdFeatureSliderAsync(FeatureSliderID);
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFeatureSlider(CreateFeatureSliderDto createFeatureSliderDto)
        {
            await _featureSliderService.CreateFeatureSliderAsync(createFeatureSliderDto);
            return Ok("Öne Çıkanlar Ekleme İşlemi Başarıyla Gerçekleşti.");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFeatureSlider(string FeatureSliderID)
        {
            await _featureSliderService.DeleteFeatureSliderAsync(FeatureSliderID);
            return Ok("Öne Çıkanlar Silme İşlemi Başarıyla Gerçekleşti.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFeatureSlider(UpdateFeatureSliderDto updateFeatureSliderDto)
        {
            await _featureSliderService.UpdateFeatureSliderAsync(updateFeatureSliderDto);
            return Ok("Öne Çıkanlar Güncelleme İşlemi Başarıyla Gerçekleşti.");
        }

        [HttpPut("FeatureSliderChangeStatusAsync")]
        public async Task<IActionResult> FeatureSliderChangeStatusAsync(string FeatureSliderID) 
        
        {
            await _featureSliderService.FeatureSliderChangeStatusAsync($"{FeatureSliderID}");
            return Ok("Öne Çıkanlar Durumu Güncelleme İşlemi Başarıyla Gerçekleşti.");
        }
    }
}