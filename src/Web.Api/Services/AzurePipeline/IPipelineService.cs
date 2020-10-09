using System.Threading.Tasks;
using Web.Api.Models;

namespace Web.Api.Services.AzurePipeline
{
    public interface IPipelineService
    {
        // Task<AzureDevOpsBuildConfirmation> QueueBuildAsync(StandUpNetworkModel networkModel);

        Task<AzureDevOpsBuildConfirmation> QueueBuildViaRestAsync(StandUpNetworkModel networkModel);
        
    }
}