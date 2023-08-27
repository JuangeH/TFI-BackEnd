using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.ApplicationModels
{
    public class Users : IdentityUser
    {
        public bool Active { get; set; }
        public UsersPrivileges UserPrivileges { get; set; }
        public virtual ICollection<RefreshToken> UserRefreshTokens { get; set; }

    }
}
