using System;
using Transversal.Helpers.JWT;

namespace Api.JwT
{
    public class RefreshTokenSettings : IRefreshTokenSettings
    {
        public TimeSpan Duration { get; set; }

    }
}
