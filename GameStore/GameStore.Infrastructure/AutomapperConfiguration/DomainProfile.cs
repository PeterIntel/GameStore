using System;
using System.Collections.Generic;
using AutoMapper;
using GameStore.DataAccess.Mongo.MongoEntities;
using GameStore.DataAccess.MSSQL.Entities;
using GameStore.DataAccess.MSSQL.Entities.Localization;
using GameStore.Domain.BusinessObjects;
using GameStore.Domain.BusinessObjects.LocalizationObjects;
using MongoDB.Bson;
using CultureEntity = GameStore.DataAccess.MSSQL.Entities.Localization.CultureEntity;

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

            CreateMap<OrderEntity, Order>()
                .ForAllMembers(opt => opt.Condition((src, dst, srcMember) => srcMember != null));
            CreateMap<OrderDetailsEntity, OrderDetails>();
            CreateMap<GameInfoEntity, GameInfo>();
            CreateMap<PublisherEntity, Publisher>();

            CreateMap<GameEntity, GameEntity>()
                .ForMember(dst => dst.GameInfo, opt => opt.Ignore())
                .ForMember(dst => dst.Genres, opt => opt.Ignore())
                .ForMember(dst => dst.PlatformTypes, opt => opt.Ignore())
                .ForMember(dst => dst.IsSqlEntity, opt => opt.Ignore())
                .ForMember(dst => dst.Locals, opt => opt.Ignore())
                .ForAllMembers(opt => opt.Condition((src, dst, srcMember) => srcMember != null));
            CreateMap<GameInfoEntity, GameInfoEntity>()
                .ForMember(dst => dst.IsSqlEntity, opt => opt.Ignore())
                .ForAllMembers(opt => opt.Condition((src, dst, srcMember) => srcMember != null)); ;
            CreateMap<CommentEntity, CommentEntity>()
                .ForMember(dst => dst.IsSqlEntity, opt => opt.Ignore())
                .ForAllMembers(opt => opt.Condition((src, dst, srcMember) => srcMember != null)); ;
            CreateMap<GenreEntity, GenreEntity>()
                .ForMember(dst => dst.IsSqlEntity, opt => opt.Ignore())
                .ForMember(dst => dst.Locals, opt => opt.Ignore())
                .ForAllMembers(opt => opt.Condition((src, dst, srcMember) => srcMember != null)); ;
            CreateMap<PlatformTypeEntity, PlatformTypeEntity>()
                .ForMember(dst => dst.IsSqlEntity, opt => opt.Ignore())
                .ForMember(dst => dst.Locals, opt => opt.Ignore())
                .ForAllMembers(opt => opt.Condition((src, dst, srcMember) => srcMember != null)); ;
            CreateMap<OrderEntity, OrderEntity>()
                .ForMember(dst => dst.IsSqlEntity, opt => opt.Ignore())
                .ForAllMembers(opt => opt.Condition((src, dst, srcMember) => srcMember != null)); ;
            CreateMap<OrderDetailsEntity, OrderDetailsEntity>()
                .ForMember(dst => dst.IsSqlEntity, opt => opt.Ignore())
                .ForAllMembers(opt => opt.Condition((src, dst, srcMember) => srcMember != null)); ;
            CreateMap<UserEntity, UserEntity>()
                .ForMember(dst => dst.Roles, opt => opt.Ignore())
                .ForMember(dst => dst.IsSqlEntity, opt => opt.Ignore())
                .ForAllMembers(opt => opt.Condition((src, dst, srcMember) => srcMember != null));
            CreateMap<PublisherEntity, PublisherEntity>()
                .ForMember(dst => dst.IsSqlEntity, opt => opt.Ignore())
                .ForMember(dst => dst.Locals, opt => opt.Ignore())
                .ForAllMembers(opt => opt.Condition((src, dst, srcMember) => srcMember != null));

            CreateMap<Game, GameEntity>()
                .MaxDepth(1)
                .ForMember(dst => dst.Publisher, opt => opt.Ignore());
            CreateMap<Comment, CommentEntity>();
            CreateMap<Genre, GenreEntity>()
               .MaxDepth(1);
            CreateMap<PlatformType, PlatformTypeEntity>()
                .MaxDepth(1);
            CreateMap<User, UserEntity>();
            CreateMap<Role, RoleEntity>();
            CreateMap<Publisher, PublisherEntity>()
                .MaxDepth(1);
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
                .ForMember(dst => dst.Locals, opt => opt.ResolveUsing(
                    m => new List<GameLocal>
                    {
                        new GameLocal() { Id = Guid.NewGuid().ToString(), Description = "", Culture = new Culture() {Code = "en"}}
                    }))
                .ForAllMembers(opt => opt.Condition((src, dst, srcMember) => srcMember != null));

            CreateMap<MongoSupplierEntity, Publisher>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.Description, opt => opt.Ignore())
                .ForMember(dst => dst.CompanyName, opt => opt.MapFrom(src => src.CompanyName))
                .ForMember(dst => dst.Games, opt => opt.MapFrom(src => src.Products))
                .ForMember(dst => dst.Locals, opt => opt.ResolveUsing(
                    m => new List<PublisherLocal>
                    {
                        new PublisherLocal() {Id = Guid.NewGuid().ToString(), Description = "", Culture = new Culture() {Code = "en"}}
                    }));

            CreateMap<MongoCategoryEntity, Genre>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.CategoryName))
                 .ForMember(dst => dst.Games, opt => opt.MapFrom(src => src.Products))
                .ForMember(dst => dst.Locals, opt => opt.ResolveUsing(
                    m => new List<GenreLocal>
                    {
                        new GenreLocal() {Id = Guid.NewGuid().ToString(), Name = m.CategoryName, Culture = new Culture() {Code = "en"}}
                    }));

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
                .ForMember(dst => dst.UnitsInStock, opt => opt.MapFrom(src => (int)src.UnitsInStock))
                .ForMember(dst => dst.Supplier, opt => opt.Ignore())
                .ForMember(dst => dst.UnitPrice, opt => opt.MapFrom(src => (double)src.Price))
                .ForMember(dst => dst.ProductID, opt => opt.MapFrom(src => src.Key));

            CreateMap<Publisher, MongoSupplierEntity>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.CompanyName, opt => opt.MapFrom(src => src.CompanyName))
                .ForMember(dst => dst.HomePage, opt => opt.MapFrom(src => src.HomePage));

            CreateMap<Genre, MongoCategoryEntity>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.CategoryName, opt => opt.MapFrom(src => src.Name));

            CreateMap<GameLocalEntity, GameLocal>().ReverseMap();
            CreateMap<GenreLocalEntity, GenreLocal>().ReverseMap();
            CreateMap<PlatformTypeLocalEntity, PlatformTypeLocal>().ReverseMap();
            CreateMap<PublisherLocalEntity, PublisherLocal>().ReverseMap();
            CreateMap<CultureEntity, Culture>().ReverseMap();
        }
    }
}

