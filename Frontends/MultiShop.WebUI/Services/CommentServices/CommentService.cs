using MultiShop.DtoLayer.CommentDtos;

namespace MultiShop.WebUI.Services.CommentServices
{
    public class CommentService : ICommentService
    {
        private readonly HttpClient _httpClient;
        public CommentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        public async Task CreateCommentAsync(CreateCommentDto createCommentDto)
        {
            var responseMessage = await _httpClient.PostAsJsonAsync<CreateCommentDto>("Comments", createCommentDto);
        }

        public async Task DeleteCommentAsync(int UserCommentID)
        {
            await _httpClient.DeleteAsync($"Comments?UserCommentID={UserCommentID}");
        }

        public async Task<List<ResultCommentDto>> GetAllCommentAsync()
        {
            var responseMessage = await _httpClient.GetAsync("Comments");
            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultCommentDto>>();
            return values;
        }

        public async Task<UpdateCommentDto> GetByIdCommentAsync(int UserCommentID)
        {
            var responseMessage = await _httpClient.GetAsync($"Comments/GetCommentById?UserCommentID={UserCommentID}");
            var values = await responseMessage.Content.ReadFromJsonAsync<UpdateCommentDto>();
            return values;
        }

        public async Task UpdateCommentAsync(UpdateCommentDto updateCommentDto)
        {
            await _httpClient.PutAsJsonAsync<UpdateCommentDto>("Comments", updateCommentDto);
        }

        public async Task<List<GetCommentByProductIdDto>> GetCommentByProductID(string ProductID)
        {
            var responseMessage = await _httpClient.GetAsync($"Comments/GetCommentByProductID?ProductID={ProductID}");
            var values = await responseMessage.Content.ReadFromJsonAsync<List<GetCommentByProductIdDto>>();
            return values;
        }

        public async Task CommentChangeStatusAsync(int UserCommentID)
        {
            await _httpClient.PutAsync($"Comments/CommentChangeStatusAsync?UserCommentID={UserCommentID}", null);
        }

        public async Task<int> GetCommentCountByProductID(string ProductID)
        {
            var responseMessage = await _httpClient.GetAsync($"Comments/GetCommentCountByProductID?ProductID={ProductID}");
            var content = await responseMessage.Content.ReadAsStringAsync();
            return int.Parse(content); // hata fırlatırsa try-catch eklenebilir
        }
    }
}