using GS.Data.Repositories.TripRead;
using GS.Data.Repositories.TripWrite;
using GS.Data.Repositories.UserRead;
using GS.Data.Repositories.UserWrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace GS.Data.Modules
{
    public static class DataModule
    {
        public static void AddDataModule(this IServiceCollection services, IConfiguration configuration)
        {
            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));

            services.AddScoped<IUserReadRepository, UserReadRepository>();
            services.AddScoped<IUserWriteRepository, UserWriteRepository>();
            services.AddDbContext<GSDbContext>(
                options => options.UseSqlServer(
                        configuration.GetConnectionString("SqlServerConnection"),
                        x => x.MigrationsAssembly("GS.Data")
                    )
                );

            services.AddScoped<TripDbContext>()
                .AddScoped<ITripReadRepository, TripReadRepository>()
                .AddScoped<ITripWriteRepository, TripWriteRepository>();
        }
    }
}
