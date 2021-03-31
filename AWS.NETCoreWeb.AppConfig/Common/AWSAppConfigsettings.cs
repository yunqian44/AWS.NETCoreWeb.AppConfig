using AWS.NETCoreWeb.AppConfig.Core;
using AWS.NETCoreWeb.AppConfig.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWS.NETCoreWeb.AppConfig.Common
{
    public class AWSAppConfigsettings
    {
        public IAppConfigService _appConfigService;

        public AWSAppConfigsettings(AppConfigService appConfigService)
        {
            _appConfigService = appConfigService;
        }
    }
}
