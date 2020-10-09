using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Web.Api.Services.AzurePipeline
{
    public class Resources
    {
       public Repositories repositories { get; set; } 
    }
        public class Repositories
    {
        public Self self { get; set; }
    }

    public class Self
    {
        public string refName { get; set; }
    }
}