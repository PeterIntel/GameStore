using System;
using System.Collections.Generic;
using GameStore.DataAccess.Mongo.MongoEntities;

namespace GameStore.DataAccess.Mongo
{
    public static class CollectionName
    {
        public static IDictionary<Type, string> CollectionNames
        {
            get
            {
                var _collectionNames = new Dictionary<Type, string>()
                {
                    {typeof(MongoCategory), "categories"},
                    {typeof(MongoOrder), "orders"},
                    {typeof(MongoOrderDetails), "order-details"},
                    {typeof(MongoProduct), "products"},
                    {typeof(MongoShipper), "shippers"},
                    {typeof(MongoSupplier), "suppliers"}
                };
                return _collectionNames;
            }
        }
    }
}
