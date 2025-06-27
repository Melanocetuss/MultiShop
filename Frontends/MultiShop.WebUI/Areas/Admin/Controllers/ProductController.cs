using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MultiShop.DtoLayer.CatalogDtos.CategoryDtos;
using MultiShop.DtoLayer.CatalogDtos.ProductDetailDtos;
using MultiShop.DtoLayer.CatalogDtos.ProductDtos;
using MultiShop.DtoLayer.CatalogDtos.ProductImageDtos;
using MultiShop.WebUI.Services.CatalogServices.CategoryServices;
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
        public ProductController(IHttpClientFactory httpClientFactory, IProductService productService, ICategoryService categoryService)
        {
            _httpClientFactory = httpClientFactory;
            _productService = productService;
            _categoryService = categoryService;
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
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createProductDto);
            StringContent stringContent = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7127/api/Products", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                await CheckProductAndAddNewProductImage();
                await CheckProductAndAddNewProductDetail();
                return RedirectToAction("Index", "Product", new { Area = "Admin" });               
            }
            return View();
        }

        public async Task<IActionResult> DeleteProduct(string id)
        {
            /* 
               Refactoring:
               await _productService.DeleteProductAsync(id); 
               need if IsSuccessStatusCode == 200 Run 
               await CheckProductAndDeleteProductImages();
               await CheckProductAndDeleteProductDetail();
            */
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:7127/api/Products?ProductID={id}");
            if (responseMessage.IsSuccessStatusCode)
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

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7127/api/Products/GetProductById?ProductID={id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var productDto = JsonConvert.DeserializeObject<UpdateProductDto>(jsonData);               
                return View(productDto);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduct(UpdateProductDto updateProductDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateProductDto);
            StringContent stringContent = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:7127/api/Products", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Product", new { Area = "Admin" });
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CheckProductAndAddNewProductImage()
        {
            var client = _httpClientFactory.CreateClient();
            var productResponseMessage = await client.GetAsync("https://localhost:7127/api/Products");

            if (productResponseMessage.IsSuccessStatusCode)
            {
                var jsonData = await productResponseMessage.Content.ReadAsStringAsync();
                var productList = JsonConvert.DeserializeObject<List<ResultProductDto>>(jsonData);

                foreach (var item in productList)
                {
                    var productImageResponseMessage = await client.GetAsync(
                        $"https://localhost:7127/api/ProductImages/GetProductImageByProductId?ProductID={item.ProductID}");

                    // 204 veya başarısız ise görsel yok demektir, yeni görsel ekle
                    if (productImageResponseMessage.StatusCode == System.Net.HttpStatusCode.NoContent
                        || !productImageResponseMessage.IsSuccessStatusCode)
                    {
                        var newImage = new CreateProductImageDto
                        {
                            ProductID = item.ProductID,
                            Image1 = item.ProductImageUrl,
                            Image2 = "",
                            Image3 = "",
                            Image4 = ""
                        };

                        var content = new StringContent(JsonConvert.SerializeObject(newImage), Encoding.UTF8, "application/json");
                        var createResponse = await client.PostAsync("https://localhost:7127/api/ProductImages", content);
                    }
                }
                return null;
            }
            return null;
        }

        [HttpGet]
        public async Task<IActionResult> CheckProductAndDeleteProductImages()
        {
            var client = _httpClientFactory.CreateClient();
            var productResponseMessage = await client.GetAsync("https://localhost:7127/api/Products");
            var productImageResponseMessage = await client.GetAsync("https://localhost:7127/api/ProductImages");

            if (productResponseMessage.IsSuccessStatusCode && productImageResponseMessage.IsSuccessStatusCode)
            {
                var productJsonData = await productResponseMessage.Content.ReadAsStringAsync();
                var productList = JsonConvert.DeserializeObject<List<ResultProductDto>>(productJsonData);

                var productImageJsonData = await productImageResponseMessage.Content.ReadAsStringAsync();
                var productImageList = JsonConvert.DeserializeObject<List<ResultProductImageDto>>(productImageJsonData);

                foreach (var productImage in productImageList)
                {
                    bool productExists = productList.Exists(p => p.ProductID == productImage.ProductID);
                    if (!productExists)
                    {
                        var deleteResponse = await client.DeleteAsync($"https://localhost:7127/api/ProductImages?ProductImageID={productImage.ProductImageID}");
                    }
                }
                return null;
            }
            return null;
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

        void ProductViewBag() 
        {
            ViewBag.PageTitle = "Ürün İşlemleri";
            ViewBag.index1 = "Ana Sayfa";
            ViewBag.index2 = "Ürünler";
            ViewBag.index3 = "Ürün İşlemleri";
        }
    }
}