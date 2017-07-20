using System.Data.Entity;
using GameStore.DataAccess.Entities;
using GameStore.DataAccess.EntitiesConfigurations;

namespace GameStore.DataAccess.Context
{
    public class GamesContext : DbContext
    {
        public GamesContext() : base("GamesContext")
        {
            this.Configuration.LazyLoadingEnabled = true;
        }

        public virtual DbSet<CommentEntity> Comments { set; get; }
        public virtual DbSet<GameEntity> Games { set; get; }
        public virtual DbSet<GenreEntity> Genres { set; get; }
        public virtual DbSet<PlatformTypeEntity> PlatformTypes { set; get; }
        public virtual DbSet<PublisherEntity> Publishers { set; get; }
        public virtual DbSet<OrderDetailsEntity> OrderDetails { set; get; }
        public virtual DbSet<OrderEntity> Orders { set; get; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new PublisherConfiguration());
            modelBuilder.Configurations.Add(new OrderDetailsConfiguration());
            modelBuilder.Entity<OrderEntity>().Property(x => x.OrderDate).HasColumnType("datetime2");
            base.OnModelCreating(modelBuilder);
        }
    }
}
