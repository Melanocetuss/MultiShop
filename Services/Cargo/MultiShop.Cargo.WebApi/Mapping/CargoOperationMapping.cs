﻿using AutoMapper;
using MultiShop.Cargo.DtoLayer.Dtos.CargoOperationDtos;
using MultiShop.Cargo.EntityLayer.Entities;

namespace MultiShop.Cargo.WebApi.Mapping
{
    public class CargoOperationMapping : Profile
    {
        public CargoOperationMapping()
        {
            CreateMap<CargoOperation, ResultCargoOperationDto>().ReverseMap();
            CreateMap<CargoOperation, CreateCargoOperationDto>().ReverseMap();
            CreateMap<CargoOperation, UpdateCargoOperationDto>().ReverseMap();
            CreateMap<CargoOperation, GetCargoOperationByIdDto>().ReverseMap();
        }
    }
}