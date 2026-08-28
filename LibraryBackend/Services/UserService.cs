using LibraryBackend.Models;
using LibraryBackend.Repositories.Interfaces;
using LibraryBackend.Services.Interfaces;

namespace LibraryBackend.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await _userRepository.GetAllAsync();
    }
}