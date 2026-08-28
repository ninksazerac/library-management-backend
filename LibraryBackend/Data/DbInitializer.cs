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
            u => u.Username == "user1"))
        {
            context.Users.Add(new User
            {
                Username = "user1",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("User@111"),
                Role = "EndUser"
            });
        }

        if (!await context.Users.AnyAsync(
            u => u.Username == "user2"))
        {
            context.Users.Add(new User
            {
                Username = "user2",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("User@222"),
                Role = "EndUser"
            });
        }

        await context.SaveChangesAsync();

        await context.SaveChangesAsync();
    }
}