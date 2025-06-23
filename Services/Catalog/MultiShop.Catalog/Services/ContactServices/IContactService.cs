using MultiShop.Catalog.Dtos.ContactDtos;

namespace MultiShop.Catalog.Services.ContactServices
{
    public interface IContactService
    {
        Task<List<ResultContactDto>> GetAllContactAsync();
        Task CreateContactAsync(CreateContactDto createContactDto);
        Task<GetByIdContactDto> GetContactByIdAsync(string ContactID);
        Task DeleteContactAsync(string ContactID);

    }
}
