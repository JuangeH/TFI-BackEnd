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
    public class VideojuegoTagTypeBuilder : IEntityTypeConfiguration<VideojuegoTagModel>
    {
        public void Configure(EntityTypeBuilder<VideojuegoTagModel> builder)
        {
            builder.HasKey(x => x.ID);

            builder.HasOne(x => x.videojuego)
                .WithMany(y => y.videojuegoTagModels)
                .HasForeignKey(z => z.Videojuego_ID);

            builder.HasOne(x => x.tagModel)
                .WithMany(y => y.videojuegoTagModels)
                .HasForeignKey(z => z.Tag_ID);

            builder.ToTable("VideojuegoTag");
        }
    }
}
