using FeatureToggle.Domain.Entity.Enum;

namespace FeatureToggle.Application.Events
{
    public class UpdateFeatureEvent 
    {
        public int FeatureId { get; set; }
        public int? BusinessId { get; set; }
        public Actions Action {  get; set; }
    }
}
