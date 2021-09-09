using Microsoft.Extensions.DependencyInjection;
using SimpleCrud.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public static class ServiceCollection
    {
        public static IServiceCollection AddCommon(this IServiceCollection services)
        {
            AddRepository(services);
            return services;
        }

        private static void AddRepository(IServiceCollection services)
        {
            services.AddScoped((typeof(IRepository<>)), typeof(IRepositoryImpl<>));
        }
    }
}
