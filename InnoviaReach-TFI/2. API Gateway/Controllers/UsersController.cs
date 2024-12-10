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
using System.Security.Claims;
using Core.Domain.Response.Gateway;
using _3._Core.Services;
using Microsoft.AspNetCore.Localization;
using Core.Domain.Request.Gateway;
using Core.Domain.Request;

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
        private readonly IUsuarioBaneadoService _usuarioBaneadoService;
        private readonly IPrivilegesService _privilegesService;
        private readonly ISteamAccountService _steamAccountService;
        private string _userId
        {
            get
            {
                var data = User.FindFirstValue(ClaimTypes.NameIdentifier);
                return data;
            }
        }

        public UsersController(
            IMapper mapper,
            ILogger<UserManagementController> logger,
            IUsersPrivilegesService userPrivilegesService,
            IUsersService usersService,
            IPrivilegesService privilegesService,
            ISteamAccountService steamAccountService,
            IUsuarioBaneadoService usuarioBaneadoService)
        {
            _mapper = mapper;
            _logger = logger;
            _userPrivilegesService = userPrivilegesService;
            _usersService = usersService;
            _privilegesService = privilegesService;
            _steamAccountService = steamAccountService;
            _usuarioBaneadoService = usuarioBaneadoService;
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
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
        [HttpGet("ValidarSteamAccount")]
        [AllowAnonymous]
        public async Task<IActionResult> ValidarSteamAccount()
        {
            try
            {
                string userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var result = await _steamAccountService.ValidarSteamAccount(userid);
                if (result is null)
                {
                    return Problem("No se encuentran usuarios");
                }
                else
                {
                    var response = _mapper.Map<SteamAccountResponse>(result);
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener usuarios" + ex.Message);
                return Problem(ex.Message);
            }
        }
        [HttpGet("ObtenerUsuario")]
        public async Task<IActionResult> ObtenerUsuario(string UserName)
        {
            try
            {
                var resultado = await _usersService.GetUserAsync(UserName);
                var response = _mapper.Map<UserConfigResponse>(resultado);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener el usuario {UserName}");
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("Culture/Set")]
        public async Task<IActionResult> Set([FromBody] CultureRequest cultureRequest)
        {
            if (_userId is not null)
            {
                await _usersService.UpdateCulture(cultureRequest.culture, _userId);
            }

            return Ok(cultureRequest.redirectUri);
        }

        [AllowAnonymous]
        [HttpGet("Culture")]
        public async Task<IActionResult> Get([FromQuery] CultureRequest cultureRequest)
        {
            if (cultureRequest.culture != null)
            {
                HttpContext.Response.Cookies.Append(
                    CookieRequestCultureProvider.DefaultCookieName,
                    CookieRequestCultureProvider.MakeCookieValue(
                        new RequestCulture(cultureRequest.culture, cultureRequest.culture)));
            }

            return Redirect(cultureRequest.redirectUri);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("BanearUsuario")]
        public async Task<IActionResult> BanearUsuario(UsuarioBaneadoRequest usuarioBaneadoRequest)
        {
            try
            {
                string userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
                usuarioBaneadoRequest.UserAdmin_ID = userid;
                await _usuarioBaneadoService.BanearUsuario(usuarioBaneadoRequest);
                return Ok();
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, $"Error intentando banear al usuario {usuarioBaneadoRequest.UserName}");
                return BadRequest(ex.Message);
            }
            
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("DesbanearUsuario")]
        public async Task<IActionResult> DesbanearUsuario(UsuarioDesbanRequest usuarioDesbanRequest)
        {
            try
            {
                await _usuarioBaneadoService.DesbanearUsuario(usuarioDesbanRequest.UserName);
                return Ok();
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, $"Error intentando desbanear al usuario {usuarioDesbanRequest.UserName}");
                return BadRequest(ex.Message);
            }
            
        }
    }
}
