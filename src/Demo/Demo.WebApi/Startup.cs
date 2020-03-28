using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sprite.Common.Dependency;
using Sprite.EntityFrameWorkCore.Default;
using Microsoft.EntityFrameworkCore;

namespace Demo.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDependencyConfig();

            services.AddDbContext<DefaultDbContext>((provider, builder) =>
            {
                var configuration = provider.GetService<IConfiguration>();
                builder.UseSqlServer(configuration.GetSection("ConnectionStrings:DefaultDatabase").Value);
                //builder.UseMySql(configuration.GetSection("ConnectionStrings:DefaultDatabase").Value);
            });

            // swagger 
            services.AddSwaggerGen(options =>
           {
               options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
               {
                   Title = "Demo api",
                   Version = "v1"
               });
           });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger(c =>
            {
                c.RouteTemplate = "swagger/{documentName}/swagger.json";
            })
                .UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "demo api");
                });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
