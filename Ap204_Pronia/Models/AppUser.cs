using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Ap204_Pronia.Models
{
    public class AppUser:IdentityUser
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
     
     
    }
}
