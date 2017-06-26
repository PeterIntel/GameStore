using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.contracts.DomainModels;
using GameDataAccessLayer.DAL.Repositories;

namespace GameDataAccessLayer.DAL.UnitOfWork
{
    interface IUnitOfWork
    {
        GenericDataRepository<Comment> Comments { get; }
        GenericDataRepository<Game> Games { get; }
        GenericDataRepository<Genre> Genres { get; }
        GenericDataRepository<PlatformType> PlatformTypes { get; }
    }
}
