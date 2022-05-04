using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QuizProjectFE.Models;
using QuizProjectFE.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizProjectFE
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddScoped<IApiRequest<Quiz>, API_Request<Quiz>>();
            services.AddScoped<IApiRequest<Question>,API_Request<Question>>();
            services.AddScoped<IApiRequest<Option>, API_Request<Option>>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddHttpClient("ApiClient", c =>
            {
                c.BaseAddress = new Uri(Configuration["OnlineUri"]);
                c.DefaultRequestHeaders.Clear();
                c.DefaultRequestHeaders.Accept
                    .Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            });
            // Stores the session data in the servers memory
            services.AddDistributedMemoryCache();

            services.AddSession(c =>
            {
                // Naming the cookie responsible for this session
                c.Cookie.Name = "QuizyCookie";
                // Cannot be accessed by Javascript
                c.Cookie.HttpOnly = true;
                // Bypass policy checks
                c.Cookie.IsEssential = true;
                // 5 minute session timeout
                c.IdleTimeout = TimeSpan.FromSeconds(300);
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
