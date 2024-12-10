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
    public class VideojuegoTiendaTypeBuilder : IEntityTypeConfiguration<VideojuegoTiendaModel>
    {
        public void Configure(EntityTypeBuilder<VideojuegoTiendaModel> builder)
        {
            builder.HasKey(x => x.ID);

            builder.HasOne(x => x.videojuego)
                .WithMany(y => y.videojuegoTiendaModels)
                .HasForeignKey(z => z.Videojuego_ID);

            builder.HasOne(x => x.tiendaModel)
                .WithMany(y => y.videojuegoTiendaModels)
                .HasForeignKey(z => z.Tienda_ID);

            builder.ToTable("VideojuegoTienda");
        }
    }
}
