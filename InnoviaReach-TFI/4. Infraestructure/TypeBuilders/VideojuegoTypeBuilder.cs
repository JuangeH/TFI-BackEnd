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

            builder.Property(x => x.Recomendaciones).HasColumnType("int");

            builder.Property(x => x.SteamAppid).HasColumnType("int").IsRequired();

            builder.Property(x => x.Header_image).HasColumnType("varchar(max)");

            builder.Property(x => x.Metacritic_score).HasColumnType("int");

            builder.Property(x => x.Metacritic_url).HasColumnType("varchar(max)");

            builder.HasOne(x => x.Plataforma)
                       .WithMany(y => y.videojuegoModels).HasForeignKey(z => z.Plataforma_ID);

            builder.ToTable("Videojuego");
        }
    }
}
