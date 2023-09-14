using System.ComponentModel.DataAnnotations;

namespace Api.Request.Privileges
{
    public class PrivilegesPostRequest
    {
        [Required(ErrorMessage = "Campo requerido")]
        public string PrivilegeName { get; set; }
    }
}
