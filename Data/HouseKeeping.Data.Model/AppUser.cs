using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace HouseKeeping.Data.Models
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<Location> Locations { get; set; }
    }
}
