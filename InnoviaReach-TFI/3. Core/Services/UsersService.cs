using Core.Contracts.Repositories;
using Core.Contracts.Services;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Transversal.Helpers.ResultClasses;
using Transversal.Helpers.JWT;
using Core.Domain.Enum;
using Core.Domain.ApplicationModels;
using Core.Domain.DTOs;
using AutoMapper;
using Transversal.Extensions;
using System.Security.Claims;
using System.Collections;

namespace Core.Business.Services
{
    public class UsersService : GenericService<Users>, IUsersService
    {
        private readonly UserManager<Users> _userManager;

        private readonly IJwtBearerTokenHelper _jwtBearerTokenHelper;
        private readonly ITokenGenerator _refreshTokenFactory;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;

        public UsersService(
            IUnitOfWork unitOfWork,
            UserManager<Users> userManager,
            IJwtBearerTokenHelper jwtBearerTokenHelper,
            ITokenGenerator tokenGenerator,
            IRefreshTokenService refreshTokenService,
            IMapper mapper,
            IEmailService emailService)
            : base(unitOfWork, unitOfWork.GetRepository<IUsersRepository>())
        {
            _userManager = userManager;
            _jwtBearerTokenHelper = jwtBearerTokenHelper;
            _refreshTokenFactory = tokenGenerator;
            _refreshTokenService = refreshTokenService;
            _emailService = emailService;
            _mapper = mapper;
        }
        
