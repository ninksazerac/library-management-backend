using LibraryBackend.Services.Interfaces;
public class AuthorizationService : IAuthorizationService
{
    public bool IsAdministrator(string role)
    {
        return role == "Administrator";
    }

    public bool IsEndUser(string role)
    {
        return role == "EndUser";
    }
}