using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.DataAccess.Entities;

namespace GameStore.DataAccess.EntitiesConfigurations
{
    class PublisherConfiguration : EntityTypeConfiguration<PublisherEntity>
    {
        public PublisherConfiguration()
        {
            Property(p => p.CompanyName).HasColumnType("nvarchar").HasMaxLength(40);
            Property(p => p.Description).HasColumnType("ntext");
            Property(p => p.HomePage).HasColumnType("ntext");
        }
    }
}
