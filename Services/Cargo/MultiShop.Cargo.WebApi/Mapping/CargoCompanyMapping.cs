using AutoMapper;
using MultiShop.Cargo.DtoLayer.Dtos.CargoCompanyDtos;
using MultiShop.Cargo.EntityLayer.Entities;

namespace MultiShop.Cargo.WebApi.Mapping
{
    public class CargoCompanyMapping : Profile
    {
        public CargoCompanyMapping()
        {
            CreateMap<CargoCompany, ResultCargoCompanyDto>().ReverseMap();          
            CreateMap<CargoCompany, CreateCargoCompanyDto>().ReverseMap();
            CreateMap<CargoCompany, UpdateCargoCompanyDto>().ReverseMap();
            CreateMap<CargoCompany, GetCargoCompanyByIdDto>().ReverseMap();
        }       
    }
}