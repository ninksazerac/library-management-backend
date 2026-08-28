using LibraryBackend.Configuration;
using LibraryBackend.DTOs.Borrowing;
using LibraryBackend.Models;
using LibraryBackend.Exceptions;
using LibraryBackend.Repositories.Interfaces;
using LibraryBackend.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace LibraryBackend.Services;

public class BorrowingService : IBorrowingService
{
    private readonly IBorrowingRepository _borrowingRepository;
    private readonly IUserRepository _userRepository;
    private readonly IBookRepository _bookRepository;
    private readonly BorrowingSettings _borrowingSettings;

    public BorrowingService(
        IBorrowingRepository borrowingRepository,
        IUserRepository userRepository,
        IBookRepository bookRepository,
        IOptions<BorrowingSettings> borrowingOptions)
    {
        _borrowingRepository = borrowingRepository;
        _userRepository = userRepository;
        _bookRepository = bookRepository;
        _borrowingSettings = borrowingOptions.Value;
    }

    public async Task<BorrowingResponseDto> BorrowBookAsync(
        int userId,
        BorrowBookRequestDto request)
    {
        var user = await _userRepository
            .GetByIdAsync(userId);

        if (user == null)
        {
            throw new NotFoundException(
                $"User with ID {userId} was not found.");
        }

        var book = await _bookRepository
            .GetByIdAsync(request.BookID);

        if (book == null)
        {
            throw new NotFoundException(
                $"Book with ID {request.BookID} was not found.");
        }

        if (book.AvailabilityStatus != "Available")
        {
            throw new ConflictException(
                "The book is not available for borrowing.");
        }

        var now = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(
            DateTime.UtcNow,
            "SE Asia Standard Time");

        var borrowing = new Borrowing
        {
            UserID = userId,
            BookID = request.BookID,
            BorrowedAt = now,
            DueAt = now.AddDays(_borrowingSettings.LoanPeriodDays),
            Status = "Borrowed"
        };

        book.AvailabilityStatus = "Borrowed";

        var createdBorrowing =
            await _borrowingRepository.AddAsync(borrowing);

        await _bookRepository.UpdateAsync(book);

        return MapToResponse(createdBorrowing);
    }

    public async Task<BorrowingResponseDto> AssignBookAsync(
    AssignBookRequestDto request)
    {
        var user = await _userRepository
            .GetByIdAsync(request.UserID);

        if (user == null)
        {
            throw new NotFoundException(
                $"User with ID {request.UserID} was not found.");
        }

        var book = await _bookRepository
            .GetByIdAsync(request.BookID);

        if (book == null)
        {
            throw new NotFoundException(
                $"Book with ID {request.BookID} was not found.");
        }

        if (book.AvailabilityStatus != "Available")
        {
            throw new ConflictException(
                "The book is not available for assigning.");
        }

        var now = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(
            DateTime.UtcNow,
            "SE Asia Standard Time");

        var borrowing = new Borrowing
        {
            UserID = request.UserID,
            BookID = request.BookID,
            BorrowedAt = now,
            DueAt = now.AddDays(
                _borrowingSettings.LoanPeriodDays),
            Status = "Borrowed"
        };

        book.AvailabilityStatus = "Borrowed";

        var createdBorrowing =
            await _borrowingRepository.AddAsync(borrowing);

        await _bookRepository.UpdateAsync(book);

        return MapToResponse(createdBorrowing);
    }
    public async Task<BorrowingResponseDto> ReturnBookAsync(
    int borrowingId)
    {
        var borrowing = await _borrowingRepository
            .GetByIdAsync(borrowingId);

        if (borrowing == null)
        {
            throw new NotFoundException(
                $"Borrowing with ID {borrowingId} was not found.");
        }

        if (borrowing.Status == "Returned")
        {
            throw new ConflictException(
                "This book has already been returned.");
        }

        var book = await _bookRepository
            .GetByIdAsync(borrowing.BookID);

        if (book == null)
        {
            throw new NotFoundException(
                $"Book with ID {borrowing.BookID} was not found.");
        }

        var now = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(
            DateTime.UtcNow,
            "SE Asia Standard Time");

        borrowing.ReturnedAt = now;
        borrowing.Status = "Returned";

        book.AvailabilityStatus = "Available";

        await _borrowingRepository.UpdateAsync(borrowing);
        await _bookRepository.UpdateAsync(book);

        return MapToResponse(borrowing);
    }

    public async Task<BorrowingResponseDto> ProcessReturnAsync(
        int borrowingId)
    {
        var borrowing =
            await _borrowingRepository
                .GetByIdAsync(borrowingId);

        if (borrowing == null)
        {
            throw new NotFoundException(
                $"Borrowing with ID {borrowingId} was not found.");
        }

        if (borrowing.Status == "Returned")
        {
            throw new ConflictException(
                "This book has already been returned.");
        }

        var book =
            await _bookRepository
                .GetByIdAsync(borrowing.BookID);

        if (book == null)
        {
            throw new NotFoundException(
                $"Book with ID {borrowing.BookID} was not found.");
        }

        var now =
            TimeZoneInfo.ConvertTimeBySystemTimeZoneId(
                DateTime.UtcNow,
                "SE Asia Standard Time");

        borrowing.ReturnedAt = now;
        borrowing.Status = "Returned";

        book.AvailabilityStatus = "Available";

        await _borrowingRepository
            .UpdateAsync(borrowing);

        await _bookRepository
            .UpdateAsync(book);

        return MapToResponse(borrowing);
    }
    private BorrowingResponseDto MapToResponse(Borrowing borrowing)
    {
        return new BorrowingResponseDto
        {
            BorrowingID = borrowing.BorrowingID,
            UserID = borrowing.UserID,
            BookID = borrowing.BookID,
            BorrowedAt = borrowing.BorrowedAt,
            DueAt = borrowing.DueAt,
            ReturnedAt = borrowing.ReturnedAt,
            Status = borrowing.Status,
        };
    }
}
