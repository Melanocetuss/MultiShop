using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CommentDtos;
using Newtonsoft.Json;

namespace MultiShop.WebUI.ViewComponents.ProductDetailViewComponents
{
    public class _ReviewProductDetailComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public _ReviewProductDetailComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync(string ProductID)
        {
            var client = _httpClientFactory.CreateClient();

            var responseMessage = await client.GetAsync($"https://localhost:7132/api/Comments/GetCommentByProductID?ProductID={ProductID}");
            List<GetCommentByProductIdDto> values = new();

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                values = JsonConvert.DeserializeObject<List<GetCommentByProductIdDto>>(jsonData);
            }

            var responseCount = await client.GetAsync($"https://localhost:7132/api/Comments/GetCommentCountByProductID?ProductID={ProductID}");
            if (responseCount.IsSuccessStatusCode)
            {
                var countData = await responseCount.Content.ReadAsStringAsync();
                ViewBag.CommentCount = countData;
            }
            else
            {
                ViewBag.CommentCount = 0;
            }
            return View(values);
        }
    }
}