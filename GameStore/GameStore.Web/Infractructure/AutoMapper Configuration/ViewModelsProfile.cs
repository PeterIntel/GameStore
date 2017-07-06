using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using GameStore.Web.ViewModels;
using GameStore.Domain.BusinessObjects;

namespace GameStore.Web.Infrastructure.AutoMapper
{
    public class ViewModelsProfile : Profile
    {
        public ViewModelsProfile()
        {
            CreateMap<GameViewModel, Game>();
            CreateMap<CommentViewModel, Comment>();
            CreateMap<Game, GameViewModel>();
            CreateMap<Comment, CommentViewModel>();
            CreateMap<GenreViewModel, Genre>();
            CreateMap<Genre, GenreViewModel>();
            CreateMap<PlatformTypeViewModel, PlatformType>();
            CreateMap<PlatformType, PlatformTypeViewModel>();
        }
    }
}