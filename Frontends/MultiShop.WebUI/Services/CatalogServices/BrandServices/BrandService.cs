using MultiShop.DtoLayer.CatalogDtos.BrandDtos;

namespace MultiShop.WebUI.Services.CatalogServices.BrandServices
{
    public class BrandService : IBrandService
    {
        private readonly HttpClient _httpClient;
        public BrandService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateBrandAsync(CreateBrandDto createBrandDto)
        {
            var responseMessage = await _httpClient.PostAsJsonAsync<CreateBrandDto>("Brands", createBrandDto);
        }

        public async Task DeleteBrandAsync(string BrandID)
        {
            await _httpClient.DeleteAsync($"Brands?BrandID={BrandID}");
        }

        public async Task<List<ResultBrandDto>> GetAllBrandAsync()
        {
            var responseMessage = await _httpClient.GetAsync("Brands");
            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultBrandDto>>();
            return values;
        }

        public async Task<UpdateBrandDto> GetByIdBrandAsync(string BrandID)
        {
            var responseMessage = await _httpClient.GetAsync($"Brands/GetBrandById?BrandID={BrandID}");
            var values = await responseMessage.Content.ReadFromJsonAsync<UpdateBrandDto>();
            return values;
        }

        public async Task UpdateBrandAsync(UpdateBrandDto updateBrandDto)
        {
            await _httpClient.PutAsJsonAsync<UpdateBrandDto>("Brands", updateBrandDto);
        }
    }
}