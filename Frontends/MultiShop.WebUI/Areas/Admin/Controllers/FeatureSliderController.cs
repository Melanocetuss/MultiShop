using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.FeatureSliderDtos;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FeatureSliderController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public FeatureSliderController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.PageTitle = "Öne Çıkanlar Listesi";
            ViewBag.index1 = "Ana Sayfa";
            ViewBag.index2 = "Öne Çıkanlar";
            ViewBag.index3 = "Öne Çıkanlar Listesi";

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7127/api/FeatureSliders");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultFeatureSliderDto>>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpGet]
        public IActionResult CreateFeatureSlider()
        {
            ViewBag.PageTitle = "Öne Çıkanlar Ekleme";
            ViewBag.index1 = "Ana Sayfa";
            ViewBag.index2 = "Öne Çıkanlar";
            ViewBag.index3 = "Öne Çıkanlar Ekleme";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateFeatureSlider(CreateFeatureSliderDto createFeatureSliderDto)
        {
            createFeatureSliderDto.Status = false;
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createFeatureSliderDto);
            StringContent stringContent = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7127/api/FeatureSliders", stringContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "FeatureSlider", new { Area = "Admin" });
            }

            return View();
        }

        public async Task<IActionResult> DeleteFeatureSlider(string id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:7127/api/FeatureSliders?FeatureSliderID={id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "FeatureSlider", new { Area = "Admin" });
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateFeatureSlider(string id)
        {
            ViewBag.PageTitle = "Öne Çıkanlar Güncelleme";
            ViewBag.index1 = "Ana Sayfa";
            ViewBag.index2 = "Öne Çıkanlar";
            ViewBag.index3 = "Öne Çıkanlar Güncelleme";

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7127/api/FeatureSliders/GetFeatureSliderById?FeatureSliderID={id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateFeatureSliderDto>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateFeatureSlider(UpdateFeatureSliderDto updateFeatureSliderDto)
        {
            updateFeatureSliderDto.Status = false;
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateFeatureSliderDto);
            StringContent stringContent = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:7127/api/FeatureSliders", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "FeatureSlider", new { Area = "Admin" });
            }
            return View();
        }

        public async Task<IActionResult> ChangeStatus(string id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.PutAsync($"https://localhost:7127/api/FeatureSliders/FeatureSliderChangeStatusAsync?FeatureSliderID={id}", null);
            if (responseMessage.IsSuccessStatusCode) 
            {
                return RedirectToAction("Index", "FeatureSlider", new { Area = "Admin" });
            }
            return View();
        }
    }
}