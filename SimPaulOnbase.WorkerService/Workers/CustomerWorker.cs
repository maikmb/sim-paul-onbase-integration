using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SimPaulOnbase.Application.UseCases.Customers;
using SimPaulOnbase.Core.Exceptions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SimPaulOnbase.WorkerService.Workers
{
    public class CustomerWorker : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<CustomerWorker> _logger;

        public CustomerWorker(ILogger<CustomerWorker> logger, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var customerIntegrationUseCase = (ICustomerIntegrationUseCase)scope.ServiceProvider
                          .GetRequiredService(typeof(ICustomerIntegrationUseCase));

                        var output = customerIntegrationUseCase.Handle();
                        this._logger.LogInformation($"Integration executed: ${ output.IntegratedCount } customers sended");
                    }

                    
                }
                catch (CustomerApiRequestException ex)
                {
                    this._logger.LogError(ex, "Error on retrieve data from api. Check exception for details.");
                }
                catch (OnbaseConnectionException ex)
                {
                    this._logger.LogError(ex, "Cound't connect to onbase server. Check exception for details.");
                }

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
