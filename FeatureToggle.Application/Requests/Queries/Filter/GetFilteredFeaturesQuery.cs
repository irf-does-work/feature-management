using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeatureToggle.Application.DTOs;
using MediatR;

namespace FeatureToggle.Application.Requests.Queries.Filter
{
    public class GetFilteredFeaturesQuery : IRequest<List<FilteredFeatureDTO>>
    {
        public bool? IsEnabled { get; set; } // True for enabled, false for disabled
        public bool? IsDisabled { get; set; } // True for disabled, false for enabled
        public int? FeatureToggleType { get; set; } // 1 for Feature Toggle, 2 for Release Toggle
        public int? ReleaseToggleType { get; set; } // 1 for Release Toggle, 2 for Feature Toggle
    }
}
