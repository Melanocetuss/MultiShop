using MultiShop.DtoLayer.CatalogDtos.FeaturedDtos;

namespace MultiShop.WebUI.Services.CatalogServices.FeaturedServices
{
    public interface IFeaturedService
    {
        Task<List<ResultFeaturedDto>> GetAllFeaturedAsync();
        Task CreateFeaturedAsync(CreateFeaturedDto createFeaturedDto);
        Task UpdateFeaturedAsync(UpdateFeaturedDto updateFeaturedDto);
        Task DeleteFeaturedAsync(string FeaturedID);
        Task<UpdateFeaturedDto> GetByIdFeaturedAsync(string FeaturedID);
    }
}