using System.Drawing.Drawing2D;
using AutoMapper;
using GameStore.Domain.BusinessObjects;
using GameStore.Web.ViewModels;

namespace GameStore.Web.Infrastructure.AutoMapperConfiguration
{
    public class ViewModelsProfile : Profile
    {
        public ViewModelsProfile()
        {
            CreateMap<GameViewModel, Game>()
                .ForMember(dst => dst.Publisher, opt => opt.ResolveUsing<PublisherResolver>())
                .ForAllMembers(opt => opt.Condition((src, dst, srcMember) => srcMember != null));
            CreateMap<Game, GameViewModel>()
                .ForMember(dst => dst.Comments, opt => opt.Ignore())
                .ForMember(dst => dst.SelectedPublisher, opt => opt.MapFrom(src => src.Publisher.CompanyName));
            CreateMap<CommentViewModel, Comment>();
            CreateMap<Comment, CommentViewModel>()
                .ForMember(dst => dst.GameKey, opt => opt.Ignore());
            CreateMap<Genre, GenreViewModel>().ReverseMap();
            CreateMap<PlatformTypeViewModel, PlatformType>();
            CreateMap<PlatformType, PlatformTypeViewModel>();
            CreateMap<Publisher, PublisherViewModel>().ReverseMap();
            CreateMap<Order, OrderViewModel>().ReverseMap();
            CreateMap<OrderDetails, OrderDetailsViewModel>().ReverseMap();
            CreateMap<FilterCriteria, FilterCriteriaViewModel>().ReverseMap();
            CreateMap<GameInfo, GameInfoViewModel>().ReverseMap();
            CreateMap<FilterOrders, FilterOrdersViewModel>().ReverseMap();
            CreateMap<UserViewModel, User>()
                .ForMember(dst => dst.Orders, opt => opt.Ignore())
                .ForAllMembers(opt => opt.Condition((src, dst, srcMember) => srcMember != null));
            CreateMap<User, UserViewModel>()
                .ForMember(dst => dst.ConfirmPassword, opt => opt.MapFrom(src => src.Password));
            CreateMap<Role, RoleViewModel>()
                .ForMember(dst => dst.Role, opt => opt.MapFrom(src => src.RoleEnum));
            CreateMap<RoleViewModel, Role>()
                .ForMember(dst => dst.RoleEnum, opt => opt.MapFrom(src => src.Role));
        }
    }
}