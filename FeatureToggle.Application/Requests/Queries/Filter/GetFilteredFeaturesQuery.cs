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
        public bool? IsEnabledFilter { get; set; } // True for enabled, false for disabled
        public bool? IsDisabledFilter { get; set; } // True for disabled, false for enabled
        public bool? ReleaseToggleFilter { get; set; }
        public bool? FeatureToggleFilter { get; set; } 
        
    }
}
