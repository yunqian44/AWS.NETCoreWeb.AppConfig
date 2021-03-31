using AWS.NETCoreWeb.AppConfig.Common;
using AWS.NETCoreWeb.AppConfig.Core;
using AWS.NETCoreWeb.AppConfig.Model;
using AWS.NETCoreWeb.AppConfig.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AWS.NETCoreWeb.AppConfig.Data
{
    public class UserContext : DbContext
    {
        public IAppConfigDataService _appConfigDataService;

        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {

        }

        public DbSet<UserModel> Users { get; set; }

        /// <summary>
        /// 重写连接数据库
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // 从 appsetting.json 中获取配置信息
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // 定义要使用的数据库
            optionsBuilder.UseMySql(Appsettings.app("MySql"));
        }
    }
}
