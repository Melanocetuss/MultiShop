using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.FeaturedDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.FeaturedServices
{
    public class FeaturedService : IFeaturedService
    {
        private readonly IMongoCollection<Featured> _featuredCollection;
        private readonly IMapper _mapper;

        public FeaturedService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _featuredCollection = database.GetCollection<Featured>(databaseSettings.FeaturedCollectionName);
            _mapper = mapper;
        }

        public async Task CreateFeaturedAsync(CreateFeaturedDto createFeaturedDto)
        {
            var values = _mapper.Map<Featured>(createFeaturedDto);
            await _featuredCollection.InsertOneAsync(values);
        }

        public async Task DeleteFeaturedAsync(string FeaturedID)
        {
            await _featuredCollection.DeleteOneAsync(x=> x.FeaturedID == FeaturedID);
        }

        public async Task<List<ResultFeaturedDto>> GetAllFeaturedAsync()
        {
            var values = await _featuredCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultFeaturedDto>>(values);
        }

        public async Task<GetByIdFeaturedDto> GetByIdFeaturedAsync(string FeaturedID)
        {
            var values = await _featuredCollection.Find(x => x.FeaturedID == FeaturedID).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdFeaturedDto>(values);
        }

        public async Task UpdateFeaturedAsync(UpdateFeaturedDto updateFeaturedDto)
        {
            var values = _mapper.Map<Featured>(updateFeaturedDto);
            await _featuredCollection.ReplaceOneAsync(x => x.FeaturedID == updateFeaturedDto.FeaturedID, values);
        }
    }
}