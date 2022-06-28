using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MongoExample.Models.BaseModels;

namespace MongoExample
{
    public class Startup
    {
        private IConfiguration _configuration { get; }

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private void MapBaseModels()
        {

        }

        public void ConfigureService(IServiceCollection services)
        {
            // Configuration
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetParent("../../MongoExample")?.Parent?.FullName)
            .AddJsonFile("Content/appsettings.json", optional: false)
            .Build();

            services.Configure<MongoDbConnection>(opts => builder.GetSection("MongoDB").Bind(opts));
            services.AddOptions();
            services.AddMvc();

            // Auto mappers

            // Other service

            services.AddControllers();
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
        }

    }
}
