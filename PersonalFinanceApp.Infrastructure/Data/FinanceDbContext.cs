using PersonalFinanceApp.Application.Common.Interfaces;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using PersonalFinanceApp.Domain.Entities;
using PersonalFinanceApp.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace PersonalFinanceApp.Infrastructure.Data;

public class FinanceDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
{
    private readonly ICurrentUserService _currentUserService;

    public FinanceDbContext(
        DbContextOptions<FinanceDbContext> options,
        ICurrentUserService currentUserService) : base(options)
    {
        _currentUserService = currentUserService;
    }
    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<Transaction> Transactions => Set<Transaction>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Tag> Tags => Set<Tag>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FinanceDbContext).Assembly);

        // Tenant query filters
        modelBuilder.Entity<Account>()
            .HasQueryFilter(a => _currentUserService.IsAdmin || a.UserId == _currentUserService.UserId);
            
        modelBuilder.Entity<Transaction>()
            .HasQueryFilter(t => _currentUserService.IsAdmin || t.UserId == _currentUserService.UserId);

        modelBuilder.Entity<Category>()
            .HasQueryFilter(c => _currentUserService.IsAdmin || c.UserId == _currentUserService.UserId);

        modelBuilder.Entity<Tag>()
            .HasQueryFilter(t => _currentUserService.IsAdmin || t.UserId == _currentUserService.UserId);

        // Seed Admin User
        var adminId = "00000000-0000-0000-0000-000000000001";
        var adminUser = new ApplicationUser
        {
            Id = adminId,
            UserName = "admin@example.com",
            NormalizedUserName = "ADMIN@EXAMPLE.COM",
            Email = "admin@example.com",
            NormalizedEmail = "ADMIN@EXAMPLE.COM",
            EmailConfirmed = true,
            FirstName = "System",
            LastName = "Admin",
            SecurityStamp = "A70E28B0-8CAE-4DEE-BE39-C79178E89086",
            IsActive = true,
            CreatedAtUtc = DateTime.Parse("2025-01-01T00:00:00Z").ToUniversalTime()
        };
        
        adminUser.PasswordHash = "AQAAAAIAAYagAAAAEJqMnnZQkjVdnMLhJYvR83GrrwZW12KF6ikdfHShxEkC4g0xzvPJqXrvmAWk8xgilg==";
        
        modelBuilder.Entity<ApplicationUser>().HasData(adminUser);
    }
}