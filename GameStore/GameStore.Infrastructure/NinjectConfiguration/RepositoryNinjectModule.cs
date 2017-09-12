using GameStore.DataAccess.Decorators;
using GameStore.DataAccess.Interfaces;
using GameStore.DataAccess.Mongo.MongoEntities;
using GameStore.DataAccess.Mongo.MongoRepositories;
using GameStore.DataAccess.MSSQL.Entities;
using GameStore.DataAccess.MSSQL.Repositories;
using GameStore.Domain.BusinessObjects;
using GameStore.Services.ServicesImplementation;
using Ninject.Modules;

namespace GameStore.Infrastructure.NinjectConfiguration
{
    public class RepositoryNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IGameRepository>().To<GameDecoratorRepository>().WhenInjectedInto<GameService>();
            Bind<IGameRepository>().To<GameDecoratorRepository>().WhenInjectedInto<OrderService>();
            Bind<IDecoratorOrderRepository>().To<DecoratorOrderDecoratorRepository>();
            Bind<IOrderRepository>().To<OrderRepository>().WhenInjectedInto<DecoratorOrderDecoratorRepository>();
            Bind<IGenericDataRepository<GenreEntity, Genre>>().To<GenericDecoratorRepository<GenreEntity, MongoCategoryEntity, Genre>>().WhenInjectedInto<GameService>();
            Bind<IGenericDataRepository<GenreEntity, Genre>>().To<GenericDecoratorRepository<GenreEntity, MongoCategoryEntity, Genre>>().WhenInjectedInto<GenreService>();
            Bind<IGenericDataRepository<PublisherEntity, Publisher>>().To<GenericDecoratorRepository<PublisherEntity, MongoSupplierEntity, Publisher>>().WhenInjectedInto<GameService>();
            Bind<IGenericDataRepository<PublisherEntity, Publisher>>().To<GenericDecoratorRepository<PublisherEntity, MongoSupplierEntity, Publisher>>().WhenInjectedInto<PublisherService>();
            Bind<IGenericDataRepository<OrderEntity, Order>>().To<GenericDecoratorRepository<OrderEntity, MongoOrderEntity, Order>>().WhenInjectedInto<OrderService>();
            Bind<IGenericDataRepository<OrderDetailsEntity, OrderDetails>>().To<GenericDecoratorRepository<OrderDetailsEntity, MongoOrderDetailsEntity, OrderDetails>>().WhenInjectedInto<OrderService>();
            Bind<IGenericDataRepository<PublisherEntity, Publisher>>().To<GenericDecoratorRepository<PublisherEntity, MongoSupplierEntity, Publisher>>().WhenInjectedInto<AccountService>();

            Bind(typeof(IGenericDataRepository<,>)).To(typeof(GenericDataRepository<,>));
            Bind<IGenericDataRepository<GameEntity, Game>>().To<GameRepository>();
            Bind<IGenericDataRepository<UserEntity, User>>().To<UserRepository>();
            Bind<IGenreRepository>().To<GenreRepository>();
            Bind<IGenericDataRepository<GenreEntity, Genre>>().To<GenreRepository>();
            Bind<IPlatformTypeRepository>().To<PlatformTypeRepository>();
            Bind<IRoleRepository>().To<RoleRepository>();
            Bind<ICultureRepository>().To<CultureRepository>();
            Bind<IGenericDataRepository<PublisherEntity, Publisher>>().To<PublisherRepository>();

            Bind(typeof(IReadOnlyGenericRepository<,>)).To(typeof(ReadOnlyGenericRepository<,>));
            Bind(typeof(IReadOnlyGenericRepository<MongoCategoryEntity, Genre>)).To<ReadOnlyGenreRepository>();
            Bind(typeof(IReadOnlyGenericRepository<MongoSupplierEntity, Publisher>)).To<ReadOnlyPublisherRepository>();
            Bind<IReadOnlyGenericRepository<MongoOrderEntity, Order>>().To<ReadOnlyOrderRepository>();
            Bind<IReadOnlyGenericRepository<MongoProductEntity, Game>>().To<ReadOnlyGameRepository>();
        }
    }
}
