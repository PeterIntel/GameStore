using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.DataAccess.CustomMongoSerializers;

namespace GameStore.DataAccess.MongoEntities
{
    public class MongoOrder
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { set; get; }
        public int OrderId { set; get; }
        [BsonSerializer(typeof(DateTimeCustomSerializer))]
        public DateTime OrderDate { set; get; }
        [BsonIgnore]
        public IEnumerable<MongoOrderDetails> OrderDetails { set; get; }
    }
}
