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
    public class TagTypeBuilder : IEntityTypeConfiguration<TagModel>
    {
        public void Configure(EntityTypeBuilder<TagModel> builder)
        {
            builder.HasKey(x => x.Tag_ID);

            builder.Property(x => x.Nombre)
                .HasColumnType("text")
                .IsRequired();

            builder.Property(x => x.Slug)
                .HasColumnType("text")
                .IsRequired();

            builder.Property(x => x.TagRawgId)
                .HasColumnType("integer")
                .IsRequired();

            builder.ToTable("Tag");
        }
    }
}
