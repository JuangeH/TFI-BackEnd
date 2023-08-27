using Core.Domain.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.TypeBuilders
{
    public class RefreshTokenTypeBuilder : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.HasKey(x => x.Id);
            
            builder.Property(x => x.Id)
                   .ValueGeneratedOnAdd();
            
            builder.Property(x => x.Token)
                   .IsRequired()
                   .IsUnicode(false)
                   .HasMaxLength(100);

            builder.Property(x => x.Expires)
                   .IsRequired();
            
            builder.HasOne(x => x.Users)
                   .WithMany(x => x.UserRefreshTokens)
                   .HasForeignKey(x => x.UserId);

            builder.ToTable("RefreshTokens");
        }
    }
}
