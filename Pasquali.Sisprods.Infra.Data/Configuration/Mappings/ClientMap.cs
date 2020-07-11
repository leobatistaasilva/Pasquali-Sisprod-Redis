using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Pasquali.Sisprods.Domain.Entities;

namespace Pasquali.Sisprods.Infra.Data.Configuration.Mappings
{
    
    public class ClientMap : EntityTypeConfiguration<Client>
    {
        public ClientMap()
        {
            ToTable("Client");
            Property(x => x.ClientId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Name).HasMaxLength(150).HasColumnType("varchar(150)");
            Property(x => x.CreateDate).IsRequired().HasColumnType("datetime2");
            Property(x => x.LastUpdateDate).IsRequired().HasColumnType("datetime2");
        }

    }
}