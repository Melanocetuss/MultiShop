using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.ProductDtos;
using MultiShop.DtoLayer.CatalogDtos.ProductImageDtos;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductImageController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ProductImageController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index(string ProductID)
        {
            ViewBag.PageTitle = "Ürünün Görseleri";
            ViewBag.index1 = "Ana Sayfa";
            ViewBag.index2 = "Ürün Görseli";
            ViewBag.index3 = "Ürünün Görseleri";

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7127/api/ProductImages/GetProductImageByProductId?ProductID={ProductID}");
            if (responseMessage.IsSuccessStatusCode) 
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<GetProductImageByProductIdDto>(jsonData);
                return View(values);
            }
            return View();
        }

        public async Task<IActionResult> ProductImageList() 
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7127/api/ProductImages");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultProductImageDto>>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpGet("Admin/ProductImage/UpdateProductImage/{ProductImageID}")]
        public async Task<IActionResult> UpdateProductImage(string ProductImageID)
        {
            ViewBag.PageTitle = "Ürün Görseli Güncelleme";
            ViewBag.index1 = "Ana Sayfa";
            ViewBag.index2 = "Ürün Görseli";
            ViewBag.index3 = "Ürün Görseli Güncelleme";

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7127/api/ProductImages/GetProductImageById?ProductImageID={ProductImageID}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateProductImageDto>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProductImage(UpdateProductImageDto updateProductImageDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateProductImageDto);
            StringContent stringContent = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:7127/api/ProductImages", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("ProductImageList", "ProductImage", new { Area = "Admin" });
            }
            return View();
        }       
    }
}