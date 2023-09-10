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
    public class TipoPagoTypeBuilder : IEntityTypeConfiguration<TipoPagoModel>
    {
        public void Configure(EntityTypeBuilder<TipoPagoModel> builder)
        {
            builder.HasKey(x => x.TipoPago_ID);

            builder.Property(x => x.Descripcion).HasColumnType("varchar(50)").IsRequired();

            builder.ToTable("TipoPago");
        }
    }
}
