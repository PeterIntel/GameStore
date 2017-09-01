using System;
using System.Configuration;
using MongoDB.Driver;

namespace GameStore.Logging.Loggers
{
    public class MongoLogger<TDomain> : IMongoLogger<TDomain> where TDomain : class

    {
        private readonly IMongoClient _client;
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<LogEntity<TDomain>> _collection;

        public MongoLogger()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["GameStoreMongoContext"].ConnectionString;
            _client = new MongoClient(connectionString);
            _database = _client.GetDatabase(MongoUrl.Create(connectionString).DatabaseName);
            _collection = _database.GetCollection<LogEntity<TDomain>>("LoggingEntities");
        }

        public void Write(Operation operation, TDomain currentEntity, TDomain updatedEntity)
        {
            _collection.InsertOne(new LogEntity<TDomain>(currentEntity, updatedEntity) { Operation = operation, EntityType = typeof(TDomain).Name, DataTimeLogging = DateTime.UtcNow });
        }

        public void Write(Operation operation, TDomain currentEntity)
        {
            _collection.InsertOne(new LogEntity<TDomain>(currentEntity) { Operation = operation, EntityType = typeof(TDomain).Name, DataTimeLogging = DateTime.UtcNow });
        }
    }
}
