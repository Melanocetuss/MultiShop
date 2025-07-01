using MultiShop.DtoLayer.BasketDtos;

namespace MultiShop.WebUI.Services.BasketServices
{
    public class BasketService : IBasketService
    {
        private readonly HttpClient _httpClient;
        public BasketService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<BasketTotalDto> GetBasket()
        {
            var responseMessage = await _httpClient.GetAsync("Baskets");
            var values = await responseMessage.Content.ReadFromJsonAsync<BasketTotalDto>();
            return values;
        }

        public async Task SaveBasket(BasketTotalDto basketTotalDto)
        {
            var responseMessage = await _httpClient.PostAsJsonAsync<BasketTotalDto>("Baskets", basketTotalDto);
        }

        public Task DeleteBasket(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task AddBasketItem(BasketItemDto basketItemDto)
        {
            var values = await GetBasket();
            if (values!= null)
            {
                if(!values.BasketItems.Any(x => x.ProductID == basketItemDto.ProductID))
                {
                    values.BasketItems.Add(basketItemDto);
                }
                else 
                {
                    values = new BasketTotalDto();
                    values.BasketItems.Add(basketItemDto);
                }
            }
            await SaveBasket(values);
        }

        public async Task<bool> RemoveBasketItem(string productId)
        {
            var values = await GetBasket();
            var deleteItem = values.BasketItems.FirstOrDefault(x => x.ProductID == productId);
            var result = values.BasketItems.Remove(deleteItem);
            await SaveBasket(values);
            return true;
        }
    }
}