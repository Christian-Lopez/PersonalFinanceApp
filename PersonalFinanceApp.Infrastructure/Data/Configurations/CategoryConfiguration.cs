using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalFinanceApp.Domain.Entities;

namespace PersonalFinanceApp.Infrastructure.Data.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("categories");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .HasMaxLength(80)
            .IsRequired();

        builder.Property(c => c.ColorHex)
            .HasMaxLength(7)
            .IsRequired();

        builder.Property(c => c.Icon)
            .HasMaxLength(50);

        // Self-referencing parent/child relationship
        builder.HasOne(c => c.ParentCategory)
            .WithMany(c => c.SubCategories)
            .HasForeignKey(c => c.ParentCategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Navigation(c => c.SubCategories)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}