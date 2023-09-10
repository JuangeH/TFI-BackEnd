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
    public class VideojuegoEstiloTypeBuilder : IEntityTypeConfiguration<VideojuegoEstiloModel>
    {
        public void Configure(EntityTypeBuilder<VideojuegoEstiloModel> builder)
        {
            builder.HasKey(x => x.ID);
            builder.HasOne(x => x.videojuego)
                   .WithMany(y => y.videojuegoEstiloModels)
                   .HasForeignKey(z => z.Videojuego_ID);

            builder.HasOne(x => x.estiloModel)
                   .WithMany(y => y.videojuegoEstiloModels)
                   .HasForeignKey(z => z.Estilo_ID);

            builder.ToTable("VideojuegoEstilo");
        }
    }
}
