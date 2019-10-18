using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace DL.SCA.Security.Entities
{
    public class AppUser : IdentityUser<int>
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string DisplayName => $"{ FirstName } { LastName }";
    }
}
