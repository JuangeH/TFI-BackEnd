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
    public class LogTableTypeBuilder : IEntityTypeConfiguration<LogTableModel>
    {
        public void Configure(EntityTypeBuilder<LogTableModel> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Message).HasColumnType("nvarchar(MAX)");

            builder.Property(x => x.Level).HasColumnType("nvarchar(MAX)");

            builder.Property(x => x.Timestamp).HasColumnType("datetime");

            builder.Property(x => x.Exception).HasColumnType("nvarchar(MAX)");

            builder.Property(x => x.LogEvent).HasColumnType("nvarchar(MAX)");

            builder.Property(x => x.ReferenceNumber).HasColumnType("int");

            builder.Property(x => x.ReferenceType).HasColumnType("varchar(50)");

            builder.ToTable("LogTable");
        }
    
    }
}
