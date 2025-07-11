﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.ProductDtos;
using MultiShop.Catalog.Services.ProductServices;

namespace MultiShop.Catalog.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _ProductService;
        public ProductsController(IProductService ProductService)
        {
            _ProductService = ProductService;
        }

        [HttpGet]
        public async Task<IActionResult> ProductList()
        {
            var values = await _ProductService.GetAllProductAsync();
            return Ok(values);
        }

        [HttpGet("GetProductById")]
        public async Task<IActionResult> GetProductById(string ProductID)
        {
            var values = await _ProductService.GetByIdProductAsync(ProductID);
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto)
        {
            await _ProductService.CreateProductAsync(createProductDto);
            return Ok("Ürün Ekleme İşlemi Başarıyla Gerçekleşti.");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(string ProductID)
        {
            await _ProductService.DeleteProductAsync(ProductID);
            return Ok("Ürün Silme İşlemi Başarıyla Gerçekleşti.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(UpdateProductDto updateProductDto)
        {
            await _ProductService.UpdateProductAsync(updateProductDto);
            return Ok("Ürün Güncelleme İşlemi Başarıyla Gerçekleşti.");
        }

        [HttpGet("GetAllProductWithCategory")]
        public async Task<IActionResult> GetAllProductWithCategory()
        {
            var values = await _ProductService.GetAllProductWithCategoryAsync();
            return Ok(values);
        }

        [HttpGet("GetAllProductByCategoryId")]
        public async Task<IActionResult> GetAllProductByCategoryId(string CategoryID)
        {
            var values = await _ProductService.GetAllProductByCategoryIdAsync(CategoryID);
            return Ok(values);
        }
    }
}