        public async Task<IGenericResult<RegisterDto>> CreateUserAsync(Users user, string password)
        {
            RegisterDto registerDto = new RegisterDto();
            GenericResult<RegisterDto> usuarioDto = new GenericResult<RegisterDto>();
            try
            {
                var result = await _userManager.CreateAsync(user, password);
                usuarioDto = _mapper.Map<GenericResult<RegisterDto>>(result);
                if (!result.Succeeded)
                {
                    //string Errores = string.Join("\n", result.Errors.Select(p => p.Description));
                    return usuarioDto;
                }
                var role = PrivilegeEnum.User.ToString();
                result = await _userManager.AddToRoleAsync(user, role.ToUpper());
                if (!result.Succeeded)
                {
                    await _userManager.DeleteAsync(await _userManager.FindByNameAsync(user.UserName));
                    usuarioDto.Data = registerDto;
                    return usuarioDto;
                }
                
                await _emailService.RegistrationEmailAsync(user);
                registerDto.IsRegistred = true;
                usuarioDto.Data = registerDto;
                return usuarioDto;
            }
            catch (InvalidOperationException ex) when (ex is Exception) //Si cae a esta exepción es porque no existe el rol user en la base...
            {
                await _userManager.DeleteAsync(await _userManager.FindByNameAsync(user.UserName));
                usuarioDto.Data.IsRegistred = false;
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteUserAsync(string id)
        {
            try
            {
                var user = (await _repository.Get(x => x.Id == id)).FirstOrDefault();
                if(user== null)
                {
                    return false;
                }
                user.Active = false;
                await _repository.Update(user);
                var userPrivilegesRepository = _unitOfWork.GetRepository<IUsersPrivilegesRepository>();
                var relations = (await userPrivilegesRepository.Get(x => x.UserId == user.Id)).ToList();
                relations.ForEach(x =>
                {
                    userPrivilegesRepository.Delete(x);
                });
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Users>> GetUsersAsync()
        {
            return (await _repository.Get()).ToList();            
        }       
        

        public async Task<IGenericResult<LoginTokenDto>> LoginUserAsync(string email, string password)
        {
            var identityUser = await _userManager.FindByEmailAsync(email);
            
            if (identityUser is null) return new GenericResult<LoginTokenDto>("UserNotExist");
            
            if (identityUser.EmailConfirmed /*&& identityUser.Active //Mas adelante agregar active*/)
            {
                var result = await _userManager.CheckPasswordAsync(identityUser, password);
                return result ? await GenerateLoginToken(identityUser) : new GenericResult<LoginTokenDto>("InvalidCredentials");
            }
            else
                return new GenericResult<LoginTokenDto>("UserNotConfirmed");    
        }

        public async Task<IdentityResult> ConfirmUserEmailAsync(string userId, string token)
        {
            var identityUser = await _userManager.FindByIdAsync(userId);
            return await _userManager.ConfirmEmailAsync(identityUser ?? throw new Exception("Ocurrió un error al confirmar el usuario"), token);
        }

        public async Task ForgotPasswordGenerateToken(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null || !user.EmailConfirmed /*|| !user.Active*/)
            {
                throw new Exception("El usuario no existe o su email no está confirmado");
            }

            await _emailService.ForgotPasswordSendEmail(user);
        }

        public async Task ChangePasswordGenerateToken(ChangePasswordDto changePasswordDto)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(changePasswordDto.UserId);
                if (user == null)
                {
                    throw new Exception("UserNotExist");
                }

                var result = await _userManager.ResetPasswordAsync(user, changePasswordDto.Token, changePasswordDto.Password);
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.ToString("\n"));
                }

                //await _emailService.SendPasswordChangeConfirmationEmailAsync(user);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<LoginTokenDto> GenerateRefreshToken(Users user, string token)
        {
            LoginTokenDto loginTokenDto = new LoginTokenDto();

            try
            {
                // Busco al usuario en base a los claims del bearer token
                
                if (user == null)
                {
                    throw new Exception($"Invalid bearer token claims.");
                }

                var role = _userManager.GetRolesAsync(user).Result.First();

                var newBearerToken = _jwtBearerTokenHelper.CreateJwtToken(user.Id, user.UserName, new List<string>{ role});
                var refreshToken = _refreshTokenFactory.GenerateToken();

                var replaceResult = await _refreshTokenService.ReplaceAsync(user.Id, token, refreshToken);
                if (!replaceResult.Success)
                {
                    throw new Exception(replaceResult.Errors.ToString("\n"));
                }

                return new LoginTokenDto
                {
                    Token = newBearerToken,
                    RefreshToken = refreshToken,
                    ValidFrom = _jwtBearerTokenHelper.GetValidFromDate(newBearerToken),
                    ExpirationDate = _jwtBearerTokenHelper.GetExpirationDate(newBearerToken)
                };

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Helpers
        private async Task<IGenericResult<LoginTokenDto>> GenerateLoginToken(Users user)
        {
            var result = new GenericResult<LoginTokenDto>();

            var role = await _userManager.GetRolesAsync(user);
            var roleBase = role.ToList();
            bool IsAdmin = false;
            roleBase.ForEach(x =>
            {
                if (!IsAdmin)
                    IsAdmin = x.ToLower() == "administrador" ? true : false;
            }
            );


            var bearerToken = _jwtBearerTokenHelper.CreateJwtToken(user.Id, user.UserName, role.ToList());
            if (bearerToken is null)
            {
                result.AddError("TokenError");
                return result;
            }

            var refreshToken = _refreshTokenFactory.GenerateToken();
            var creationResult = await _refreshTokenService.CreateAsync(user.Id, refreshToken);

            if (!creationResult.Success)
            {
                result.Issues = creationResult.Errors;
            }
            //transform role tolowwer all items


            var response = new LoginTokenDto
            {
                Token = bearerToken,
                RefreshToken = creationResult.Success ? refreshToken : null,
                ValidFrom = _jwtBearerTokenHelper.GetValidFromDate(bearerToken),
                ExpirationDate = _jwtBearerTokenHelper.GetExpirationDate(bearerToken),
                UserName = user.UserName,
                Email = user.Email,
                RoleName = IsAdmin ? "Administrador" : role.FirstOrDefault()
            };

            result.Data = response;
            return result;
        }

        #endregion
    }
}
