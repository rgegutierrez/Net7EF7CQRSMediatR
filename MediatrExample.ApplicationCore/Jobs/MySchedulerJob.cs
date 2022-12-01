using MediatR;
using MediatrExample.ApplicationCore.Features.MateriaPrimaFeatures.Commands;
using MediatrExample.ApplicationCore.Features.MateriaPrimaFeatures.Queries;
using MediatrExample.ApplicationCore.Features.Products.Commands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BackgroundJob.Cron.Jobs;

public class MySchedulerJob : CronBackgroundJob
{
    private readonly ILogger<MySchedulerJob> _log;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly IMediator _mediator;

    public MySchedulerJob(CronSettings<MySchedulerJob> settings, ILogger<MySchedulerJob> log, IServiceScopeFactory serviceScopeFactory)
        : base(settings.CronExpression, settings.TimeZone)
    {
        _log = log;
        _serviceScopeFactory = serviceScopeFactory;
        var scope = _serviceScopeFactory.CreateScope();
        _mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
    }

    protected override Task DoWork(CancellationToken stoppingToken)
    {
        _log.LogInformation("Inicio... at {0}", DateTime.UtcNow);

        SyncMateriaPrimaCommand command = new();
        var aux = _mediator.Send(command);
        _log.LogInformation("Termino... at {0}", DateTime.UtcNow);

        return Task.CompletedTask;
    }
}
