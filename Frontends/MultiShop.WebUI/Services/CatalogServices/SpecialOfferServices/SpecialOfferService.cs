using MultiShop.DtoLayer.CatalogDtos.SpecialOfferDtos;

namespace MultiShop.WebUI.Services.CatalogServices.SpecialOfferServices
{
    public class SpecialOfferService : ISpecialOfferService
    {
        private readonly HttpClient _httpClient;
        public SpecialOfferService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateSpecialOfferAsync(CreateSpecialOfferDto createSpecialOfferDto)
        {
            var responseMessage = await _httpClient.PostAsJsonAsync<CreateSpecialOfferDto>("SpecialOffers", createSpecialOfferDto);
        }

        public async Task DeleteSpecialOfferAsync(string SpecialOfferID)
        {
            await _httpClient.DeleteAsync($"SpecialOffers?SpecialOfferID={SpecialOfferID}");
        }

        public async Task<List<ResultSpecialOfferDto>> GetAllSpecialOfferAsync()
        {
            var responseMessage = await _httpClient.GetAsync("SpecialOffers");
            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultSpecialOfferDto>>();
            return values;
        }

        public async Task<UpdateSpecialOfferDto> GetByIdSpecialOfferAsync(string SpecialOfferID)
        {
            var responseMessage = await _httpClient.GetAsync($"SpecialOffers/GetSpecialOfferById?SpecialOfferID={SpecialOfferID}");
            var values = await responseMessage.Content.ReadFromJsonAsync<UpdateSpecialOfferDto>();
            return values;
        }

        public async Task SpecialOfferChangeStatusAsync(string SpecialOfferID)
        {
            await _httpClient.PutAsync($"SpecialOffers/SpecialOfferChangeStatusAsync?SpecialOfferID={SpecialOfferID}", null);
        }

        public async Task UpdateSpecialOfferAsync(UpdateSpecialOfferDto updateSpecialOfferDto)
        {
            await _httpClient.PutAsJsonAsync<UpdateSpecialOfferDto>("SpecialOffers", updateSpecialOfferDto);
        }
    }
}