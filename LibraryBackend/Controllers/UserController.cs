using LibraryBackend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryBackend.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [Authorize(Roles = "Administrator")]
    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var result = await _userService.GetAllAsync();

        return Ok(result);
    }
}