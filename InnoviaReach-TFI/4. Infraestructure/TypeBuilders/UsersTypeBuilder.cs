using Core.Domain.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.TypeBuilders
{
    public class UsersTypeBuilder : IEntityTypeConfiguration<Users>
    {
        public void Configure(EntityTypeBuilder<Users> builder)
        {

            builder.Property(x => x.CommunityBanned).HasColumnType("boolean").IsRequired();

            builder.Ignore(x => x.UserPrivileges);

            builder.ToTable("Users");
        }
    }
}
