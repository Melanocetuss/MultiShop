using MultiShop.DtoLayer.CatalogDtos.CategoryDtos;

namespace MultiShop.WebUI.Services.CatalogServices.CategoryServices
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _httpClient;
        public CategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateCategoryAsync(CreateCategoryDto createCategoryDto)
        {
            var responseMessage = await _httpClient.PostAsJsonAsync<CreateCategoryDto>("Categories", createCategoryDto);
        }

        public async Task DeleteCategoryAsync(string CategoryID)
        {
            await _httpClient.DeleteAsync($"Categories?CategoryID={CategoryID}");
        }

        public async Task<List<ResultCategoryDto>> GetAllCategoryAsync()
        {
            var responseMessage = await _httpClient.GetAsync("Categories");
            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultCategoryDto>>();
            return values;
        }

        public async Task<GetByIdCategoryDto> GetByIdCategoryAsync(string CategoryID)
        {
            var responseMessage = await _httpClient.GetAsync($"Categories/GetCategoryById?CategoryID={CategoryID}");
            var values = await responseMessage.Content.ReadFromJsonAsync<GetByIdCategoryDto>();
            return values;
        }

        public async Task UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto)
        {
            await _httpClient.PutAsJsonAsync<UpdateCategoryDto>("Categories", updateCategoryDto);
        }
    }
}