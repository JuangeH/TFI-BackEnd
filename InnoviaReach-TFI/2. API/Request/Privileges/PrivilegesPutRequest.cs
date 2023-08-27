
using Newtonsoft.Json;

using System.ComponentModel.DataAnnotations;

namespace Api.Request.Privileges
{
    public class PrivilegesPutRequest
    {
        [Required(ErrorMessage = "Campo requerido")]
        public string PrivilegeNewName { get; set; }
        public string Id { get; set; }
        public string concurrencyStamp { get; set; }
    }
}
