using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.FeatureSliderDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.FeatureSliderServices
{
    public class FeatureSliderService : IFeatureSliderService
    {
        private readonly IMongoCollection<FeatureSlider> _featureSliderCollection;
        private readonly IMapper _mapper;

        public FeatureSliderService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _featureSliderCollection = database.GetCollection<FeatureSlider>(databaseSettings.FeatureSliderCollectionName);
            _mapper = mapper;
        }

        public async Task CreateFeatureSliderAsync(CreateFeatureSliderDto createFeatureSliderDto)
        {
            var values = _mapper.Map<FeatureSlider>(createFeatureSliderDto);
            await _featureSliderCollection.InsertOneAsync(values);
        }

        public async Task DeleteFeatureSliderAsync(string FeatureSliderID)
        {
            await _featureSliderCollection.DeleteOneAsync(x => x.FeatureSliderID == FeatureSliderID);
        }

        public async Task FeatureSliderChangeStatusAsync(string FeatureSliderID)
        {
            // İlgili dokümanı bul
            var filter = Builders<FeatureSlider>.Filter.Eq(x => x.FeatureSliderID, FeatureSliderID);
            var slider = await _featureSliderCollection.Find(filter).FirstOrDefaultAsync();

            if (slider != null)
            {
                // Mevcut durumu tersine çevir
                var newStatus = !slider.Status;

                // Status alanını güncelle
                var update = Builders<FeatureSlider>.Update.Set(x => x.Status, newStatus);
                await _featureSliderCollection.UpdateOneAsync(filter, update);
            }
        }

        public async Task<List<ResultFeatureSliderDto>> GetAllFeatureSliderAsync()
        {
            var values = await _featureSliderCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultFeatureSliderDto>>(values);
        }

        public async Task<GetbyIdFeatureSliderDto> GetByIdFeatureSliderAsync(string FeatureSliderID)
        {
            var values = await _featureSliderCollection.Find(x => x.FeatureSliderID == FeatureSliderID).FirstOrDefaultAsync();
            return _mapper.Map<GetbyIdFeatureSliderDto>(values);
        }

        public async Task UpdateFeatureSliderAsync(UpdateFeatureSliderDto updateFeatureSliderDto)
        {
            var values = _mapper.Map<FeatureSlider>(updateFeatureSliderDto);
            await _featureSliderCollection.ReplaceOneAsync(x => x.FeatureSliderID == updateFeatureSliderDto.FeatureSliderID, values);
        }
    }
}