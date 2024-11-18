using Microsoft.AspNetCore.Identity;

namespace FeatureToggle.Domain.Entity.FeatureManagementSchema
{
    public class User : IdentityUser
    {
        public string Name { get;private set; }
        public bool IsAdmin { get; private set; }
        //public List<Log> Logs { get; private set; }

        public User() { }

        public User(string email, string name)
        {
            Email = email;
            Name = name;
            UserName = email;
        }
    }
}
