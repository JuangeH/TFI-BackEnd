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
    public class RatingTypeBuilder : IEntityTypeConfiguration<RatingModel>
    {
        public void Configure(EntityTypeBuilder<RatingModel> builder)
        {
            builder.HasKey(x => x.Rating_ID);

            builder.Property(x => x.Titulo)
                .HasColumnType("text")
                .IsRequired();

            builder.Property(x => x.CantidadVotos)
                .HasColumnType("integer")
                .IsRequired();

            builder.Property(x => x.Porcentaje)
                .HasColumnType("double precision")
                .IsRequired();

            builder.HasOne(x => x.Videojuego)
                .WithMany(y => y.ratingModels)
                .HasForeignKey(z => z.Videojuego_ID);

            builder.ToTable("Rating");
        }
    }
}
