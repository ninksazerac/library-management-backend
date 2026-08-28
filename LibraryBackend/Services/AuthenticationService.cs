using LibraryBackend.DTOs.Authentication;
using LibraryBackend.Repositories.Interfaces;
using LibraryBackend.Services.Interfaces;
public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;

    public AuthenticationService(
        IUserRepository userRepository,
        ITokenService tokenService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
    }

    public async Task<LoginResponseDto?> LoginAsync(
        LoginRequestDto request)
    {
        var user = await _userRepository.GetByUsernameAsync(request.Username);
        // * Get username not match
        if (user == null)
        {
            return null;
        }

        if (!BCrypt.Net.BCrypt.Verify(
            request.Password,
            user.PasswordHash))
        {
            return null;
        }

        var token = _tokenService.GenerateToken(
            user.UserID,
            user.Username,
            user.Role);

        var response = new LoginResponseDto
        {
            Token = token,
            Username = user.Username,
            Role = user.Role
        };

        return response;

    }
}