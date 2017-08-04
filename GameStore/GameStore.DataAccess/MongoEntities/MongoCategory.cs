using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GameStore.DataAccess.MongoEntities
{
    public class MongoCategory
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { set; get; }
        public int CategoryId { set; get; }
        public string CategoryName { set; get; }
        public string Description { set; get; }
        [BsonIgnore]
        public IEnumerable<MongoProduct> Products { set; get; }

    }
}
