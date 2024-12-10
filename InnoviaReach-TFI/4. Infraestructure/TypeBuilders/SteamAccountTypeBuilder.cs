using Core.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Domain.ApplicationModels;

namespace _4._Infraestructure.TypeBuilders
{
    public class SteamAccountTypeBuilder : IEntityTypeConfiguration<SteamAccountModel>
    {
        public void Configure(EntityTypeBuilder<SteamAccountModel> builder)
        {
            builder.HasKey(x => x.SteamAccount_ID);

            builder.Property(x => x.steamid)
                .HasColumnType("text")
                .IsRequired();

            builder.Property(x => x.ApiKey)
                .HasColumnType("text")
                .IsRequired(false);

            builder.Property(x => x.personaname)
                .HasColumnType("text")
                .IsRequired();

            builder.Property(x => x.avatarfull)
                .HasColumnType("text")
                .IsRequired();

            builder.Property(x => x.profileurl)
                .HasColumnType("text")
                .IsRequired();

            builder.HasOne(x => x.users)
                .WithOne(x => x.SteamAccountModel)
                .HasForeignKey<SteamAccountModel>(x => x.User_ID)
                .IsRequired();

            builder.ToTable("SteamAccount");
        }
    }
}
