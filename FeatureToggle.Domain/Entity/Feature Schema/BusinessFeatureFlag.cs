using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureToggle.Domain.Entity.Custom_Schema
{
    public class BusinessFeatureFlag
    {
        public int Id { get; set; }
        public Business? Business { get; set; }
        public int BusinessId { get; set; }
        public Feature? Feature { get; set; }
        public int FeatureId { get; set; }

        public bool IsEnabled { get; set; }

    }
}
