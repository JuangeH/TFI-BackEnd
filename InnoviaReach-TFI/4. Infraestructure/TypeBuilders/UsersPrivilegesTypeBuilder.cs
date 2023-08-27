using Core.Domain.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;


namespace Infrastructure.Data.TypeBuilders
{
    public class UsersPrivilegesTypeBuilder : IEntityTypeConfiguration<UsersPrivileges>
    {
        public void Configure(EntityTypeBuilder<UsersPrivileges> builder)
        {
            builder.Ignore(x => x.PrivilegesUsers);
        }
    }
}
