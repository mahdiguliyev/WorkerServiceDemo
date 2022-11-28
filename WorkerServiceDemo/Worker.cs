using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;

namespace WorkerServiceDemo
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            Log.Information("Service started at: {time}", DateTimeOffset.Now);
            //_logger.LogInformation("Service started at: {time}", DateTimeOffset.Now);
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            Log.Information("Service stopped at: {time}", DateTimeOffset.Now);
            //_logger.LogInformation("Service stopped at: {time}", DateTimeOffset.Now);
            return base.StopAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var period = Convert.ToInt32(ConfigurationManager.AppSettings["WorkerServicePeriod"]);
            while (!stoppingToken.IsCancellationRequested)
            {
                Log.Information("Service started at: {time}", DateTimeOffset.Now);
                //_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(period, stoppingToken);
            }
        }

        // Command: sc.exe create WorkerService binpath= F:\Projects\WorkerServiceDemo\WorkerServiceDemo\bin\Release\netcoreapp3.1\publish\WorkerServiceDemo.exe start=auto
    }
}
