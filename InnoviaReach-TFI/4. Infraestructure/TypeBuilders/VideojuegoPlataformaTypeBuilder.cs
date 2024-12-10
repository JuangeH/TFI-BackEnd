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
    public class VideojuegoPlataformaTypeBuilder : IEntityTypeConfiguration<VideojuegoPlataformaModel>
    {
        public void Configure(EntityTypeBuilder<VideojuegoPlataformaModel> builder)
        {
            builder.HasKey(x => x.ID);

            builder.HasOne(x => x.videojuego)
                .WithMany(y => y.videojuegoPlataformaModels)
                .HasForeignKey(z => z.Videojuego_ID);

            builder.HasOne(x => x.plataformaModel)
                .WithMany(y => y.videojuegoPlataformaModels)
                .HasForeignKey(z => z.Plataforma_ID);

            builder.ToTable("VideojuegoPlataforma");
        }
    }
}
