namespace LibraryBackend.Services.Interfaces;

public interface IAuthorizationService
{
    bool IsAdministrator(string role);
    bool IsEndUser(string role);
}