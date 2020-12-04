using BusinessLogic.Repository;
using DataLogic.Commands;
using DataLogic.DbContexts;
using DataLogic.Oueries;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace ElectronicSignaturesService
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
            services.AddDbContext<DocumentContext>(o => o.UseSqlServer(Configuration.GetConnectionString("documentDB")));
            services.AddTransient<IDocumentRepository, DocumentRepository>();
            services.AddTransient<IDocumentQueries, DocumentQueries>();
            services.AddTransient<IImageSignature, ImageSignature>();
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
