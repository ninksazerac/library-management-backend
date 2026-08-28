namespace LibraryBackend.DTOs.Search;
public class SearchBooksRequestDto
{
    public string? Title { get; set; }
    public string? Author { get; set; }
    public string? ISBN { get; set; }
    public string? Category { get; set; }
    public string? AvailabilityStatus { get; set; }
}