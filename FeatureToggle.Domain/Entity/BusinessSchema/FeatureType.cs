using System;
using System.Collections.Generic;

namespace FeatureToggle.Domain.Entity.BusinessSchema;

public partial class FeatureType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Feature> Features { get; set; } = new List<Feature>();
}
