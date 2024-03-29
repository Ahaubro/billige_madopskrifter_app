﻿using billige_madopskrifter.Service;
using billige_madopskrifter.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace billige_madopskrifter.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private IUserService _userService;

        //Constructor
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // Authenticate
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateRequestDto model)
        {
            var response = await _userService.Authenticate(model);

            if (response == null)
                return new NotFoundResult();

            return new OkObjectResult(response);
        }

        // Create user
        [Produces("application/json")]
        [HttpPost]
        public async Task<CreateUserResponseDto> Create([FromBody] CreateUserRequestDto dto)
        {
            return await _userService.Create(dto);
        }

        // Get user by id
        [Produces("application/json")]
        [HttpGet("{id:int}")]
        public async Task<GetUserByIdResponseDto> GetById(int id)
        {
            return await _userService.GetById(id);
        }
    }
}
