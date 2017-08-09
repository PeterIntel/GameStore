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
    public class MongoSupplierEntity : BasicMongoEntity
    {
        public int SupplierID { set; get; }
        public string CompanyName { set; get; }
        public string HomePage { set; get; }
    }
}
