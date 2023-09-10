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
    public class ValoracionTypeBuilder : IEntityTypeConfiguration<ValoracionModel>
    {
        public void Configure(EntityTypeBuilder<ValoracionModel> builder)
        {
            builder.HasKey(x => x.Valoracion_ID);

            builder.Property(x => x.Puntuacion).HasColumnType("int").IsRequired();

            builder.HasOne(x => x.Videojuego)
                  .WithMany(y => y.valoracionModel)
                  .HasForeignKey(z => z.Videojuego_ID);

            builder.ToTable("Valoracion");
        }
    }
}
