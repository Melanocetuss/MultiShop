using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MultiShop.DtoLayer.CatalogDtos.CategoryDtos;
using MultiShop.DtoLayer.CatalogDtos.ProductDetailDtos;
using MultiShop.DtoLayer.CatalogDtos.ProductDtos;
using MultiShop.DtoLayer.CatalogDtos.ProductImageDtos;
using MultiShop.WebUI.Services.CatalogServices.CategoryServices;
using MultiShop.WebUI.Services.CatalogServices.ProductDetailServices;
using MultiShop.WebUI.Services.CatalogServices.ProductImageServices;
using MultiShop.WebUI.Services.CatalogServices.ProductServices;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IProductImageService _productImageService;
        private readonly IProductDetailService _productDetailService;

        public ProductController(IProductService productService, ICategoryService categoryService, IProductImageService productImageService, IProductDetailService productDetailService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _productImageService = productImageService;
            _productDetailService = productDetailService;
        }

        private async Task<List<ResultCategoryDto>> GetCategoryList()
        {
            var values = await _categoryService.GetAllCategoryAsync();
            return values;
        }

        public async Task<IActionResult> Index()
        {
            ProductViewBag();

            var values = await _productService.GetAllProductWithCategoryAsync();
            return View(values);
        }

        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            ProductViewBag();
            var categoryList = await GetCategoryList();
            ViewBag.Categories = new SelectList(categoryList, "CategoryID", "CategoryName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto)
        {
            var response = await _productService.CreateProductAsync(createProductDto);
            if (response.IsSuccessStatusCode)
            {
                await CheckProductAndAddNewProductImage();
                await CheckProductAndAddNewProductDetail();
                return RedirectToAction("Index", "Product", new { Area = "Admin" });
            }
            return View();
        }

        public async Task<IActionResult> DeleteProduct(string id)
        {
            var response = await _productService.DeleteProductAsync(id);
            if (response.IsSuccessStatusCode)
            {
                await CheckProductAndDeleteProductImages();
                await CheckProductAndDeleteProductDetail();
                return RedirectToAction("Index", "Product", new { Area = "Admin" });
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProduct(string id)
        {
            ProductViewBag();
            var categoryList = await GetCategoryList();
            ViewBag.Categories = new SelectList(categoryList, "CategoryID", "CategoryName");
            var values = await _productService.GetByIdProductAsync(id);
            return View(values);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduct(UpdateProductDto updateProductDto)
        {
            await _productService.UpdateProductAsync(updateProductDto);
            return RedirectToAction("Index", "Product", new { Area = "Admin" });
        }
 
        [HttpGet]
        public async Task<IActionResult> CheckProductAndAddNewProductImage()
        {
            try
            {
                var products = await _productService.GetAllProductAsync();

                if (products == null || products.Count == 0)
                    return NotFound("Hiç ürün bulunamadı.");

                int createdCount = 0;

                foreach (var product in products)
                {
                    var productImage = await _productImageService.GetProductImageByProductIdAsync(product.ProductID);

                    // Görsel yoksa ve ürünün görsel URL'si varsa ekle
                    if (productImage == null && !string.IsNullOrEmpty(product.ProductImageUrl))
                    {
                        var newImageDto = new CreateProductImageDto
                        {
                            ProductID = product.ProductID,
                            Image1 = product.ProductImageUrl,
                            Image2 = "null",
                            Image3 = "null",
                            Image4 = "null"
                        };

                        await _productImageService.CreateProductImageAsync(newImageDto);
                        createdCount++;
                    }
                }

                return Ok($"{createdCount} ürün için yeni görsel oluşturuldu.");
            }
            catch (Exception ex)
            {
                // Loglama eklenebilir
                return StatusCode(500, "Sunucu hatası: " + ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> CheckProductAndDeleteProductImages()
        {
            try
            {
                var products = await _productService.GetAllProductAsync();
                var productImages = await _productImageService.GetAllProductImageAsync();

                if (products == null || productImages == null)
                    return NotFound("Ürünler veya ürün görselleri bulunamadı.");

                int deletedCount = 0;

                foreach (var productImage in productImages)
                {
                    bool productExists = products.Exists(p => p.ProductID == productImage.ProductID);

                    if (!productExists)
                    {
                        await _productImageService.DeleteProductImageAsync(productImage.ProductImageID);
                        deletedCount++;
                    }
                }

                return Ok($"{deletedCount} ürün görseli, ilgili ürün bulunamadığı için silindi.");
            }
            catch (Exception ex)
            {
                // Burada loglama yapılabilir
                return StatusCode(500, "Sunucu hatası: " + ex.Message);
            }
        }
        
        [HttpGet]
        public async Task<IActionResult> CheckProductAndAddNewProductDetail()
        {
            try
            {
                var products = await _productService.GetAllProductAsync();

                if (products == null || products.Count == 0)
                    return NotFound("Hiç ürün bulunamadı.");

                int createdCount = 0;

                foreach (var product in products)
                {
                    var productDetail = await _productDetailService.GetProductDetailByProductIdAsync(product.ProductID);
                    
                    // Ürün detayı yoksa ve ürün açıklaması varsa ekle
                    if (productDetail == null && !string.IsNullOrEmpty(product.ProductDescription))
                    {
                        var newDetailDto = new CreateProductDetailDto
                        {
                            ProductID = product.ProductID,
                            ProductDescription = product.ProductDescription,
                            ProductInfo = "null"
                        };

                        await _productDetailService.CreateProductDetailAsync(newDetailDto);
                        createdCount++;
                    }
                }

                return Ok($"{createdCount} ürün için yeni görsel oluşturuldu.");
            }
            catch (Exception ex)
            {
                // Loglama eklenebilir
                return StatusCode(500, "Sunucu hatası: " + ex.Message);
            }            
        }

        [HttpGet]
        public async Task<IActionResult> CheckProductAndDeleteProductDetail()
        {
            try
            {
                var products = await _productService.GetAllProductAsync();
                var productDetails = await _productDetailService.GetAllProductDetailAsync();
                
                if (products == null || productDetails == null)
                    return NotFound("Ürünler veya ürün detayları bulunamadı.");
                
                int deletedCount = 0;

                foreach (var productDetail in productDetails)
                {
                    bool productExists = products.Exists(p => p.ProductID == productDetail.ProductID);
                    
                    if (!productExists)
                    {
                        await _productDetailService.DeleteProductDetailAsync(productDetail.ProductDetailID);
                        deletedCount++;
                    }
                }
                
                return Ok($"{deletedCount} ürün görseli, ilgili ürün bulunamadığı için silindi.");
            }

            catch (Exception ex)
            {
                // Burada loglama yapılabilir
                return StatusCode(500, "Sunucu hatası: " + ex.Message);
            }          
        }

        void ProductViewBag() 
        {
            ViewBag.PageTitle = "Ürün İşlemleri";
            ViewBag.index1 = "Ana Sayfa";
            ViewBag.index2 = "Ürünler";
            ViewBag.index3 = "Ürün İşlemleri";
        }
    }
}