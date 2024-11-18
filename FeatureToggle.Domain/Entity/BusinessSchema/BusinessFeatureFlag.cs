using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeatureToggle.Domain.Entity.FeatureManagementSchema;

namespace FeatureToggle.Domain.Entity.BusinessSchema
{
    public class BusinessFeatureFlag
    {
        public int Id { get; private set; }
        public Business? Business { get; private set; }
        public int BusinessId { get; private set; }
        public Feature? Feature { get; private set; }
        public int FeatureId { get; private set; }

        public bool IsEnabled { get; private set; }

        public FeatureType FeatureType { get; private set; }
        public int FeatureTypeId { get; private set; }

    }
}
