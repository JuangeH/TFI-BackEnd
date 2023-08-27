using System;
using System.Collections.Generic;
using System.Text;

namespace Transversal.Helpers.JWT
{
    public interface ITokenGenerator
    {
        public string GenerateToken();
    }
}
