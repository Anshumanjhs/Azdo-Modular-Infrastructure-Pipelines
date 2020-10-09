using System.Text.Json.Serialization;

namespace Web.Api.Models
{
    public class StandUpNetworkModel
    {       
        private string _region;
        private string _resourceGroupName;
        private string _vnetName;
        private string _networkVnetAddressSpace;
        private string _networkSubnets;
        private string _tagEnvironment;

        /// <summary>
        /// Azure region for the network resource group
        /// </summary>
        /// <returns></returns>
        public string Region { get => _region; set => _region = value.ToLower(); }

        /// <summary>
        /// Azure resource group name 
        /// </summary>
        /// <returns></returns>
        public string ResourceGroupName { get => _resourceGroupName; set => _resourceGroupName = value.ToLower(); }
        
        public string VnetName { get => _vnetName; set => _vnetName = value.ToLower(); }

        public string NetworkVnetAddressSpace { get => _networkVnetAddressSpace; set => _networkVnetAddressSpace = value.ToLower(); }

        public string NetworkSubnets { get => _networkSubnets; set => _networkSubnets = value.ToLower(); }

        public string TagEnvironment { get => _tagEnvironment; set => _tagEnvironment = value.ToLower(); }
        
    }
    
}