using Microsoft.AspNetCore.Identity;

namespace DAL.EF.Identity
{
    public class NorthwindUser: IdentityUser
    {
        public int Year { get; set; }   
    }
}