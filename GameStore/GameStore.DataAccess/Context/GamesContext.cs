using System.Data.Entity;
using GameStore.DataAccess.Entities;

namespace GameStore.DataAccess.Context
{
    public class GamesContext : DbContext
    {
        public GamesContext() : base("GamesContext")
        {
            //this.Configuration.LazyLoadingEnabled = false;
        }

        public virtual DbSet<CommentEntity> Comments { set; get; }
        public virtual DbSet<GameEntity> Games { set; get; }
        public virtual DbSet<GenreEntity> Genres { set; get; }
        public virtual DbSet<PlatformTypeEntity> PlatformTypes { set; get; }
    }
}
