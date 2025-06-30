using MultiShop.DtoLayer.CatalogDtos.ContactDtos;

namespace MultiShop.WebUI.Services.CatalogServices.ContactServices
{
    public class ContactService : IContactService
    {
        private readonly HttpClient _httpClient;
        public ContactService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> CreateContactAsync(CreateContactDto createContactDto)
        {
            var responseMessage = await _httpClient.PostAsJsonAsync("Contacts", createContactDto);
            return responseMessage.IsSuccessStatusCode;
        }

        public async Task DeleteContactAsync(string ContactID)
        {
            await _httpClient.DeleteAsync($"Contacts?ContactID={ContactID}");
        }

        public async Task<List<ResultContactDto>> GetAllContactAsync()
        {
            var responseMessage = await _httpClient.GetAsync("Contacts");
            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultContactDto>>();
            return values;
        }

        public async Task<GetByIdContactDto> GetContactByIdAsync(string ContactID)
        {
            var responseMessage = await _httpClient.GetAsync($"Contacts?ContactID={ContactID}");
            var values = await responseMessage.Content.ReadFromJsonAsync<GetByIdContactDto>();
            return values;
        }
    }
}