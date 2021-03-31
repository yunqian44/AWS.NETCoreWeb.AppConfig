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
        private readonly AmazonAppConfigClient _AmazonAppConfigClient;

        public AppConfigService(Guid clientId,
            AmazonAppConfigClient amazonAppConfigClient)
        {
            _clientId = clientId;
            _AmazonAppConfigClient = amazonAppConfigClient;
        }

        public async Task<GetConfigurationResponse> GetConfigurationResponse()
        {

            //var amazonAppConfigClient = Program.AwsOptions.CreateServiceClient<IAmazonAppConfig>();
            var amazonAppConfigClient = _AmazonAppConfigClient;

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
