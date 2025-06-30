using MultiShop.DtoLayer.CatalogDtos.FeaturedDtos;

namespace MultiShop.WebUI.Services.CatalogServices.FeaturedServices
{
    public class FeaturedService : IFeaturedService
    {
        private readonly HttpClient _httpClient;
        public FeaturedService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateFeaturedAsync(CreateFeaturedDto createFeaturedDto)
        {
            var responseMessage = await _httpClient.PostAsJsonAsync<CreateFeaturedDto>("Featureds", createFeaturedDto);
        }

        public async Task DeleteFeaturedAsync(string FeaturedID)
        {
            await _httpClient.DeleteAsync($"Featureds?FeaturedID={FeaturedID}");
        }

        public async Task<List<ResultFeaturedDto>> GetAllFeaturedAsync()
        {
            var responseMessage = await _httpClient.GetAsync("Featureds");
            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultFeaturedDto>>();
            return values;
        }

        public async Task<UpdateFeaturedDto> GetByIdFeaturedAsync(string FeaturedID)
        {
            var responseMessage = await _httpClient.GetAsync($"Featureds/GetFeaturedById?FeaturedID={FeaturedID}");
            var values = await responseMessage.Content.ReadFromJsonAsync<UpdateFeaturedDto>();
            return values;
        }

        public async Task UpdateFeaturedAsync(UpdateFeaturedDto updateFeaturedDto)
        {
            await _httpClient.PutAsJsonAsync<UpdateFeaturedDto>("Featureds", updateFeaturedDto);
        }
    }
}
