using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.XpressionMapper;
using GameStore.DataAccess.Entities;
using GameStore.Domain.Business_objects;

namespace GameStore.Infrastructure.Automapper_configuration
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<GameEntity, Game>();
            CreateMap<CommentEntity, Comment>();
            CreateMap<Game, GameEntity>();
            CreateMap<Comment, CommentEntity>();

        }
    }
}
