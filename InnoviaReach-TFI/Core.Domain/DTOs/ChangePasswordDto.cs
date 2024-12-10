using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.DTOs
{
    public class ChangePasswordDto
    {
        public string UserId { get; set; }
        public string NewPassword { get; set; }
        public string OldPassword { get; set; }
    }
}
