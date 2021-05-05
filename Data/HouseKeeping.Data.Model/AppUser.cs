using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace HouseKeeping.Data.Models
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<Location> Locations { get; set; }

        public List<Task> AssignedTasks { get; set; }

        public List<Task> CreatedTasks { get; set; }
    }
}
