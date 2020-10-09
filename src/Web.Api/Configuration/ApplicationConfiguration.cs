using System;
// using Azure.Core;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Web.Api.Configuration
{
    public class ApplicationConfiguration
    {
        // public string TenantId { get; set; }
        // public string SubscriptionId { get; set; }
        // public string AppInsightsAppId { get; set; }
        // public string AppInsightsApiQueryTimeSpan { get; set; }

        public int AzureDevOpsPipelineBuildId { get; set; }
        public string AzureDevOpsProjectName { get; set; }

        public string AzureDevOpsPersonalAccessToken { get; set; }

        //public string AzureDevOpsPersonalAccessTokenForBasicAuth => Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes("Basic:" + AzureDevOpsPersonalAccessToken));
        public string AzureDevOpsPersonalAccessTokenForBasicAuth => Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes("Basic:" + AzureDevOpsPersonalAccessToken));

        // //Ex https://dev.azure.com/SceneToScreen
        public string AzureDevOpsUri { get; set; }

        // // Git refs for each module
        // // You need to set these in configuration.  Either locally in your secrets file 
        // // while testing or in Environment Variables in the webapp
        // // You can point to either branches refs/heads/<branch I am targeting/testing> or
        // // for production point to tags refs/tags/v0.0.12 for example
        public string OrchestrationRef { get; set; } = "refs/heads/main";

        // public string TargetEnvironmentCode { get; set; }

        // public string CoreResourceGroupName { get; set; }
        // public string CoreNetworkName { get; set; }
        
        // //Fetch Vault Name from Application Config
        // public string KeyVaultName { get; set; }

        // //Required Secrets from Vault to be initiated in run time.
        // public KeyVault KeyVault { get; set; }

        // public OverriddenAzureConfiguration OverriddenAzureConfiguration { get; set; }

    }
}
