using LibraryBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryBackend.Data;

public static class DbInitializer
{
    public static async Task InitializeAsync(AppDbContext context)
    {
        if (!await context.Users.AnyAsync(
        u => u.Username == "admin"))
        {
            context.Users.Add(new User
            {
                Username = "admin",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
                Role = "Administrator"
            });
        }

        if (!await context.Users.AnyAsync(
            u => u.Username == "user"))
        {
            context.Users.Add(new User
            {
                Username = "user",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("User@123"),
                Role = "EndUser"
            });
        }

        await context.SaveChangesAsync();

        await context.SaveChangesAsync();
    }
}