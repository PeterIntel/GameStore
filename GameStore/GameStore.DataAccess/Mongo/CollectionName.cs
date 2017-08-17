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
                    {typeof(MongoCategoryEntity), "categories"},
                    {typeof(MongoOrderEntity), "orders"},
                    {typeof(MongoOrderDetailsEntity), "order-details"},
                    {typeof(MongoProductEntity), "products"},
                    {typeof(MongoShipperEntity), "shippers"},
                    {typeof(MongoSupplierEntity), "suppliers"}
                };
                return _collectionNames;
            }
        }
    }
}
