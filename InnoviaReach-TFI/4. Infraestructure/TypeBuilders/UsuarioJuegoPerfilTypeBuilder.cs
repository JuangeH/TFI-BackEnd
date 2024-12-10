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
    public class UsuarioJuegoPerfilTypeBuilder : IEntityTypeConfiguration<UsuarioJuegoPerfilModel>
    {
        public void Configure(EntityTypeBuilder<UsuarioJuegoPerfilModel> builder)
        {
            builder.HasKey(x => x.Perfil_ID);

            builder.Property(x => x.ClusterID)
                .HasColumnType("integer")
                .IsRequired(false);

            builder.Property(x => x.GameGenresJson)
                .HasColumnType("text")
                .IsRequired(false);

            builder.Property(x => x.GameTagsJson)
                .HasColumnType("text")
                .IsRequired(false);

            builder.Property(x => x.GameHistoryJson)
                .HasColumnType("text")
                .IsRequired(false);

            builder.Property(x => x.TipoRecomendacion)
                .HasColumnType("text")
                .IsRequired(false);

            builder.HasOne(x => x.User)
                .WithMany(y => y.usuarioJuegoPerfilModels)
                .HasForeignKey(z => z.User_ID);

            builder.ToTable("UsuarioJuegoPerfil");
        }
    }
}
