using MultiShop.DtoLayer.CatalogDtos.ProductImageDtos;

namespace MultiShop.WebUI.Services.CatalogServices.ProductImageServices
{
    public interface IProductImageService
    {
        Task<List<ResultProductImageDto>> GetAllProductImageAsync();
        Task CreateProductImageAsync(CreateProductImageDto createProductImageDto);
        Task UpdateProductImageAsync(UpdateProductImageDto updateProductImageDto);
        Task DeleteProductImageAsync(string ProductImageID);
        Task<UpdateProductImageDto> GetByIdProductImageAsync(string ProductImageID);
        Task<GetProductImageByProductIdDto> GetProductImageByProductIdAsync(string ProductID);
    }
}