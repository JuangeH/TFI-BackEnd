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
    public class ComentarioTypeBuilder : IEntityTypeConfiguration<ComentarioModel>
    {
        public void Configure(EntityTypeBuilder<ComentarioModel> builder)
        {
            builder.HasKey(x => x.Comentario_ID);

            builder.Property(x => x.Contenido)
                .HasColumnType("nvarchar(max)")
                .IsRequired();

            builder.Property(x => x.FechaCreacion)
                .HasColumnType("datetime")
                .HasDefaultValueSql("GETDATE()")
                .IsRequired();

            builder.HasOne(x => x.foro)
                .WithMany(y => y.comentarioModels)
                .HasForeignKey(z => z.Foro_ID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.usuario)
                .WithMany(y => y.comentarioModels)
                .HasForeignKey(z => z.User_ID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.comentarioPadre)
                .WithMany(y => y.comentarioModels)
                .HasForeignKey(z => z.ComentarioPadre_ID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("Comentario");
        }
    }
}
