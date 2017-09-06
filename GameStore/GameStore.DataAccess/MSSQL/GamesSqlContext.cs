using System.Data.Entity;
using GameStore.DataAccess.MSSQL.Entities;
using GameStore.DataAccess.MSSQL.Entities.Localization;
using GameStore.DataAccess.MSSQL.EntitiesConfigurations;
using GameStore.DataAccess.MSSQL.Migrations;

namespace GameStore.DataAccess.MSSQL
{
    public class GamesSqlContext : DbContext
    {
        public GamesSqlContext() : base("GamesContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<GamesSqlContext, Configuration>("GamesContext"));
            //Database.SetInitializer(new Configuration());
            this.Configuration.LazyLoadingEnabled = true;
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
        public DbSet<CultureEntity> Cultures { get; set; }
        public DbSet<GameLocalEntity> GameLocal { set; get; }
        public DbSet<GenreLocalEntity> GenreLocal { set; get; }
        public DbSet<PlatformTypeLocalEntity> PlatformLocal { set; get; }
        public DbSet<PublisherLocalEntity> PublisherLocal { set; get; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new PublisherConfiguration());
            modelBuilder.Configurations.Add(new OrderDetailsConfiguration());
            modelBuilder.Configurations.Add(new GameConfiguration());
            modelBuilder.Entity<OrderEntity>().Property(x => x.OrderDate).HasColumnType("datetime2");
            modelBuilder.Entity<GameInfoEntity>().Property(x => x.UploadDate).HasColumnType("datetime2");
            modelBuilder.Entity<OrderEntity>().HasRequired(x => x.Customer).WithMany(x => x.Orders).HasForeignKey(fk => fk.CustomerId);
            modelBuilder.Entity<PublisherLocalEntity>().Property(p => p.Description).HasColumnType("ntext");
            base.OnModelCreating(modelBuilder);
        }
    }
}
