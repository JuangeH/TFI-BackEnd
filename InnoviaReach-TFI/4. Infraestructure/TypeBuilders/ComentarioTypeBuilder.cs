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

            builder.Property(x => x.Descripcion).HasColumnType("varchar(50)").IsRequired();

            builder.HasOne(x => x.foro)
                   .WithMany(y => y.comentarioModels)
                   .HasForeignKey(z => z.Foro_ID);

            builder.HasOne(x => x.usuario)
                   .WithMany(y => y.comentarioModels)
                   .HasForeignKey(z => z.User_ID);

            builder.ToTable("Comentario");
        }
    }
}
