using System.Collections.Generic;
using GameStore.Domain.BusinessObjects.LocalizationObjects;
using MongoDB.Bson.Serialization.Attributes;

namespace GameStore.DataAccess.Mongo.MongoEntities
{
    [BsonIgnoreExtraElements]
    public class MongoCategoryEntity : BasicMongoEntity
    {
        public int CategoryID { set; get; }

        public string CategoryName { set; get; }
        [BsonIgnore]
        public IEnumerable<GenreLocal> Locals { set; get; }
    }
}
