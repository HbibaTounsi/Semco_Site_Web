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
    public class ContactMessageConfiguration : IEntityTypeConfiguration<ContactMessage>
    {
        public void Configure(EntityTypeBuilder<ContactMessage> builder)
        {
            builder.ToTable("ContactMessages");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.FullName)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(m => m.Email)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(m => m.PhoneNumber)
                .HasMaxLength(50);

            builder.Property(m => m.CompanyName)
                .HasMaxLength(150);

            builder.Property(m => m.Subject)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(m => m.Message)
                .IsRequired();

            builder.Property(m => m.ProductReference)
                .HasMaxLength(50);

            builder.Property(m => m.ProductName)
                .HasMaxLength(150);

            builder.Property(m => m.SentAt)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(m => m.IsRead)
                .HasDefaultValue(false);
        }
    }

}
