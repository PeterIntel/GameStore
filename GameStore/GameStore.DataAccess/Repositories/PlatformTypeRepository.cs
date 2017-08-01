﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GameStore.DataAccess.Context;
using GameStore.Domain.BusinessObjects;
using GameStore.DataAccess.Entities;

namespace GameStore.DataAccess.Repositories
{
    public class PlatformTypeRepository : GenericDataRepository<PlatformTypeEntity, PlatformType>, IPlatformTypeRepository
    {
        public PlatformTypeRepository(GamesContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public IEnumerable<PlatformTypeEntity> GetPlatformTypes(IList<string> platformtypes)
        {
            var platforms = from i in platformtypes
                from platform in _dbSet
                where i == platform.TypeName
                select platform;
            return platforms;
        }
    }
}
