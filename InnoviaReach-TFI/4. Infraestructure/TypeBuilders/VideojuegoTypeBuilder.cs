﻿using Core.Domain.ApplicationModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Domain.Models;

namespace _4._Infraestructure.TypeBuilders
{
    public class VideojuegoTypeBuilder : IEntityTypeConfiguration<VideojuegoModel>
    {
        public void Configure(EntityTypeBuilder<VideojuegoModel> builder)
        {
            builder.HasKey(x => x.Videojuego_ID);

            builder.Property(x => x.Nombre).HasColumnType("varchar(50)").IsRequired();

            builder.HasOne(x => x.Plataforma)
                   .WithMany(y => y.videojuegoModels).HasForeignKey(z => z.Plataforma_ID);

            builder.ToTable("Videojuego");
        }
    }
}
