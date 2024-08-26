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
    public class PuntuacionTypeBuilders : IEntityTypeConfiguration<PuntuacionModel>
    {
        public void Configure(EntityTypeBuilder<PuntuacionModel> builder)
        {
            builder.HasKey(x => x.Puntuacion_ID);

            builder.Property(x => x.Puntaje).HasColumnType("int").IsRequired();

            builder.HasOne(x => x.comentario)
                   .WithMany(y => y.puntuacionModels)
                   .HasForeignKey(z => z.Comentario_ID);

            builder.HasOne(x => x.usuario)
                .WithMany(y => y.puntuacionModels)
                .HasForeignKey(z => z.User_ID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("Puntuacion");
        }
    }
}
