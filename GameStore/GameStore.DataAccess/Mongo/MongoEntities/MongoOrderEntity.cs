﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.DataAccess.Mongo.CustomMongoSerializers;

namespace GameStore.DataAccess.Mongo.MongoEntities
{
    [BsonIgnoreExtraElements]
    public class MongoOrderEntity : BasicMongoEntity
    {
        public string CustomerID { set; get; }
        [BsonSerializer(typeof(StringSerializer))]
        public string OrderID { set; get; }
        [BsonSerializer(typeof(DateTimeCustomSerializer))]
        public DateTime? OrderDate { set; get; }
        [BsonIgnore]
        public IEnumerable<MongoOrderDetailsEntity> OrderDetails { set; get; }
    }
}
