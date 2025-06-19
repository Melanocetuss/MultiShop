using MultiShop.Catalog.Dtos.ProductImageDtos;

namespace MultiShop.Catalog.Services.ProductImageServices
{
    public interface IProductImageService
    {
        Task<List<ResultProductImageDto>> GetAllProductImageAsync();
        Task CreateProductImageAsync(CreateProductImageDto createProductImageDto);
        Task UpdateProductImageAsync(UpdateProductImageDto updateProductImageDto);
        Task DeleteProductImageAsync(string ProductImageID);
        Task<GetByIdProductImageDto> GetByIdProductImageAsync(string ProductImageID);
        Task<GetProductImageByProductIdDto> GetProductImageByProductIdAsync(string ProductID);
    }
}