namespace FeatureToggle.Application.DTOs
{
    public class FilteredFeatureDTO
    {
        public int? FeatureId { get; set; }
        public string FeatureName { get; set; } = string.Empty;
        public int FeatureType { get; set; }
        public bool? IsEnabled { get; set; }

    }
}
