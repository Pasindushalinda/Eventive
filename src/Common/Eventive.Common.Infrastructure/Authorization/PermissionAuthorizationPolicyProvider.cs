using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
namespace Eventive.Common.Infrastructure.Authorization;

//If the required policy isn’t predefined, the PermissionAuthorizationPolicyProvider
//dynamically creates it based on the requested permission.This provider checks if
//the policy already exists; if not, it generates a new policy with the required PermissionRequirement

//This class inherits from DefaultAuthorizationPolicyProvider, allowing you to override its behavior

//Example Usage: Imagine you have a set of dynamic permissions like
//"ReadReports", "EditReports", "DeleteReports", etc. Instead of defining each policy manually in
//your Startup.cs or Program.cs, you can simply use the PermissionAuthorizationPolicyProvider to
//handle these dynamically
internal sealed class PermissionAuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider
{
    private readonly AuthorizationOptions _authorizationOptions;

    //The constructor takes AuthorizationOptions, passing them to the base class constructor
    //and storing them in a private field for later use
    public PermissionAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options)
        : base(options)
    {
        _authorizationOptions = options.Value;
    }

    public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        //Checks if the policy already exists using the base method
        AuthorizationPolicy? policy = await base.GetPolicyAsync(policyName);

        if (policy is not null)
        {
            return policy;
        }

        //If the policy does not exist, it creates a new AuthorizationPolicy
        //with a PermissionRequirement based on the policy name
        AuthorizationPolicy permissionPolicy = new AuthorizationPolicyBuilder()
            .AddRequirements(new PermissionRequirement(policyName))
            .Build();

        _authorizationOptions.AddPolicy(policyName, permissionPolicy);

        return permissionPolicy;
    }
}

