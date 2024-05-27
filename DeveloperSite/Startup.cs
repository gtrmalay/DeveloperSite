using DeveloperSite.Models;
using DeveloperSite.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DeveloperSite
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
            services.AddOptions();
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<MobileContext>(options => options.UseSqlServer(connection));
            services.AddMvc();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
            });

            // Регистрация UserRepositoriesImpl
            services.AddScoped<UserRepositoriesImpl>();
            services.AddScoped<GameRepositories>();

        }

        public void Configure(WebApplication app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseRouting();
            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            /*app.MapGet("/", () => { });*/

            var dbContext = serviceProvider.GetService<MobileContext>();
            SampleData.Initialize(dbContext, env);
        }
    }
}
