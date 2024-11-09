using GymAppBackend.Application.Abstractions;
using GymAppBackend.Application.Responses;
using GymAppBackend.Application.Security;
using GymAppBackend.Application.Security.DTO;
using GymAppBackend.Application.Users.Commands;
using GymAppBackend.Application.Users.Commands.SignIn;
using GymAppBackend.Application.Users.Commands.SignUp;
using GymAppBackend.Application.Users.Queries.DTO;
using GymAppBackend.Application.Users.Queries.GetUser;
using GymAppBackend.Application.Users.Queries.GetUsers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymAppBackend.Api.Controllers;

[ApiController]
[Route("users")]
public class UsersController : ControllerBase
{
    private readonly ICommandHandler<SignUpCommand, CreateOrUpdateResponse> _signUpHandler;
    private readonly IQueryHandler<GetUserQuery, UserDto> _getUserHandler;
    private readonly IQueryHandler<GetUsersQuery, IEnumerable<UserDto>> _getUsersHandler;
    private readonly ICommandHandler<SignInCommand, JwtDto> _signInHandler;

    public UsersController(
        ICommandHandler<SignUpCommand, CreateOrUpdateResponse> signUpHandler,
        IQueryHandler<GetUserQuery, UserDto> getUserHandler,
        IQueryHandler<GetUsersQuery, IEnumerable<UserDto>> getUsersHandler,
        ICommandHandler<SignInCommand, JwtDto> signInHandler)
    {
        _signUpHandler = signUpHandler;
        _getUserHandler = getUserHandler;
        _getUsersHandler = getUsersHandler;
        _signInHandler = signInHandler;
    }

    [HttpPost("sign-up")]
    public async Task<IActionResult> Post([FromBody] SignUpCommand command)
    {
        var response = await _signUpHandler.HandleAsync(command);
        return CreatedAtAction(nameof(Get), new { id = response.Id }, response.Id);
    }

    [HttpPost("sign-in")]
    public async Task<ActionResult<JwtDto>> Post([FromBody] SignInCommand command)
    {
        var response = await _signInHandler.HandleAsync(command);
        return Ok(response);
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<ActionResult<UserDto>> Get()
    {
        if (string.IsNullOrWhiteSpace(User.Identity?.Name))
        {
            return NotFound();
        }

        var userId = Guid.Parse(User.Identity?.Name);
        var user = await _getUserHandler.HandleAsync(new GetUserQuery() {Id = userId});

        return user;
    }

    [Authorize(Policy = "is-admin")]
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<UserDto>> Get(Guid id)
    {
        return await _getUserHandler.HandleAsync(new GetUserQuery{Id = id});
    }
}