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
        IGenericDataRepository<CommentEntity, Comment> CommentRepository { get; }
        IGenericDataRepository<GameEntity, Game> GameRepository { get; }
        IGenericDataRepository<GenreEntity, Genre> GenreRepository { get; }
        IGenericDataRepository<PlatformTypeEntity, PlatformType> PlatformTypeRepository { get; }
        void Save();
    }
}
