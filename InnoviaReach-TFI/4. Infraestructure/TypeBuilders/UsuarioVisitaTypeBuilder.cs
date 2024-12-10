using Core.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4._Infraestructure.TypeBuilders
{
    public class UsuarioVisitaTypeBuilder : IEntityTypeConfiguration<UsuarioVisitaModel>
    {
        public void Configure(EntityTypeBuilder<UsuarioVisitaModel> builder)
        {
            builder.HasKey(x => x.Visita_ID);

            builder.HasOne(x => x.Videojuego)
                .WithMany(y => y.usuarioVisitaModels)
                .HasForeignKey(z => z.Videojuego_ID);

            builder.HasOne(x => x.Usuario)
                .WithMany(y => y.usuarioVisitaModels)
                .HasForeignKey(z => z.User_ID);

            builder.Property(x => x.Fecha)
                .HasColumnType("timestamp with time zone")
                .IsRequired();

            builder.ToTable("UsuarioVisita");
        }
    }
}
