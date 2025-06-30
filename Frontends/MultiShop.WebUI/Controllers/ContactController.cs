using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.ContactDtos;
using MultiShop.WebUI.Services.CatalogServices.ContactServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace MultiShop.WebUI.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;
        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        public async Task<IActionResult> Index(CreateContactDto createContactDto)
        {
            ViewBag.FirstBreadcrump = "Ana Sayfa";
            ViewBag.SecondBreadcrump = "İletişim";
       
            createContactDto.SendDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            createContactDto.IsRead = false;
            bool response = await _contactService.CreateContactAsync(createContactDto);
            if(response == true)
            {
                return RedirectToAction("Index", "Default");
            }

            return View();
        }
    }
}