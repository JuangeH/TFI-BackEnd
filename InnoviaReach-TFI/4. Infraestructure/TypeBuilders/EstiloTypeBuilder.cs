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
    public class EstiloTypeBuilder : IEntityTypeConfiguration<EstiloModel>
    {
        public void Configure(EntityTypeBuilder<EstiloModel> builder)
        {
            builder.HasKey(x => x.Estilo_ID);

            builder.Property(x => x.Descripcion).HasColumnType("varchar(50)").IsRequired();

            builder.ToTable("Estilo");
        }
    }
}
