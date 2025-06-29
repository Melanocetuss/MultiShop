using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MultiShop.DtoLayer.CatalogDtos.CategoryDtos;
using MultiShop.DtoLayer.CatalogDtos.ProductDetailDtos;
using MultiShop.DtoLayer.CatalogDtos.ProductDtos;
using MultiShop.DtoLayer.CatalogDtos.ProductImageDtos;
using MultiShop.WebUI.Services.CatalogServices.CategoryServices;
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
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IProductImageService _productImageService;

        public ProductController(IHttpClientFactory httpClientFactory, IProductService productService, ICategoryService categoryService, IProductImageService productImageService)
        {
            _httpClientFactory = httpClientFactory;
            _productService = productService;
            _categoryService = categoryService;
            _productImageService = productImageService;
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
                //await CheckProductAndAddNewProductDetail();
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
                //await CheckProductAndDeleteProductDetail();
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

        // Need Refactoring
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
            var client = _httpClientFactory.CreateClient();
            var productResponseMessage = await client.GetAsync("https://localhost:7127/api/Products");

            if (productResponseMessage.IsSuccessStatusCode)
            {
                var jsonData = await productResponseMessage.Content.ReadAsStringAsync();
                var productList = JsonConvert.DeserializeObject<List<ResultProductDto>>(jsonData);

                foreach (var item in productList)
                {
                    var productDetailResponseMessage = await client.GetAsync(
                        $"https://localhost:7127/api/ProductDetails/GetProductDetailByProductId?ProductID={item.ProductID}");

                    // 204 veya başarısız ise görsel yok demektir, yeni görsel ekle
                    if (productDetailResponseMessage.StatusCode == System.Net.HttpStatusCode.NoContent
                        || !productDetailResponseMessage.IsSuccessStatusCode)
                    {
                        var newProductDetail = new CreateProductDetailDto
                        {
                            ProductID = item.ProductID,
                            ProductDescription = item.ProductDescription,
                            ProductInfo = ""
                        };

                        var content = new StringContent(JsonConvert.SerializeObject(newProductDetail), Encoding.UTF8, "application/json");
                        var createResponse = await client.PostAsync("https://localhost:7127/api/ProductDetails", content);
                    }
                }
                return null;
            }
            return null;
        }

        [HttpGet]
        public async Task<IActionResult> CheckProductAndDeleteProductDetail()
        {
            var client = _httpClientFactory.CreateClient();
            var productResponseMessage = await client.GetAsync("https://localhost:7127/api/Products");
            var productDetailResponseMessage = await client.GetAsync("https://localhost:7127/api/ProductDetails");

            if (productResponseMessage.IsSuccessStatusCode && productDetailResponseMessage.IsSuccessStatusCode)
            {
                var productJsonData = await productResponseMessage.Content.ReadAsStringAsync();
                var productList = JsonConvert.DeserializeObject<List<ResultProductDto>>(productJsonData);

                var productDetailJsonData = await productDetailResponseMessage.Content.ReadAsStringAsync();
                var productDetailList = JsonConvert.DeserializeObject<List<ResultProductDetailDto>>(productDetailJsonData);

                foreach (var productDetail in productDetailList)
                {
                    bool productExists = productList.Exists(p => p.ProductID == productDetail.ProductID);
                    if (!productExists)
                    {
                        var deleteResponse = await client.DeleteAsync($"https://localhost:7127/api/ProductDetails?ProductDetailID={productDetail.ProductDetailID}");
                    }
                }
                return null;
            }
            return null;
        }
        // Need Refactoring

        void ProductViewBag() 
        {
            ViewBag.PageTitle = "Ürün İşlemleri";
            ViewBag.index1 = "Ana Sayfa";
            ViewBag.index2 = "Ürünler";
            ViewBag.index3 = "Ürün İşlemleri";
        }
    }
}