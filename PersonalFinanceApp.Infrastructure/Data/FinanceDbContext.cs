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
        // Must run first to configure Identity tables (AspNetUsers, AspNetRoles, etc.)
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FinanceDbContext).Assembly);

        // Tenant query filter
        modelBuilder.Entity<Account>()
            .HasQueryFilter(a => _currentUserService.IsAdmin || a.UserId == _currentUserService.UserId);
    }
}