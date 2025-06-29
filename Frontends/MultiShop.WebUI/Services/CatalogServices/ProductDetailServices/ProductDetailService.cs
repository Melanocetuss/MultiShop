using MultiShop.DtoLayer.CatalogDtos.ProductDetailDtos;
using MultiShop.DtoLayer.CatalogDtos.ProductImageDtos;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.CatalogServices.ProductDetailServices
{
    public class ProductDetailService : IProductDetailService
    {
        private readonly HttpClient _httpClient;
        public ProductDetailService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateProductDetailAsync(CreateProductDetailDto createProductDetailDto)
        {
            var responseMessage = await _httpClient.PostAsJsonAsync<CreateProductDetailDto>("ProductDetails", createProductDetailDto);
        }

        public async Task DeleteProductDetailAsync(string ProductDetailID)
        {
            await _httpClient.DeleteAsync($"ProductDetails?ProductDetailID={ProductDetailID}");
        }

        public async Task<List<ResultProductDetailDto>> GetAllProductDetailAsync()
        {
            var responseMessage = await _httpClient.GetAsync("ProductDetails");
            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultProductDetailDto>>();
            return values;
        }

        public async Task<UpdateProductDetailDto> GetByIdProductDetailAsync(string ProductDetailID)
        {
            var responseMessage = await _httpClient.GetAsync($"ProductDetails/GetProductDetailById?ProductDetailID={ProductDetailID}");
            var values = await responseMessage.Content.ReadFromJsonAsync<UpdateProductDetailDto>();
            return values;
        }

        public async Task<GetByIdProductDetailDto> GetProductDetailByProductIdAsync(string ProductID)
        {
            var responseMessage = await _httpClient.GetAsync($"ProductDetails/GetProductDetailByProductId?ProductID={ProductID}");

            if (responseMessage.IsSuccessStatusCode)
            {
                var content = await responseMessage.Content.ReadAsStringAsync();
                
                if (string.IsNullOrWhiteSpace(content))
                {
                    // Boş içerik varsa null dön
                    return null;
                }

                // JSON varsa deserialize et
                var values = JsonConvert.DeserializeObject<GetByIdProductDetailDto>(content);

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

        public async Task UpdateProductDetailAsync(UpdateProductDetailDto updateProductDetailDto)
        {
            await _httpClient.PutAsJsonAsync<UpdateProductDetailDto>("ProductDetails", updateProductDetailDto);
        }
    }
}