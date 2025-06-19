using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MultiShop.DtoLayer.CatalogDtos.CategoryDtos;
using MultiShop.DtoLayer.CatalogDtos.ProductDtos;
using MultiShop.DtoLayer.CatalogDtos.ProductImageDtos;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ProductController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        private async Task<List<ResultCategoryDto>> GetCategoryList()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7127/api/Categories");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData);
                return values;
            }
            return null;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.PageTitle = "Ürün Listesi";
            ViewBag.index1 = "Ana Sayfa";
            ViewBag.index2 = "Ürünler";
            ViewBag.index3 = "Ürün Listesi";

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7127/api/Products/GetAllProductWithCategory");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultProductWithCategoryDto>>(jsonData);
                return View(values);
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            ViewBag.PageTitle = "Ürün Ekle";
            ViewBag.index1 = "Ana Sayfa";
            ViewBag.index2 = "Ürünler";
            ViewBag.index3 = "Ürün Ekle";

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
                return RedirectToAction("Index", "Product", new { Area = "Admin" });               
            }
            return View();
        }

        public async Task<IActionResult> DeleteProduct(string id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:7127/api/Products?ProductID={id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                await CheckProductAndDeleteProductImages();
                return RedirectToAction("Index", "Product", new { Area = "Admin" });
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProduct(string id)
        {
            ViewBag.PageTitle = "Ürün Güncelle";
            ViewBag.index1 = "Ana Sayfa";
            ViewBag.index2 = "Ürünler";
            ViewBag.index3 = "Ürün Güncelle";
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
                            Image1 = "",
                            Image2 = "",
                            Image3 = "",
                            Image4 = ""
                        };

                        var content = new StringContent(JsonConvert.SerializeObject(newImage), Encoding.UTF8, "application/json");
                        var createResponse = await client.PostAsync("https://localhost:7127/api/ProductImages", content);

                        if (!createResponse.IsSuccessStatusCode)
                        {
                            Console.WriteLine($"HATA: {item.ProductID} için görsel eklenemedi.");
                        }
                    }
                }

                return RedirectToAction("ProductImageList", "ProductImage", new { Area = "Admin" });
            }

            return BadRequest("Ürün listesi alınamadı.");
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
                        if (!deleteResponse.IsSuccessStatusCode)
                        {
                            Console.WriteLine($"Silme hatası: ProductImageID = {productImage.ProductImageID}");
                        }
                    }
                }

                return RedirectToAction("ProductImageList", "ProductImage", new { Area = "Admin" });
            }

            return BadRequest("Ürün veya görsel listesi alınamadı.");
        }

    }
}