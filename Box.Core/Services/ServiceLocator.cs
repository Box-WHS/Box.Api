using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Box.Core.Services
{
    public static class ServiceLocator
    {
        public static IServiceCollection AddServices( this IServiceCollection serviceCollection, ILogger logger )
        {
            Services( serviceCollection, logger );
            return serviceCollection;
        }

        public static IServiceCollection AddServices( this IServiceCollection serviceCollection )
        {
            Services( serviceCollection, null );
            return serviceCollection;
        }

        private static void Services( IServiceCollection serviceCollection, ILogger logger )
        {
            foreach ( var assembly in AppDomain.CurrentDomain.GetAssemblies() )
            {
                foreach ( var type in assembly.GetTypes() )
                {
                    if ( type.GetCustomAttributes( typeof( ServiceAttribute ), true )
                        .FirstOrDefault() is ServiceAttribute attribute )
                    {
                        var implementation = attribute.HasType ? attribute.ExportedType : type;
                        logger?.LogDebug( $"Found service {implementation}, added as {attribute.Lifetime}" );

                        switch ( attribute.Lifetime )
                        {
                            case ServiceLifetime.Singleton:
                                serviceCollection.AddSingleton( implementation, type );
                                break;

                            case ServiceLifetime.Transient:
                                serviceCollection.AddTransient( implementation, type );
                                break;

                            case ServiceLifetime.Scoped:
                                serviceCollection.AddScoped( implementation, type );
                                break;

                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }
                }
            }
        }
    }
}