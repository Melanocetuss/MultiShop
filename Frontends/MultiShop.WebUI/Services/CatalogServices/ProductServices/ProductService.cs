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

        public async Task<HttpResponseMessage> CreateProductAsync(CreateProductDto createProductDto)
        {
            return await _httpClient.PostAsJsonAsync<CreateProductDto>("Products", createProductDto);
        }

        public async Task<HttpResponseMessage> DeleteProductAsync(string ProductID)
        {
            return await _httpClient.DeleteAsync($"Products?ProductID={ProductID}");
        }

        public async Task<List<ResultProductDto>> GetAllProductAsync()
        {
            var responseMessage = await _httpClient.GetAsync("Products");
            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultProductDto>>();
            return values;
        }

        public async Task<List<ResultProductByCategoryIdDto>> GetAllProductByCategoryIdAsync(string CategoryID)
        {
            var responseMessage = await _httpClient.GetAsync($"Products/GetAllProductByCategoryId?CategoryID={CategoryID}");
            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultProductByCategoryIdDto>>();
            return values;
        }

        public async Task<List<ResultProductWithCategoryDto>> GetAllProductWithCategoryAsync()
        {
            var responseMessage = await _httpClient.GetAsync("Products/GetAllProductWithCategory");
            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultProductWithCategoryDto>>();
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