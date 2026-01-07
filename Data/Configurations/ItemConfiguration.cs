using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVCCourse.Models;

namespace MVCCourse.Data.Configurations;

public class ItemConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.HasKey(i => i.Id);

        builder.Property(i => i.Name)
               .IsRequired()
               .HasMaxLength(100);

        // ضروري جداً لتحديد دقة الـ decimal في قاعدة البيانات
        builder.Property(i => i.Price)
               .HasColumnType("decimal(18,2)");

        builder.Property(i => i.Description)
               .HasMaxLength(500);
    }
}