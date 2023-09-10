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
    public class SuscripcionTypeBuilder : IEntityTypeConfiguration<SuscripcionModel>
    {
        public void Configure(EntityTypeBuilder<SuscripcionModel> builder)
        {
            builder.HasKey(x => x.Suscripcion_ID);

            builder.Property(x => x.Descripcion).HasColumnType("varchar(50)").IsRequired();

            builder.ToTable("Suscripcion");
        }
    }
}
