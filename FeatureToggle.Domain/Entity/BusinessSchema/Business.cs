using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureToggle.Domain.Entity.Custom_Schema
{
    public class Business
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        
        public List<BusinessFeatureFlag>? BusinessFeatures { get; private set; }

    }
}
