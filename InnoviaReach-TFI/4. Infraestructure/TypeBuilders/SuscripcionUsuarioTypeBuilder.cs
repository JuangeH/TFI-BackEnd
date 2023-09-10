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
    public class SuscripcionUsuarioTypeBuilder : IEntityTypeConfiguration<SuscripcionUsuarioModel>
    {
        public void Configure(EntityTypeBuilder<SuscripcionUsuarioModel> builder)
        {
            builder.HasKey(x => x.ID);

            builder.HasOne(x => x.Suscripcion)
                   .WithMany(y => y.suscripcionUsrdModels)
                   .HasForeignKey(z => z.Suscripcion_ID);

            builder.HasOne(x => x.Usuario)
                   .WithOne(y => y.suscripcionUsuarioModel)
                   .HasForeignKey<SuscripcionUsuarioModel>(z => z.User_ID);

            builder.ToTable("SuscripcionUsuario");
        }
    }
}
