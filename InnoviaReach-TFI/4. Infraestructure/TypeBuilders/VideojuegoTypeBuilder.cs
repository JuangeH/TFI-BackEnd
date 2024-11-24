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

            builder.Property(x => x.Nombre).HasColumnType("varchar(max)").IsRequired();

            builder.Property(x => x.AppRawgId).HasColumnType("int");

            builder.Property(x => x.Slug).HasColumnType("varchar(max)");

            builder.Property(x => x.Nombre).HasColumnType("varchar(max)");

            builder.Property(x => x.FechaSalida).HasColumnType("datetime");

            builder.Property(x => x.CaracteristicasVector).HasColumnType("varchar(max)").IsRequired(false);

            builder.Property(x => x.ClusterID).HasColumnType("int").IsRequired(false);

            builder.Property(x => x.Imagen).HasColumnType("varchar(max)").IsRequired(false);

            builder.Property(x => x.Rating).HasColumnType("float");

            builder.Property(x => x.Metacritic).HasColumnType("int").IsRequired(false);

            builder.Property(x => x.Descripcion).HasColumnType("varchar(max)").IsRequired(false);

            builder.ToTable("Videojuego");
        }
    }
}
