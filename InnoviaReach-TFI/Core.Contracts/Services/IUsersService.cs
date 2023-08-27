using Core.Domain.ApplicationModels;
using Core.Domain.DTOs;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Transversal.Helpers.ResultClasses;

namespace Core.Contracts.Services
{
    public interface IUsersService : IGenericService<Users>
    {
        public Task<IGenericResult<RegisterDto>> CreateUserAsync(Users users, string password);
        public Task<IdentityResult> ConfirmUserEmailAsync(string users, string token);
        public Task<IGenericResult<LoginTokenDto>> LoginUserAsync(string email, string password);
        public Task<List<Users>> GetUsersAsync();
        public Task ForgotPasswordGenerateToken(string userName);
        public Task ChangePasswordGenerateToken(ChangePasswordDto model);
        public Task<LoginTokenDto> GenerateRefreshToken(Users user, string token);
        public Task<bool> DeleteUserAsync(string id);


    }
}