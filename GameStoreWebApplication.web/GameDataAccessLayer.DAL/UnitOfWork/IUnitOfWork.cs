using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.contracts.DomainModels;
using GameDataAccessLayer.DAL.Repositories;

namespace GameDataAccessLayer.DAL.UnitOfWork
{
    public interface IUnitOfWork
    {
        IGenericDataRepository<Comment> Comments { get; }
        IGenericDataRepository<Game> Games { get; }
        IGenericDataRepository<Genre> Genres { get; }
        IGenericDataRepository<PlatformType> PlatformTypes { get; }
        void Save();
    }
}
