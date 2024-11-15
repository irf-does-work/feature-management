using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeatureToggle.Domain.Configurations;
using FeatureToggle.Domain.Entity.FeatureManagementSchema;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace FeatureToggle.Infrastructure.Models
{
    public class UserContext : IdentityDbContext<User>
    {
        public DbSet<Log> Logs { get; set; }  //Log to logs
        public UserContext(DbContextOptions<UserContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // for Identity
            modelBuilder.ApplyConfiguration(new LogConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            //modelBuilder.AddConventions
        }

    }
}
