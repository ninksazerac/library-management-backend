using LibraryBackend.Models;

namespace LibraryBackend.Services.Interfaces;

public interface IUserService
{
    Task<List<User>> GetAllAsync();
}