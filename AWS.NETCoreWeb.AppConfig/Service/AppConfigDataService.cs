using Amazon.AppConfig.Model;
using AWS.NETCoreWeb.AppConfig.Core;
using AWS.NETCoreWeb.AppConfig.Extension;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWS.NETCoreWeb.AppConfig.Service
{
    public class AppConfigDataService : IAppConfigDataService
    {
        private readonly Guid _clientId;

        private readonly IAppConfigService _appConfigService;

        public AppConfigDataService(Guid clientId,
            IAppConfigService appConfigService)
        {
            _clientId = clientId;
            _appConfigService = appConfigService;
        }

        public async Task<AppConfigData> GetAppConfigData()
        {
            if (DateTime.UtcNow > AppConstants.TimeToLiveExpiration || String.IsNullOrEmpty(AppConstants.ClientConfigurationVersion))
            {
                // get Amazon.AppConfig.Model.GetConfigurationResponse from GetConfiguration API Call
                GetConfigurationResponse getConfigurationResponse = await _appConfigService.GetConfigurationResponse();

                AppConstants.ClientConfigurationVersion = getConfigurationResponse.ConfigurationVersion;

                string decodedResponseData = getConfigurationResponse.Content.Length > 0 ? getConfigurationResponse.Content.DecodeMemoryStreamToString() : String.Empty;

                AppConstants.AppConfigData = null;
                // convert DecodedResponseData to our AppConfigData model which consists of:
                //AppConfigData appConfigData = String.IsNullOrEmpty(decodedResponseData) ? AppConstants.AppConfigData : JsonConvert.DeserializeObject<AppConfigData>(decodedResponseData);

                AppConfigData appConfigData = JsonConvert.DeserializeObject<AppConfigData>(decodedResponseData);

                if (!appConfigData.BoolEnableLimitResults && appConfigData.IntResultLimit == 0)
                {
                    throw new Exception();
                }

                // add AppConfigData to our cache in AppConstants
                AppConstants.AppConfigData = appConfigData;

                return AppConstants.AppConfigData;
            }
            else
            {
                Console.WriteLine("DID NOT call GetConfigurationAPI to get data.  Return AppConfigData from cached value in AppConstants.AppConfigData instead. \n");
                return AppConstants.AppConfigData;
            }
        }
    }
}
