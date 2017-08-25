using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.XpressionMapper;
using GameStore.DataAccess.MSSQL.Entities;
using GameStore.Domain.BusinessObjects;
using GameStore.DataAccess.Mongo.MongoEntities;

namespace GameStore.Infrastructure.AutomapperConfiguration
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<GameEntity, Game>()
                .MaxDepth(1);

            CreateMap<CommentEntity, Comment>()
                .MaxDepth(1);

            CreateMap<GenreEntity, Genre>()
                .MaxDepth(1);

            CreateMap<PlatformTypeEntity, PlatformType>()
                .MaxDepth(1);

            CreateMap<UserEntity, User>()
                .MaxDepth(1);

            CreateMap<RoleEntity, Role>()
                .ForMember(dst => dst.RoleEnum, opt => opt.MapFrom(src => src.Role))
                .MaxDepth(1);

            CreateMap<OrderEntity, Order>();
            CreateMap<OrderDetailsEntity, OrderDetails>();
            CreateMap<GameInfoEntity, GameInfo>();

            CreateMap<GameEntity, GameEntity>();
            CreateMap<GameInfoEntity, GameInfoEntity>();
            CreateMap<CommentEntity, CommentEntity>();
            CreateMap<GenreEntity, GenreEntity>();
            CreateMap<PlatformTypeEntity, PlatformTypeEntity>();
            CreateMap<OrderEntity, OrderEntity>();
            CreateMap<OrderDetailsEntity, OrderDetailsEntity>();
            CreateMap<UserEntity, UserEntity>()
                .ForMember(dst => dst.Roles, opt => opt.Ignore());

            CreateMap<Game, GameEntity>().ForMember(dst => dst.Publisher, opt => opt.Ignore());
            CreateMap<Comment, CommentEntity>();
            CreateMap<Genre, GenreEntity>();
            CreateMap<PlatformType, PlatformTypeEntity>();
            CreateMap<User, UserEntity>()
                .ForMember(dst => dst.Roles, opt => opt.Ignore());
            CreateMap<Role, RoleEntity>();
            CreateMap<Publisher, PublisherEntity>().ReverseMap();
            CreateMap<Order, OrderEntity>();
            CreateMap<OrderDetails, OrderDetailsEntity>();
            CreateMap<GameInfo, GameInfoEntity>();

            CreateMap<MongoProductEntity, Game>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.Key, opt => opt.MapFrom(src => src.ProductID))
                .ForMember(dst => dst.Description, opt => opt.Ignore())
                .ForMember(dst => dst.Price, opt => opt.MapFrom(src => (decimal)src.UnitPrice))
                .ForMember(dst => dst.UnitsInStock, opt => opt.MapFrom(src => (short)src.UnitsInStock))
                .ForMember(dst => dst.Publisher, opt => opt.MapFrom(src => src.Supplier))
                .ForMember(dst => dst.Genres, opt => opt.MapFrom(src => src.Categories ?? new List<MongoCategoryEntity>()))
                .ForMember(dst => dst.PlatformTypes, opt => opt.Ignore())
                .ForAllMembers(opt => opt.Condition((src, dst, srcMember) => srcMember != null));

            CreateMap<MongoSupplierEntity, Publisher>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.Description, opt => opt.Ignore())
                .ForMember(dst => dst.CompanyName, opt => opt.MapFrom(src => src.CompanyName));

            CreateMap<MongoCategoryEntity, Genre>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.CategoryName));

            CreateMap<MongoOrderDetailsEntity, OrderDetails>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.GameId, opt => opt.MapFrom(src => src.ProductID))
                .ForMember(dst => dst.OrderId, opt => opt.MapFrom(src => src.OrderID))
                .ForMember(dst => dst.Game, opt => opt.MapFrom(src => src.Product))
                .ForMember(dst => dst.Price, opt => opt.MapFrom(src => src.UnitPrice))
                .ForMember(dst => dst.Order, opt => opt.MapFrom(src => src.Order));

            CreateMap<MongoOrderEntity, Order>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.CustomerId, opt => opt.MapFrom(src => src.CustomerID))
                .ForMember(dst => dst.OrderDetails, opt => opt.MapFrom(src => src.OrderDetails))
                .ForAllMembers(opt => opt.Condition((src, dst, srcMember) => srcMember != null));


            CreateMap<Game, MongoProductEntity>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.Categories, opt => opt.Ignore())
                .ForMember(dst => dst.ProductName, opt => opt.Ignore())
                .ForMember(dst => dst.UnitsInStock, opt => opt.MapFrom(src => (int)src.UnitsInStock))
                .ForMember(dst => dst.Supplier, opt => opt.Ignore())
                .ForMember(dst => dst.ProductName, opt => opt.MapFrom(src => (double)src.Price))
                .ForMember(dst => dst.ProductID, opt => opt.MapFrom(src => src.Key));

            CreateMap<Publisher, MongoSupplierEntity>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.CompanyName, opt => opt.MapFrom(src => src.CompanyName))
                .ForMember(dst => dst.HomePage, opt => opt.MapFrom(src => src.HomePage));

            CreateMap<Genre, MongoCategoryEntity>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.CategoryName, opt => opt.MapFrom(src => src.Name));
        }
    }
}

