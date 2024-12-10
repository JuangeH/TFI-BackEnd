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
    public class UsuarioBaneadoTypeBuilder : IEntityTypeConfiguration<UsuarioBaneadoModel>
    {
        public void Configure(EntityTypeBuilder<UsuarioBaneadoModel> builder)
        {
            builder.HasKey(x => x.Baneo_ID);

            builder.Property(x => x.Motivo)
                .HasColumnType("text")
                .IsRequired();

            builder.Property(x => x.FechaDeBaneo)
                .HasColumnType("timestamp with time zone")
                .IsRequired();

            builder.HasOne(x => x.usuarioBaneado)
                .WithMany(y => y.usuarioBaneadoModels)
                .HasForeignKey(x => x.User_ID)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.usuarioAdmin)
                .WithMany(y => y.usuarioBaneadoAdminModels)
                .HasForeignKey(x => x.UserAdmin_ID)
                .OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("UsuarioBaneado");
        }
    }
}
