using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Web.Store.DAL;
using WebStore.DomainNew.Entities;
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
            //�� ���� ����� �������� ������ �� ��� ����������� � �� ������
            services.AddMvc(option =>
            {
                option.Filters.Add(new SimpleActionFilter());
            });

            services.AddDbContext<WebStoreContext>(options => options
                .UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<WebStoreContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options => // �������������
            {
                // Password settings
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 5;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.RequireUniqueEmail = true;
            });

            services.ConfigureApplicationCookie(options => // �������������
            {
                // Cookie settings
                //options.Cookie.HttpOnly = true;
                //options.Cookie.Expiration = TimeSpan.FromDays(150);
                options.LoginPath = "/Account/Login"; // If the LoginPath is not set here, ASP.NET Core will default to /Account/Login
                options.LogoutPath = "/Account/Logout"; // If the LogoutPath is not set here, ASP.NET Core will default to /Account/Logout
                options.AccessDeniedPath = "/Account/AccessDenied"; // If the AccessDeniedPath is not set here, ASP.NET Core will default to /Account/AccessDenied
                options.SlidingExpiration = true;
            });

            //��������� ���������� ������������
            //��� ��������, ��� ������ ���, ����� �� ��������� ��������� IEmployeesService � ���� ������ ����������
            //�� ������������� ����������� ����� InMemoryEmployeeService
            services.AddSingleton<IEmployeesService, InMemoryEmployeeService>();
            services.AddSingleton<ICarService, InMemoryCarService>();//����� ���� ��� ����� ����� ����������
            services.AddScoped<IProductService, SqlProductService>();
            //services.AddScoped<IEmployeesService, InMemoryEmployeeService>();//����� ����� http �������
            //services.AddTransient<IEmployeesService, InMemoryEmployeeService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ICartService, CookieCartService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.Map("/index", CustomIndexHandler);

            app.UseMiddleware<TokenMiddleware>();

            UseSampleErrorCheck(app);

            //ConfigV22(app, env);

            ConfigV31(app, env);

            app.UseWelcomePage("/welcome");

            //RunSample(app);
        }

        private void UseSampleErrorCheck(IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                bool isError = false;
                // ...
                if (isError)
                {
                    await context.Response
                        .WriteAsync("Error occured. You're in custom pipeline module...");
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
                await context.Response.WriteAsync("������ �� �������� ��������� �������� (����� app.Run()");
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
            var helloMsg = _configuration["CustomHelloWorld"];
            helloMsg = _configuration["Logging:LogLevel:Default"];
            #region ������ ����������� ������ ��� ��������� ������������� �� ���������:
            //app.UseMvcWithDefaultRoute();
            /* ���� ����� ��������� �� ��� ������� ����:
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
            // ���������� ������������ �������������� MVC
            app.UseMvc(routes =>
            {
                // ��������� ���������� �������� �� ���������
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
                // ������� �� ��������� ������� �� ��� ������ ���������� �/�
                // ������ ������ ����������� ��� �����������,
                // ������ - ��� �������� (������) � �����������,
                // ������ - ������������ �������� � ������ �id�
                // ���� ����� �� ������� - ������������ �������� �� ���������:
                // ��� ����������� ��� �Home�,
                // ��� �������� - �Index�
            });

        }
    }
}
