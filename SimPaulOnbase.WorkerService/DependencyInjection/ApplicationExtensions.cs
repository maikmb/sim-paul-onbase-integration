using Microsoft.Extensions.DependencyInjection;

namespace SimPaulOnbase.WorkerService.DependencyInjection
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            services.AddScoped<Application.UseCases.Customers.ICustomerIntegrationUseCase, Application.UseCases.Customers.CustomerIntegrationUseCase>();
            services.AddScoped<Application.Repositories.ICustomerRepository, Infraestructure.ApiDataAccess.CustomerRepository>();
            services.AddScoped<Application.Services.ICustomerOnbaseService, Infraestructure.Services.Customers.CustomerOnbaseService>();


            return services;
        }

        public static IServiceCollection AddAppSettings(this IServiceCollection services)
        {
            services.BindSingletonFromConfiguration<Infraestructure.ApiDataAccess.CustomerApiSettings>("CustomerApiSettings");
            services.BindSingletonFromConfiguration<Infraestructure.Services.OnbaseSettings>("OnbaseSettings");


            return services;
        }
    }
}
