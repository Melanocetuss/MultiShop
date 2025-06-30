using MultiShop.DtoLayer.CommentDtos;

namespace MultiShop.WebUI.Services.CommentServices
{
    public interface ICommentService
    {
        Task<List<ResultCommentDto>> GetAllCommentAsync();
        Task CreateCommentAsync(CreateCommentDto createCommentDto);
        Task UpdateCommentAsync(UpdateCommentDto updateCommentDto);
        Task DeleteCommentAsync(int UserCommentID);
        Task<UpdateCommentDto> GetByIdCommentAsync(int UserCommentID);
        Task CommentChangeStatusAsync(int UserCommentID);
        Task<List<GetCommentByProductIdDto>> GetCommentByProductID(string ProductID);
        Task<int> GetCommentCountByProductID(string ProductID);
    }
}