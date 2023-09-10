using Core.Domain.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.TypeBuilders
{
    public class UsersTypeBuilder : IEntityTypeConfiguration<Users>
    {
        public void Configure(EntityTypeBuilder<Users> builder)
        {
            //builder.HasKey(x => x.Id);

            //builder.Property(x => x.Active)
            //       .IsRequired();

            builder.Ignore(x => x.UserPrivileges);
            //       .WithMany(x => x.PrivilegesUsers);

            builder.Property(x => x.Estilo_preferido).HasColumnType("varchar(50)").IsRequired();

            builder.Property(x => x.Genero_preferido).HasColumnType("varchar(50)").IsRequired();

            builder.Property(x => x.Descuentos).HasColumnType("bit").IsRequired();

            builder.Property(x => x.Actualizaciones).HasColumnType("bit").IsRequired();

            builder.ToTable("Users");
        }
    }
}
