using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.DataAccess.MongoEntities
{
    public class MongoSupplier
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { set; get; }
        public string CompanyName { set; get; }
        [BsonIgnoreIfNull]
        public string Description { set; get; }
        public string HomePage { set; get; }
    }
}
