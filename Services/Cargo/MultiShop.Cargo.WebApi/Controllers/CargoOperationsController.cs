using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Cargo.BusinessLayer.Abstract;
using MultiShop.Cargo.DtoLayer.Dtos.CargoOperationDtos;
using MultiShop.Cargo.EntityLayer.Entities;

namespace MultiShop.Cargo.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CargoOperationsController : ControllerBase
    {
        private readonly ICargoOperationService _cargoOperationService;
        private readonly IMapper _mapper;
        public CargoOperationsController(ICargoOperationService cargoOperationService, IMapper mapper)
        {
            _cargoOperationService = cargoOperationService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> CargoOperationList()
        {
            var values = _mapper.Map<List<ResultCargoOperationDto>>(await _cargoOperationService.TGetAllAsync());
            return Ok(values);
        }

        [HttpGet("GetCargoOperationById")]
        public async Task<IActionResult> CargoOperationById(int id)
        {
            var values = await _cargoOperationService.TGetByIdAsync(id);
            return Ok(_mapper.Map<GetCargoOperationByIdDto>(values));
        }

        [HttpPost]
        public async Task<IActionResult> CreateCargoOperation(CreateCargoOperationDto createCargoOperationDto)
        {
            var cargoOperationEntity = _mapper.Map<CargoOperation>(createCargoOperationDto);
            await _cargoOperationService.TInsertAsync(cargoOperationEntity);
            return Ok("Kargo Operasyonu Ekleme İşlemi Başarıyla Gerçekleşti.");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCargoOperation(int id)
        {
            await _cargoOperationService.TDeleteAsync(id);
            return Ok("Kargo Operasyonu Silme İşlemi Başarıyla Gerçekleşti.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCargoOperation(UpdateCargoOperationDto updateCargoOperationDto)
        {
            var cargoOperationEntity = _mapper.Map<CargoOperation>(updateCargoOperationDto);
            await _cargoOperationService.TUpdateAsync(cargoOperationEntity);
            return Ok("Kargo Operasyonu Güncelleme İşlemi Başarıyla Gerçekleşti.");
        }
    }
}