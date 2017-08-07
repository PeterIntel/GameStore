using System;
using System.Collections.Generic;
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

            CreateMap<Game, GameEntity>().ForMember(dst => dst.Publisher, opt => opt.Ignore());
            CreateMap<Comment, CommentEntity>();
            CreateMap<Genre, GenreEntity>();
            CreateMap<PlatformType, PlatformTypeEntity>();
            CreateMap<Genre, string>().ConstructUsing(src => src.Name ?? string.Empty);
            CreateMap<PlatformType, string>().ConstructUsing(src => src.TypeName ?? string.Empty);
            CreateMap<Publisher, PublisherEntity>().ReverseMap();
            CreateMap<OrderEntity, Order>().ReverseMap();
            CreateMap<OrderDetailsEntity, OrderDetails>().ReverseMap();
            CreateMap<GameInfo, GameInfoEntity>().ReverseMap();

            CreateMap<MongoProduct, Game>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.Key, opt => opt.MapFrom(src => src.ProductID))
                .ForMember(dst => dst.Comments, opt => opt.Ignore())
                .ForMember(dst => dst.Description, opt => opt.Ignore())
                .ForMember(dst => dst.Price, opt => opt.MapFrom(src => (decimal) src.UnitPrice))
                .ForMember(dst => dst.PublishedDate, opt => opt.UseValue(new DateTime()))
                .ForMember(dst => dst.UnitsInStock, opt => opt.MapFrom(src => (short) src.UnitsInStock))
                .ForMember(dst => dst.GameInfo, opt => opt.UseValue(new GameInfo() {UploadDate = DateTime.UtcNow}))
                .ForMember(dst => dst.Publisher, opt => opt.Ignore())
                .ForMember(dst => dst.Genres, opt => opt.Ignore())
                .ForMember(dst => dst.PlatformTypes, opt => opt.Ignore());

            CreateMap<MongoSupplier, Publisher>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.Description, opt => opt.Ignore());

            CreateMap<MongoCategory, Genre>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.CategoryName));

            CreateMap<MongoOrderDetails, OrderDetails>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.GameId, opt => opt.MapFrom(src => src.ProductID))
                .ForMember(dst => dst.OrderId, opt => opt.MapFrom(src => src.OrderID))
                .ForMember(dst => dst.Game, opt => opt.MapFrom(src => src.Product));

            CreateMap<MongoOrder, Order>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.CustomerId, opt => opt.MapFrom(src => src.CustomerID))
                .ForMember(dst => dst.Status, opt => opt.UseValue(CompletionStatus.Complete))
                .ForMember(dst => dst.OrderDetails, opt => opt.MapFrom(src => src.OrderDetails));

        }
    }
}
