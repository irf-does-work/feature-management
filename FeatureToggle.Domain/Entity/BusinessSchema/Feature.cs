using System;
using System.Collections.Generic;

namespace FeatureToggle.Domain.Entity.BusinessSchema;

public partial class Feature
{
    public int FeatureId { get; set; }

    public string FeatureName { get; set; } = null!;

    public int FeatureTypeId { get; set; }

    public virtual ICollection<BusinessFeatureFlag> BusinessFeatureFlags { get; set; } = new List<BusinessFeatureFlag>();

    public virtual FeatureType FeatureType { get; set; } = null!;
}
