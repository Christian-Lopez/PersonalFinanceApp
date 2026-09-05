using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalFinanceApp.Domain.Entities;

namespace PersonalFinanceApp.Infrastructure.Data.Configurations;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable("accounts");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(a => a.Currency)
            .HasMaxLength(3)
            .IsRequired();

        // Exact decimal precision for financial amounts (PostgreSQL numeric(18,2))
        builder.Property(a => a.CurrentBalance)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(a => a.Type)
            .HasConversion<string>()
            .HasMaxLength(30)
            .IsRequired();

        // Map the private backing field for DDD collection encapsulation
        builder.Navigation(a => a.Transactions)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}