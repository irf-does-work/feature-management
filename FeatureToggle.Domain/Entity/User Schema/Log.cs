using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeatureToggle.Domain.Entity.Custom_Schema;

namespace FeatureToggle.Domain.Entity.User_Schema
{
    public class Log
    {
        public int Id { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
        public BusinessFeatureFlag BusinessFeature { get; set; }
        public int BusinessFeatureId { get; set; }
        public DateTime Time { get; set; }
        public Actions Action { get; set; }

        public enum Actions
        {
            Enabled, Disabled
        }
    }
}
