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

namespace WebStore
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            //ConfigV22(app, env);

            ConfigV31(app, env);
        }

        private void ConfigV31(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            var helloMsg = _configuration["CustomHelloWorld"];
            helloMsg = _configuration["Logging:LogLevel:Default"];

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                // GET: /<controller>/details/{id}

                endpoints.MapGet("/", async context => { await context.Response.WriteAsync(helloMsg); });
            });
        }

        private void ConfigV22(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // ѕроизводим конфигурацию инфраструктуры MVC
            app.UseMvc(routes =>
            {
                // ƒобавл€ем обработчик маршрута по умолчанию
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
                // ћаршрут по умолчанию состоит из трЄх частей разделЄнных У/Ф
                // ѕервой частью указываетс€ им€ контроллера,
                // второй - им€ действи€ (метода) в контроллере,
                // третей - опциональный параметр с именем УidФ
                // ≈сли часть не указана - используютс€ значени€ по умолчанию:
                // дл€ контроллера им€ УHomeФ,
                // дл€ действи€ - УIndexФ
            });

        }
    }
}
