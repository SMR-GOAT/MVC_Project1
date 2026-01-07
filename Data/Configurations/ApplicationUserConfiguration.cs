using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVCCourse.Models;

namespace MVCCourse.Data.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUserModel>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserModel> builder)
        {
            // 1. قيد الاسم الأول
            builder.Property(u => u.FirstName)
                .IsRequired()             // إلزامي (NOT NULL)
                .HasMaxLength(50)         // أقصى حد 50 حرف (كافي جداً للأسماء)
                .HasColumnType("varchar(50)"); // تحديد النوع في Postgres ليكون دقيقاً

            // 2. قيد الاسم الأخير
            builder.Property(u => u.LastName)
                .IsRequired()             // إلزامي
                .HasMaxLength(50)         // أقصى حد 50 حرف
                .HasColumnType("varchar(50)");

            builder.Property(u => u.Salary)
                .HasColumnType("decimal(18,2)") // تحديد النوع بدقة (18 خانة، 2 بعد الفاصلة)
                .HasDefaultValue(0);            // القيمة الافتراضية 0

            builder.Property(u => u.Address)
                .HasMaxLength(200);              // أقصى حد 200 حرف
        }
    }
}