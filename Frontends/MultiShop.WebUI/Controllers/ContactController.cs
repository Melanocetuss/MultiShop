using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.ContactDtos;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Controllers
{
    public class ContactController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;
        public ContactController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index(CreateContactDto createContactDto)
        {
            ViewBag.FirstBreadcrump = "Ana Sayfa";
            ViewBag.SecondBreadcrump = "İletişim";

            createContactDto.SendDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            createContactDto.IsRead = false;
            var client = httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createContactDto);
            StringContent stringContent = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7127/api/Contacts", stringContent);
            if (responseMessage.IsSuccessStatusCode) 
            {
                return RedirectToAction("Index", "Default");
            }

            return View();
        }
    }
}