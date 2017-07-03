using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.DataAccess.Entities;
using GameStore.Domain.Business_objects;
using GameStore.DataAccess.Repositories;

namespace GameStore.DataAccess.UnitOfWork
{
    public interface IUnitOfWork
    {
        IGenericDataRepository<CommentEntity, Comment> Comments { get; }
        IGenericDataRepository<GameEntity, Game> Games { get; }
        IGenericDataRepository<GenreEntity, Genre> Genres { get; }
        IGenericDataRepository<PlatformTypeEntity, PlatformType> PlatformTypes { get; }
        void Save();
    }
}
