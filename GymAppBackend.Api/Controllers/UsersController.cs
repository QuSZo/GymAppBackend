using GymAppBackend.Application.Abstractions;
using GymAppBackend.Application.Responses;
using GymAppBackend.Application.Users.Commands;
using GymAppBackend.Application.Users.Queries.DTO;
using GymAppBackend.Application.Users.Queries.GetUser;
using GymAppBackend.Application.Users.Queries.GetUsers;
using Microsoft.AspNetCore.Mvc;

namespace GymAppBackend.Api.Controllers;

[ApiController]
[Route("users")]
public class UsersController : ControllerBase
{
    private readonly ICommandHandler<SignUpCommand, CreateOrUpdateResponse> _signUpHandler;
    private readonly IQueryHandler<GetUserQuery, UserDto> _getUserHandler;
    private readonly IQueryHandler<GetUsersQuery, IEnumerable<UserDto>> _getUsersHandler;

    public UsersController(
        ICommandHandler<SignUpCommand, CreateOrUpdateResponse> signUpHandler,
        IQueryHandler<GetUserQuery, UserDto> getUserHandler,
        IQueryHandler<GetUsersQuery, IEnumerable<UserDto>> getUsersHandler)
    {
        _signUpHandler = signUpHandler;
        _getUserHandler = getUserHandler;
        _getUsersHandler = getUsersHandler;
    }

    [HttpPost("sign-up")]
    public async Task<IActionResult> Post([FromBody] SignUpCommand command)
    {
        var response = await _signUpHandler.HandleAsync(command);
        return CreatedAtAction(nameof(Get), new { id = response.Id }, response.Id);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<UserDto>> Get(Guid id)
    {
        return await _getUserHandler.HandleAsync(new GetUserQuery{Id = id});
    }
}