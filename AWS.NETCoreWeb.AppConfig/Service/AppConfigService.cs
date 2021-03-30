using Amazon;
using Amazon.AppConfig;
using Amazon.AppConfig.Model;
using AWS.NETCoreWeb.AppConfig.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWS.NETCoreWeb.AppConfig.Service
{
    public class AppConfigService : IAppConfigService
    {
        private readonly Guid _clientId;

        public AppConfigService(Guid clientId)
        {
            _clientId = clientId;
        }

        public async Task<GetConfigurationResponse> GetConfigurationResponse()
        {
            
            var amazonAppConfigClient = Program.AwsOptions.CreateServiceClient<IAmazonAppConfig>();
            var amazonAppConfigClient = new AmazonAppConfigClient("AKIA2C2TL65VGJ7DOGQX", "xy8DIdJBkTTxxUO7DyGVVuvJo2LCFO/K7UVhbNdF", RegionEndpoint.APNortheast1);

            var request = new GetConfigurationRequest
            {
                Application = AppConstants.AppConfigApplication,
                Environment = AppConstants.AppConfigEnvironment,
                Configuration = AppConstants.AppConfigConfiguration,
                ClientId = _clientId.ToString(),
                ClientConfigurationVersion = AppConstants.ClientConfigurationVersion
            };

            return await amazonAppConfigClient.GetConfigurationAsync(request);
        }
    }
}
