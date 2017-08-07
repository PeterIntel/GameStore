using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GameStore.DataAccess.Mongo.MongoEntities
{
    [BsonIgnoreExtraElements]
    public class MongoCategory
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { set; get; }
        public int CategoryID { set; get; }
        public string CategoryName { set; get; }

    }
}
