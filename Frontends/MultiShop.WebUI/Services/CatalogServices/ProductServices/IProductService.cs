﻿using MultiShop.DtoLayer.CatalogDtos.CategoryDtos;
using MultiShop.DtoLayer.CatalogDtos.ProductDtos;

namespace MultiShop.WebUI.Services.CatalogServices.ProductServices
{
    public interface IProductService
    {
        Task<List<ResultProductDto>> GetAllProductAsync();
        Task<HttpResponseMessage> CreateProductAsync(CreateProductDto createProductDto);
        Task UpdateProductAsync(UpdateProductDto updateProductDto);
        Task<HttpResponseMessage> DeleteProductAsync(string ProductID);
        Task<UpdateProductDto> GetByIdProductAsync(string ProductID);
        Task<List<ResultProductWithCategoryDto>> GetAllProductWithCategoryAsync();
        Task<List<ResultProductByCategoryIdDto>> GetAllProductByCategoryIdAsync(string CategoryID);
    }
}