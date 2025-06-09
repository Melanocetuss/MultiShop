using AutoMapper;
using MultiShop.Cargo.DtoLayer.Dtos.CargoDetailDtos;
using MultiShop.Cargo.EntityLayer.Entities;

namespace MultiShop.Cargo.WebApi.Mapping
{
    public class CargoDetailMapping : Profile
    {
        public CargoDetailMapping()
        {
            CreateMap<CargoDetail, ResultCargoDetailDto>().ReverseMap();
            CreateMap<CargoDetail, CreateCargoDetailDto>().ReverseMap();
            CreateMap<CargoDetail, UpdateCargoDetailDto>().ReverseMap();
            CreateMap<CargoDetail, GetCargoDetailByIdDto>().ReverseMap();
        }
    }
}