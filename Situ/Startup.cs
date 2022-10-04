using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Situ.Data;
using Situ.Security;
using System;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace Situ
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            ReadSettingsIntoStaticVariables();
        }

        private void ReadSettingsIntoStaticVariables()
        {
#if DEBUG
            CentralVariables.WorkingFolderRoot = System.AppContext.BaseDirectory;
            return;
#endif

            var settingsFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Settings.json");
            if (!File.Exists(settingsFile))
            {
                File.WriteAllText(settingsFile, "{" +
                                                "\"WorkingFolderRoot\": \"\"" +
                                                "}");
                throw new FileNotFoundException("Settings.json did not exist. It was created, but the parameters are not set");
            }

            var settingsJson = File.ReadAllText(settingsFile);
            var settings = System.Text.Json.JsonSerializer.Deserialize<Settings>(settingsJson);

            CentralVariables.WorkingFolderRoot = settings.WorkingFolderRoot;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddAuthorizationCore();
            services.AddSingleton<DepartmentService>();
            services.AddSingleton<UserService>();
            services.AddScoped<L1DataService>();
            services.AddSingleton<ReportService>();
            services.AddSingleton<StatisticService>();
            services.AddScoped<AuthenticationStateProvider, WinAuthStateProvider>();
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
