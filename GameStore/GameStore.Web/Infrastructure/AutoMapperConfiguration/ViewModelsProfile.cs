using AutoMapper;
using GameStore.Domain.BusinessObjects;
using GameStore.Web.ViewModels;

namespace GameStore.Web.Infrastructure.AutoMapperConfiguration
{
    public class ViewModelsProfile : Profile
    {
        public ViewModelsProfile()
        {
            CreateMap<GameViewModel, Game>();
            CreateMap<CommentViewModel, Comment>();
            CreateMap<Game, GameViewModel>()
                .ForMember(dst => dst.Comments, opt => opt.Ignore());
            CreateMap<Comment, CommentViewModel>();
            CreateMap<GenreViewModel, Genre>();
            CreateMap<Genre, GenreViewModel>();
            CreateMap<PlatformTypeViewModel, PlatformType>();
            CreateMap<PlatformType, PlatformTypeViewModel>();
        }
    }
}