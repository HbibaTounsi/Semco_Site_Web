using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SiteWeb.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteWeb.Infrastructure.Data.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(p => p.Slug)
                .IsRequired()
                .HasMaxLength(180);

            builder.HasIndex(p => p.Slug)
                .IsUnique();

            builder.Property(p => p.Reference)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(p => p.Reference);

            builder.Property(p => p.ShortDescription)
                .IsRequired()
                .HasMaxLength(300);

            builder.Property(p => p.Description)
                .IsRequired();

            builder.Property(p => p.ImageUrl)
                .HasMaxLength(500);

            builder.Property(p => p.Dimensions)
                .HasMaxLength(100);

            builder.Property(p => p.Color)
                .HasMaxLength(100);

            builder.Property(p => p.Material)
                .HasMaxLength(150);

            builder.Property(p => p.IsFeatured)
                .HasDefaultValue(false);

            builder.Property(p => p.IsActive)
                .HasDefaultValue(true);

            builder.Property(p => p.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.HasIndex(p => p.CategoryId);
        }
    }
}
