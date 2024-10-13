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
    public class AdquisicionTypeBuilder : IEntityTypeConfiguration<AdquisicionModel>
    {
        public void Configure(EntityTypeBuilder<AdquisicionModel> builder)
        {
            builder.HasKey(x => x.Adquisicion_id);

            builder.HasOne(x => x.usuario)
                  .WithMany(y => y.adquicionesModel)
                  .HasForeignKey(z => z.User_ID);

            builder.HasOne(x => x.videojuego)
                   .WithMany(y => y.adquisicionesModel)
                   .HasForeignKey(z => z.Videojuego_ID);
            //.OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.TiempoJuego).HasColumnType("int").IsRequired();

            builder.Property(x => x.TiempoJuegoReciente).HasColumnType("int");

            builder.Property(x => x.CantidadLogros).HasColumnType("int");

            builder.ToTable("Adquisicion");
        }
    }
}
