using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.XpressionMapper;
using GameStore.DataAccess.Entities;
using GameStore.Domain.BusinessObjects;

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

            CreateMap<Game, GameEntity>();
            CreateMap<Game, GameEntity>();
            CreateMap<Comment, CommentEntity>();
            CreateMap<Genre, GenreEntity>();
            CreateMap<PlatformType, PlatformTypeEntity>();
            CreateMap<Genre, string>().ConstructUsing(src => src.Name ?? string.Empty);
            CreateMap<PlatformType, string>().ConstructUsing(src => src.TypeName ?? string.Empty);
            CreateMap<Publisher, PublisherEntity>().ReverseMap();
            CreateMap<OrderEntity, Order>().ReverseMap();
            CreateMap<OrderDetailsEntity, OrderDetails>().ReverseMap();
            CreateMap<GameInfo, GameInfoEntity>().ReverseMap();
        }
    }
}
