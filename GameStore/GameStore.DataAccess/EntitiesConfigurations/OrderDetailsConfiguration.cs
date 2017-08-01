using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using GameStore.DataAccess.Entities;

namespace GameStore.DataAccess.EntitiesConfigurations
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
