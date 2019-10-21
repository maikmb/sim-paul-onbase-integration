using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SimPaulOnbase.Application.UseCases.Customers;
using SimPaulOnbase.Core.Exceptions;
using System.Threading;
using System.Threading.Tasks;

namespace SimPaulOnbase.WorkerService.Workers
{
    public class CustomerWorker : BackgroundService
    {
        private readonly ICustomerIntegrationUseCase _customerIntegrationUseCase;'
        private readonly ILogger<CustomerWorker> _logger;

        public CustomerWorker(ILogger<CustomerWorker> logger, ICustomerIntegrationUseCase customerIntegrationUseCase)
        {
            _logger = logger;
            _customerIntegrationUseCase = customerIntegrationUseCase;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var output = this._customerIntegrationUseCase.Handle();
                    this._logger.LogInformation($"Integration executed: ${ output.IntegratedCount } customers sended");
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
