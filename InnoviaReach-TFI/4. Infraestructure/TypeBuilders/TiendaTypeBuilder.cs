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
    public class TiendaTypeBuilder : IEntityTypeConfiguration<TiendaModel>
    {
        public void Configure(EntityTypeBuilder<TiendaModel> builder)
        {
            builder.HasKey(x => x.Tienda_ID);
            // Definir propiedades
            builder.Property(x => x.Nombre).HasColumnType("varchar(max)").IsRequired();
            builder.Property(x => x.Slug).HasColumnType("varchar(max)").IsRequired();
            builder.Property(x => x.StoreRawgId).HasColumnType("int").IsRequired();
            builder.Property(x => x.Dominio).HasColumnType("varchar(max)").IsRequired();

            builder.ToTable("Tienda");
        }
    }
}
