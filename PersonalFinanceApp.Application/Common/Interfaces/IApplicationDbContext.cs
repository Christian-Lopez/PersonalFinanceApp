using Microsoft.EntityFrameworkCore;
using PersonalFinanceApp.Domain.Entities;

namespace PersonalFinanceApp.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Account> Accounts { get; }
    DbSet<Transaction> Transactions { get; }
    DbSet<Category> Categories { get; }
    DbSet<Tag> Tags { get; }
    DbSet<RecurringTransaction> RecurringTransactions { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}