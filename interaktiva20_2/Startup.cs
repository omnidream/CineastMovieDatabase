using interaktiva20_2.Data;
using interaktiva20_2.Infra;
using interaktiva20_2.Test;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace interaktiva20_2
{
    public class Startup
    {

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddScoped<IApiClient, ApiClient>();
            //services.AddScoped<IMovieRepo, MovieRepo>();
            services.AddScoped<IMovieRepo, MovieMockRepo>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseStaticFiles(); //S�ger att vi �r medvetna om att vi exponerar filerna i wwwroot p� webben. M�ste ha med f�r att f� in v�ra assets.
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                    );
            });
        }
    }
}
