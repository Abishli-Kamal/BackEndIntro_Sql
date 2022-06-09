using System.ComponentModel.DataAnnotations;

namespace Ap204_Pronia.ViewModels
{
    public class EditUserVM
    {
        [Required, StringLength(maximumLength: 20)]
        public string Firstname { get; set; }
        [Required, StringLength(maximumLength: 20)]
        public string Username { get; set; }

        [Required, StringLength(maximumLength: 20)]
        public string Lastname { get; set; }
        [DataType(DataType.Password)]
        public string CurrenPassword { get; set; }
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
