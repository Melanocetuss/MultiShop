using MultiShop.DtoLayer.CatalogDtos.AboutDtos;

namespace MultiShop.WebUI.Services.CatalogServices.AboutServices
{
    public class AboutService : IAboutService
    {
        private readonly HttpClient _httpClient;
        public AboutService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ResultAboutDto>> GetAllAboutAsync()
        {
            var responseMessage = await _httpClient.GetAsync("Abouts");
            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultAboutDto>>();
            return values;
        }

        public async Task<UpdateAboutDto> GetByIdAboutAsync(string AboutID)
        {
            var responseMessage = await _httpClient.GetAsync($"Abouts/GetAboutById?AboutID={AboutID}");
            var values = await responseMessage.Content.ReadFromJsonAsync<UpdateAboutDto>();
            return values;
        }

        public async Task UpdateAboutAsync(UpdateAboutDto updateAboutDto)
        {
            await _httpClient.PutAsJsonAsync<UpdateAboutDto>("Abouts", updateAboutDto);
        }
    }
}