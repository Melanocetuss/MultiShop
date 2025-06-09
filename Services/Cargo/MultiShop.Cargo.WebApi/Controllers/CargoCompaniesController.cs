using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Cargo.BusinessLayer.Abstract;
using MultiShop.Cargo.DtoLayer.Dtos.CargoCompanyDtos;
using MultiShop.Cargo.EntityLayer.Entities;

namespace MultiShop.Cargo.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CargoCompaniesController : ControllerBase
    {
        private readonly ICargoCompanyService _cargoCompanyService;
        private readonly IMapper _mapper;
        public CargoCompaniesController(ICargoCompanyService cargoCompanyService, IMapper mapper)
        {
            _cargoCompanyService = cargoCompanyService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> CargoCompanyList()
        {
            var values = _mapper.Map<List<ResultCargoCompanyDto>>(await _cargoCompanyService.TGetAllAsync());
            return Ok(values);
        }

        [HttpGet("GetCargoCompanyById")]
        public async Task<IActionResult> CargoCompanyById(int id)
        {
            var values = await _cargoCompanyService.TGetByIdAsync(id);
            return Ok(_mapper.Map<GetCargoCompanyByIdDto>(values));
        }

        [HttpPost]
        public async Task<IActionResult> CreateCargoCompany(CreateCargoCompanyDto createCargoCompanyDto)
        {
            var cargoCompanyEntity = _mapper.Map<CargoCompany>(createCargoCompanyDto);
            await _cargoCompanyService.TInsertAsync(cargoCompanyEntity);
            return Ok("Kargo Şirketi Ekleme İşlemi Başarıyla Gerçekleşti.");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCargoCompany(int id)
        {
            await _cargoCompanyService.TDeleteAsync(id);
            return Ok("Kargo Şirketi Silme İşlemi Başarıyla Gerçekleşti.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCargoCompany(UpdateCargoCompanyDto updateCargoCompanyDto)
        {
            var cargoCompanyEntity = _mapper.Map<CargoCompany>(updateCargoCompanyDto);
            await _cargoCompanyService.TUpdateAsync(cargoCompanyEntity);
            return Ok("Kargo Şirketi Güncelleme İşlemi Başarıyla Gerçekleşti.");
        }
    }
}