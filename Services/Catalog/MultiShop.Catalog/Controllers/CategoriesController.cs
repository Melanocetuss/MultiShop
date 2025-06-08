using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.CategoryDtos;
using MultiShop.Catalog.Services.CategoryServices;

namespace MultiShop.Catalog.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> CategoryList()
        {
            var values = await _categoryService.GetAllCategoryAsync();
            return Ok(values);
        }

        [HttpGet("GetCategoryById")]
        public async Task<IActionResult> GetCategoryById(string CategoryID)
        {
            var values = await _categoryService.GetByIdCategoryAsync(CategoryID);
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto createCategoryDto)
        {
            await _categoryService.CreateCategoryAsync(createCategoryDto);
            return Ok("Kategori Ekleme İşlemi Başarıyla Gerçekleşti.");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCategory(string CategoryID)
        {
            await _categoryService.DeleteCategoryAsync(CategoryID);
            return Ok("Kategori Silme İşlemi Başarıyla Gerçekleşti.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto updateCategoryDto)
        {
            await _categoryService.UpdateCategoryAsync(updateCategoryDto);
            return Ok("Kategori Güncelleme İşlemi Başarıyla Gerçekleşti.");
        }
    }
}