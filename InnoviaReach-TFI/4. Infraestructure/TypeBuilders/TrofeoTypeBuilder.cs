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
    public class TrofeoTypeBuilder : IEntityTypeConfiguration<TrofeoModel>
    {
        public void Configure(EntityTypeBuilder<TrofeoModel> builder)
        {
            builder.HasKey(x => x.Trofeo_ID);

            builder.Property(x => x.Descripcion).HasColumnType("varchar(50)").IsRequired();

            builder.HasOne(x => x.usuario)
                   .WithMany(y => y.trofeosModel)
                   .HasForeignKey(z => z.User_ID);

            builder.HasOne(x => x.videojuego)
                   .WithMany(y => y.trofeosModel)
                   .HasForeignKey(z => z.Videojuego_ID);

            builder.ToTable("Trofeo");
        }
    }
}
