using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalFinanceApp.Domain.Entities;

namespace PersonalFinanceApp.Infrastructure.Data.Configurations;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable("transactions");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Amount)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(t => t.Type)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(t => t.Description)
            .HasMaxLength(255)
            .IsRequired();

        // Relationships
        builder.HasOne(t => t.Account)
            .WithMany(a => a.Transactions)
            .HasForeignKey(t => t.AccountId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(t => t.Category)
            .WithMany()
            .HasForeignKey(t => t.CategoryId)
            .OnDelete(DeleteBehavior.SetNull);

        // Many-to-many relationship with Tag via auto-generated join table
        builder.HasMany(t => t.Tags)
            .WithMany(tg => tg.Transactions)
            .UsingEntity(j => j.ToTable("transaction_tags"));

        builder.Navigation(t => t.Tags)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}