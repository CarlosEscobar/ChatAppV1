using ChatAppV1.Core;
using ChatAppV1.DataAccess.Context;
using ChatAppV1.DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System;
using System.IO;
using WebSocketManager;

namespace ChatAppV1
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddDirectoryBrowser();
            
            services.AddSingleton<ChatAppContext>();
            services.AddSingleton<IUnitOfWork, UnitOfWork>();

            services.AddIdentity<IdentityUser, IdentityRole>()
                    .AddEntityFrameworkStores<ChatAppContext>()
                    .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 1;
            });

            services.AddWebSocketManager();
            services.AddTransient<ChatHandler>();

            services.AddHttpClient();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
                              IHostingEnvironment env,
                              IServiceProvider serviceProvider,
                              UserManager<IdentityUser> userManager,
                              RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseFileServer(new FileServerOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Views")),
                RequestPath = "/Views",
                EnableDirectoryBrowsing = true
            });

            app.UseAuthentication();
            ChatAppSeeder.SeedData(userManager, roleManager);

            app.UseWebSockets(new WebSocketOptions());
            app.MapWebSocketManager("/chatapp", serviceProvider.GetService<ChatHandler>());
        }
    }
}
