using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.SpecialOfferDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.SpecialOfferServices
{
    public class SpecialOfferService : ISpecialOfferService
    {
        private readonly IMongoCollection<SpecialOffer> _specialOfferCollection;
        private readonly IMapper _mapper;

        public SpecialOfferService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _specialOfferCollection = database.GetCollection<SpecialOffer>(databaseSettings.SpecialOfferCollectionName);
            _mapper = mapper;
        }

        public async Task CreateSpecialOfferAsync(CreateSpecialOfferDto createSpecialOfferDto)
        {
            var values = _mapper.Map<SpecialOffer>(createSpecialOfferDto);
            await _specialOfferCollection.InsertOneAsync(values);
        }

        public async Task DeleteSpecialOfferAsync(string SpecialOfferID)
        {
            await _specialOfferCollection.DeleteOneAsync(x => x.SpecialOfferID == SpecialOfferID);
        }

        public async Task<List<ResultSpecialOfferDto>> GetAllSpecialOfferAsync()
        {
            var values = await _specialOfferCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultSpecialOfferDto>>(values);
        }

        public async Task<GetByIdSpecialOfferDto> GetByIdSpecialOfferAsync(string SpecialOfferID)
        {
            var values = await _specialOfferCollection.Find(x=> x.SpecialOfferID == SpecialOfferID).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdSpecialOfferDto>(values);
        }

        public async Task UpdateSpecialOfferAsync(UpdateSpecialOfferDto updateSpecialOfferDto)
        {
            var values = _mapper.Map<SpecialOffer>(updateSpecialOfferDto);
            await _specialOfferCollection.FindOneAndReplaceAsync(x => x.SpecialOfferID == updateSpecialOfferDto.SpecialOfferID, values);
        }

        public async Task SpecialOfferChangeStatusAsync(string SpecialOfferID)
        {
            // İlgili dokümanı bul
            var filter = Builders<SpecialOffer>.Filter.Eq(x => x.SpecialOfferID, SpecialOfferID);
            var special = await _specialOfferCollection.Find(filter).FirstOrDefaultAsync();
            
            if (special != null) 
            {
                // Mevcut durumu tersine çevir
                var newStatus = !special.Status;

                // Status alanını güncelle
                var update = Builders<SpecialOffer>.Update.Set(x => x.Status, newStatus);
                await _specialOfferCollection.UpdateOneAsync(filter, update);
            }
        }
    }
}