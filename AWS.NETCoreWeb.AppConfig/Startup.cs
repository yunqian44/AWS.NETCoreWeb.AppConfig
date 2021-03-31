using Amazon;
using Amazon.AppConfig;
using AWS.NETCoreWeb.AppConfig.Common;
using AWS.NETCoreWeb.AppConfig.Data;
using AWS.NETCoreWeb.AppConfig.Repository.Implements;
using AWS.NETCoreWeb.AppConfig.Repository.Interface;
using AWS.NETCoreWeb.AppConfig.Service;
using AWS.NETCoreWeb.AppConfig.Service.AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWS.NETCoreWeb.AppConfig
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        public IConfiguration Configuration { get; }

        public IWebHostEnvironment Env { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddSingleton(new Appsettings(Env.ContentRootPath));

            // 注入 应用层Application
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAppConfigDataService, AppConfigDataService>(x=>new AppConfigDataService(Guid.NewGuid(),x.GetRequiredService<IAppConfigService>()));
            services.AddScoped<IAppConfigService, AppConfigService>(x => new AppConfigService(Guid.NewGuid(), x.GetRequiredService<AmazonAppConfigClient>()));
            services.AddScoped<AmazonAppConfigClient>(x=> new AmazonAppConfigClient(Appsettings.app("AWS", "AWSAccessKey"), Appsettings.app("AWS", "AWSSecretKey"),RegionEndpoint.APNortheast1));

            // 注入 基础设施层 - 数据层
            services.AddScoped<IUserRepository, UserRepository>();

            //添加服务
            services.AddAutoMapper(typeof(AutoMapperConfig));
            //启动配置
            AutoMapperConfig.RegisterMappings();

            services.AddDbContext<UserContext>();

            //services.AddDbContext<UserContext>(options=>options.UseMySql(Appsettings.app("MySql")));
            //services.AddScoped(UserContext);
            //services.AddScoped<UserContext>(x =>new UserContext(x.GetRequiredService<DbContextOptions<UserContext>>(), x.GetRequiredService<AppConfigDataService>()));
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
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
