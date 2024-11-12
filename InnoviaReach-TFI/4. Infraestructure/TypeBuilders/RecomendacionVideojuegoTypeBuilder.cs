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
            // Clave primaria
            builder.HasKey(x => x.RecomendacionId);

            // Columnas con tipo de datos y requisitos
            builder.Property(x => x.Similitud).HasColumnType("float").IsRequired();
            builder.Property(x => x.TipoRecomendacion).HasColumnType("nvarchar(MAX)").IsRequired();
            builder.Property(x => x.FechaRecomendacion).HasColumnType("datetime").IsRequired();

            // Definición de relaciones (si es necesario en tu modelo)
            builder.HasOne(x => x.videojuegoReferencia) // Relación con Users
                   .WithMany(y => y.recomendacionVideojuegoRefModels)
                   .HasForeignKey(x => x.VideojuegoReferenciaId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.videojuegoRecomendado) // Relación con Users
                   .WithMany(y => y.recomendacionVideojuegoRecModels)
                   .HasForeignKey(x => x.VideojuegoRecomendadoId)
                   .OnDelete(DeleteBehavior.NoAction);

            // Definición de relaciones (si es necesario en tu modelo)
            builder.HasOne(x => x.usuario) // Relación con Users
                   .WithMany(y => y.recomendacionVideojuegoModels)
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(DeleteBehavior.NoAction);

            // Nombre de la tabla
            builder.ToTable("RecomendacionVideojuego");
        }
    }
}
