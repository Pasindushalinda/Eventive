namespace Eventive.Common.Infrastructure.Authentication;

//Custom Claims: You define custom claims like "sub" for the user identifier
//and "permission" for user permissions. This allows you to store and retrieve
//additional information about the user, such as their roles and permissions.
public static class CustomClaims
{
    //the "sub" claim, which is typically used to store the subject identifier
    public const string Sub = "sub";

    //the "permission" claim, used to store user permissions
    public const string Permission = "permission";
}
