using Core.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4._Infraestructure.TypeBuilders
{
    public class ForoUsuarioTypeBuilder : IEntityTypeConfiguration<ForoUsuarioModel>
    {
        public void Configure(EntityTypeBuilder<ForoUsuarioModel> builder)
        {
            builder.HasKey(x => x.ID);

            builder.Property(x => x.Tipo).HasColumnType("bit").IsRequired();

            builder.HasOne(x => x.foro)
                   .WithMany(y => y.foroUsuarioModels)
                   .HasForeignKey(z => z.Foro_ID);

            builder.HasOne(x => x.usuario)
                   .WithMany(y => y.foroUsuarioModels)
                   .HasForeignKey(z => z.User_ID);

            builder.ToTable("ForoUsuario");
        }
    }
}
