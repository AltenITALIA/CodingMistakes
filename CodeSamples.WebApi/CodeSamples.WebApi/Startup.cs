using System;
using System.IO;
using System.Reflection;
using CodeSamples.BusinessLayer;
using CodeSamples.DataAccessLayer;
using CodeSamples.WebApi.Documentation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

namespace CodeSamples.WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                // TODO: Uncomment the following code to properly support UTC DATE TIME ZONE HANDLING.
                //.AddJsonOptions(options =>
                //{
                //    options.JsonSerializerOptions.Converters.Add(new UtcDateTimeConverter());
                //})
                ;

            services.AddSwaggerGen(options =>
            {
                options.OperationFilter<CultureAwareOperationFilter>();

                options.MapType<DateTime>(() => new OpenApiSchema
                {
                    Type = "string",
                    Example = new OpenApiString(DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz"))
                });

                options.SwaggerDoc("v1", new OpenApiInfo { Title = "CodeSamples.WebApi", Version = "v1" });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });

            // TODO: Uncomment the following code to enable CULTURE REQUEST LOCALIZATION.
            //var supportedCultures = new[] { "it-IT", "en-US" };
            //var localizationOptions = new RequestLocalizationOptions()
            //    .AddSupportedCultures(supportedCultures)
            //    .AddSupportedUICultures(supportedCultures)
            //    .SetDefaultCulture(supportedCultures[0]);

            //services.Configure<RequestLocalizationOptions>(options =>
            //{
            //    options.SupportedCultures = localizationOptions.SupportedCultures;
            //    options.SupportedUICultures = localizationOptions.SupportedCultures;
            //    options.DefaultRequestCulture = localizationOptions.DefaultRequestCulture;
            //});

            services.AddDbContext<DataContext>(options =>
            {
                var connectionString = Configuration.GetConnectionString("SqlConnection");
                options.UseSqlServer(connectionString);
            });

            services.AddScoped<ITransactionService, TransactionService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "CodeSamples.WebApi v1");
                options.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            // TODO: Uncomment the following code to enable CULTURE REQUEST LOCALIZATION.
            //app.UseRequestLocalization();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
