using System.Data.Entity;
using GameStore.DataAccess.MSSQL.Entities;
using GameStore.DataAccess.MSSQL.EntitiesConfigurations;
using GameStore.DataAccess.MSSQL.Migrations;

namespace GameStore.DataAccess.MSSQL
{
    public class GamesSqlContext : DbContext
    {
        public GamesSqlContext() : base("GamesContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<GamesSqlContext, Configuration>("GamesContext"));
            this.Configuration.LazyLoadingEnabled = false;
        }

        public virtual DbSet<CommentEntity> Comments { set; get; }
        public virtual DbSet<GameEntity> Games { set; get; }
        public virtual DbSet<GenreEntity> Genres { set; get; }
        public virtual DbSet<PlatformTypeEntity> PlatformTypes { set; get; }
        public virtual DbSet<PublisherEntity> Publishers { set; get; }
        public virtual DbSet<OrderDetailsEntity> OrderDetails { set; get; }
        public virtual DbSet<OrderEntity> Orders { set; get; }
        public virtual DbSet<GameInfoEntity> GamesInfo { set; get; }
        public virtual DbSet<UserEntity> Users { set; get; }
        public virtual DbSet<RoleEntity> Roles { set; get; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new PublisherConfiguration());
            modelBuilder.Configurations.Add(new OrderDetailsConfiguration());
            modelBuilder.Configurations.Add(new GameConfiguration());
            modelBuilder.Entity<OrderEntity>().Property(x => x.OrderDate).HasColumnType("datetime2");
            modelBuilder.Entity<GameInfoEntity>().Property(x => x.UploadDate).HasColumnType("datetime2");
            //modelBuilder.Entity<OrderEntity>().Property(x => x.)
            base.OnModelCreating(modelBuilder);
        }
    }
}
