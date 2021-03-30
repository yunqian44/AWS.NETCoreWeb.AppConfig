using Amazon.AppConfig.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWS.NETCoreWeb.AppConfig.Service
{
    public interface IAppConfigService
    {
        Task<GetConfigurationResponse> GetConfigurationResponse();
    }
}
