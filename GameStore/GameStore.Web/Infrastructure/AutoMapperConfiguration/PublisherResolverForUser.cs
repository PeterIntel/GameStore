using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using GameStore.Domain.BusinessObjects;
using GameStore.Web.ViewModels;

namespace GameStore.Web.Infrastructure.AutoMapperConfiguration
{
    public class PublisherResolverForUser : IValueResolver<UserViewModel, User, Publisher>
    {
        public Publisher Resolve(UserViewModel source, User destination, Publisher destMember, ResolutionContext context)
        {
            return source.SelectedPublisher != null ? new Publisher() { Id = source.SelectedPublisher } : null;
        }
    }
}