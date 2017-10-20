using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Box.Api.Services
{
    public static class ServiceLocator
    {
        public static IServiceCollection SetupServices(this IServiceCollection serviceCollection)
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var type in assembly.GetTypes())
                {
                    var attribute = type.GetCustomAttributes(typeof(ServiceAttribute), true)
                        .FirstOrDefault() as ServiceAttribute;

                    if (attribute != null)
                    {
                        var implementation = attribute.HasType ? attribute.ExportedType : type;

                        switch (attribute.Lifetime)
                        {
                            case Lifetime.Singleton:
                                serviceCollection.AddSingleton(implementation, type);
                                break;

                            case Lifetime.Transient:
                                serviceCollection.AddTransient(implementation, type);
                                break;

                            case Lifetime.Scoped:
                                serviceCollection.AddScoped(implementation, type);
                                break;

                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }
                }
            }
            return serviceCollection;
        }
    }
}