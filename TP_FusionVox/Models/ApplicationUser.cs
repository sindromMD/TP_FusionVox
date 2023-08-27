using Microsoft.AspNetCore.Identity;

namespace TP_FusionVox.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Pseudonymes { get ; set; }
    }
}
