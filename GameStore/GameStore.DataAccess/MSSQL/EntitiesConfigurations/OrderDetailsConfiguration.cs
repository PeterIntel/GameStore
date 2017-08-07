using System.Data.Entity.ModelConfiguration;
using GameStore.DataAccess.MSSQL.Entities;

namespace GameStore.DataAccess.MSSQL.EntitiesConfigurations
{
    class OrderDetailsConfiguration : EntityTypeConfiguration<OrderDetailsEntity>
    {
        public OrderDetailsConfiguration()
        {
            Property(x => x.Quantity).HasColumnType("smallint");
            Property(x => x.Discount).HasColumnType("float");
        }
    }
}
