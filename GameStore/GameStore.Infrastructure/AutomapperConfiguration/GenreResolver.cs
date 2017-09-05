using System;
using System.Collections.Generic;
using AutoMapper;
using GameStore.DataAccess.Mongo.MongoEntities;
using GameStore.Domain.BusinessObjects;
using GameStore.Domain.BusinessObjects.LocalizationObjects;

namespace GameStore.Infrastructure.AutomapperConfiguration
{
    public class GenreResolver : IValueResolver<MongoCategoryEntity, Genre, IEnumerable<GenreLocal>>
    {
        public IEnumerable<GenreLocal> Resolve(MongoCategoryEntity source, Genre destination, IEnumerable<GenreLocal> destMember, ResolutionContext context)
        {
            return new List<GenreLocal>() { new GenreLocal() { Name = source.CategoryName, Culture = new Culture() {Code = "en"} } };
        }
    }
}