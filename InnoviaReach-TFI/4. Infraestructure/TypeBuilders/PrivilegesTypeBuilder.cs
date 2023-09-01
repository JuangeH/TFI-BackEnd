using Core.Domain.ApplicationModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4._Infraestructure.TypeBuilders
{
    public class PrivilegesTypeBuilder : IEntityTypeConfiguration<Privileges>
    {
        public void Configure(EntityTypeBuilder<Privileges> builder)
        {
            builder.ToTable("Roles");
        }
    }
}
