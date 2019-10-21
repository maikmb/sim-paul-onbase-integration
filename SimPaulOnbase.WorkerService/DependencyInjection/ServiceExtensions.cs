using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimPaulOnbase.WorkerService.DependencyInjection
{
    public static class ServicesExtensios
    {
        public static void BindSingletonFromConfiguration<T>(this IServiceCollection services, string configurationKey) where T : class
        {
            T instance = (T)Activator.CreateInstance(typeof(T));
            var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetService<IConfiguration>();


            configuration.Bind(configurationKey, instance);
            services.AddSingleton(instance);
        }

        public static void BindSingleton<T>(this IServiceCollection services) where T : class
        {
            T instance = (T)Activator.CreateInstance(typeof(T));
            services.AddSingleton(instance);
        }
    }
}
