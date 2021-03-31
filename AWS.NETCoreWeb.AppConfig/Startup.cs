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

            services.AddDbContext<UserContext>(options => options.UseMySql(Appsettings.app("MySql")));


            // ע�� Ӧ�ò�Application
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAppConfigDataService, AppConfigDataService>(x=>new AppConfigDataService(Guid.NewGuid(),x.GetRequiredService<AppConfigService>()));
            services.AddScoped<IAppConfigService, AppConfigService>(x => new AppConfigService(Guid.NewGuid(), x.GetRequiredService<AmazonAppConfigClient>()));
            services.AddSingleton<AmazonAppConfigClient>(x=> new AmazonAppConfigClient(Appsettings.app("AWS", "AWSAccessKey"), Appsettings.app("AWS", "AWSSecretKey"),RegionEndpoint.APNortheast1));

            // ע�� ������ʩ�� - ���ݲ�
            services.AddScoped<IUserRepository, UserRepository>();

            //��ӷ���
            services.AddAutoMapper(typeof(AutoMapperConfig));
            //��������
            AutoMapperConfig.RegisterMappings();
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
