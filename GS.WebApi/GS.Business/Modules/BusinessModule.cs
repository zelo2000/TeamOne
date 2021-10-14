using GS.Business.Command.Core;
using GS.Business.Infrastructure.Command;
using GS.Business.Infrastructure.Query;
using GS.Business.Query.Core;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace GS.Business.Modules
{
    public static class BusinessModule
    {
        public static void AddBusinessModule(this IServiceCollection services)
        {
            services.AddScoped<ICommandHandlerFactory, CommandHandlerFactory>();
            services.AddScoped<ICommandHandler, CommandHandler>();

            services.AddScoped<IQueryHandlerFactory, QueryHandlerFactory>();
            services.AddScoped<IQueryHandler, QueryHandler>();

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
