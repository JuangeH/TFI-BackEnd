using Core.Contracts.Repositories;
using Core.Contracts.Services;
using Core.Domain.ApplicationModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Transversal.Helpers.JWT;
using Transversal.Helpers.ResultClasses;

namespace Core.Business.Services
{
    public class RefreshTokenService : GenericService<RefreshToken>, IRefreshTokenService
    {
        private readonly IRefreshTokenSettings _refreshTokenSettings;
        private readonly IRefreshTokenRepository refreshTokenRepository; //Usamos este repositorio para utilizar su método ya que _repository solo tiene los metodos del genérico
        public RefreshTokenService(IUnitOfWork unitOfWork, IRefreshTokenSettings refreshTokenSettings)
            : base(unitOfWork, unitOfWork.GetRepository<IRefreshTokenRepository>())
        {
            refreshTokenRepository = unitOfWork.GetRepository<IRefreshTokenRepository>();
            _refreshTokenSettings = refreshTokenSettings;
        }
        
        private TimeSpan Duration => _refreshTokenSettings.Duration;

        public async Task<IGenericResult> CreateAsync(string userId, string token)
        {
            try
            {
                var toInsert = new RefreshToken(userId, token, CalculateExpirationDate(Duration));
                await _repository.Insert(toInsert);
                await _unitOfWork.SaveChangesAsync();
                return new GenericResult();
            }
            catch (Exception ex)
            {
                return new GenericResult(ex.Message);
            }
        }

        public async Task<IGenericResult> ReplaceAsync(string userId, string oldToken, string newToken)
        {
            try
            {
                var oldRecord = await refreshTokenRepository.GetMatch(userId, oldToken);
                if (oldRecord == null)
                    return new GenericResult("User and refresh token don't match.");

                if (IsExpired(oldRecord))
                    return new GenericResult("Refresh token is expired.");

                //if (IsReplaced(oldRecord))
                //    return new GenericResult("Refresh token has already been replaced.");

                var replacement = new RefreshToken(userId, newToken, CalculateExpirationDate(Duration));
                await base.CreateAsync(replacement);

                if (replacement.Id < 1)
                    return new GenericResult("Error storing new refresh token.");

                await base.UpdateAsync(oldRecord);
                return new GenericResult();
            }
            catch (Exception ex)
            {
                return new GenericResult(ex.Message);
            }
        }

        private DateTime CalculateExpirationDate(TimeSpan duration) => DateTime.UtcNow.Add(duration);

        private bool IsExpired(RefreshToken record) => record.Expires < DateTime.UtcNow;

        //private bool IsReplaced(RefreshToken record) => record.IdReplacement != null;

    }
}
