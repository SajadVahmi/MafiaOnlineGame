using AutoMapper;
using IDP.Administration.Api.V1.Users.Models;
using IDP.Administration.Services.Users.Dto;
using IDP.Administration.Services.Users.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IDP.Administration.Api.V1.Users.Controllers;

[ApiController]
[Route("v1/users")]
public class UsersController(IUserServices userServices, IMapper mapper) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserRequestBody requestBody,
        CancellationToken cancellationToken = default)
    {

        var createUserModel=mapper.Map<CreateUserDto>(requestBody);

        (IdentityResult result,string userId) createUserResult=await userServices.CreateUserAsync(createUserModel, cancellationToken);

        if (createUserResult.result.Succeeded)
            return Created(string.Empty, createUserResult.userId);

        else return BadRequest(createUserResult.result.Errors);

    }
}