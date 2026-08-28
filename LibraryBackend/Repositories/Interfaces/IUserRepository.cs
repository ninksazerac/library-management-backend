using LibraryBackend.Models;

namespace LibraryBackend.Repositories.Interfaces;
public interface IUserRepository
{
    Task<User?> GetByUsernameAsync(string username);

    Task<User?> GetByIdAsync(int userId);
}