using Application.Users.Create;
using Application.Users.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Create(
        CreateUserService createUserService,
        CreateUserRequest request,
        CancellationToken ct)
    {
        var response = await createUserService.ExecuteAsync(request, ct);
        return Ok(response);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(
        LoginUserService loginUserService,
        LoginUserRequest request,
        CancellationToken ct)
    {
        var response = await loginUserService.ExecuteAsync(request, ct);
        return Ok(response);
    }
}
