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
    public class ForoUsuarioFavoritoTypeBuilder : IEntityTypeConfiguration<ForoUsuarioFavoritoModel>
    {
        public void Configure(EntityTypeBuilder<ForoUsuarioFavoritoModel> builder)
        {
            builder.HasKey(x => new { x.User_ID, x.Foro_ID });

            builder.HasOne(x => x.foro)
                   .WithMany(y => y.foroUsuarioFavoritoModels)
                   .HasForeignKey(z => z.Foro_ID);

            builder.HasOne(x => x.usuario)
                   .WithMany(y => y.foroUsuarioFavoritoModels)
                   .HasForeignKey(z => z.User_ID);

            builder.ToTable("ForoUsuarioFavorito");
        }
    }
}
