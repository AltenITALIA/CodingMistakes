
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace CodingMistakes.API
{
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The current configuration.</param>
        /// <param name="environment">The web hosting environment.</param>
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        internal IWebHostEnvironment Environment { get; }

        internal IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //var configuration = new TMRConfiguration
            //{
            //    IsDevelopment = Environment.IsDevelopment(),
            //    Connections = new Dictionary<string, string> {
            //        { "TMRDB", Configuration.GetConnectionString("TMRDB") },
            //        { "TMRUsersDB", Configuration.GetConnectionString("TMRUsersDB") },
            //    },
            //    LDAPHost = Configuration.GetValue<string>("LDAPHost"),
            //    JWT = Configuration.GetSection("JWT").Get<JWTSettings>(),
            //};

            //TMRBootstrap.ConfigureServices(services, configuration);

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v3", new OpenApiInfo { Title = "Alten: Common Coding Mistakes", Version = "v1" });
            });

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .SetIsOriginAllowed(_ => true)
                        .AllowCredentials()
                        .WithExposedHeaders(Microsoft.Net.Http.Headers.HeaderNames.ContentDisposition);
                });
            });

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v3/swagger.json", "Alten: Common Coding Mistakes v3"));
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                app.UseHttpsRedirection();
            }

            app.UseRouting();

            app.UseCors();

            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501
                spa.Options.SourcePath = "wwwroot";
            });
        }
    }
}
