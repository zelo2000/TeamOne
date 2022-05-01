using GS.Business.Command.Core;
using GS.Business.Infrastructure;
using GS.Business.Infrastructure.Command;
using GS.Business.Infrastructure.Query;
using GS.Business.Query.Core;
using GS.Domain.Models.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System;
using System.Linq;

namespace GS.Business.Modules
{
    public static class BusinessModule
    {
        public static void AddBusinessModule(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = new MongoDbSettings();
            configuration.GetSection("MongoDatabaseSettings").Bind(settings);
            var mongoClient = new MongoClient(settings.ConnectionString);

            services
                .AddSingleton<IMongoClient>(mongoClient)
                .AddScoped<ICommandHandlerFactory, CommandHandlerFactory>()
                .AddScoped<ICommandHandler, CommandHandler>()
                .AddScoped<IQueryHandlerFactory, QueryHandlerFactory>()
                .AddScoped<IQueryHandler, QueryHandler>()
                .AddScoped<IDateTimeProvider, DateTimeProvider>()
                .AddScoped<ITripStatusProvider, TripStatusProvider>()
                .AddScoped<IAuthService, AuthService>();

            services.AddHandlers(typeof(ICommandHandler<>));
            services.AddHandlers(typeof(IQueryHandler<,>));
        }

        private static void AddHandlers(this IServiceCollection services, Type handlerInterface)
        {
            var handlers = typeof(BusinessModule)
                .Assembly.GetTypes()
                .Where(t =>
                    t.GetInterfaces()
                     .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterface)
                );

            foreach (var handler in handlers)
            {
                var serviceType = handler.GetInterfaces()
                    .First(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterface);

                services.AddScoped(serviceType, handler);
            }
        }
    }
}
