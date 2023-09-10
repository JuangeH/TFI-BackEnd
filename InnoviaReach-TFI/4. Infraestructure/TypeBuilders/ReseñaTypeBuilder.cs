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
    public class ReseñaTypeBuilder : IEntityTypeConfiguration<ReseñaModel>
    {
        public void Configure(EntityTypeBuilder<ReseñaModel> builder)
        {
            builder.HasKey(x => x.Reseña_ID);

            builder.Property(x => x.Descripcion).HasColumnType("varchar(50)").IsRequired();

            builder.HasOne(x => x.Videojuego)
                  .WithMany(y => y.reseñaModel)
                  .HasForeignKey(z => z.Videojuego_ID);

            builder.ToTable("Reseña");
        }
    }
}
