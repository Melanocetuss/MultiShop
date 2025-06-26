using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.ContactDtos;
using MultiShop.Catalog.Services.ContactServices;

namespace MultiShop.Catalog.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContactService _contactService;
        public ContactsController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public async Task<IActionResult> ContactList()
        {
            var values = await _contactService.GetAllContactAsync();
            return Ok(values);
        }

        [HttpGet("GetContactById")]
        public async Task<IActionResult> GetContactById(string ContactID)
        {
            var value = await _contactService.GetContactByIdAsync(ContactID);
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateContact(CreateContactDto createContactDto)
        {
            await _contactService.CreateContactAsync(createContactDto);
            return Ok("İletişim Ekleme İşlemi Başarıyla Gerçekleşti.");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteContact(string ContactID)
        {
            await _contactService.DeleteContactAsync(ContactID);
            return Ok("İletişim Silme İşlemi Başarıyla Gerçekleşti.");
        }
    }
}