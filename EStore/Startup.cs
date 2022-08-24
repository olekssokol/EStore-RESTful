using EStore.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EStore
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            
            string con = "Host=localhost;Port=5433;Database=EStore;Username=postgres;Password=Admin";

            services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(con));

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
