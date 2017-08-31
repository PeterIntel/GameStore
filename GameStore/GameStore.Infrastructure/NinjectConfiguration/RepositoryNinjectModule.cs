using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Modules;
using GameStore.DataAccess.MSSQL.Repositories;
using GameStore.Domain.BusinessObjects;
using GameStore.DataAccess;
using GameStore.DataAccess.Decorators;
using GameStore.DataAccess.Interfaces;
using GameStore.DataAccess.Mongo.MongoEntities;
using GameStore.DataAccess.Mongo.MongoRepositories;
using GameStore.DataAccess.MSSQL.Entities;
using GameStore.Services.ServicesImplementation;

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
            Bind<IPlatformTypeRepository>().To<PlatformTypeRepository>();
            Bind<IRoleRepository>().To<RoleRepository>();

            Bind(typeof(IReadOnlyGenericRepository<,>)).To(typeof(ReadOnlyGenericRepository<,>));
            Bind<IReadOnlyGenericRepository<MongoOrderEntity, Order>>().To<ReadOnlyOrderRepository>();
            Bind<IReadOnlyGenericRepository<MongoProductEntity, Game>>().To<ReadOnlyGameRepository>();
        }
    }
}
