using AWS.NETCoreWeb.AppConfig.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWS.NETCoreWeb.AppConfig.Service
{
    public interface IAppConfigDataService
    {
        Task<AppConfigData> GetAppConfigData();
    }
}
