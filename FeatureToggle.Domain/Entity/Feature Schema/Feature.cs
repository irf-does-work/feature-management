using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureToggle.Domain.Entity.Custom_Schema
{
    public class Feature
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<BusinessFeatureFlag> BusinessFeatures { get; set; }
    }
}
