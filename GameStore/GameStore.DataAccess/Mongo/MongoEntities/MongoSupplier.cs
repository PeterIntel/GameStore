using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.DataAccess.Mongo.MongoEntities
{
    [BsonIgnoreExtraElements]
    public class MongoSupplier
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { set; get; }
        public int SupplierID { set; get; }
        public string CompanyName { set; get; }
        [BsonIgnore]
        public string HomePage { set; get; }
    }
}
