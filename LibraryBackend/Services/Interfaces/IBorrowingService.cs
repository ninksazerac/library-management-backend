using LibraryBackend.DTOs.Borrowing;

namespace LibraryBackend.Services.Interfaces;

public interface IBorrowingService
{
    Task<BorrowingResponseDto> BorrowBookAsync(
        BorrowBookRequestDto request);

    Task<BorrowingResponseDto> ReturnBookAsync(
        int borrowingId);
}