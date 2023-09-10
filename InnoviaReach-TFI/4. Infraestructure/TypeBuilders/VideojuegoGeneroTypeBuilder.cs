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
    public class VideojuegoGeneroTypeBuilder : IEntityTypeConfiguration<VideojuegoGeneroModel>
    {
        public void Configure(EntityTypeBuilder<VideojuegoGeneroModel> builder)
        {
            builder.HasKey(x => x.ID);

            builder.HasOne(x => x.videojuego)
                   .WithMany(y => y.videojuegoGeneroModels)
                   .HasForeignKey(z => z.Videojuego_ID);

            builder.HasOne(x => x.generoModel)
                   .WithMany(y => y.videojuegoGeneroModels)
                   .HasForeignKey(z => z.Genero_ID);

            builder.ToTable("VideojuegoGenero");
        }
    }
}
