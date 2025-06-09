using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Cargo.BusinessLayer.Abstract;
using MultiShop.Cargo.DtoLayer.Dtos.CargoCustomerDtos;
using MultiShop.Cargo.EntityLayer.Entities;

namespace MultiShop.Cargo.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CargoCustomersController : ControllerBase
    {
        private readonly ICargoCustomerService _cargoCustomerService;
        private readonly IMapper _mapper;
        public CargoCustomersController(ICargoCustomerService cargoCustomerService, IMapper mapper)
        {
            _cargoCustomerService = cargoCustomerService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> CargoCustomerList()
        {
            var values = _mapper.Map<List<ResultCargoCustomerDto>>(await _cargoCustomerService.TGetAllAsync());
            return Ok(values);
        }

        [HttpGet("GetCargoCustomerById")]
        public async Task<IActionResult> CargoCustomerById(int id)
        {
            var values = await _cargoCustomerService.TGetByIdAsync(id);
            return Ok(_mapper.Map<GetCargoCustomerByIdDto>(values));
        }

        [HttpPost]
        public async Task<IActionResult> CreateCargoCustomer(CreateCargoCustomerDto createCargoCustomerDto)
        {
            var cargoCustomerEntity = _mapper.Map<CargoCustomer>(createCargoCustomerDto);
            await _cargoCustomerService.TInsertAsync(cargoCustomerEntity);
            return Ok("Kargo Müşteri Ekleme İşlemi Başarıyla Gerçekleşti.");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCargoCustomer(int id)
        {
            await _cargoCustomerService.TDeleteAsync(id);
            return Ok("Kargo Müşteri Silme İşlemi Başarıyla Gerçekleşti.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCargoCustomer(UpdateCargoCustomerDto updateCargoCustomerDto)
        {
            var cargoCustomerEntity = _mapper.Map<CargoCustomer>(updateCargoCustomerDto);
            await _cargoCustomerService.TUpdateAsync(cargoCustomerEntity);
            return Ok("Kargo Müşteri Güncelleme İşlemi Başarıyla Gerçekleşti.");
        }
    }
}