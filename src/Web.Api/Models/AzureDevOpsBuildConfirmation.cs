namespace Web.Api.Models
{        
    public class AzureDevOpsBuildConfirmation
    {
        public string BuildNumber { get; set; }
        public string BuildUrl { get; set; }
        public int BuildId { get; set; }    
    }
        
}