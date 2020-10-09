using Web.Api.Models;
using Web.Api.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.TeamFoundation.Build.WebApi;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.OAuth;
using Microsoft.VisualStudio.Services.WebApi;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace Web.Api.Services.AzurePipeline
{
    public class AzurePipelineService : IPipelineService
    {
        private readonly ApplicationConfiguration _applicationConfiguration;

        public AzurePipelineService(ApplicationConfiguration applicationConfiguration)
        {
            _applicationConfiguration = applicationConfiguration;
        }

        // Below is for QueueBuildAsync uncomment if you would like to use instead of QueueBuildViaRestAsync

        // public async Task<AzureDevOpsBuildConfirmation> QueueBuildAsync(StandUpNetworkModel networkModel)
        // {
        //     VssCredentials credentials;

        //     // Use PAT
        //     credentials = new VssBasicCredential(string.Empty, _applicationConfiguration.AzureDevOpsPersonalAccessToken);
            

        //     VssConnection connection =
        //        new VssConnection(
        //            new Uri(_applicationConfiguration.AzureDevOpsUri),
        //            credentials);

        //     try
        //     {
        //        var buildClient = connection.GetClient<BuildHttpClient>();

        //         //Get the build definition for the one we want to queue up
        //         //This also validates our AzureDevOpsPipelineBuildId is valid.
        //         List<BuildDefinitionReference> buildDefinitions = new List<BuildDefinitionReference>();
        //         var build = await buildClient.GetDefinitionAsync(
        //            _applicationConfiguration.AzureDevOpsProjectName,
        //            _applicationConfiguration.AzureDevOpsPipelineBuildId);

        //         var buildInstance = new Build
        //         {
        //             Definition = new DefinitionReference
        //             {
        //                 Id = build.Id,
        //             },
        //             Project = build.Project,
        //             SourceBranch = _applicationConfiguration.OrchestrationRef
        //         };

        //         var parameters = new Dictionary<string, string>();
        //         parameters.Add("REGION", networkModel.Region);
        //         parameters.Add("RESOURCE_GROUP_NAME", networkModel.ResourceGroupName);
        //         parameters.Add("VNET_NAME", networkModel.VnetName);
        //         parameters.Add("NETWORK_VNET_ADDRESS_SPACE", networkModel.NetworkVnetAddressSpace);
        //         parameters.Add("NETWORK_SUBNETS", $"[{string.Join(",", networkModel.NetworkSubnets)}]");
        //         parameters.Add("TAG_ENVIRONMENT", networkModel.TagEnvironment);

        //         buildInstance.Parameters = JsonConvert.SerializeObject(parameters);

        //         var queuedBuild = await buildClient.QueueBuildAsync(buildInstance);
               
        //         return new AzureDevOpsBuildConfirmation()
        //         {
        //             BuildNumber = queuedBuild.BuildNumber,
        //             BuildUrl = queuedBuild.Url,
        //             BuildId = build.Id
        //         };
        //     }
        //     catch (BuildRequestValidationFailedException ex)
        //     {
        //         var sb = new StringBuilder();
        //         foreach (var validationError in ex.ValidationResults)
        //         {
        //             sb.Append(validationError.Message);
        //             sb.Append(Environment.NewLine);
        //         }
        //         throw;
        //     }
        // }

        public async Task<AzureDevOpsBuildConfirmation> QueueBuildViaRestAsync(StandUpNetworkModel networkModel)
        {
            using var httpClient = new HttpClient();

            //Since other code for QueueBuildAsync I believe relies on the URI not having
            //a trailing slash, this needs it, I'm just appending here.
            httpClient.BaseAddress = new Uri(_applicationConfiguration.AzureDevOpsUri + "/");
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            //If we have a PAT in the config, use it. This enables better local testing without having
            //to get the OAuth token which has some friction in retrieving. 
            if (!string.IsNullOrEmpty(_applicationConfiguration.AzureDevOpsPersonalAccessToken))
            {
                //Authorization: Basic
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Basic {_applicationConfiguration.AzureDevOpsPersonalAccessTokenForBasicAuth}");
            }
            else
            {
                // var aes = new AesEncryption(_applicationConfiguration.KeyVault.AesKey, _applicationConfiguration.KeyVault.HmacKey);
                // var plainTextOauthToken = aes.DecryptAndCheckSignature(Model.EncryptedAzdoOauthToken);
                // httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {plainTextOauthToken}");
            }

            var request = new HttpRequestMessage(HttpMethod.Post, $"{_applicationConfiguration.AzureDevOpsProjectName}/_apis/pipelines/{_applicationConfiguration.AzureDevOpsPipelineBuildId}/runs?api-version=5.1-preview");

            var pipelineRunRequest = new PipelineRunRequest();
            pipelineRunRequest.resources = new Resources();
            pipelineRunRequest.resources.repositories = new Repositories();
            pipelineRunRequest.resources.repositories.self = new Self();
            pipelineRunRequest.resources.repositories.self.refName = _applicationConfiguration.OrchestrationRef;
            pipelineRunRequest.variables = new Variables();

            pipelineRunRequest.templateParameters = new Templateparameters();

            pipelineRunRequest.templateParameters.REGION = networkModel.Region.ToString();
            pipelineRunRequest.templateParameters.RESOURCE_GROUP_NAME = networkModel.ResourceGroupName.ToString();
            pipelineRunRequest.templateParameters.VNET_NAME = networkModel.VnetName.ToString();
            pipelineRunRequest.templateParameters.NETWORK_VNET_ADDRESS_SPACE = networkModel.NetworkVnetAddressSpace.ToString();
            pipelineRunRequest.templateParameters.NETWORK_SUBNETS = networkModel.NetworkSubnets.ToString();
            pipelineRunRequest.templateParameters.TAG_ENVIRONMENT = networkModel.TagEnvironment.ToString();

            //Any missing values we leave off the json. This ensures the pipeline doesn't blow up 
            //with null values and we only send over what we need. 
            var json = JsonConvert.SerializeObject(pipelineRunRequest, new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            request.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var result = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            if (result.StatusCode != HttpStatusCode.OK)
            {
                if (result.StatusCode == HttpStatusCode.NonAuthoritativeInformation)
                {
                    //_logger.LogWarning("Bearer token seems to not be valid. It has possibly expired or was granted under a different org upon login.");
                }

                var outputContent = await result.Content.ReadAsStringAsync();

                var error = $"An error occurred calling the Azure DevOps API. Status:{result.StatusCode} Reason: {result.ReasonPhrase} Content:{outputContent}";
                //_logger.LogError(error);
                throw new Exception(error);
            }

            //Note we're note reading a big string and deserializing it, hence the use of streams below. 
            using (var contentStream = await result.Content.ReadAsStreamAsync())
            {
                using (StreamReader sr = new StreamReader(contentStream))
                {
                    using (JsonReader reader = new JsonTextReader(sr))
                    {
                        var serializer = new Newtonsoft.Json.JsonSerializer();
                        var response = serializer.Deserialize<PipelineRunResponse>(reader);
                        return new AzureDevOpsBuildConfirmation()
                        {
                            BuildId = response.id,
                            BuildNumber = response.name,
                            BuildUrl = response.url
                        };
                    }
                }
            }
        }
    }
}