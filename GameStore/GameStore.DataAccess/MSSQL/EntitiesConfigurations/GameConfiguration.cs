using System.Data.Entity.ModelConfiguration;
using GameStore.DataAccess.MSSQL.Entities;

namespace GameStore.DataAccess.MSSQL.EntitiesConfigurations
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
