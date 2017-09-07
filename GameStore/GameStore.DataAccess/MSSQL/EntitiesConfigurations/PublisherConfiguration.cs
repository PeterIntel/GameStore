using System.Data.Entity.ModelConfiguration;
using GameStore.DataAccess.MSSQL.Entities;

namespace GameStore.DataAccess.MSSQL.EntitiesConfigurations
{
    class PublisherConfiguration : EntityTypeConfiguration<PublisherEntity>
    {
        public PublisherConfiguration()
        {
            Property(p => p.CompanyName).HasColumnType("nvarchar").HasMaxLength(40);
            Property(p => p.HomePage).HasColumnType("ntext");
        }
    }
}
