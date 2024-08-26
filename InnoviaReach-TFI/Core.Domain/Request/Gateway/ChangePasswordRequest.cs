using System.ComponentModel.DataAnnotations;

namespace Api.Request
{
    public class ChangePasswordRequest
    {
        [Required]
        public string Token { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required(ErrorMessage = "Campo requerido")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "La contraseña debe contener al menos {2} caracteres", MinimumLength = 6)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden")]
        public string ConfirmPassword { get; set; }
    }
}
