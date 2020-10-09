using Web.Api.Models;
using FluentValidation;

namespace Web.Api.Models.Validators
{
    public class StandUpNetworkModelValidator : AbstractValidator<StandUpNetworkModel>
    {
        //Optional mask at end: /xyz
    // private const string IpWithSubnetRegex = @"^([0-9]{1,3}\.){3}[0-9]{1,3}(\/([0-9]|[1-2][0-9]|3[0-2]))?$";
    //Not optional mask
    // private const string IpWithSubnetRegex = @"^([0-9]{1,3}\.){3}[0-9]{1,3}(\/([0-9]|[1-2][0-9]|3[0-2]))$";

    // private const string IpRegex = @"^(?:(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[1-9]?[0-9])(\.(?!$)|$)){4}$";
    public StandUpNetworkModelValidator()
    {
        //ToDo: additional validators will be added after we lock on parameters
        
        // names are 3-4 chars long. This is a limitation because this feeds
        //into vm names which have a 15 char limit and the pipeline appends info to the name.
        //RuleFor(x => x.name).NotEmpty().MinimumLength(3).MaximumLength(4);
        //RuleFor(x => x.ResourceGroupName).NotEmpty().MinimumLength(6).MaximumLength(7);
        //RuleFor(x => x.RegionId).NotEmpty().MinimumLength(3).MaximumLength(20);
        //RuleFor(x => x.SubscriptionId).NotEmpty();
        //RuleFor(x => x.VnetAddressSpace).NotEmpty().Matches(IpWithSubnetRegex);
        //RuleFor(x => x.VnetSubnetAddressSpace).NotEmpty().Matches(IpWithSubnetRegex);
        //RuleFor(x => x.VmSize).NotEmpty();
        //Ensure >0
        //RuleFor(x => x.WorkerVmInstances).GreaterThanOrEqualTo(1);

    }
    }
}
