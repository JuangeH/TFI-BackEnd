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
    public class RecomendacionVideojuegoTypeBuilder : IEntityTypeConfiguration<RecomendacionVideojuegoModel>
    {
        public void Configure(EntityTypeBuilder<RecomendacionVideojuegoModel> builder)
        {
            builder.HasKey(x => x.RecomendacionId);

            builder.Property(x => x.Similitud)
                .HasColumnType("double precision")
                .IsRequired();

            builder.Property(x => x.TipoRecomendacion)
                .HasColumnType("text")
                .IsRequired();

            builder.Property(x => x.FechaRecomendacion)
                .HasColumnType("timestamp with time zone")
                .IsRequired();

            builder.HasOne(x => x.videojuegoReferencia)
                .WithMany(y => y.recomendacionVideojuegoRefModels)
                .HasForeignKey(x => x.VideojuegoReferenciaId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.videojuegoRecomendado)
                .WithMany(y => y.recomendacionVideojuegoRecModels)
                .HasForeignKey(x => x.VideojuegoRecomendadoId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.usuario)
                .WithMany(y => y.recomendacionVideojuegoModels)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("RecomendacionVideojuego");
        }
    }
}
