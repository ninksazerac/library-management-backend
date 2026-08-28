using LibraryBackend.DTOs.Authentication;

namespace LibraryBackend.Services.Interfaces;

public interface IAuthenticationService
{
    Task<LoginResponseDto?> LoginAsync(LoginRequestDto request);
}