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
    public class MedioDePagoTypeBuilder : IEntityTypeConfiguration<MedioDePagoModel>
    {
        public void Configure(EntityTypeBuilder<MedioDePagoModel> builder)
        {
            builder.HasKey(x => x.Medio_ID);

            builder.Property(x => x.Cod_Postal).HasColumnType("int").IsRequired();
            builder.Property(x => x.Cod_Verificador).HasColumnType("int").IsRequired();
            builder.Property(x => x.Direccion).HasColumnType("varchar(50)").IsRequired();
            builder.Property(x => x.Estado).HasColumnType("bit").IsRequired();
            builder.Property(x => x.Numero).HasColumnType("int").IsRequired();

            builder.HasOne(x => x.usuario)
                   .WithMany(y => y.medioDePagoModels)
                   .HasForeignKey(z => z.User_ID);

            builder.HasOne(x => x.tipoPago)
                   .WithMany(y => y._mediosPagoModel)
                   .HasForeignKey(z => z.TipoPago_ID);

            builder.ToTable("MedioDePago");
        }
    }
}
