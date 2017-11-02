using Box.Api.Data.DataContexts;
using Box.Core.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Box.Api
{
    public class Startup
    {
        public Startup( IConfiguration configuration )
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices( IServiceCollection services )
        {
            services.AddSingleton( Configuration ); // Add the configuration to the DI
            services.AddLogging();

            services.AddServices(); // Locate and add all services with the services attribute

            services.AddDbContext<BoxApiDataContext>();

            services.AddMvc()
                .AddXmlSerializerFormatters();

            services.AddSwaggerGen( c => { c.SwaggerDoc( "v1", new Info { Title = "Box API", Version = "v1" } ); } );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure( IApplicationBuilder app, IHostingEnvironment env )
        {
            if ( env.IsDevelopment() )
            {
                app.UseDeveloperExceptionPage();
            }

            using ( var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope() )
            {
                var context = serviceScope.ServiceProvider.GetService<BoxApiDataContext>();
                context.Database.EnsureCreated();
            }

            app.UseSwagger();
            app.UseSwaggerUI( c => { c.SwaggerEndpoint( "/swagger/v1/swagger.json", "Box API V1" ); } );

            app.UseMvc();
        }
    }
}