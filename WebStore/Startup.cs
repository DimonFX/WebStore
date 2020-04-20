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
using WebStore.Infrastructure;
using WebStore.Infrastructure.Interfaces;
using WebStore.Infrastructure.Services;

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
            //services.AddMvc();
            //ќт сюда можно добавить фильтр на все контроллеры и их методы
            services.AddMvc(option =>
            {
                option.Filters.Add(new SimpleActionFilter());
            });

            //ƒобавл€ем разрешение зависимостей
            //Ёто означает, что каждый раз, когда мы встречаем интерфейс IEmployeesService в коде нашего приложени€
            //мы автоматически подставл€ем класс InMemoryEmployeeService
            services.AddSingleton<IEmployeesService, InMemoryEmployeeService>();
            services.AddSingleton<ICarService, InMemoryCarService>();//Ѕудет жить все врем€ жизни приложени€
            //services.AddScoped<IEmployeesService, InMemoryEmployeeService>();//¬рем€ жизни http запроса
            //services.AddTransient<IEmployeesService, InMemoryEmployeeService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            app.UseStaticFiles();

            app.Map("/Index", CustomIndexHandler);

            app.UseMiddleware<Infrastructure.TokenMiddleware>();

            UseSampleErrorCheck(app);

            //ConfigV22(app, env);

            ConfigV31(app, env);

            RunSample(app); 
        }

        private void UseSampleErrorCheck(IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                bool isError = false;
                //...
                if (isError)
                {
                    await context.Response.WriteAsync("Error occured. You're in custom pipeline module...");
                }
                else
                {
                    await next.Invoke();
                }
            });
        }

        private void RunSample(IApplicationBuilder obj)
        {
            obj.Run(async context =>
            {
                await context.Response.WriteAsync("ѕривет из конвеера обрадотки запросоа (метод app.Run()");
            });
        }

        private void CustomIndexHandler(IApplicationBuilder obj)
        {
            obj.Run(async context =>
            {
                await context.Response.WriteAsync("Index");
            });
        }

        private void ConfigV31(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            var helloMsg = _configuration["CustomHelloWorld"];
            helloMsg = _configuration["Logging:LogLevel:Default"];
            #region пример сокращенной метода дл€ настройки маршрутизации по умолчанию:
            //app.UseMvcWithDefaultRoute();
            /* Ётот метод повтор€ет то что описано ниже:
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                // GET: /<controller>/details/{id}

                //endpoints.MapGet("/", async context => { await context.Response.WriteAsync(helloMsg); });
            });
             */
            #endregion


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                // GET: /<controller>/details/{id}

                //endpoints.MapGet("/", async context => { await context.Response.WriteAsync(helloMsg); });
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
