using Api.Request;
using AutoMapper;
using Core.Business.Services;
using Core.Contracts.Data;
using Core.Contracts.Services;
using Core.Domain.ApplicationModels;
using Core.Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using Transversal.Extensions;
using Transversal.Helpers.JWT;

namespace Api.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<AuthController> _logger;
        private readonly IUsersPrivilegesService _privilegesService;
        private readonly IUsersService _usersService;
        private readonly ActionLoggerMiddlewareConfiguration _actionLoggerMiddlewareConfiguration;
        private readonly IJwtBearerTokenHelper _jwtBearerTokenHelper;
        private readonly UserManager<Users> _userManager;

        public AuthController(
            IMapper mapper,
            ILogger<AuthController> logger,
            IUsersService usersService,
            IUsersPrivilegesService privilegesService,
            ActionLoggerMiddlewareConfiguration actionLoggerMiddlewareConfiguration,
            IJwtBearerTokenHelper jwtBearerTokenHelper,
            UserManager<Users> userManager)
        {
            _mapper = mapper;
            _logger = logger;
            _usersService = usersService;
            _privilegesService = privilegesService;
            _actionLoggerMiddlewareConfiguration = actionLoggerMiddlewareConfiguration;
            _jwtBearerTokenHelper = jwtBearerTokenHelper;
            _userManager=userManager;
        }


        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterRequest registerRequest)
        {
            try
            {
                Users user = _mapper.Map<Users>(registerRequest);
                var result = await _usersService.CreateUserAsync(user, registerRequest.Password);
                if (!result.Data.IsRegistred)
                {
                    _logger.LogInformation("Failed to create new user {errors}", result.Errors.ToJson());

                    if (result.Data.Code.Any(e => (e == "DuplicateUserName" || e == "DuplicateEmail")))
                    {
                        return Conflict(new { Message = "Duplicated User Or Duplicated Email", Errors = ModelState.SerializeErrors() });
                    }

                    return Conflict(new { Message = "User Registration Failed", Errors = ModelState.SerializeErrors() });
                }


                _logger.LogInformation($"New user with UserName: {registerRequest.FirstName + string.Empty + registerRequest.LastName} has been registered succesfully.");

                return Ok(user.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while registering new user.");
                return BadRequest(ex.Message);
            }
        }

        //create login endpoint
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            try
            {
                var result = await _usersService.LoginUserAsync(loginRequest.username, loginRequest.password);

                if (!result.Success)
                {
                    switch (result.Errors.First())
                    {
                        case "InvalidCredentials":
                            return BadRequest(new { Message = "El usuario y/o contraseña es incorrecto" });
                        case "TokenError":
                            return StatusCode(500, "Bearer token couldn't be created");
                        case "UserNotExist":
                            return BadRequest(new { Message = "El usuario no existe" });
                        case "UserNotConfirmed":
                            return BadRequest(new { Message = "El usuario no confirmó su email" });
                    }
                }

                _logger.LogInformation("{userName} has logged in", result.Data.UserName);

                if (result.Issues.Count > 0)
                {
                    _logger.LogInformation("{ServiceName}.{MethodName} failed. {Errors}",
                        nameof(RefreshTokenService),
                        result.Issues);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while logging in.");
                return BadRequest(ex.Message);
            }

        }

        [AllowAnonymous]
        [HttpGet]
        [Route("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token))
                {
                    ModelState.AddModelError(string.Empty, $"{nameof(userId)} and {nameof(token)} are required.");
                    return BadRequest(ModelState.SerializeErrors());
                }

                var result = await _usersService.ConfirmUserEmailAsync(userId, token);
                if (!result.Succeeded)
                {
                    _logger.LogInformation("Email confirmation failed: {errors}", result.Errors.ToJson());
                    ModelState.AddIdentityResultErrors(result);
                    return BadRequest(new { Message = "Email confirmation failed.", Errors = ModelState.SerializeErrors() });
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(string userName)
        {
            try
            {
                await _usersService.ForgotPasswordGenerateToken(userName);
                return Ok();
            }
            catch (Exception)
            { 
                throw;
            }
           
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest model)
        {
            try
            {
                var modelDto=_mapper.Map<ChangePasswordDto>(model);
                await _usersService.ChangePasswordGenerateToken(modelDto);
                return Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("Refresh-Token")]
        public async Task<IActionResult> RefreshToken(RefreshTokenRequest refreshTokenModel)
        {
            try
            {
                if (string.IsNullOrEmpty(refreshTokenModel?.BearerToken) || string.IsNullOrEmpty(refreshTokenModel?.RefreshToken))
                {
                    return BadRequest($"{nameof(refreshTokenModel.BearerToken)} and {nameof(refreshTokenModel.RefreshToken)} are required.");
                }

                // Corroboro que el bearer token tenga un formato jwt válido
                if (!_jwtBearerTokenHelper.IsValidJwt(refreshTokenModel.BearerToken))
                {
                    return BadRequest($"{nameof(refreshTokenModel.BearerToken)} is not a valid Json Web Token.");
                }

                // El bearer token tiene que haber expirado para generar uno nuevo
                if (!_jwtBearerTokenHelper.IsExpired(refreshTokenModel.BearerToken))
                {
                    return BadRequest($"{nameof(refreshTokenModel.BearerToken)} must be expired for this request.");
                }

                // Al validar el bearer token ignoro la fecha de expiración ya que es necesario que haya expirado
                var jwtValidationResult = _jwtBearerTokenHelper.ValidateJwtToken(refreshTokenModel.BearerToken, validateExpiration: false);
                if (!jwtValidationResult.Success)
                {
                    return BadRequest(jwtValidationResult.Errors);
                }

                var user = await _userManager.GetUserAsync(User);
                var result = await _usersService.GenerateRefreshToken(user, refreshTokenModel.RefreshToken);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(1,ex,"Ocurrió un error en Endpoint RefreshToken, Error " + ex.Message);
                throw;
            }
        }


        #region Helpers

        #endregion
    }
}