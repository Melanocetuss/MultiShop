using MultiShop.DtoLayer.CatalogDtos.CategoryDtos;
using MultiShop.DtoLayer.CatalogDtos.ProductDtos;

namespace MultiShop.WebUI.Services.CatalogServices.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateProductAsync(CreateProductDto createProductDto)
        {
            await _httpClient.PostAsJsonAsync<CreateProductDto>("Products", createProductDto);
        }

        public async Task DeleteProductAsync(string ProductID)
        {
            await _httpClient.DeleteAsync($"Products?ProductID={ProductID}");
        }

        public Task<List<ResultProductDto>> GetAllProductAsync()
        {
            var responseMessage = _httpClient.GetAsync("Products");
            var values = responseMessage.Result.Content.ReadFromJsonAsync<List<ResultProductDto>>();
            return values;
        }

        public Task<List<ResultProductByCategoryIdDto>> GetAllProductByCategoryIdAsync(string CategoryID)
        {
            var responseMessage = _httpClient.GetAsync($"Products/GetAllProductByCategoryId?CategoryID={CategoryID}");
            var values = responseMessage.Result.Content.ReadFromJsonAsync<List<ResultProductByCategoryIdDto>>();
            return values;
        }

        public Task<List<ResultProductWithCategoryDto>> GetAllProductWithCategoryAsync()
        {
            var responseMessage = _httpClient.GetAsync("Products/GetAllProductWithCategory");
            var values = responseMessage.Result.Content.ReadFromJsonAsync<List<ResultProductWithCategoryDto>>();
            return values;
        }

        public async Task<UpdateProductDto> GetByIdProductAsync(string ProductID)
        {
            var responseMessage = await _httpClient.GetAsync($"Products/GetProductById?ProductID={ProductID}");
            var values = await responseMessage.Content.ReadFromJsonAsync<UpdateProductDto>();
            return values;
        }

        public async Task UpdateProductAsync(UpdateProductDto updateProductDto)
        {
            await _httpClient.PutAsJsonAsync<UpdateProductDto>("Products", updateProductDto);
        }
    }
}