namespace FeatureToggle.Domain.Entity.BusinessSchema
{
    public class BusinessFeatureFlag
    {
        public int FeatureFlagId { get; private set; }
        public Business? Business { get; private set; }
        public int BusinessId { get; private set; }
        public Feature? Feature { get; private set; }
        public int FeatureId { get; private set; }

        public bool IsEnabled { get; private set; }


    }
}
