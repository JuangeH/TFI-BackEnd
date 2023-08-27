using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.DTOs
{
    public class ChangePasswordDto
    {
        public string Token { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
