using System;
using System.Collections.Generic;

namespace FeatureToggle.Domain.Entity.BusinessSchema;

public partial class BusinessFeatureFlag
{
    public int? BusinessId { get; set; }

    public bool IsEnabled { get; set; }

    public int FeatureFlagId { get; set; }

    public int FeatureId { get; set; }

    public virtual Business? Business { get; set; }

    public virtual Feature Feature { get; set; } = null!;
}
