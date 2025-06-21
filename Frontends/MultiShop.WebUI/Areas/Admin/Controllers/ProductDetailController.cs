using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.ProductDetailDtos;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductDetailController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ProductDetailController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        
        [Route("/Admin/ProductDetail/index/{ProductID}")]
        public async Task<IActionResult> Index(string ProductID)
        {
            ViewBag.PageTitle = "Ürün Detayları Listesi";
            ViewBag.index1 = "Ana Sayfa";
            ViewBag.index2 = "Ürün Detayları";
            ViewBag.index3 = "Ürün Detayları Listesi";

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7127/api/ProductDetails/GetProductDetailByProductId?ProductID={ProductID}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<GetByIdProductDetailDto>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpGet("Admin/ProductDetail/UpdateProductDetail/{ProductDetailID}")]
        public async Task<IActionResult> UpdateProductDetail(string ProductDetailID)
        {
            ViewBag.PageTitle = "Ürün Detayları Güncelleme";
            ViewBag.index1 = "Ana Sayfa";
            ViewBag.index2 = "Ürün Detayları";
            ViewBag.index3 = "Ürün Detayları Güncelleme";

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7127/api/ProductDetails/GetCategoryById?ProductDetailID={ProductDetailID}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateProductDetailDto>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProductDetail(UpdateProductDetailDto updateProductDetailDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateProductDetailDto);
            StringContent stringContent = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:7127/api/ProductDetails", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "ProductDetail", new { Area = "Admin" });
            }
            return View();
        }        
    }
}