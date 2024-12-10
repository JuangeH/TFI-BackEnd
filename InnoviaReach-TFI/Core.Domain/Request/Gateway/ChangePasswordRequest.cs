using System.ComponentModel.DataAnnotations;

namespace Api.Request
{
    public class ChangePasswordRequest
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
