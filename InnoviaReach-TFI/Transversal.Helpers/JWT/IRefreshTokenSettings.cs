using System;
using System.Collections.Generic;
using System.Text;

namespace Transversal.Helpers.JWT
{
    public interface IRefreshTokenSettings
    {
        public TimeSpan Duration { get; set; }
    }
}
