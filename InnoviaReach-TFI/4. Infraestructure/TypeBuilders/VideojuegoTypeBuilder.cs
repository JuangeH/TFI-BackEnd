using Core.Domain.ApplicationModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Domain.Models;

namespace _4._Infraestructure.TypeBuilders
{
    public class VideojuegoTypeBuilder : IEntityTypeConfiguration<VideojuegoModel>
    {
        public void Configure(EntityTypeBuilder<VideojuegoModel> builder)
        {
            builder.HasKey(x => x.Videojuego_ID);

            builder.Property(x => x.Nombre)
                .HasColumnType("text")
                .IsRequired();

            builder.Property(x => x.AppRawgId)
                .HasColumnType("integer");

            builder.Property(x => x.Slug)
                .HasColumnType("text");

            builder.Property(x => x.FechaSalida)
                .HasColumnType("timestamp with time zone");

            builder.Property(x => x.CaracteristicasVector)
                .HasColumnType("text")
                .IsRequired(false);

            builder.Property(x => x.ClusterID)
                .HasColumnType("integer")
                .IsRequired(false);

            builder.Property(x => x.Imagen)
                .HasColumnType("text")
                .IsRequired(false);

            builder.Property(x => x.Rating)
                .HasColumnType("double precision");

            builder.Property(x => x.Metacritic)
                .HasColumnType("integer")
                .IsRequired(false);

            builder.Property(x => x.Descripcion)
                .HasColumnType("text")
                .IsRequired(false);

            builder.ToTable("Videojuego");
        }
    }
}
