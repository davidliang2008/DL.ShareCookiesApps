using Microsoft.AspNetCore.Identity;

namespace DL.SCA.Security.Entities
{
    public class AppRole : IdentityRole<int>
    {
        public string Description { get; set; }
    }
}
