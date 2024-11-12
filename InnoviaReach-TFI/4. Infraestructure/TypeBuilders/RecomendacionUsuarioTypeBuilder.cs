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
            // Clave primaria
            builder.HasKey(x => x.RecomendacionId);

            // Columnas con tipo de datos y requisitos
            builder.Property(x => x.Frecuencia).HasColumnType("int").IsRequired();
            builder.Property(x => x.TipoRecomendacion).HasColumnType("nvarchar(MAX)").IsRequired();
            builder.Property(x => x.FechaRecomendacion).HasColumnType("datetime").IsRequired();

            // Definición de relaciones (si es necesario en tu modelo)
            builder.HasOne<Users>() // Relación con Users
                   .WithMany()
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne<VideojuegoModel>() // Relación con VideojuegoModel para VideojuegoRecomendadoId
                   .WithMany()
                   .HasForeignKey(x => x.VideojuegoRecomendadoId)
                   .OnDelete(DeleteBehavior.NoAction);

            // Nombre de la tabla
            builder.ToTable("RecomendacionUsuario");
        }
    }
}
