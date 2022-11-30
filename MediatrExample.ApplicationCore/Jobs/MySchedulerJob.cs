using MediatR;
using MediatrExample.ApplicationCore.Features.MateriaPrimaFeatures.Queries;
using MediatrExample.ApplicationCore.Features.Products.Commands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BackgroundJob.Cron.Jobs;

public class MySchedulerJob : CronBackgroundJob
{
    private readonly ILogger<MySchedulerJob> _log;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public MySchedulerJob(CronSettings<MySchedulerJob> settings, ILogger<MySchedulerJob> log, IServiceScopeFactory serviceScopeFactory)
        : base(settings.CronExpression, settings.TimeZone)
    {
        _log = log;
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override Task DoWork(CancellationToken stoppingToken)
    {
        _log.LogInformation("Running... at {0}", DateTime.UtcNow);

        using var scope = _serviceScopeFactory.CreateScope();

        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

        CreateProductCommand command = new();
        var ahora = DateTime.Now.ToString();
        command.Description = $"Producto desde Job {ahora}";
        command.Price = 555;
        var aux = mediator.Send(command);

        return Task.CompletedTask;
    }
}
