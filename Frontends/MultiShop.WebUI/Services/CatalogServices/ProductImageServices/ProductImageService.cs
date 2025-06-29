using MultiShop.DtoLayer.CatalogDtos.ProductImageDtos;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.CatalogServices.ProductImageServices
{
    public class ProductImageService : IProductImageService
    {
        private readonly HttpClient _httpClient;

        public ProductImageService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateProductImageAsync(CreateProductImageDto createProductImageDto)
        {
            var responseMessage = await _httpClient.PostAsJsonAsync<CreateProductImageDto>("ProductImages", createProductImageDto);
        }

        public async Task DeleteProductImageAsync(string ProductImageID)
        {
            await _httpClient.DeleteAsync($"ProductImages?ProductImageID={ProductImageID}");
        }

        public async Task<List<ResultProductImageDto>> GetAllProductImageAsync()
        {
            var responseMessage = await _httpClient.GetAsync("ProductImages");
            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultProductImageDto>>();
            return values;
        }

        public async Task<UpdateProductImageDto> GetByIdProductImageAsync(string ProductImageID)
        {
            var responseMessage = await _httpClient.GetAsync($"ProductImages/GetProductImageById?ProductImageID={ProductImageID}");
            var values = await responseMessage.Content.ReadFromJsonAsync<UpdateProductImageDto>();
            return values;
        }

        public async Task<GetProductImageByProductIdDto> GetProductImageByProductIdAsync(string ProductID)
        {
            var responseMessage = await _httpClient.GetAsync($"ProductImages/GetProductImageByProductId?ProductID={ProductID}");

            if (responseMessage.IsSuccessStatusCode)
            {
                var content = await responseMessage.Content.ReadAsStringAsync();

                if (string.IsNullOrWhiteSpace(content))
                {
                    // Boş içerik varsa null dön
                    return null;
                }

                // JSON varsa deserialize et
                var values = JsonConvert.DeserializeObject<GetProductImageByProductIdDto>(content);

                return values;
            }
            else if (responseMessage.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                // Bulunamadıysa null dön
                return null;
            }
            else
            {
                throw new Exception($"API çağrısında hata: {responseMessage.StatusCode}");
            }
        }

        public async Task UpdateProductImageAsync(UpdateProductImageDto updateProductImageDto)
        {
            await _httpClient.PutAsJsonAsync<UpdateProductImageDto>("ProductImages", updateProductImageDto);
        }
    }
}