using LibraryBackend.Models;

namespace LibraryBackend.Data;

public static class DbInitializer
{
    public static async Task InitializeAsync(AppDbContext context)
    {
        if (context.Users.Any())
            return;

        var hash = BCrypt.Net.BCrypt.HashPassword("Admin@123");

        var user = new User
        {
            Username = "admin",
            PasswordHash = hash,
            Role = "Administrator"
        };

        context.Users.Add(user);

        await context.SaveChangesAsync();
    }
}