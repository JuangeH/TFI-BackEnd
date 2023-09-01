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
            //builder.HasKey(x => x.Id);

            //builder.Property(x => x.Active)
            //       .IsRequired();

            builder.Ignore(x => x.UserPrivileges);
            //       .WithMany(x => x.PrivilegesUsers);

            builder.ToTable("Users");
        }
    }
}
