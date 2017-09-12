using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GameStore.DataAccess.Mongo.DataProviders;
using GameStore.DataAccess.Mongo.MongoEntities;
using GameStore.Domain.BusinessObjects;
using MongoDB.Driver;

namespace GameStore.DataAccess.Mongo.MongoRepositories
{
    public class ReadOnlyPublisherRepository : ReadOnlyGenericRepository<MongoSupplierEntity, Publisher>
    {
        public ReadOnlyPublisherRepository(GamesMongoContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override IEnumerable<Publisher> Get()
        {
            IQueryable<MongoSupplierEntity> queryToEntity = Collection.AsQueryable();
            queryToEntity = queryToEntity.GetNestedEntities();
            var result = Mapper.Map<IQueryable<MongoSupplierEntity>, IEnumerable<Publisher>>(queryToEntity);

            return result;
        }

        public override IEnumerable<Publisher> Get(Expression<Func<Publisher, bool>> filterToDomain)
        {
            IQueryable<MongoSupplierEntity> queryToEntity = Collection.AsQueryable();
            queryToEntity = queryToEntity.GetNestedEntities();
            var queryToDomain = Mapper.Map<IQueryable<MongoSupplierEntity>, IEnumerable<Publisher>>(queryToEntity);
            if (filterToDomain != null)
            {
                var predicate = filterToDomain.Compile();
                queryToDomain = queryToDomain.Where(predicate);
            }

            return queryToDomain;
        }

        public override Publisher First(Expression<Func<Publisher, bool>> filter)
        {
            IQueryable<MongoSupplierEntity> queryToEntity = Collection.AsQueryable();
            queryToEntity = queryToEntity.GetNestedEntities();
            var queryToDomain = Mapper.Map<IQueryable<MongoSupplierEntity>, IEnumerable<Publisher>>(queryToEntity);
            if (filter != null)
            {
                var predicate = filter.Compile();
                queryToDomain = queryToDomain.Where(predicate);
            }

            var game = queryToDomain.FirstOrDefault();

            return game;
        }
    }
}
