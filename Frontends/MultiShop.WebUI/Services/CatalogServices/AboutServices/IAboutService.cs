using MultiShop.DtoLayer.CatalogDtos.AboutDtos;

namespace MultiShop.WebUI.Services.CatalogServices.AboutServices
{
    public interface IAboutService
    {
        Task<List<ResultAboutDto>> GetAllAboutAsync();
        Task UpdateAboutAsync(UpdateAboutDto updateAboutDto);
        Task<UpdateAboutDto> GetByIdAboutAsync(string AboutID);
    }
}