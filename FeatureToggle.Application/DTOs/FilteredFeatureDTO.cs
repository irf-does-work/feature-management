using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureToggle.Application.DTOs
{
    public class FilteredFeatureDTO
    {
        public int FeatureFlagId { get; set; }
        public int? FeatureId { get; set; }
        public string FeatureName { get; set; }
        //public bool isEnabled {  get; set; }

    }
}
