using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Transversal.Helpers.JWT
{
    public class TokenGenerator : ITokenGenerator
    {
        public string GenerateToken(int size)
        {
            var randomNumber = new byte[size];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public string GenerateToken() => GenerateToken(64);
    }
}
