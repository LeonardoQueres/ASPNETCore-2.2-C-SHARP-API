using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(p => p.Email)
                .IsUnique();

            builder.Property(p => p.Email)
                .IsRequired()                
                .HasMaxLength(255);

            builder.Property(p => p.CreateAt);
            builder.Property(p => p.UpdateAt);
        }
    }
}
