using System;
using System.Diagnostics;
using System.Threading;
using Box.Api.Extensions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace Box.Api
{
    public class Program
    {
        public static void Main( string[] args )
        {
            BuildWebHost( args ).Run();
        }

        public static IWebHost BuildWebHost( string[] args )
        {
            return WebHost.CreateDefaultBuilder( args )
                .UseKestrel()
                .ConfigureLogging( ( hostingContext, logging ) =>
                {
                    logging.AddConfiguration( hostingContext.Configuration.GetSection( "Logging" ) );
                } )
                .UseStartup<Startup>()
                .Build();
        }
    }
}