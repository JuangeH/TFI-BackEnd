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
    public class GeneroTypeBuilder : IEntityTypeConfiguration<GeneroModel>
    {
        public void Configure(EntityTypeBuilder<GeneroModel> builder)
        {
            builder.HasKey(x => x.Genero_ID);

            builder.Property(x => x.Nombre)
                .HasColumnType("text")
                .IsRequired();

            builder.Property(x => x.Slug)
                .HasColumnType("text")
                .IsRequired();

            builder.Property(x => x.GenreRawgID)
                .HasColumnType("integer")
                .IsRequired();

            builder.ToTable("Genero");
        }
    }
}
