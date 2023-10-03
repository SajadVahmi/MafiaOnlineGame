using AutoMapper;
using FluentValidation;
using IDP.Administration.Api.Users.Models;
using IDP.Administration.Services.Users.Dtos;
using IDP.Administration.Services.Users.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;

namespace IDP.Administration.Api.Users.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserServices _userServices;
        private readonly IMapper _mapper;

        public UsersController(IUserServices userServices, IMapper mapper)
        {
            _userServices = userServices;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserRequestBody requestBody,
            CancellationToken cancellationToken = default)
        {

            var createUserModel = _mapper.Map<CreateUserDto>(requestBody);

            (IdentityResult result, string userId) createUserResult = await _userServices.CreateUserAsync(createUserModel, cancellationToken);

            if (createUserResult.result.Succeeded)
                return Created(string.Empty, createUserResult.userId);

            else return BadRequest(createUserResult.result.Errors);

        }
    }
}
