namespace Petstagram.Server
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Petstagram.Server.Data;
    using Petstagram.Server.Infrastructure;
    using Petstagram.Server.Infrastructure.Extensions;
    using Petstagram.Server.Infrastructure.Filters;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
            => services
                .AddDatabase(this.Configuration) //wszystkie te funkcje dodane s¹ w infrastructure
                .AddIdentity()//moja funkcja
                .AddJwtAuthentication(services.GetApplicationSettings(this.Configuration))//moja funkcje
                .AddApplicationServices()//moja funkcja
                .AddSwagger()
                .AddApiControllers();
                
        

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
 
            app
                .UseSwaggerUI()
                .UseRouting()
                .UseCors(options => options
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod())
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                    {
                        endpoints.MapControllers();
                    })
                .ApplyMigrations();
        }
    }
}
