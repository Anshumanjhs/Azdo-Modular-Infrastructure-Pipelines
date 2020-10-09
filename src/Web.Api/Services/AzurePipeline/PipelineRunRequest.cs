using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;
namespace Web.Api.Services.AzurePipeline
{
    public class PipelineRunRequest
    {
        //public object[] stagesToSkip { get; set; }
        public Resources resources { get; set; }
        public Templateparameters templateParameters { get; set; }
        public Variables variables { get; set; }
    }

    public class Templateparameters
    {
        public string REGION { get; set; }

        public string RESOURCE_GROUP_NAME { get; set; }

        public string VNET_NAME { get; set; }

        public string NETWORK_VNET_ADDRESS_SPACE { get; set; }

        public string NETWORK_SUBNETS { get; set; }

        public string TAG_ENVIRONMENT { get; set; }
    }

    public class Variables
    { }
}