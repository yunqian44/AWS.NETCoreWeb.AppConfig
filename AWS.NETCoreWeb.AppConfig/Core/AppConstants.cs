using Amazon.AppConfig.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWS.NETCoreWeb.AppConfig.Core
{
    public static class AppConstants
    {
        /// <summary>
        /// AppConfig 应用
        /// </summary>
        public const string AppConfigApplication = "SampleAppConfig";

        /// <summary>
        /// AppConfig 环境
        /// </summary>
        public const string AppConfigEnvironment = "ProductionEnv";

        /// <summary>
        /// AppConfig 配置文件
        /// </summary>
        public const string AppConfigConfiguration = "AppconfigConfigurationProfile";

        /// <summary>
        /// 配置文件版本
        /// </summary>
        public static string ClientConfigurationVersion;

        public static DateTime TimeToLiveExpiration;

        public static double TimeToLiveInSeconds = 15;

        /// <summary>
        /// AppConfig 配置文件数据
        /// </summary>
        public static AppConfigData AppConfigData;

        /// <summary>
        /// 配置响应
        /// </summary>
        public static GetConfigurationResponse GetConfigurationResponse;
    }
}
