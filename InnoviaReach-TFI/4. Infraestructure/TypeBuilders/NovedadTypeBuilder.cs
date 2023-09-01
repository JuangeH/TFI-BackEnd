using Microsoft.AspNetCore.Identity;
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
    public class NovedadTypeBuilder : IEntityTypeConfiguration<NovedadModel>
    {
        public void Configure(EntityTypeBuilder<NovedadModel> builder)
        {
            builder.HasKey(x => x.Novedad_ID);

            builder.Property(x => x.Descripcion).HasColumnType("varchar(50)").IsRequired();

            builder.HasOne(x => x.Videojuego)
                   .WithMany(y => y.novedadModels)
                   .HasForeignKey(z => z.Videojuego_ID);

            builder.ToTable("Novedad");
        }
    }
}
