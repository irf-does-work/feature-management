using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace FeatureToggle.Domain.Entity.FeatureManagementSchema
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public List<Log> Logs { get; set; }
        public bool IsAdmin { get; set; }

        public User() { }

        public User(string email, string name)
        {
            Email = email;
            Name = name;
            UserName = email;
        }
    }
}
