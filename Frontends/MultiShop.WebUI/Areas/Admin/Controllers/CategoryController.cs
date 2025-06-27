using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.CategoryDtos;
using MultiShop.WebUI.Services.CatalogServices.CategoryServices;
using MultiShop.WebUI.Services.CatalogServices.ProductServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _catgoryService;
        private readonly IProductService _productService;
        public CategoryController(ICategoryService catgoryService, IProductService productService)
        {
            _catgoryService = catgoryService;
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            CategoryViewBag();
            var values = await _catgoryService.GetAllCategoryAsync();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateCategory()
        {
            CategoryViewBag();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto createCategoryDto)
        {
            await _catgoryService.CreateCategoryAsync(createCategoryDto);
            return RedirectToAction("Index", "Category", new { Area = "Admin" });
        }

        public async Task<IActionResult> DeleteCategory(string id)
        {
            await _catgoryService.DeleteCategoryAsync(id);
            return RedirectToAction("Index", "Category", new { Area = "Admin" });
        }

        [HttpGet]
        public async Task<IActionResult> UpdateCategory(string id)
        {
            CategoryViewBag();
            var values = await _catgoryService.GetByIdCategoryAsync(id);
            return View(values);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto updateCategoryDto)
        {
            await _catgoryService.UpdateCategoryAsync(updateCategoryDto);
            return RedirectToAction("Index", "Category", new { Area = "Admin" });
        }

        // Prodect Refactoring And Methods
        public async Task<IActionResult> GetAllProductByCategoryId(string id)
        {
            CategoryViewBag();

            var values = await _productService.GetAllProductByCategoryIdAsync(id);
            return View(values);
        }

        void CategoryViewBag()
        {
            ViewBag.PageTitle = "Kategori İşlemleri";
            ViewBag.index1 = "Ana Sayfa";
            ViewBag.index2 = "Kategoriler";
            ViewBag.index3 = "Kategori İşlemleri";
        }
    }
}