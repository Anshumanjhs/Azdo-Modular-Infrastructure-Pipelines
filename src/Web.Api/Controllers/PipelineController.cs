using System.Linq;
using System.Threading.Tasks;
using Web.Api.Models;
using Web.Api.Services.AzurePipeline;
using Microsoft.AspNetCore.Mvc;
using Web.Api.Models.Validators;

namespace Web.Api.Controllers
{
    /// <summary>
    /// The PipelineController controller is responsible for starting the Azure DevOps pipeline
    /// to create or move resources. This requires a user to already be authenticated to AzDO and 
    /// expects a token to be provided to operate on a user's behalf. 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PipelineController : ControllerBase
    {

        private readonly IPipelineService _pipelineService;
        public PipelineController(IPipelineService pipelineService)
        {
            _pipelineService = pipelineService;
        }   

        /// <summary>
        /// QueueBuild is responsible for queueing a build in AzDO.
        /// This is how the top level operation is triggered off for the creation of resources.
        /// </summary>
        /// <param name="networkInput"></param>
        /// <returns></returns>
        [HttpPost("queuebuild")]
        public async Task<IActionResult> QueueBuild(StandUpNetworkModel networkInput)
        {
            #region validation of parameters

            // var validator = new StandUpNetworkModelValidator();
            // var validationResult = validator.Validate(networkInput);

            // if (!validationResult.IsValid)
            // {
            //     return new BadRequestObjectResult(validationResult.Errors.Select(e => new
            //     {
            //         Field = e.PropertyName,
            //             Error = e.ErrorMessage
            //     }));
            // }

            #endregion
            
            // changed QueueBuildAsync to QueueBuildViaRestAsync 
            // QueueBuildAsync is commented out, to use SDK please uncomment
            var buildConfirmation = await _pipelineService.QueueBuildViaRestAsync(networkInput);
            return new OkObjectResult(buildConfirmation);
        }

    }
}