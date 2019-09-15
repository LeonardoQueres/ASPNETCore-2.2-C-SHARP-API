using Api.Domain.Interfaces.Service;
using Api.Service.Service;
using Microsoft.Extensions.DependencyInjection;

namespace Api.CrossCutting
{
    public class ConfigureService
    {
        public static void ConfigureDependenciesService(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IUserService, UserService>();
            serviceCollection.AddTransient<ILoginService, LoginService>();
        }
    }
}
