using System;

namespace Web.Api.Services.AzurePipeline
{
    public class PipelineRunResponse
    {
        public _Links _links { get; set; }
        public Pipeline1 pipeline { get; set; }
        public string state { get; set; }
        public DateTime createdDate { get; set; }
        public string url { get; set; }
        public Resources resources { get; set; }
        public int id { get; set; }
        public string name { get; set; }
    }

        public class _Links
    {
        public Self self { get; set; }
        public Web web { get; set; }
        public PipelineWeb pipelineweb { get; set; }
        public Pipeline pipeline { get; set; }
    }

    public class Web
    {
        public string href { get; set; }
    }

    public class PipelineWeb
    {
        public string href { get; set; }
    }

    public class Pipeline
    {
        public string href { get; set; }
    }

    public class Pipeline1
    {
        public string url { get; set; }
        public int id { get; set; }
        public int revision { get; set; }
        public string name { get; set; }
        public string folder { get; set; }
    }

    public class Repository
    {
        public string id { get; set; }
        public string type { get; set; }
    }
}