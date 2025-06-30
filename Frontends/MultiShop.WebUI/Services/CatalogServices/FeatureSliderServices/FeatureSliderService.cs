using MultiShop.DtoLayer.CatalogDtos.FeatureSliderDtos;

namespace MultiShop.WebUI.Services.CatalogServices.FeatureSliderServices
{
    public class FeatureSliderService : IFeatureSliderService
    {
        private readonly HttpClient _httpClient;

        public FeatureSliderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateFeatureSliderAsync(CreateFeatureSliderDto createFeatureSliderDto)
        {
            var responseMessage = await _httpClient.PostAsJsonAsync<CreateFeatureSliderDto>("FeatureSliders", createFeatureSliderDto);
        }

        public async Task DeleteFeatureSliderAsync(string FeatureSliderID)
        {
            await _httpClient.DeleteAsync($"FeatureSliders?FeatureSliderID={FeatureSliderID}");
        }

        public async Task FeatureSliderChangeStatusAsync(string FeatureSliderID)
        {
            await _httpClient.PutAsync($"FeatureSliders/FeatureSliderChangeStatusAsync?FeatureSliderID={FeatureSliderID}", null);
        }

        public async Task<List<ResultFeatureSliderDto>> GetAllFeatureSliderAsync()
        {
            var responseMessage = await _httpClient.GetAsync("FeatureSliders");
            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultFeatureSliderDto>>();
            return values;
        }

        public async Task<UpdateFeatureSliderDto> GetByIdFeatureSliderAsync(string FeatureSliderID)
        {
            var responseMessage = await _httpClient.GetAsync($"FeatureSliders/GetFeatureSliderById?FeatureSliderID={FeatureSliderID}");
            var values = await responseMessage.Content.ReadFromJsonAsync<UpdateFeatureSliderDto>();
            return values;
        }

        public async Task UpdateFeatureSliderAsync(UpdateFeatureSliderDto updateFeatureSliderDto)
        {
            await _httpClient.PutAsJsonAsync<UpdateFeatureSliderDto>("FeatureSliders", updateFeatureSliderDto);
        }
    }
}