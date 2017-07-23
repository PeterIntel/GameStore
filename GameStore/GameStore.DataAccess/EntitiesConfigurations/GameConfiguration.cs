using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.DataAccess.Entities;

namespace GameStore.DataAccess.EntitiesConfigurations
{
    class GameConfiguration : EntityTypeConfiguration<GameEntity>
    {
        public GameConfiguration()
        {
            Property(x => x.PublishedDate).HasColumnType("datetime2");
            HasRequired(x => x.GameInfo).WithRequiredPrincipal(x => x.Game);
        }
    }
}
