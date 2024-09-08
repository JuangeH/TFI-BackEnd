using AutoMapper;
using Core.Contracts.Data;
using Core.Contracts.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Request;
using Api.Request.Privileges;
using Core.Domain.ApplicationModels;
using Core.Domain.Helper;
using Core.Domain.Response.Business;

namespace Api.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [ApiController]
    [Route("[controller]")]

    public class UsersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<UserManagementController> _logger;
        private readonly IUsersPrivilegesService _userPrivilegesService;
        private readonly IUsersService _usersService;
        private readonly IPrivilegesService _privilegesService;

        public UsersController(
            IMapper mapper,
            ILogger<UserManagementController> logger,
            IUsersPrivilegesService userPrivilegesService,
            IUsersService usersService,
            IPrivilegesService privilegesService)
        {
            _mapper = mapper;
            _logger = logger;
            _userPrivilegesService = userPrivilegesService;
            _usersService = usersService;
            _privilegesService = privilegesService;
        }

        [HttpDelete]
        //[Authorize(Roles = "Admin")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteUser(string id)
        {
            try
            {
                var result = await _usersService.DeleteUserAsync(id);
                if (!result)
                {
                    return Problem("Error al eliminar el usuario.");
                }
                _logger.LogInformation($"Deleted privilege: {id} succesfully.");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in DeleteUser" + ex.Message);
                return Problem(ex.Message);
            }
        }

        [HttpGet("ObtenerUsuarios")]
        [AllowAnonymous]
        public async Task<IActionResult> ObtenerUsuarios()
        {
            try
            {
                var result = await _usersService.GetAllAsync();
                if (result is null)
                {
                    return Problem("No se encuentran usuarios");
                }
                else
                {
                    var response = _mapper.Map<List<UserResponse>>(result);
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener usuarios" + ex.Message);
                return Problem(ex.Message);
            }
        }
    }
}
