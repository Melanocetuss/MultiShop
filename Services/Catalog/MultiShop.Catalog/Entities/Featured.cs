using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MultiShop.Catalog.Entities
{
    public class Featured
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string FeaturedID { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; }
    }
}