using LibraryBackend.DTOs.Borrowing;

namespace LibraryBackend.Services.Interfaces;

public interface IBorrowingService
{
    Task<BorrowingResponseDto> BorrowBookAsync(
    int userId,
    BorrowBookRequestDto request);
    Task<BorrowingResponseDto> AssignBookAsync(
        AssignBookRequestDto request);
    Task<BorrowingResponseDto> ReturnBookAsync(
        int borrowingId);
    Task<BorrowingResponseDto> ProcessReturnAsync(
        int borrowingId);
}