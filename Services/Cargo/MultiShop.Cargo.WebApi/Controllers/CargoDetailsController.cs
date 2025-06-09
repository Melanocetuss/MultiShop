using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Cargo.BusinessLayer.Abstract;
using MultiShop.Cargo.DtoLayer.Dtos.CargoDetailDtos;
using MultiShop.Cargo.EntityLayer.Entities;

namespace MultiShop.Cargo.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CargoDetailsController : ControllerBase
    {
        private readonly ICargoDetailService _cargoDetailService;
        private readonly IMapper _mapper;
        public CargoDetailsController(ICargoDetailService cargoDetailService, IMapper mapper)
        {
            _cargoDetailService = cargoDetailService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> CargoDetailList()
        {
            var values = _mapper.Map<List<ResultCargoDetailDto>>(await _cargoDetailService.TGetAllAsync());
            return Ok(values);
        }

        [HttpGet("GetCargoDetailById")]
        public async Task<IActionResult> CargoDetailById(int id)
        {
            var values = await _cargoDetailService.TGetByIdAsync(id);
            return Ok(_mapper.Map<GetCargoDetailByIdDto>(values));
        }

        [HttpPost]
        public async Task<IActionResult> CreateCargoDetail(CreateCargoDetailDto createCargoDetailDto)
        {
            var cargoDetailEntity = _mapper.Map<CargoDetail>(createCargoDetailDto);
            await _cargoDetailService.TInsertAsync(cargoDetailEntity);
            return Ok("Kargo Detayı Ekleme İşlemi Başarıyla Gerçekleşti.");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCargoDetail(int id)
        {
            await _cargoDetailService.TDeleteAsync(id);
            return Ok("Kargo Detayı Silme İşlemi Başarıyla Gerçekleşti.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCargoDetail(UpdateCargoDetailDto updateCargoDetailDto)
        {
            var cargoDetailEntity = _mapper.Map<CargoDetail>(updateCargoDetailDto);
            await _cargoDetailService.TUpdateAsync(cargoDetailEntity);
            return Ok("Kargo Detayı Güncelleme İşlemi Başarıyla Gerçekleşti.");
        }
    }
}
