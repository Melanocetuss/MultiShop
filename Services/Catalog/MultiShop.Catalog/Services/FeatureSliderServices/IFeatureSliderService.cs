using MultiShop.Catalog.Dtos.FeatureSliderDtos;

namespace MultiShop.Catalog.Services.FeatureSliderServices
{
    public interface IFeatureSliderService
    {
        Task<List<ResultFeatureSliderDto>> GetAllFeatureSliderAsync();
        Task CreateFeatureSliderAsync(CreateFeatureSliderDto createFeatureSliderDto);
        Task UpdateFeatureSliderAsync(UpdateFeatureSliderDto updateFeatureSliderDto);
        Task DeleteFeatureSliderAsync(string FeatureSliderID);
        Task<GetbyIdFeatureSliderDto> GetByIdFeatureSliderAsync(string FeatureSliderID);
        Task FeatureSliderChangeStatusAsync(string FeatureSliderID);
    }
}