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
    public class ForoUsuarioVisitaTypeBuilder : IEntityTypeConfiguration<ForoUsuarioVisitaModel>
    {
        public void Configure(EntityTypeBuilder<ForoUsuarioVisitaModel> builder)
        {
            builder.HasKey(x => new {x.User_ID,x.Foro_ID});

            builder.HasOne(x => x.foro)
                   .WithMany(y => y.foroUsuarioVisitaModels)
                   .HasForeignKey(z => z.Foro_ID);

            builder.HasOne(x => x.usuario)
                   .WithMany(y => y.foroUsuarioVisitaModels)
                   .HasForeignKey(z => z.User_ID);

            builder.ToTable("ForoUsuarioVisita");
        }
    }
}
