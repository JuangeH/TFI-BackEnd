using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.DTOs
{
    public class RegisterDto
    {
        public RegisterDto()
        {
            IsRegistred = false;
        }
        public bool IsRegistred { get; set; }
        public List<string> Code { get; set; }
    }
}
