using GS.Data.Repositories.UserRead;
using GS.Data.Repositories.UserWrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GS.Data.Modules
{
    public static class DataModule
    {
        public static void AddDataModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserReadRepository, UserReadRepository>();
            services.AddScoped<IUserWriteRepository, UserWriteRepository>();

            services.AddDbContext<GSDbContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("SqlServerConnection")));
        }
    }
}
