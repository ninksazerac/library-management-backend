using LibraryBackend.Models;

namespace LibraryBackend.Repositories.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByUsernameAsync(string username);
    Task<List<User>> GetAllAsync();
    Task<User?> GetByIdAsync(int userId);
}