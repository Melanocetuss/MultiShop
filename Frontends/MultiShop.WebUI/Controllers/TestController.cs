﻿using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.CategoryDtos;
using MultiShop.WebUI.Services.CatalogServices.CategoryServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace MultiShop.WebUI.Controllers
{
    public class TestController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ICategoryService _catgoryService;
        public TestController(IHttpClientFactory httpClientFactory, ICategoryService catgoryService)
        {
            _httpClientFactory = httpClientFactory;
            _catgoryService = catgoryService;
        }

        public async Task<IActionResult> Index()
        {
            string token = "";
            using (var httpClient = new HttpClient())
            {
                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri("http://localhost:5001/connect/token"),
                    Method = HttpMethod.Post,
                    Content = new FormUrlEncodedContent(new Dictionary<string, string>
                    {
                        { "client_id", "MultiShopVisitorId" },
                        { "client_secret", "multishopsecret" },
                        { "grant_type", "client_credentials" },
                    })
                };

                using (var response = await httpClient.SendAsync(request))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var tokenResponse = JObject.Parse(content);
                        token = tokenResponse["access_token"].ToString();
                    }
                }
            }

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var responseMessage = await client.GetAsync("https://localhost:7127/api/Categories");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData);
                return View(values);
            }
            return View();
        }

        public async Task<IActionResult> Deneme()
        {
            var values = await _catgoryService.GetAllCategoryAsync();
            return View(values);
        }
    }
}