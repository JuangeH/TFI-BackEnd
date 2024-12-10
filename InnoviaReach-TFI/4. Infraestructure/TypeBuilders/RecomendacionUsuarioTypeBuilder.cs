using Core.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Domain.ApplicationModels;

namespace _4._Infraestructure.TypeBuilders
{
    public class RecomendacionUsuarioTypeBuilder : IEntityTypeConfiguration<RecomendacionUsuarioModel>
    {
        public void Configure(EntityTypeBuilder<RecomendacionUsuarioModel> builder)
        {
            builder.HasKey(x => x.RecomendacionId);

            builder.Property(x => x.Frecuencia)
                .HasColumnType("integer")
                .IsRequired();

            builder.Property(x => x.TipoRecomendacion)
                .HasColumnType("text")
                .IsRequired();

            builder.Property(x => x.FechaRecomendacion)
                .HasColumnType("timestamp with time zone")
                .IsRequired();

            builder.HasOne(x => x.usuario)
                .WithMany(y => y.recomendacionUsuarioModels)
                .HasForeignKey(z => z.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.videojuego)
                .WithMany(y => y.recomendacionUsuarioModels)
                .HasForeignKey(z => z.VideojuegoRecomendadoId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("RecomendacionUsuario");
        }
    }
}
