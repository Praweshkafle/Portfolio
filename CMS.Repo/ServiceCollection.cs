using CMS.Repo.Repository;
using CMS.Repo.RepositoryImpl;
using Microsoft.Extensions.DependencyInjection;
using SimpleCrud.Repository.UserRepository;
using SimpleCrud.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Repo
{
   public static class ServiceCollection
    {
        public static IServiceCollection AddCMS( this IServiceCollection services)
        {
            AddRepository(services);
            return services;
        }

        private static void AddRepository(IServiceCollection services)
        {
            services.AddScoped<UserRepository, UserRepositoryImpl>();
            services.AddScoped<MyWorkRepository, MyWorkRepositoryImpl>();
        }
    }
}
