using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeatureToggle.Domain.Configurations;
using FeatureToggle.Domain.Entity.User_Schema;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FeatureToggle.Infrastructure.Models
{
    public class FeatureManagementContext : IdentityDbContext<User>
    {
        public DbSet<Log> Logs { get; set; }  //Log to logs
        public FeatureManagementContext(DbContextOptions<FeatureManagementContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // for Identity
            modelBuilder.ApplyConfiguration(new LogConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            //modelBuilder.AddConventions
        }
    }
}
