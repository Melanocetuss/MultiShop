using MultiShop.DtoLayer.CatalogDtos.ContactDtos;

namespace MultiShop.WebUI.Services.CatalogServices.ContactServices
{
    public interface IContactService
    {
        Task<List<ResultContactDto>> GetAllContactAsync();
        Task<bool> CreateContactAsync(CreateContactDto createContactDto);
        Task<GetByIdContactDto> GetContactByIdAsync(string ContactID);
        Task DeleteContactAsync(string ContactID);
    }
}