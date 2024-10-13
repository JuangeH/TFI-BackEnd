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

            builder.Property(x => x.steamid).HasColumnType("varchar(max)").IsRequired();

            builder.Property(x => x.ApiKey).HasColumnType("varchar(max)").IsRequired(false);

            builder.Property(x => x.personaname).HasColumnType("varchar(max)").IsRequired();

            builder.Property(x => x.avatarfull).HasColumnType("varchar(max)").IsRequired();

            builder.Property(x => x.profileurl).HasColumnType("varchar(max)").IsRequired();

            builder.HasOne(x => x.users)
               .WithOne(x => x.SteamAccountModel)
               .HasForeignKey<SteamAccountModel>(x => x.User_ID)
               .IsRequired();

            builder.ToTable("SteamAccount");
        }
    }
}
