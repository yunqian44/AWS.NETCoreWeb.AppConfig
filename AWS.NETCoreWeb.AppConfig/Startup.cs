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
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<UserContext>(options => options.UseMySql("server=database-2.cdiostsnewls.ap-northeast-1.rds.amazonaws.com;uid=admin;pwd=Passw0rd;database=CnBateBlogWeb1"));

            // 注入 应用层Application
            services.AddScoped<IUserService, UserService>();

            // 注入 基础设施层 - 数据层
            services.AddScoped<IUserRepository, UserRepository>();

            //添加服务
            services.AddAutoMapper(typeof(AutoMapperConfig));
            //启动配置
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
