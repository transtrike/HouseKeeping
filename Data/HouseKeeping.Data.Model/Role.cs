using Microsoft.AspNetCore.Identity;

namespace HouseKeeping.Data.Models
{
    public class Role : IdentityRole
    {
        public const string Admin = "Administrator";
        public const string User = "Client";
        public const string HouseKeeper = "HouseKeeper";
    }
}
