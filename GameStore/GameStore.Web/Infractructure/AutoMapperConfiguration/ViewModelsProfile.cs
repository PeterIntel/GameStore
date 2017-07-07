using AutoMapper;
using GameStore.Domain.BusinessObjects;
using GameStore.Web.ViewModels;

namespace GameStore.Web.Infractructure.AutoMapperConfiguration
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