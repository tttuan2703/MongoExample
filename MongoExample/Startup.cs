using Autofac;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using MongoExample.Contracts;
using MongoExample.Infrastructure.Helpers.Contracts;
using MongoExample.Infrastructure.Helpers.Models;
using MongoExample.Infrastructure.Helpers.MongoDbHelpers;
using MongoExample.Infrastructure.Helpers.Services;
using MongoExample.Services;
using System.Reflection;

namespace MongoExample
{
    public class Startup
    {
        protected IConfiguration _configuration { get; set; }

        protected IWebHostEnvironment _environment { get; set; }

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment;
        }

        private void MapBaseModels()
        {

        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Configuration
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetParent("../../MongoExample")?.Parent?.FullName)
            .AddJsonFile("Content/appsettings.json", optional: false)
            .Build();

            services.Configure<MongoDbConnection>(opts => builder.GetSection("MongoDB").Bind(opts));
            services.AddScoped<MongoDbAccessConnection>();
            services.AddOptions();
            services.AddMvc();

            //// Auto mappers

            //// Other service

            //services.AddControllers();

            // Play list services
            services.AddControllers();
            services.Configure<KestrelServerOptions>(_configuration.GetSection("Kestrel"));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo { Version = "v1", Title = "Api" });
            });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1", "API");
                c.RoutePrefix = string.Empty;

                c.DisplayRequestDuration();
                c.EnableFilter();
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            RegisterServiceCommon(builder);
            RegisterServices(builder);
        }

        private void RegisterServiceCommon(ContainerBuilder builder)
        {
            builder.RegisterType<ConfigurationApp>()
                        .As<IConfigurationApp>();

            builder.RegisterBuildCallback(c =>
            {
#pragma warning disable CS8604 // Possible null reference argument.
                InitializerServices(c as IContainer);
#pragma warning restore CS8604 // Possible null reference argument.
            });

#pragma warning disable CS8604 // Possible null reference argument.
            builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(MongoDbAccessConnection)))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
#pragma warning restore CS8604 // Possible null reference argument.

        }

        private void InitializerServices(IContainer container)
        {
            var dependencyInjection = new IoCHelper(container);
            DependencyInjectionHelper.Init(dependencyInjection);
        }

        private void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<PlayListService>()
                        .As<IPlayListService>()
                        .InstancePerLifetimeScope();

        }
    }
}
