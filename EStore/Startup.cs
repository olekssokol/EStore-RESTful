using EStore.Data;
using EStore.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EStore
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            
            string con = "Host=localhost;Port=5433;Database=EStore;Username=postgres;Password=Admin";
            // устанавливаем контекст данных
            services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(con));

            services.AddControllers(); // используем контроллеры без представлений
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); // подключаем маршрутизацию на контроллеры
            });

        }
    }
}
