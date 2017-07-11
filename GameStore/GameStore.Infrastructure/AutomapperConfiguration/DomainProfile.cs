using System;
using System.Collections.Generic;
using System.Linq;
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
                .ForMember(dst => dst.Id, opt =>
                {
                    //opt.SetMappingOrder(1);
                })
                .ForMember(dst => dst.Key, opt =>
                {
                    //opt.SetMappingOrder(2);
                })
                .ForMember(dst => dst.Description, opt =>
                {
                    //opt.SetMappingOrder(3);
                })
                .ForMember(dst => dst.Comments, opt =>
                {
                    //opt.SetMappingOrder(4);
                })
                .ForMember(dst => dst.Genres, opt =>
                {
                    //opt.MapFrom(src => src.Genres);
                    //opt.SetMappingOrder(1);
                })
                .ForMember(dst => dst.PlatformTypes, opt =>
                {
                    opt.Ignore();
                    //opt.SetMappingOrder(6);
                });

            CreateMap<CommentEntity, Comment>();
                //.ForMember(dst => dst.Id, opt => { opt.SetMappingOrder(1); })
                //.ForMember(dst => dst.Name, opt => { opt.SetMappingOrder(2); })
                //.ForMember(dst => dst.Body, opt => { opt.SetMappingOrder(3); })
                //.ForMember(dst => dst.Comments, opt => { opt.SetMappingOrder(4); })
                //.ForMember(dst => dst.Game, opt => { opt.SetMappingOrder(5); })
                //.ForMember(dst => dst.GameId, opt => { opt.SetMappingOrder(6); })
                //.ForMember(dst => dst.ParentCommentId, opt => { opt.SetMappingOrder(7); });

            CreateMap<GenreEntity, Genre>()
                .ForMember(dst => dst.Id, opt => { opt.SetMappingOrder(1); })
                .ForMember(dst =>  dst.Name , opt => { opt.SetMappingOrder(2); })
                .ForMember(dst => dst.ParentGenreId, opt => { opt.SetMappingOrder(3); })
                .ForMember(dst => dst.Games, opt => { opt.SetMappingOrder(4); })
                .ForMember(dst => dst.Genres, opt => { opt.SetMappingOrder(5); });

            CreateMap<PlatformTypeEntity, PlatformType>()
                .ForMember(dst => dst.Id, opt => { opt.SetMappingOrder(1); })
                .ForMember(dst => dst.TypeName, opt => { opt.SetMappingOrder(2); })
                .ForMember(dst => dst.Games, opt => { opt.SetMappingOrder(3); }).MaxDepth(1);

            CreateMap<Game, GameEntity>();
            CreateMap<Game, GameEntity>();
            CreateMap<Comment, CommentEntity>();
            CreateMap<Genre, GenreEntity>();
            CreateMap<PlatformType, PlatformTypeEntity>();
        }
    }
}
