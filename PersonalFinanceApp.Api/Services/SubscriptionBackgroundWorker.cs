using MediatR;
using PersonalFinanceApp.Application.Features.RecurringTransactions.Commands;

namespace PersonalFinanceApp.Api.Services;

public class SubscriptionBackgroundWorker : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<SubscriptionBackgroundWorker> _logger;

    public SubscriptionBackgroundWorker(IServiceProvider serviceProvider, ILogger<SubscriptionBackgroundWorker> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Subscription Background Worker starting.");
        
        // Initial catch-up on startup (wait 5 seconds to ensure app is fully booted)
        await Task.Delay(5000, stoppingToken);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                _logger.LogInformation("Checking for due recurring transactions...");
                
                using var scope = _serviceProvider.CreateScope();
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                var processedCount = await mediator.Send(new ProcessRecurringTransactionsCommand(), stoppingToken);
                
                if (processedCount > 0)
                {
                    _logger.LogInformation("Successfully processed {Count} recurring transactions.", processedCount);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing recurring transactions.");
            }

            // Sleep for 24 hours
            await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
        }
    }
}
