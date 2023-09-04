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

namespace Api.Controllers
{
    //[Authorize]
    [Produces("application/json")]
    [ApiController]
    [Route("[controller]")]
    public class UserManagementController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<UserManagementController> _logger;
        private readonly IUsersPrivilegesService _userPrivilegesService;
        private readonly IUsersService _usersService;
        private readonly IPrivilegesService _privilegesService;
        private readonly ActionLoggerMiddlewareConfiguration _actionLoggerMiddlewareConfiguration;

        public UserManagementController(
            IMapper mapper,
            ILogger<UserManagementController> logger,
            IUsersPrivilegesService userPrivilegesService,
            IUsersService usersService,
            IPrivilegesService privilegesService,
            ActionLoggerMiddlewareConfiguration actionLoggerMiddlewareConfiguration)
        {
            _mapper = mapper;
            _logger = logger;
            _userPrivilegesService = userPrivilegesService;
            _usersService = usersService;
            _privilegesService = privilegesService;
            _actionLoggerMiddlewareConfiguration = actionLoggerMiddlewareConfiguration;
        }

        [HttpPost]
        //[Authorize(Roles = "Administrador")]
        [AllowAnonymous]
        public async Task<IActionResult> CreatePrivilege(PrivilegesPostRequest privilegesRequest)
        {
            Privileges privileges = _mapper.Map<Privileges>(privilegesRequest);
            var result = await _privilegesService.CreatePrivilegeAsync(privileges);
            if (!result)
            {
                return Problem("Error al crear el rol.");
            }
            _logger.LogInformation($"Registered privilege: {privileges.Id} - {privileges.NormalizedName} succesfully.");
            return Ok();
        }

        [HttpDelete]
        [Authorize(Roles = "Administrador")]
        //[AllowAnonymous]
        public async Task<IActionResult> DeletePrivilege([FromBody] string id)
        {
            try
            {
                var result = await _privilegesService.DeletePrivilegeAsync(id);
                if (!result)
                {
                    return Problem("Error al eliminar el rol.");
                }
                _logger.LogInformation($"Deleted privilege: {id} succesfully.");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in DeletePrivileges"+ ex.Message);
                return Problem(ex.Message);
            }           
        }

        [HttpPut("Privileges")]
        [Authorize(Roles = "Administrador")]
        //[AllowAnonymous]
        public async Task<IActionResult> UpdatePrivilege(PrivilegesPutRequest privilegesRequest)
        {
            try
            {
                if (privilegesRequest.Id == "3bdb5928-e28e-4848-8969-e6aae1a8893b")
                    throw new Exception("No se puede editar el rol ADMINISTRADOR.");
                Privileges privileges = _mapper.Map<Privileges>(privilegesRequest);

                if (await _privilegesService.UpdatePrivilegeAsync(privileges) == null)
                {
                    return Problem("Error al actualizar el rol.");
                }
                _logger.LogInformation($"Updated privilege: {privileges.Id} - {privileges.NormalizedName} succesfully.");
                return Ok();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("Privileges")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> GetPrivileges()
        {
            try
            {
                var privileges = await _privilegesService.GetPrivilegesAsync();

                return Ok(privileges);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut("AssignPrivilegesToUser/{id}")]
        [Authorize(Roles = "Admin")]
        //[AllowAnonymous]
        public async Task<IActionResult> AssignPrivilegesToUser(string id, [FromBody]List<string> privilegesNames)
        {
            try
            {
                var result = await _userPrivilegesService.AssignPrivilegesToUser(id, privilegesNames);
                if (!result)
                {
                    return Problem("Error al asignar los privilegios al usuario.");
                }
                return Ok();
            }
            catch (Exception)
            {

                throw;
            }
        }
        
        
        [HttpGet("Privileges/{id}")]
        [Authorize(Roles = "Administrador")]
        //[AllowAnonymous]
        public async Task<IActionResult> GetPrivilegeById(string id)
        {
            try
            {
                var privilege = await _privilegesService.GetPrivilegeByIdAsync(id);

                if(privilege == null)
                    return Problem("No existe el privilegio.");

                return Ok(privilege);
            }
            catch (Exception)
            {

                throw;
            }
        }


        [HttpGet("Users")]
        //[Authorize(Roles = "Administrador")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var users = await _usersService.GetUsersAsync();

                return Ok(users);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("UserPrivileges/{id}")]
        [Authorize(Roles = "Admin")]
        //[AllowAnonymous]
        public async Task<IActionResult> GetUserPrivileges(string id)
        {
            try
            {
                var result = await _userPrivilegesService.GetUserPrivileges(id);
                if (result == null)
                    return Problem("Error al obtener los privilegios.");
               
                return Ok(result);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
