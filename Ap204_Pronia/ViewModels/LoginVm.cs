using System.ComponentModel.DataAnnotations;

namespace Ap204_Pronia.ViewModels
{
    public class LoginVm
    {
        [Required, StringLength(maximumLength: 20)]
        public string Username { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
